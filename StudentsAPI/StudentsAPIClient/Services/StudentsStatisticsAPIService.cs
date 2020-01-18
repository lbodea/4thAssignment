using AutoMapper;
using StudentsAPI.Core.Entities;
using StudentsAPI.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudentsAPIClient.Services
{
    public class StudentsStatisticsAPIService
    {
        private readonly HttpClient client;
        private readonly IMapper mapper;

        public StudentsStatisticsAPIService(HttpClient client, IMapper mapper)
        {
            this.client = client;
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Add("api-version", "2.0");
            client.DefaultRequestHeaders.Add("x-api-key", "123456");
            this.mapper = mapper;
        }

        public IEnumerable<StudentStatistics> Get()
        {
            var students = GetAllStudentsAsync().Result;
            var codeCommits = GetAllCodeCommits().Result;

            List<StudentStatistics> studentStatisctics = new List<StudentStatistics>();

            for (int i = 0; i < students.Count(); i++)
            {
                var studentStatistic = new StudentStatistics();
                var element = students.ElementAt(i);

                //studentStatistic = mapper.Map<Student>(students.ElementAt(i));
                studentStatistic.Id = element.Id;
                studentStatistic.LastName = element.LastName;
                studentStatistic.FirstName = element.FirstName;
                studentStatistic.Email = element.Email;
                studentStatistic.Phone = element.Phone;

                List<CodeCommit> codeCommitList = new List<CodeCommit>();
                int? numberOfCommits = 0;
                long? codeLines = 0;
                for (int j = 0; j < codeCommits.Count(); j++)
                {
                    if (studentStatistic.Id == codeCommits.ElementAt(j).UserId)
                    {
                        numberOfCommits++;
                        codeLines += codeCommits.ElementAt(i).LinesModified;
                    }
                }
                studentStatistic.NumberOfCommits = numberOfCommits > 0 ? numberOfCommits : null;
                studentStatistic.NumberOfModifiedLines = codeLines > 0 ? codeLines : null;
                
                studentStatisctics.Add(studentStatistic);
            }

            return studentStatisctics;
        }

        private async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/students");

            Task<System.IO.Stream> responseStreamTask = GetResponseStreamAsync(request);

            return await JsonSerializer.DeserializeAsync<IEnumerable<Student>>(responseStreamTask.Result);
        }

        private async Task<IEnumerable<CodeCommit>> GetAllCodeCommits()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/codeCommits");

            Task<System.IO.Stream> responseStreamTask = GetResponseStreamAsync(request);

            return await JsonSerializer.DeserializeAsync<IEnumerable<CodeCommit>>(responseStreamTask.Result);
        }

        private async Task<System.IO.Stream> GetResponseStreamAsync(HttpRequestMessage request)
        {
            HttpResponseMessage firstResponse = await client.SendAsync(request);

            if (!firstResponse.IsSuccessStatusCode)
            {
                string message = string.Format(
                    "Error while using StudentsAPI (status code: {0}, reason: {1})",
                    firstResponse.StatusCode,
                    firstResponse.ReasonPhrase);

                throw new HttpRequestException(message);
            }

            return await firstResponse.Content.ReadAsStreamAsync();
        }

    }
}
