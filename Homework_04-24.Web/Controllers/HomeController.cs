using Homework_04_24.Data;
using Homework_04_24.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework_04_24.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source = .\sqlexpress; Initial Catalog = Cars_Games_People; Integrated Security = true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            var people = repo.GetPeople();
            return Json(people);
        }

        public IActionResult GetById(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            var person = repo.GetPersonById(id);
            return Json(person);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.AddPerson(person);
        }

        [HttpPost]
        public void EditPerson(Person p)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.UpdatePerson(p);
        }

        [HttpPost]
        public void DeletePerson(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePerson(id);
        }
    }
}