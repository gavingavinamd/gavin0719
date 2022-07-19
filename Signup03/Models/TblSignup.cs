using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Signup03.Models
{
    public partial class TblSignup
    {
        
        public int CId { get; set; }

        [Required(ErrorMessage = "手機號碼必填")]
        public string? CMobile { get; set; }
        [Required(ErrorMessage = "姓名必填")]
        public string? CName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "信箱必填")]
        public string? CEmail { get; set; }
        public DateTime? CCreateDt { get; set; }
    }
}
