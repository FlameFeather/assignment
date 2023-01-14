using System;
using System.Collections.Generic;

#nullable disable

namespace assignment.Models
{
    public partial class TempUser
    {
        public string TempUid { get; set; }
        public string TempUpwd { get; set; }
        public bool? TempGender { get; set; }
        public string TempDate { get; set; }
        public string TempContext { get; set; }
    }
}
