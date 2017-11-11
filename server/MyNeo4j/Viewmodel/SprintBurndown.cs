using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.Viewmodel
{
    public class SprintBurndown
    {
        public int sprintId { get; set; }

        public string sprintName { get; set; }

        public int expectedTime { get; set; }

        public int actualTime { get; set; }
    }
}
