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
    public enum ReleasePlanStatus
    {
        Unplanned,
        Inprogress,
        Release
    }
    public class ReleasePlanMaster
    {//it will store the data of release plan of the project
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReleasePlanId { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectMaster ProjectMaster { get; set; }

        public List<SprintBacklog> Sprints { get; set; }

        public string Description { get; set; }

        public DateTime ActualReleaseDate { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime StartDate { get; set; }

        public string ReleaseName { get; set; }

        [EnumDataType(typeof(ReleasePlanStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReleasePlanStatus Status { get; set; }
    }
}
