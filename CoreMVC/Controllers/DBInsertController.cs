using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;
namespace CoreMVC.Controllers
{
    public class DBInsertController : Controller
    {
        EmployeeDB dbobj=new EmployeeDB();
        public IActionResult Index_Pageload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index_Click(Employee clsobj)
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5028/EMP/");
                var postTask = client.PostAsJsonAsync<Employee>("posttab", clsobj);
                postTask.Wait();
                var result=postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllProfile_Pageload", "DBDisplayAll");
                }
            }
            return RedirectToAction("AllProfile_Pageload", "DBDisplayAll");
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        string resp = dbobj.InsertDB(clsobj);
            //        TempData["msg"] = resp;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    TempData["msg"]= ex.Message;
            //}
            //return View("Index_Pageload");
        }
    }
}
