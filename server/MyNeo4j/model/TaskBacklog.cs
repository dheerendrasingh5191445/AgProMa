using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public enum TaskBacklogStatus
    {
        Unplanned,
        Inprogress,
        Completed
    }
    public class TaskBacklog
    {
        [Key]
        public int TaskId { get; set; }

        public int SprintId { get; set; }
        [ForeignKey("SprintId")]
        public SprintBacklog SprintBacklog { get; set; }

        public string TaskName { get; set; }

        public int PersonId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [EnumDataType(typeof(TaskBacklogStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public TaskBacklogStatus Status { get; set; }
    }
}
