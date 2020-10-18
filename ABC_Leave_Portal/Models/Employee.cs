using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ABC_Leave_Portal.Models
{
    public class Employee
    {
        [Key]
        [StringLength(10)]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        
        [Display(Name = "Supervisor Code")]
        [StringLength(10)]
        public string SupervisorCode { get; set; }

        [Display(Name = "Leave Package")]
        [StringLength(10)]
        public string LeavePackage { get; set; } // Shop, Office or Wages Board

        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[ForeignKey("SupervisorCode")]
        //public virtual Employee EmployeeID { get; set; }
    }
}