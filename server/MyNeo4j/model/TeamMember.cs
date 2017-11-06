using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public class TeamMember
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int MemberId { get; set; }
    }
}
