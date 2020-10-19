using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABC_Leave_Portal.Models
{
    public class LeaveEntry
    {
        //Assumtion I made here, A particular employee cannot request same type of leave within the same date

        [Key, Column(Order = 0)]
        [Editable(false)]
        [Required(ErrorMessage = "Employee Code is required.")]
        public string EmployeeCode { get; set; }


        public string CheckByName { get; set; } // approved or rejected by

        [Key, Column(Order = 1)]
        public string LeaveTypeId { get; set; }

        [Key, Column(Order = 2)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime RequestedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckDate { get; set; } // Leave approved or rejected date

        public string Status { get; set; } // Requested, Approved, Cancelled


        public string CommentsEmployee { get; set; } // Comments By Employee


        public string CommentsApprover { get; set; } // Comments by Approver


        public int AutoNumber { get; set; }

        [ForeignKey("EmployeeCode"), Column(Order=0)]
        public virtual Employee Employee1 { get; set; }
       // public ICollection<Employee> Employee1 { get; set; }

        [ForeignKey("CheckByName"), Column(Order=1)]
        public virtual Employee Employee2 { get; set; }
        //public ICollection<Employee> Employee2 { get; set; }
        
        [ForeignKey("LeaveTypeId"), Column(Order=2)]
        public virtual LeaveType LeaveTypeCode { get; set; }


    }
}