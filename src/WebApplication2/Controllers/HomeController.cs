using DataProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonRepository _personRepository;
        public HomeController(ILogger<HomeController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }
        [Route("personDetails/{id}")]
        public IActionResult PersonDetails(Guid id)
        {
            var person = _personRepository.Get(id);
            ViewBag.Id = id;
            return View(person);
        }
        [Route("createPerson/")]
        public IActionResult CreatePerson()
        {
            return View();
        }
        public IActionResult SavePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                var createdPerson = _personRepository.Create(person.FirstName, person.LastName, person.Age);
                return RedirectToAction("Index"); // Redirige vers la page principale ou toute autre action souhaitée
            }
            return View(); // Retourne la vue avec les erreurs de validation
        }
        [Route("deletePerson/{id}")]
        public IActionResult DeletePerson(Guid id)
        {
            if (ModelState.IsValid)
            {
                var createdPerson = _personRepository.Delete(id);
                return RedirectToAction("Index"); // Redirige vers la page principale ou toute autre action souhaitée
            }
            return View(); // Retourne la vue avec les erreurs de validation
        }
        [Route("updatePerson/{id}")]
        public IActionResult UpdatePerson(Guid id)
        {
            var person = _personRepository.Get(id);
            return View(person);
        }
        [Route("confirmUpdatePerson/")]
        public IActionResult ConfirmUpdatePerson(Person person)
        {
            if (ModelState.IsValid)
            {
                var createdPerson = _personRepository.Update(person.PersonId, person.FirstName, person.LastName, person.Age);
                return RedirectToAction("Index"); // Redirige vers la page principale ou toute autre action souhaitée
            }
            return View(); // Retourne la vue avec les erreurs de validation
        }
        public IActionResult Index()
        {
            var people = _personRepository.GetAll();
            return View(people);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
