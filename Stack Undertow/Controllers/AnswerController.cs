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

        public ActionResult Like(int Qid, string AId)
        {
            
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
        public ActionResult Dislike(int Qid, string AId)
        {
            Point points = new Point();
            {
                points.PointId = AId;
                points.Points = -5;
                points.Reason = "Answer was disliked";
                points.Created = DateTime.Now;
            }
            db.Points.Add(points);
            db.SaveChanges();
            return RedirectToAction("Details", "Question", new { id = Qid });
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
            return View(answer);
        }

        // GET: Answer/Create
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
                return RedirectToAction("Details", "Question", new { id = answer.QId });
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
