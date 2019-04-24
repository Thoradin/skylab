using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EgyetemiSzoftverek.Models;

namespace EgyetemiSzoftverek.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult AdminGuide() => View();

        public IActionResult Download() => View();

        public IActionResult Contact() => View();

        public IActionResult DownloadErrors() => View();

        public IActionResult DownloadGuide() => View();

        public IActionResult Faq() => View();

        public IActionResult Licence() => View();

        public IActionResult Program() => View();

        public IActionResult RegistrationGuide() => View();
    }
}
