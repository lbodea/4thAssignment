using Newtonsoft.Json;
using StudentsAPI.V2.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Text.Json;

namespace StudentsAPI.IntegrationTests
{
    public class StudentsControllerTests : IntegrationTestsBase
    {
        public StudentsControllerTests(StudentsApiWebApplicationFactory<Startup> factory) : base(factory)
        {
           // client.DefaultRequestHeaders.Remove("x-api-key");
        }

       [Fact]
       async Task TestGetStudents()
        {
            HttpResponseMessage response = await client.GetAsync("api/students");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        async Task TestPostOnStudents()
        {
            HttpResponseMessage postResponse = await client.PostAsync("api/students", 
                new StringContent (JsonConvert.SerializeObject(
                    new Student() { Id = 8, FirstName = "Laura", LastName = "Bodea", Email = "laura.bodea2fortech.ro"}), Encoding.UTF8, "application/json"));
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            HttpResponseMessage getResponse = await client.GetAsync("api/students/8");
            getResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [Fact]
        async Task TestDeleteOnStudents()
        {
            HttpResponseMessage deleteResponse = await client.DeleteAsync("api/students/5");
            deleteResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }

        [Fact]
        async Task TestHeader()
        {
            client.DefaultRequestHeaders.Remove("x-api-key");
            HttpResponseMessage response = await client.GetAsync("api/students");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
            string message = await response.Content.ReadAsStringAsync();
            Assert.Equal("Invalid ApiKey", message);
        }
    }
}
