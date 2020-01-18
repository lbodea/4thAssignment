using StudentsAPI.V2.Models;
using System.Text.Json.Serialization;

namespace StudentsAPI.Core.Entities
{
    public class StudentStatistics : Student
    {
        [JsonPropertyName("numberOfCommits")]
        public int? NumberOfCommits { get; set; }
        [JsonPropertyName("numberOfModifiedLines")]
        public long? NumberOfModifiedLines { get; set; }
    }
}
