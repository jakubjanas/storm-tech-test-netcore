using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services
{
    public static class Gravatar
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string path = "https://www.gravatar.com/{0}.json";

        public static string GetHash(string emailAddress)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.Default.GetBytes(emailAddress.Trim().ToLowerInvariant());
                var hashBytes = md5.ComputeHash(inputBytes);

                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                return builder.ToString().ToLowerInvariant();
            }
        }

        public static async Task<string> GetName(string emailAddress)
        {
            var hash = GetHash(emailAddress);
            var result = string.Empty;

            try
            {
                //https://stackoverflow.com/a/6905471
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");

                var response = await httpClient.GetAsync(string.Format(path, hash));
                var body = await response.Content.ReadAsStringAsync();

                var jsonResult = JObject.Parse(body);
                var jtokenResult = jsonResult["entry"].Children().FirstOrDefault();

                result = jtokenResult != null ? jtokenResult["displayName"]?.Value<string>() : "";
            }
            catch (Exception ex)
            {
                //Log Exception
            }

            return result;
        }
    }
}