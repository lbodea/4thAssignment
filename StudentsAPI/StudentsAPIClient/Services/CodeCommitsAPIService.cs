using StudentsAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentsAPIClient.Services
{
    public class CodeCommitsAPIService
    {
        private readonly HttpClient client;

        public CodeCommitsAPIService(HttpClient client)
        {
            this.client = client;
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Add("api-version", "2.0");
            client.DefaultRequestHeaders.Add("x-api-key", "123456");
        }

        public async Task<IEnumerable<CodeCommit>> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/codecommits");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string message = string.Format(
                    "Error while using StudentsAPI (status code: {0}, reason: {1})",
                    response.StatusCode,
                    response.ReasonPhrase);

                throw new HttpRequestException(message);
            }

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<CodeCommit>>(responseStream);
        }

    }
}
