using ABC_Leave_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABC_Leave_Portal.Controllers
{
    public class EmployeeController : Controller
    {

        private LeavePortalContext leavePortalContext = new LeavePortalContext();
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            List<Employee> employees = leavePortalContext.Employees.ToList();
            return View(employees);
        }
	}
}