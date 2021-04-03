using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Evolutional.Project.Helper
{
    public static class HttpHelper
    {
        private static HttpClient _client;
        private static StringContent _content;

        public static async Task<T> SendAsync<T>(string endPoint)
        {
            var result = _client.PostAsync(endPoint, _content).Result;

            string data = default(string);
            result.EnsureSuccessStatusCode();

            if (result.IsSuccessStatusCode)
                data = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
        }

        public static async Task<T> PutAsync<T>(string endPoint)
        {
            var result = _client.PutAsync(endPoint, _content).Result;

            string data = default(string);
            result.EnsureSuccessStatusCode();

            if (result.IsSuccessStatusCode)
                data = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(data);
        }


        public static async Task<T> GetAsync<T>(string endPoint)
        {
            var result = _client.GetAsync(endPoint).Result;

            string data = default(string);
            result.EnsureSuccessStatusCode();

            if (result.IsSuccessStatusCode)
                data = await result.Content.ReadAsStringAsync();
           
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static async Task<T> DeleteAsync<T>(string endPoint)
        {
            var result = _client.DeleteAsync(endPoint).Result;

            string data = default(string);
            result.EnsureSuccessStatusCode();

            if (result.IsSuccessStatusCode)
                data = await result.Content.ReadAsStringAsync();
          
            return JsonConvert.DeserializeObject<T>(data);
        }


        public static void CreateRequest(string url)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) { return true; };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static void CreateContentRequestUTF8<T>(T prop, string mediaType)
        {
            var contentJson = JsonConvert.SerializeObject(prop);
            _content = new StringContent(contentJson, Encoding.UTF8, mediaType);
        }

        public static void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _content.Headers.Add(header.Key, header.Value);
            }
        }

    }
}