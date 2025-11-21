using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class ProfileEMPController : Controller
    {
        EmployeeDB dbobj=new EmployeeDB();
        public IActionResult UserProfile_Pageload(int id)
        {
            Employee getlist = dbobj.SelectProfileDB(id);
            return View(getlist);
        }
        public IActionResult Profile_updateClick(Employee objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string s = dbobj.UpdateDB(objcls);
                    TempData["msg1"] = s;
                }
            }
            catch (Exception ex)
            {
                TempData["msg1"] = ex.Message.ToString();
            }
            return View("UserProfile_Pageload");
        }
    }
    
}
