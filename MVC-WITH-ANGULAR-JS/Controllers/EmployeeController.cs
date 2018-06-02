using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_WITH_ANGULAR_JS.Models;

namespace MVC_WITH_ANGULAR_JS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllEmployee()
        {
            using (EmployeeConnection emp = new EmployeeConnection())
            {
                List<EMPLOYEE> objEmp = emp.EMPLOYEEs.ToList();
                return Json(objEmp, JsonRequestBehavior.AllowGet);
            }
                      
        }
        [HttpGet]
        public JsonResult GetEmployeeById(string id)
        {
            using(EmployeeConnection emp=new EmployeeConnection())
            {
                var empid = int.Parse(id);
                return Json(emp.EMPLOYEEs.Find(empid), JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public JsonResult InsertEmployee(EMPLOYEE objemp)
        {
            if (objemp != null)
            {
                using (EmployeeConnection emp = new EmployeeConnection())
                {
                    emp.EMPLOYEEs.Add(objemp);
                    emp.SaveChanges();
                    return Json("Inserted", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("Employee Not Inserted! Try Again", JsonRequestBehavior.AllowGet);
            }
        }

        public string DeleteEmployee(EMPLOYEE Emp)
        {
            if (Emp != null)
            {
                using (EmployeeConnection Obj = new EmployeeConnection())
                {
                    var Emp_ = Obj.Entry(Emp);
                    if (Emp_.State == System.Data.EntityState.Detached)
                    {
                        Obj.EMPLOYEEs.Attach(Emp);
                        Obj.EMPLOYEEs.Remove(Emp);
                    }
                    Obj.SaveChanges();
                    return "Employee Deleted Successfully";
                }
            }
            else
            {
                return "Employee Not Deleted! Try Again";
            }
        }
        public string UpdateEmployee(EMPLOYEE Emp)
        {
            if (Emp != null)
            {
                using (EmployeeConnection Obj = new EmployeeConnection())
                {
                    var Emp_ = Obj.Entry(Emp);
                    EMPLOYEE EmpObj = Obj.EMPLOYEEs.Where(x => x.Emp_Id == Emp.Emp_Id).FirstOrDefault();
                    EmpObj.Emp_Age = Emp.Emp_Age;
                    EmpObj.Emp_City = Emp.Emp_City;
                    EmpObj.Emp_Name = Emp.Emp_Name;
                    Obj.SaveChanges();
                    return "Employee Updated Successfully";
                }
            }
            else
            {
                return "Employee Not Updated! Try Again";
            }
        }


    }
}