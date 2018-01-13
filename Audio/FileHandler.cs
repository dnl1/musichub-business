using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Audio
{
    public class FileHandler
    {
        public byte[] Base64ToBytes(string fullBase64)
        {
            string base64 = fullBase64.Replace("base64,", string.Empty);

            byte[] bytes = Convert.FromBase64String(base64);

            return bytes;
        }
    }
}
