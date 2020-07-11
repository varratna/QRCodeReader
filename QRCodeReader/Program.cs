using System;
using System.IO;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.NetworkInformation;

namespace QRCodeReader
{
    class Program
    {
        static string filesFromPath = "C:\\Users\\varratna.bondade\\Desktop\\QRCodeImage";
        static string url = "http://goqr.me/api/doc/read-qr-code/";
        static void Main(string[] args)
        {           
            string[] imagePaths = Directory.GetFiles(filesFromPath);

            foreach (string imagePath in imagePaths)
            {
                byte[] imageByteArray = ConvertImageToByteArray(imagePath);

                PostImage(imageByteArray);

                

            }
        }

        private static void PostImage(byte[] imageByteArray)
        {
            //instantiate the client
            using (var client = new HttpClient())
            {

                //api endpoint
                var apiUri = new Uri(url);

                //load the image byte[] into a System.Net.Http.ByteArrayContent
                var imageBinaryContent = new ByteArrayContent(imageByteArray);

                //create a System.Net.Http.MultiPartFormDataContent
                var multipartContent = new MultipartFormDataContent();
                multipartContent.Add(imageBinaryContent, "image");

                //make the POST request using the URI enpoint and the MultiPartFormDataContent
                var result = client.PostAsync(apiUri, multipartContent).Result;
            }
        }

        private static byte[] ConvertImageToByteArray(string imagePath)
        {
            byte[] imageToByteArray = null;

            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            using (BinaryReader binaryReader = new BinaryReader(fileStream))
            {
                imageToByteArray = new byte[binaryReader.BaseStream.Length];
                for (int i = 0; i < binaryReader.BaseStream.Length; i++)


                {
                    imageToByteArray[i] = binaryReader.ReadByte();
                }
                return imageToByteArray;
            }

        }
    }
}
