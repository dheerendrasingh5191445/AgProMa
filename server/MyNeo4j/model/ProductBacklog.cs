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

    public class ProductBacklog
    {
        [Key]
        public int StoryId { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectMaster ProjectMaster { get; set; }

        public string StoryName { get; set; }

        public int Priority { get; set; }

        public string Comments { get; set; }

        public int InSprintNo { get; set; }

        public bool Status { get; set; }
    }
}
