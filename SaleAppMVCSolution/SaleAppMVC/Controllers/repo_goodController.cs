using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleAppMVC.Models;

namespace SaleAppMVC.Controllers
{
    public class repo_goodController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: repo_good
        public ActionResult Index()
        {
            return View(db.repo_goods.ToList());
        }

        // GET: repo_good/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repo_good repo_good = db.repo_goods.Find(id);
            if (repo_good == null)
            {
                return HttpNotFound();
            }
            return View(repo_good);
        }

        // GET: repo_good/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: repo_good/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "repo_id,good_id,stock")] repo_good repo_good)
        {
            if (ModelState.IsValid)
            {
                db.repo_goods.Add(repo_good);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repo_good);
        }

        // GET: repo_good/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repo_good repo_good = db.repo_goods.Find(id);
            if (repo_good == null)
            {
                return HttpNotFound();
            }
            return View(repo_good);
        }

        // POST: repo_good/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "repo_id,good_id,stock")] repo_good repo_good)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repo_good).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repo_good);
        }

        // GET: repo_good/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            repo_good repo_good = db.repo_goods.Find(id);
            if (repo_good == null)
            {
                return HttpNotFound();
            }
            return View(repo_good);
        }

        // POST: repo_good/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo_good repo_good = db.repo_goods.Find(id);
            db.repo_goods.Remove(repo_good);
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
