using MusicHubBusiness.Amazon;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MusicHubBusiness.Audio
{
    public class AudioHelper
    {
        private string waveFileName, mp3FileName;

        public AudioHelper()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string tempFolder = string.Format("{0}/temp", baseDirectory);

            Directory.CreateDirectory(tempFolder);

            this.waveFileName = string.Format("{0}/tempWavFile{1}.wav", tempFolder, DateTime.Now.Ticks);
            this.mp3FileName = string.Format("{1}/tempMp3File{1}.mp3", tempFolder, DateTime.Now.Ticks);
        }

        public string WaveToMP3(HttpPostedFile httpPostedFile, int bitRate = 128)
        {
            if (httpPostedFile.ContentType.Contains("mp3")) return string.Empty;

            SaveSong(httpPostedFile);

            using (var reader = new AudioFileReader(waveFileName))
            using (var writer = new LameMP3FileWriter(mp3FileName, reader.WaveFormat, bitRate))
                reader.CopyTo(writer);

            return mp3FileName;
        }

        public string SaveSong(HttpPostedFile song)
        {
            using (Stream file = File.Create(waveFileName))
            {
                CopyStream(song.InputStream, file);
            }

            return mp3FileName;
        }

        public void ClearTempFiles()
        {
            File.Delete(waveFileName);
            File.Delete(mp3FileName);
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

        public TimeSpan GetTotalTime()
        {
            TimeSpan totalTime = new TimeSpan();

            using (var reader = new AudioFileReader(mp3FileName))
            {
                totalTime = reader.TotalTime;
            }

            return totalTime;
        }

        public void UploadToAmazon(string keyName)
        {
            S3Integration s3Integration = new S3Integration();
            using (var reader = new AudioFileReader(mp3FileName))
            {
                s3Integration.UploadSongFile(reader, keyName);
            }
        }
    }
}
