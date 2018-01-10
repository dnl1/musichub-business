using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHubBusiness.Util
{
    class WebConfigSettings
    {
        public static string BucketMusics
        {
            get
            {
                return ConfigurationManager.AppSettings["AWS.BucketName.Musics"].ToString();
            }
        }
    }
}
