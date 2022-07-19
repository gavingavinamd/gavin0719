using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Signup03.Models
{
    public partial class TblActiveItem
    {
        public int CItemId { get; set; }
        public string? CItemName { get; set; }
        public string? CActiveDt { get; set; }
    }
}
