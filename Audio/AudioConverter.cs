using MusicHubBusiness.Amazon;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicHubBusiness.Audio
{
    public class AudioHelper : IDisposable
    {
        public string TempFile { get; set; }

        public AudioHelper()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string tempFolder = string.Format("{0}temp", baseDirectory);

            TempFile = string.Format("{0}\\{1}", tempFolder, Guid.NewGuid().ToString());

            Directory.CreateDirectory(tempFolder);
        }

        /// <summary>
        /// Creates a mashup of two or more mp3 files by using naudio
        /// </summary>
        /// <param name="files">Name of files as an string array</param>
        /// These files should be existing in a temporay folder
        /// <returns>The path of mashed up mp3 file</returns>
        public static string CreateMashup(int projectId, string mp3Folder,  string[] files)
        {
            // because there is no mash up with less than 2 files
            if (files.Count() < 2)
            {
                throw new Exception("Not enough files selected!");
            }

            try
            {
                // Create a mixer object
                // This will be used for merging files together
                var mixer = new WaveMixerStream32
                {
                    AutoStop = true
                };

                // Set the path to store the mashed up output file
                var outputFile = Path.Combine(mp3Folder, $"{projectId}.mp3");

                foreach (var file in files)
                {
                    // for each file -
                    // check if it exists in the temp folder
                    var filePath = Path.Combine(file);
                    if (File.Exists(filePath))
                    {
                        // create mp3 reader object
                        var reader = new Mp3FileReader(filePath);

                        // create a wave stream and a channel object
                        var waveStream = WaveFormatConversionStream.CreatePcmStream(reader);
                        var channel = new WaveChannel32(waveStream)
                        {
                            //Set the volume
                            Volume = 0.5f
                        };

                        // add channel as an input stream to the mixer
                        mixer.AddInputStream(channel);
                    }
                }

                CheckAddBinPath();

                // convert wave stream from mixer to mp3
                var wave32 = new Wave32To16Stream(mixer);
                var mp3Writer = new LameMP3FileWriter(outputFile, wave32.WaveFormat, 128);
                wave32.CopyTo(mp3Writer);

                // close all streams
                wave32.Close();
                mp3Writer.Close();

                // return the mashed up file path
                return outputFile;
            }
            catch (Exception)
            {
                // TODO: handle exception
                throw;
            }
        }

        private static void CheckAddBinPath()
        {
            // find path to 'bin' folder
            var binPath = Path.Combine(new string[] { AppDomain.CurrentDomain.BaseDirectory, "bin" });
            // get current search path from environment
            var path = Environment.GetEnvironmentVariable("PATH") ?? "";

            // add 'bin' folder to search path if not already present
            if (!path.Split(Path.PathSeparator).Contains(binPath, StringComparer.CurrentCultureIgnoreCase))
            {
                path = string.Join(Path.PathSeparator.ToString(), new string[] { path, binPath });
                Environment.SetEnvironmentVariable("PATH", path);
            }
        }

        public void SaveSong(FileArchive song)
        {
            string[] splitedContentType = song.ContentType.Split('/');

            TempFile += string.Format(".{0}", splitedContentType[1]);

            File.WriteAllBytes(TempFile, song.FileBytes);

            using (var reader = new AudioFileReader(TempFile))
            {
                song.TotalTime = reader.TotalTime;
                song.FullPath = reader.FileName;
            }
        }

        private void ClearTempFiles()
        {
            File.Delete(TempFile);
        }

        /// <summary>
        /// Copies the contents of input to output. Doesn't close either stream.
        /// </summary>
        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public void UploadToAmazon(string keyName)
        {
            S3Integration s3Integration = new S3Integration();
            using (var reader = new AudioFileReader(TempFile))
            {
                s3Integration.UploadSongFile(reader, keyName);
            }
        }

        public void Dispose()
        {
            ClearTempFiles();
        }
    }
}
