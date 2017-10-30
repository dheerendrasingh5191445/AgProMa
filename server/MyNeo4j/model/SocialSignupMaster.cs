using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public class SocialSignupMaster
    {//store the data coming from social sign up
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SignId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhotoUrl { get; set; }
    }
}
