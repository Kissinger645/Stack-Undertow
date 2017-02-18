using Stack_Undertow.Migrations;
using Stack_Undertow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stack_Undertow.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.AllQuestions = db.Questions;
            return View();
        }

    }
}