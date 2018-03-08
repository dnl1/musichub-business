using BearerAuthentication;
using System;

namespace MusicHubBusiness.Repository
{
    public class BearerAuthenticationRepository : BaseRepository<BearerAuthenticationToken>, IRepository<BearerAuthenticationToken>
    {
        public BearerAuthenticationRepository() : base("BearerAuthentication")
        {
        }

        public BearerAuthenticationToken Create(BearerAuthenticationToken entity)
        {
            Execute("INSERT INTO BearerAuthentication(access_token, client, uid, expiry) VALUES (@access_token, @client, @uid, @expiry)",
                new
                {
                    entity.access_token,
                    entity.client,
                    entity.uid,
                    entity.expiry
                });

            return GetLatest();
        }

        public BearerAuthenticationToken GetByUid(string uid)
        {
            return QueryFirst("SELECT * FROM BearerAuthentication WHERE uid = @uid", new { uid });
        }

        public void UpdateExpiry(DateTime expiry, string uid)
        {
            Execute("UPDATE BearerAuthentication SET expiry = @expiry WHERE uid = @uid", new { expiry, uid });
        }

        public void Update(BearerAuthenticationToken bearerAuthenticationToken)
        {
            Execute("UPDATE BearerAuthentication SET access_token = @access_token, expiry = @expiry WHERE uid = @uid", new { bearerAuthenticationToken.access_token, bearerAuthenticationToken.expiry, bearerAuthenticationToken.uid });
        }
    }
}