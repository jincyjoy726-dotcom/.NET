using Microsoft.AspNetCore.Mvc;
using EmployeeMVC.Models;
using System.Text.Json;
using System.Text;


namespace EmployeeMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee>? employees = new List<Employee>();

            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");

            var response = await httpClient.GetAsync("api/Employees");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                employees = JsonSerializer.Deserialize<List<Employee>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // This helps if the JSON has lowercase properties
                });
            }
            return View(employees);

        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await httpClient.GetAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employee = JsonSerializer.Deserialize<Employee>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(employee);
            }
            return NotFound();
        }

        // GET: /Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");
            var jsonContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("api/Employees", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: /Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // This is the same logic as the Details method
            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await httpClient.GetAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employee = JsonSerializer.Deserialize<Employee>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(employee);
            }
            return NotFound();
        }

        // POST: /Employees/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");
            var jsonContent = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync($"api/Employees/{id}", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: /Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            // This is the same logic as the Details method
            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await httpClient.GetAsync($"api/Employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var employee = JsonSerializer.Deserialize<Employee>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(employee);
            }
            return NotFound();
        }

        // POST: /Employees/Delete/5
        [HttpPost, ActionName("Delete")] // This tells ASP.NET to use this method for the POST from the Delete view
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("EmployeeAPI");
            var response = await httpClient.DeleteAsync($"api/Employees/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            // You might want to add an error message here
            return RedirectToAction(nameof(Index));
        }
    }
}
    

