using MusicHubBusiness.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web;

namespace BearerAuthentication
{
    public class BearerDatabaseManager : IBearerManager
    {
        public BearerDatabaseManager()
        {
            bearerAuthenticationRepository = new BearerAuthenticationRepository();
        }

        public BearerDatabaseManager(string uid) : this()
        {
            this.uid = uid;
        }

        public string uid;

        private BearerAuthenticationRepository bearerAuthenticationRepository;

        public BearerAuthenticationToken GetActiveBearerAuthenticationToken()
        {
            GetUidFromHeader();
            BearerAuthenticationToken retorno = bearerAuthenticationRepository.GetByUid(uid);
            return retorno;
        }

        private void GetUidFromHeader()
        {
            if (string.IsNullOrEmpty(uid))
            {
                uid = HttpContext.Current.Request.Headers["uid"];

                if (string.IsNullOrEmpty(uid))
                {
                    var bodyStream = new StreamReader(HttpContext.Current.Request.InputStream);
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    var bodyText = bodyStream.ReadToEnd();
                    if (bodyText == string.Empty) return;
                    JObject jObject = JObject.Parse(bodyText);

                    uid = jObject["email"].ToString() ?? jObject["uid"].ToString();
                }
            }
        }

        public DateTime? GetExpire()
        {
            BearerAuthenticationToken retorno = GetActiveBearerAuthenticationToken();
            return retorno?.expiry;
        }

        public void SaveAccessToken(BearerAuthenticationToken bearerAuthenticationToken)
        {
            BearerAuthenticationToken retorno = GetActiveBearerAuthenticationToken();

            if (retorno == null)
            {
                bearerAuthenticationRepository.Create(bearerAuthenticationToken);
            }
            else
            {
                bearerAuthenticationRepository.Update(bearerAuthenticationToken);
            }
        }

        public void SetExpire(int expiryMinutes)
        {
            var expiryDate = DateTime.Now.AddMinutes(expiryMinutes);
            bearerAuthenticationRepository.UpdateExpiry(expiryDate, uid);
        }
    }
}