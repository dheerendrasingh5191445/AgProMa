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
    public enum SprintStatus
    {
        Unplanned,
        Inprogress,
        Completed
    }

    public class SprintBacklog
    {
        [Key]
        public int SprintId { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectMaster ProjectMaster { get; set; }

        public List<TaskBacklog> Tasks { get; set; }

        public string SprintName { get; set; }

        public string SprintGoal { get; set; }

        public int ReleasePlanId { get; set; }

        public int TotalDays { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? ExpectedEndDate { get; set; }

        public DateTime? ActualEndDate { get; set; }

        [EnumDataType(typeof(SprintStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public SprintStatus Status { get; set; }

    }
}
