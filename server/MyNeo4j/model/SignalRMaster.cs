using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public class SignalRMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SignalId { get; set; }

        public int MemberId { get; set; }

        public int ConnectionId { get; set; }

        public bool Online  { get; set; }


    }
}
