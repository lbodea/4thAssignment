using System.Collections.Generic;

namespace StudentsAPI.V2.Services.Interfaces
{
    public interface IStudentsStatisticsService
    {
        IEnumerable<StudentsStatisticsService> Get();
    }
}
