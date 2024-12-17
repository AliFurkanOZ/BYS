using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BYS.Data;
using BYS.Models;

namespace BYS.Controllers.WebController
{
    public class StudentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly RepositoryDbContext _repositoryDbContext;

        // IHttpClientFactory DI ile inject ediliyor
        public StudentController(IHttpClientFactory httpClientFactory, RepositoryDbContext repositoryDbContext)
        {
            _httpClient = httpClientFactory.CreateClient();
            _repositoryDbContext = repositoryDbContext;
        }

        // ID'ye göre öğrenci verilerini API'den alıyoruz
        public async Task<IActionResult> Index(int id)
        {
            // API endpoint URL'si
            var userId = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("LoginUser", "Account");
            }
            var apiUrl = $"https://localhost:7186/api/students/{id}";

            try
            {
                // API'den öğrenci verilerini al
                var response = await _httpClient.GetAsync(apiUrl);

                // Eğer HTTP yanıtı başarılı değilse, hata mesajı döndür
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Error fetching student data. Status Code: " + response.StatusCode;
                    return View("Error"); // Hata sayfasına yönlendirme
                }

                // Başarılı yanıt, JSON verisini al
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // JSON verisini Student modeline deserialize et
                var student = JsonConvert.DeserializeObject<Student>(jsonResponse);

                // Öğrenci verisini view'a gönder
                return View(student);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajını view'a gönder
                ViewBag.ErrorMessage = "An error occurred while fetching data: " + ex.Message;
                return View("Error");
            }
        }

        
        public IActionResult ConfirmedSelected()
        {
            return View();
        }







    }
}