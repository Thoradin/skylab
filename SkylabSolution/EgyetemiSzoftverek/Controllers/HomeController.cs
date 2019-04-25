using EgyetemiSzoftverek.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace EgyetemiSzoftverek.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Contact> Contacts;

        public HomeController(IOptions<List<Contact>> contactOptions)
        {
            Contacts = contactOptions.Value;
        }

        [Route("")]
        public IActionResult Index() => View();

        public IActionResult Rendszergazdaknak() => View("AdminGuide");

        public IActionResult Letoltes() => View("Download");

        public IActionResult Kapcsolat() => View("Contact", Contacts);

        public IActionResult Hibakodok() => View("DownloadErrors");

        public IActionResult LetoltesiUtmutato() => View("DownloadGuide");

        public IActionResult Gyik() => View("Faq");

        public IActionResult Licenc() => View("Licence");

        public IActionResult AzureDevTools() => View("Program");

        public IActionResult RegisztraciosUtmutato() => View("RegistrationGuide");
    }
}
