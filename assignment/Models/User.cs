using System;
using System.Collections.Generic;

#nullable disable

namespace assignment.Models
{
    public partial class User
    {
        public string Uid { get; set; }
        public string Upwd { get; set; }
        public string Udate { get; set; }
        public string Ucontext { get; set; }
        public bool? Gender { get; set; }
    }
}
