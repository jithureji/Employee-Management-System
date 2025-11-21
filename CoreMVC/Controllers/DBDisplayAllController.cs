using CoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Controllers
{
    public class DBDisplayAllController : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        public IActionResult AllProfile_PageLoad()
        {
            List<Employee> employees = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5028/EMP/");
                var responseTask = client.GetAsync("GetAlltab");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Employee>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
            }
            return View(employees);
            //List<Employee> getlist = dbobj.SelectDB();
            //return View(getlist);
            //List<Employee> employees = null;
            //using (var client=new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:5130");
            //    var responseTask = client.GetAsync("GetAllTab");
            //    responseTask.Wait();
            //}
            //return View();
        }
        public IActionResult DeleteDB(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5028/EMP/");
                var postTask = client.DeleteAsync($"deletetab/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("AllProfile_Pageload");
                }
            }
            return RedirectToAction("AllProfile_Pageload");
        }
        public IActionResult Detailstab(int? id)
        {
            Employee emp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5028/EMP/");
                var responseTask = client.GetAsync($"gettabWithId/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
                else
                {
                    emp = new Employee();
                }
            }
            return View(emp);
        }
        public IActionResult Edittab(int? id)
        {
            Employee emp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5028/EMP/");
                var responseTask = client.GetAsync($"gettabWithId/{id}");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Employee>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
                else
                {
                    emp = new Employee();
                }
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edittabn(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5028/EMP/");
                    var postTask = client.PutAsJsonAsync<Employee>($"Updatetab/{empobj.eid}", empobj);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("AllProfile_Pageload");
                    }
                }
                return View();
            }
            return View();
        }
    }
}
