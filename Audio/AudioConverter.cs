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
