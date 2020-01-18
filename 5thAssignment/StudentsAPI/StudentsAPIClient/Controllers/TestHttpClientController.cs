using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Core.Entities;
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
        public async Task<IEnumerable<CodeCommit>> Get()
        {
            return await studentsAPIservice.GetCodeCommits();
        }
    }
}