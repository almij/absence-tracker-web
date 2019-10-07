using AbsenceTrackerLibrary;
using AbsenceTrackerMVC.Helpers;
using AbsenceTrackerMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace AbsenceTrackerMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("PersonalData");
        }

        [Authorize]
        public IActionResult Absences()
        {
            var personDB = AbsenceTracker.GetPerson(User.Id());
            var absencesDB = AbsenceTracker.GetAbsences(personDB.Id);
            var absences = absencesDB.Select(_ => new AbsenceModel()
            {
                AbsenceType = _.AbsenceType.ToString(),
                EffectiveFrom = _.EffectiveFrom,
                WorkDaysTotal = _.WorkDaysTotal,
                IsSingleDay = _.IsSingleWorkDay,
                WorkHoursTotal = _.WorkHoursTotal
            }).ToList();

            return View(absences);
        }

        [Authorize]
        public IActionResult PersonalData()
        {
            ViewData["Message"] = "Personal Data";
            var personDB = AbsenceTracker.GetPerson(User.Id());
            var person = new PersonModel
            {
                EmailAddress = User.Name(),
                FirstName = personDB?.FirstName,
                LastName = personDB?.LastName
            };
            return View(person);
        }

        [HttpPost]
        public IActionResult PersonalData(PersonModel person)
        {
            var personDB = AbsenceTracker.GetPerson(User.Id());
            personDB.FirstName = person.FirstName;
            personDB.LastName = person.LastName;
            AbsenceTracker.SaveUserData(personDB);

            return RedirectToAction("PersonalData");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
