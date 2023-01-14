using System;
using System.Collections.Generic;

#nullable disable

namespace assignment.Models
{
    public partial class Item
    {
        public string Itemid { get; set; }
        public int Itime { get; set; }
        public string Iname { get; set; }
        public int AvailableQuantity { get; set; }
        public int BorrowedQuantity { get; set; }
        public int UnavailableQuantity { get; set; }
    }
}
