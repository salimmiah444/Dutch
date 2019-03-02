using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreatFinal.Data;
using DutchTreatFinal.Services;
using DutchTreatFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DutchTreatFinal.Controllers
{

    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;

        // private readonly DutchContext _context;
        //private readonly DutchContext _ctx;

        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
            //_context = context;
            //_ctx = ctx;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            // throw new InvalidOperationException("Bad things happened");
           // var results = _ctx.Products.ToList();
             return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            //ViewBag.Title = "Contact Us";
            //throw new InvalidOperationException("Bad things happen");
            return View();
        }
        [HttpPost ("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Send the email
                _mailService.SendMessage("salimmiah_2007@yahoo.com", 
                    model.Subject, $"From: {model.Name} - {model.Email} , Message: {model.Message}");
                ViewBag.UserMessage = "Mail sent";
                ModelState.Clear();
            }
        //    else
        //    {
        //        //Show the error
        //    }
            
            return View();
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            ViewBag.Title = "About Us";
            return View();
        }
        [Authorize]
        public IActionResult Shop()
        {
            //var name = Request.Query.Keys.Contains("name");
            //var nameValue = Request.Query["name"];
            //var email = Request.Query["email"];
            var results = _repository.GetAllProducts();
            //var results = _context.Products.ToList()
            //    .OrderBy(p => p.Category)
            //    .ToList();
            return View();
           // return View(results);
        }
    }
}
