using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace CalculatorMvc.Controllers
{
    public class CalculatorClientController : Controller
    {
        private readonly IHttpClientFactory _httpFactory;
        public CalculatorClientController(IHttpClientFactory httpFactory) => _httpFactory = httpFactory;

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Add(int a, int b)
        {
            var client = _httpFactory.CreateClient("CalculatorApi");
            int result = await client.GetFromJsonAsync<int>($"api/calculator/add?a={a}&b={b}");
            ViewBag.Result = result;
            return View("Result");
        }

        [HttpPost]
        public async Task<IActionResult> Subtract(int a, int b)
        {
            var client = _httpFactory.CreateClient("CalculatorApi");
            int result = await client.GetFromJsonAsync<int>($"api/calculator/subtract?a={a}&b={b}");
            ViewBag.Result = result;
            return View("Result");
        }
    }
}