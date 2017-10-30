﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyNeo4j.model
{
    public enum As
    {
        leader,
        member
    }
    public class ProjectMember
    {//stores the member working in projects
        public int id { get; set; }

        public int ProjectId { get; set; }

        public int MemberId { get; set; }

        [EnumDataType(typeof(ReleasePlanStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public As ActAs { get; set; }

    }
}
