using Microsoft.AspNetCore.Mvc;
using MvcSerializer.Models;
using System.Xml.Serialization;

namespace MvcSerializer.Controllers
{
    public class SerializationController : Controller
    {
        
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SerializationController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Serialize()
        {
            
            List<Employee> employeeList = new List<Employee>
            {
                new Employee { Id = 101, Name = "Arun Kumar", Department = "IT", Salary = 60000.00m },
                new Employee { Id = 102, Name = "Priya Menon", Department = "HR", Salary = 45000.00m },
                new Employee { Id = 103, Name = "Sandeep Nair", Department = "IT", Salary = 75000.00m }
            };

            
            string wwwRootPath = _hostingEnvironment.WebRootPath;
            string fileName = "employees.xml";
            string filePath = Path.Combine(wwwRootPath, fileName);

            
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, employeeList);
            }

            
            TempData["Message"] = $"Successfully serialized {employeeList.Count} employees to '{fileName}'!";

            
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public IActionResult Deserialize()
        {
            
            string wwwRootPath = _hostingEnvironment.WebRootPath;
            string fileName = "employees.xml";
            string filePath = Path.Combine(wwwRootPath, fileName);

            
            if (!System.IO.File.Exists(filePath))
            {
                TempData["Message"] = "Error: employees.xml not found. Please serialize first.";
                return RedirectToAction("Index");
            }

            
            List<Employee> deserializedList = new List<Employee>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                deserializedList = (List<Employee>)serializer.Deserialize(reader);
            }

            
            return View("Display", deserializedList);
        }
    }
}