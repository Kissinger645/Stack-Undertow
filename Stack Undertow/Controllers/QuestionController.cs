using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Stack_Undertow.Migrations;
using Stack_Undertow.Models;
using System.IO;

namespace Stack_Undertow.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Question
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            var cId = id.ToString();
            if (db.ImageUploads.Where(u => u.Caption == cId).FirstOrDefault() != null)
            {
                var pic = db.ImageUploads.Where(u => u.Caption == cId).FirstOrDefault();
                ViewBag.Screenshot = pic.FilePath;
            }
            
            ViewBag.Answers = db.Answers.Where(q => q.QId == id).OrderByDescending(answer => answer.Score).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }
        
        // GET: Question/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Question,Created")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                questions.Poster = User.Identity.GetUserName();
                questions.QuestionerId = User.Identity.GetUserId();
                questions.Created = DateTime.Now;
                Point points = new Point();
                {
                    points.PointId = User.Identity.GetUserId();
                    points.Points = 5;
                    points.Reason = "Asking Question";
                    points.Created = DateTime.Now;
                }
                db.Points.Add(points);
                db.Questions.Add(questions);
                db.SaveChanges();
                return RedirectToAction("Upload", "Question", new { id = questions.Id});
            }

            return View(questions);
        }

        public ActionResult Upload(int id)
        {
            var uploadViewModel = new ImageUploadViewModel();
            return View(uploadViewModel);
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel formData, int id)
        {
            var qId = id.ToString();
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);

            var uploadModel = new ImageUpload
            {
                Caption = qId,
                File = filename
            };
            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return RedirectToAction("Details", "Question", new { id });
        }

        // GET: Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Question,Created")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questions);
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = db.Questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions questions = db.Questions.Find(id);
            db.Questions.Remove(questions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
