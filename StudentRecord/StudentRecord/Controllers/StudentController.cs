using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StudentRecord.Models;
using StudentRecord.Helpers;


namespace StudentRecords.Controllers
{
    public class StudentController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public StudentController(IWebHostEnvironment env)
        {
            _env = env;
        }

        
        public IActionResult Index()
        {
            var students = FileHelper.ReadAll(_env);
            return View(students);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);

            var existing = FileHelper.FindByRoll(_env, student.RollNumber);
            if (existing != null)
            {
                ModelState.AddModelError("RollNumber", "A student with this roll number already exists.");
                return View(student);
            }

            FileHelper.AppendStudent(_env, student);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string rollNumber)
        {
            if (string.IsNullOrWhiteSpace(rollNumber))
            {
                ModelState.AddModelError("", "Enter a roll number.");
                return View();
            }

            var student = FileHelper.FindByRoll(_env, rollNumber.Trim());
            if (student == null) return View("NotFound");

            return View("Details", student);
        }

        
        public IActionResult Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();
            var student = FileHelper.FindByRoll(_env, id);
            if (student == null) return NotFound();
            return View(student);
        }
    }
}