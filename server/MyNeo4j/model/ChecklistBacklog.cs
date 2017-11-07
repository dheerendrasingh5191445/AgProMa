using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public class ChecklistBacklog
    {
        [Key]
        public int ChecklistId { get; set; }

        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public TaskBacklog TaskBacklog { get; set; }

        public string ChecklistName { get; set; }

        public bool Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}