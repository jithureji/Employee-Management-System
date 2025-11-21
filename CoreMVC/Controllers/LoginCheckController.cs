using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;

namespace CoreMVC.Controllers
{
    public class LoginCheckController : Controller
    {
        EmployeeDB obj=new EmployeeDB();
        public IActionResult Login_Load()
        {
            return View();
        }
           [HttpGet]
        public IActionResult Login_Click(Employee clsobj)
        {
                if (ModelState.IsValid)
                {
                    int i = obj.LoginDB(clsobj);
                    if (i == 1)
                    {
                        return RedirectToAction("UserProfile_Pageload","ProfileEmp",new {id=clsobj.eid});
                    }
                    else
                    {
                        TempData["msg"] = "Invalid";
                    }
                }
                return View("Login_Load");
        }
        public IActionResult EmpHome()
        {
            return View();
        }
    }
}
