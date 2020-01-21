using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsAPIClient.Models;
using StudentsAPIClient.Services;

namespace StudentsAPIClient.Controllers
{
    [Route("clientapi/[controller]")]
    [ApiController]
    public class StudentsStatisticsController : ControllerBase
    {
        private readonly StudentsAPIService studentsAPIservice;

        public StudentsStatisticsController(StudentsAPIService studentsAPIService)
        {
            this.studentsAPIservice = studentsAPIService;
        }
        public async Task<IEnumerable<StudentStatistics>> Get([FromQuery] OrderBy orderBy = OrderBy.Name)
        {
            return await studentsAPIservice.GetStudentsStatistics(orderBy);
        }
    }
}