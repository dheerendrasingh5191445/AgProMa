using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public class ProjectMaster
    {//store the data of the new project added
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public int LeaderID { get; set; }

        public string ProjectDescription { get; set; }

        public string TechnologyUsed { get; set; }

    }
}
