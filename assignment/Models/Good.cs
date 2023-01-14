using System;
using System.Collections.Generic;

#nullable disable

namespace assignment.Models
{
    public partial class Good
    {
        public string Gid { get; set; }
        public string Gcontext { get; set; }
        public string Itemid { get; set; }
        public string Uid { get; set; }
        public string BeginTime { get; set; }
        public double KeepTime { get; set; }
        public int Gstate { get; set; }
    }
}
