using Microsoft.AspNetCore.Mvc;
using StudentsAPI.Core.Entities;
using StudentsAPIClient.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentsAPIClient.Controllers
{
    [Route("clientapi/[controller]")]
    [ApiController]
    public class StudentsStatisticsController : ControllerBase
    {
        private readonly StudentsStatisticsAPIService studentStatisticsAPIService;

        public StudentsStatisticsController(StudentsStatisticsAPIService studentStatisticsAPIService)
        {
            this.studentStatisticsAPIService = studentStatisticsAPIService;
        }
        public IEnumerable<StudentStatistics> Get()
        {
            return studentStatisticsAPIService.Get();
        }

    }
}
