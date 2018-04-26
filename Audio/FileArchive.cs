using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Audio
{
    public class FileArchive
    {
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
        public string Base64 { get; set; }
        public int ContentLength { get { return FileBytes.Length; } }
        public TimeSpan TotalTime { get; internal set; }
        public string FileName { get { string retorno = Path.GetFileName(FullPath); return retorno; } }
        public string Extension { get { string retorno = Path.GetExtension(FullPath); return retorno; } }
        public string FullPath { get; internal set; }

        public FileArchive(string strBase64)
        {
            Base64 = strBase64;
            ContentType = "audio/mp3";

            FileHandler fileHandler = new FileHandler();
            FileBytes = fileHandler.Base64ToBytes(strBase64);
        }
    }
}
