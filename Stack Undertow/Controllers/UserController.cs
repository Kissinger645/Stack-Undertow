using Microsoft.AspNet.Identity;
using Stack_Undertow.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stack_Undertow.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserName();
            ViewBag.User = userId;
            ViewBag.MyPoints = 10;
            ViewBag.ProfilePic = db.ImageUploads.Where(u => u.Caption == userId).ToList();
            ViewBag.MyQuestions = db.Questions.Where(u => u.Poster == userId).ToList();
            return View();
        }
        public ActionResult Upload()
        {
            var uploadViewModel = new ImageUploadViewModel();
            return View(uploadViewModel);
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel formData)
        {
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);

            // ---------------------

            var uploadModel = new ImageUpload
            {
                Caption = User.Identity.GetUserName(),
                File = filename
            };
            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}