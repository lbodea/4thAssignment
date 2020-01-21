using Microsoft.Extensions.Configuration;
using StudentsAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentsAPIClient.Services
{
    public class ClientCredentials : IClientCredentials
    {
        public ClientCredentials(IConfiguration configuration)
        {
            var clientInfo = configuration.GetSection("ClientCredentials");
            ClientId = clientInfo.GetValue<string>("ClientId");
            ClientSecret = clientInfo.GetValue<string>("ClientSecret");
            Scope = clientInfo.GetValue<string>("Scope");
        }
        [JsonPropertyName("ClientId")]
        public string ClientId { get; }
        [JsonPropertyName("ClientSecret")]
        public string ClientSecret { get; }
        [JsonPropertyName("Scope")]
        public string Scope { get; }
    }
}
