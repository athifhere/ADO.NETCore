using Microsoft.AspNetCore.Mvc;

namespace ADO.NETCore.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}
