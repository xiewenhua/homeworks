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
    public class snapshotsController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: snapshots
        public ActionResult Index()
        {
            return View(db.snapshots.ToList());
        }

        // GET: snapshots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snapshot snapshot = db.snapshots.Find(id);
            if (snapshot == null)
            {
                return HttpNotFound();
            }
            return View(snapshot);
        }

        // GET: snapshots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: snapshots/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "snapshot_id,snapshot_time,good_id,good_name")] snapshot snapshot)
        {
            if (ModelState.IsValid)
            {
                db.snapshots.Add(snapshot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(snapshot);
        }

        // GET: snapshots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snapshot snapshot = db.snapshots.Find(id);
            if (snapshot == null)
            {
                return HttpNotFound();
            }
            return View(snapshot);
        }

        // POST: snapshots/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "snapshot_id,snapshot_time,good_id,good_name")] snapshot snapshot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(snapshot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(snapshot);
        }

        // GET: snapshots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            snapshot snapshot = db.snapshots.Find(id);
            if (snapshot == null)
            {
                return HttpNotFound();
            }
            return View(snapshot);
        }

        // POST: snapshots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            snapshot snapshot = db.snapshots.Find(id);
            db.snapshots.Remove(snapshot);
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
