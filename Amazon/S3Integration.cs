using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using MusicHubBusiness.Util;
using System.IO;

namespace MusicHubBusiness.Amazon
{
    public class S3Integration
    {
        private string bucketMusics = WebConfigSettings.BucketMusics;

        public void UploadSongFile(Stream stream, string keyName)
        {
            AmazonS3Client S3Client = new AmazonS3Client(RegionEndpoint.USEast1);

            TransferUtility fileTransferUtility = new TransferUtility(S3Client);

            using (stream)
            {
                fileTransferUtility.Upload(stream, bucketMusics, keyName);
            }
        }
    }
}