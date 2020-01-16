using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.V2.Models;
using StudentsAPIClient.Services;

namespace StudentsAPIClient.Controllers
{
    [Route("clientapi/[controller]")]
    [ApiController]
    public class TestHttpClientController : ControllerBase
    {
        private readonly StudentsAPIService studentsAPIservice;

        public TestHttpClientController(StudentsAPIService studentsAPIService)
        {
            this.studentsAPIservice = studentsAPIService;
        }
        public async Task<IEnumerable<Student>> Get()
        {
            return await studentsAPIservice.Get();
        }
    }
}