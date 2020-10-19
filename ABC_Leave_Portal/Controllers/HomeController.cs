using ABC_Leave_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ABC_Leave_Portal.Controllers
{
    public class HomeController : Controller
    {

        private LeavePortalContext context = new LeavePortalContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.isAuthenticated = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Employee loginEmployee)
        {
            ViewBag.isAuthenticated = false;
            ViewBag.empId = loginEmployee.EmployeeCode;
            FormsAuthentication.SetAuthCookie(loginEmployee.EmployeeCode, false);


            if (isEmployeeValidate(loginEmployee.EmployeeCode, loginEmployee.Password))
            {
                //make the authentication cookie
                FormsAuthentication.SetAuthCookie(loginEmployee.EmployeeCode, true);

                ViewBag.isAuthenticated = true;
                return RedirectToAction("LeaveApply");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(loginEmployee.EmployeeCode, false);

                //error message
                ModelState.AddModelError("", "Check your username and password");
                return View(loginEmployee);
            }
        }

        public bool isEmployeeValidate(string username, string password)
        {

            bool userValid = false;

            if (username != "" && password != "")
            {
                int userCount = context.Employees.Count(x => x.EmployeeCode == username && x.Password == password);
                if (userCount == 1)
                {
                    userValid = true;
                }
            }
            return userValid;
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SetAuthCookie(ViewBag.empId, false);
            ViewBag.empId = "";
            ViewBag.isAuthenticated = false;
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult LeaveApprove()
        {
            // have to display all leave applications for this Employee ID as the supervisor ID
            List<LeaveEntry> leaveEntries = context.LeaveEntries.ToList();
            return View(leaveEntries);
        }

        [Authorize]
        public ActionResult LeaveApply()
        {
            ViewBag.empID = ViewBag.empId;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult LeaveApply(LeaveEntry leaveEntry)
        {

            

            if (isValidateLeave())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        context.LeaveEntries.Add(leaveEntry);
                        context.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        Console.Write("Error when entering the data" + e.Message);
                        ModelState.AddModelError("", "Can't make your request. May be you have get all the leaves which are applicable 1");
                    }
                    return RedirectToAction("LeaveApply");
                }
                else
                {
                    ModelState.AddModelError("", "Can't make your request. May be you have get all the leaves which are applicable 2");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "Can't make your request. May be you have get all the leaves which are applicable 3");
                return View();
            }



        }

        public bool isValidateLeave()
        {

            //check all the fields are filled and ok with the business logic
            bool validate = true;


            return validate;
        }

        [Authorize]
        public ActionResult approve(string empId, string leaveTypeId, DateTime reqDate)
        {
            LeaveEntry lEntry = context.LeaveEntries.Where(x => x.EmployeeCode == empId && x.LeaveTypeId == leaveTypeId && x.RequestedDate == reqDate).SingleOrDefault();
            return View(lEntry);
        }


        [HttpPost]
        [Authorize]
        public ActionResult approve(string empId, string leaveTypeId, DateTime reqDate, LeaveEntry leaveEntry)
        {
            LeaveEntry lEntry = context.LeaveEntries.Where(x => x.EmployeeCode == empId && x.LeaveTypeId == leaveTypeId && x.RequestedDate == reqDate).SingleOrDefault();

            if (ModelState.IsValid)
            {

                //update
                lEntry.EmployeeCode = leaveEntry.EmployeeCode;
                lEntry.LeaveTypeCode = leaveEntry.LeaveTypeCode;
                lEntry.RequestedDate = leaveEntry.RequestedDate;
                lEntry.CheckByName = leaveEntry.CheckByName;
                lEntry.CheckDate = DateTime.Now;
                lEntry.Status = "Approved";
                lEntry.CommentsEmployee = leaveEntry.CommentsEmployee;
                lEntry.CommentsApprover = leaveEntry.CommentsApprover;
                lEntry.AutoNumber = leaveEntry.AutoNumber;
                context.SaveChanges();

                return RedirectToAction("LeaveApprove");

            }
            else
            {
                return View(lEntry);
            }

            
        }
    }
}