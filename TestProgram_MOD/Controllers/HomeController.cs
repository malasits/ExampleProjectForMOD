using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestProgram_MOD.Models;

namespace TestProgram_MOD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //Get all records
        public JsonResult GetAllPeople()
        {

                using (DbForMODEntity dataContext = new DbForMODEntity())
                {
                    var employeeList = dataContext.V_GetAllDataFromDatabase.ToList();
                    return Json(employeeList, JsonRequestBehavior.AllowGet);
                }
            

        }

        //Insert new person
        public string AddPerson(People p)
        {
            if (p != null)
            {
                using (DbForMODEntity dataContext = new DbForMODEntity())
                {
                    dataContext.P_InsertData(p.FirstName, p.LastName, p.BornDate);
                    dataContext.SaveChanges();
                    return "Person Updated";
                }
            }
            else
            {
                return "Invalid Person";
            }
        }

        //Update person
        public string UpdatePerson(People p)
        {
            if (p != null)
            {
                using (DbForMODEntity dataContext = new DbForMODEntity())
                {
                    int no = Convert.ToInt32(p.ID);
                    var employeeList = dataContext.P_UpdateData(no, p.FirstName, p.LastName, p.BornDate);
                    dataContext.SaveChanges();
                    return "Person Updated";
                }
            }
            else
            {
                return "Invalid Person";
            }
        }

        //Delete person
        public string DeletePerson(People p)
        {
            if (p != null)
            {
                using (DbForMODEntity dataContext = new DbForMODEntity())
                {
                    int no = Convert.ToInt32(p.ID);
                    var employeeList = dataContext.P_DeleteData(no);
                    dataContext.SaveChanges();
                    return "Person Deleted";
                }
            }
            else
            {
                return "Invalid Person";
            }
        }

        public JsonResult getEmployeeByNo(string EmpNo)
        {
            using (DbForMODEntity dataContext = new DbForMODEntity())
            {
                int no = Convert.ToInt32(EmpNo);
                var employeeList = dataContext.People.Find(no);
                return Json(employeeList, JsonRequestBehavior.AllowGet);
            }
        }
    }
}