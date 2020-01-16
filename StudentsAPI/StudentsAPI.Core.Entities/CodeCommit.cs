using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsAPI.Core.Entities
{
    public class CodeCommit
    {
        [Required]
        public long? UserId { get; set; }
        public string Description { get; set; }
        [Required]
        public long? LinesModified { get; set; }
    }
}
