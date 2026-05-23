using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AT_Work6.Service
{
    public static class ConfigService
    {
        private static IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        private static string TokenUrl => _config["Auth:TokenUrl"];
        private static string ClientId => _config["Auth:ClientId"];
        private static string ClientSecret => _config["Auth:ClientSecret"];
        private static string Scope => _config["Auth:Scope"];
        private static string GrantType => _config["Auth:GrantType"];

        public static string BaseUrl => _config["Api:BaseUrl"];

        public static async Task<string> GetToken()
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, TokenUrl);

            var body = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "scope", Scope },
                { "grant_type", GrantType }
            };

            request.Content = new FormUrlEncodedContent(body);

            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);

            return doc.RootElement.GetProperty("access_token").GetString();
        }
    }
}
