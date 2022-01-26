using ADO.NETCore.DataAccess;
using ADO.NETCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NETCore.Controllers
{
    public class StudentController : Controller
    {
        DAL dAL = new DAL();
        public IActionResult List()
        {
            IEnumerable<StudentModel> students = dAL.GetAllStudents();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(StudentModel model)
        {
            //IEnumerable<StudentModel> students = null;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                dAL.CreateStudent(model);
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewBag.flag = false;
            StudentModel student = dAL.GetStudent(id);
            return View("Create", student);
        }


        [HttpPost]
        public IActionResult Edit(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                dAL.UpdateStudent(model);
                return RedirectToAction("List");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            StudentModel student = dAL.GetStudent(id);
            if(student != null)
            {
                dAL.DeleteStudent(id);
            }
            return RedirectToAction("List");
        }

        public IActionResult Details(int id)
        {
            StudentModel student = dAL.GetStudent(id);
            ViewBag.flag = true;
            return View("Create", student);
        }
    }
}
