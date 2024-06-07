using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Uri baseAddress = new Uri("https://localhost:7192/api/Students/");

        private readonly HttpClient _client;

        public StudentsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<StudentViewModel> studentList = new List<StudentViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "GetAllStudents").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                studentList = JsonConvert.DeserializeObject<List<StudentViewModel>>(data);
            }
            return View(studentList);
        }

        [HttpGet("GetStudentsById")]
        public IActionResult GetStudentDetails(int Id)
        {
            if (Id == 0)
            {
                return BadRequest("Invalid student Id.");
            }

            HttpResponseMessage response = _client.GetAsync($"{_client.BaseAddress}GetStudentsById?Id={Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var student = JsonConvert.DeserializeObject<StudentViewModel>(data);
                return View("StudentDetails", student); // Ensure you have a view named "StudentDetails"
            }
            else
            {
                // Handle different response statuses accordingly
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound("Student not found.");
                }

                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }


        [HttpPut]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            HttpResponseMessage response = _client.PutAsync($"{_client.BaseAddress}DeleteStudent?Id={Id}", null).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }


        public async Task<IActionResult> AddStudent(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            HttpResponseMessage response = await _client.PostAsJsonAsync("AddStudent", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStudentDetails(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            HttpResponseMessage response = await _client.GetAsync($"GetstudentDetailsByID?id={id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var student = JsonConvert.DeserializeObject<StudentViewModel>(data);
                return View(student);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentDetails(int id, StudentViewModel student)
        {
            if (id != student.Id || !ModelState.IsValid)
            {
                return View(student);
            }

            HttpResponseMessage response = await _client.PostAsJsonAsync($"UpdateStudentDetails?id={id}", student);
            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }
    }
}
