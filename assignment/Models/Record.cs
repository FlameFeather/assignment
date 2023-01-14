using System;
using System.Collections.Generic;

#nullable disable

namespace assignment.Models
{
    public partial class Record
    {
        public int Rid { get; set; }
        public string Gid { get; set; }
        public string Uid { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public int State { get; set; }
    }
}
