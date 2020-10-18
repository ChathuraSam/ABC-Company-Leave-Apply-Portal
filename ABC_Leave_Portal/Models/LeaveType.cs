using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABC_Leave_Portal.Models
{
    public class LeaveType
    {
        [Key]
        public string LeaveTypeCode { get; set; }

        public string Description { get; set; }
    }
}