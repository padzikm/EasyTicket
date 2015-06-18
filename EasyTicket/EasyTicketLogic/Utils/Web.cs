using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;


namespace EasyTicketLogic
{
    public class Web
    {
        public static string MakePostRequestGzipDecompression(string url, string postData)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                string status = (((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();
                byte[] result = GZipDecompress(dataStream);
                string responseData = Encoding.UTF8.GetString(result);

                dataStream.Close();
                response.Close();

                return responseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string MakeGetRequest(string url)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse(); //await req.GetResponseAsync();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());

            return sr.ReadToEnd();
        }

        public static byte[] GZipDecompress(Stream dataStream)
        {
            using (GZipStream gzStream = new GZipStream(dataStream, CompressionMode.Decompress, true))
            {
                const int bufferSize = 4096;
                int bytesRead = 0;

                byte[] buffer = new byte[bufferSize];

                using (MemoryStream ms = new MemoryStream())
                {
                    while ((bytesRead = gzStream.Read(buffer, 0, bufferSize)) > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static object ConvertDataToJson(string data)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            return javaScriptSerializer.DeserializeObject(data);
        }

        //private object[] ConvertDataToJson(string data)
        //{
        //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

        //    object[] table = (object[])javaScriptSerializer.DeserializeObject(data);
        //    return table;
        //}
    }
}
