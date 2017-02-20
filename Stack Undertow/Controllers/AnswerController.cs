using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Stack_Undertow.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Stack_Undertow.Controllers
{
    public class AnswerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Answer
        public ActionResult Index()
        {
            var answers = db.Answers.Include(a => a.AnswerUser).Include(a => a.QuestionId);
            return View(answers.ToList());
        }

        [Authorize]
        public ActionResult Like(int Qid, string AId, int An)
        {
            Answer answer = db.Answers.Find(An);
            answer.Score++;
            Point points = new Point();
            {
                points.PointId = AId;
                points.Points = 10;
                points.Reason = "Answer was liked";
                points.Created = DateTime.Now;
            }
            db.Points.Add(points);
            db.SaveChanges();
            return RedirectToAction("Details", "Question", new { id = Qid });
        }

        [Authorize]
        public ActionResult Dislike(int Qid, string AId, int An)
        {
            Answer answer = db.Answers.Find(An);
            answer.Score--;

            Point points = new Point();
            {
                points.PointId = AId;
                points.Points = -5;
                points.Reason = "Answer was disliked";
                points.Created = DateTime.Now;
            }
            db.Points.Add(points);

            Point point1 = new Point();
            {
                points.PointId = User.Identity.GetUserId();
                points.Points = -1;
                points.Reason = "Disliked an answer";
                points.Created = DateTime.Now;
            }
            db.Points.Add(points);
             

            db.SaveChanges();
            return RedirectToAction("Details", "Question", new { id = Qid });
        }

        [Authorize]
        public ActionResult Upload(int id)
        {
            var uploadViewModel = new ImageUploadViewModel();
            return View(uploadViewModel);
        }

        [HttpPost]
        public ActionResult Upload(ImageUploadViewModel formData, int id)
        {
            var Aid = id.ToString();
            var uploadedFile = Request.Files[0];
            string filename = $"{DateTime.Now.Ticks}{uploadedFile.FileName}";
            var serverPath = Server.MapPath(@"~\Uploads");
            var fullPath = Path.Combine(serverPath, filename);
            uploadedFile.SaveAs(fullPath);
            
            var uploadModel = new ImageUpload
            {
                Caption = Aid,
                File = filename
            };
            db.ImageUploads.Add(uploadModel);
            db.SaveChanges();
            return RedirectToAction("Details", "Answer", new { id });
        }
        // GET: Answer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            if (db.ImageUploads.Where(u => u.Caption == "ANSWERNAME").FirstOrDefault() != null)
            {
                var pic = db.ImageUploads.Where(u => u.Caption == "ANSWERNAME").FirstOrDefault();
                ViewBag.AnswerPic = pic.FilePath;
            }
            return View(answer);
        }

        // GET: Answer/Create
        [Authorize]
        public ActionResult Create(int? Qid)
        {
           
            return View();
        }

        // POST: Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Score,Created,AnswerText,Best,Answerer,QId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.Created = DateTime.Now;
                answer.Answerer = User.Identity.GetUserId();
                db.Answers.Add(answer);
                db.SaveChanges();
                return RedirectToAction("Upload", "Answer", new { answer.Id });
            }

            return View(answer);
        }

        // GET: Answer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Answerer = new SelectList(db.Answers, "Id", "Email", answer.Answerer);
            ViewBag.QId = new SelectList(db.Questions, "Id", "Title", answer.QId);
            return View(answer);
        }

        // POST: Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Score,Created,AnswerText,Best,Answerer,QId")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(answer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Answerer = new SelectList(db.Answers, "Id", "Email", answer.Answerer);
            ViewBag.QId = new SelectList(db.Questions, "Id", "Title", answer.QId);
            return View(answer);
        }

        // GET: Answer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Answer answer = db.Answers.Find(id);
            if (answer == null)
            {
                return HttpNotFound();
            }
            return View(answer);
        }

        // POST: Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
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
