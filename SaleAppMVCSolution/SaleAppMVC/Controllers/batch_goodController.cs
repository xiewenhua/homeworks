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
    public class batch_goodController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: batch_good
        public ActionResult Index()
        {
            return View(db.batch_goods.ToList());
        }

        // GET: batch_good/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch_good batch_good = db.batch_goods.Find(id);
            if (batch_good == null)
            {
                return HttpNotFound();
            }
            return View(batch_good);
        }

        // GET: batch_good/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: batch_good/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "batch_id,good_id,purchase_price,purchase_quantity,surplus")] batch_good batch_good)
        {
            if (ModelState.IsValid)
            {
                db.batch_goods.Add(batch_good);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(batch_good);
        }

        // GET: batch_good/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch_good batch_good = db.batch_goods.Find(id);
            if (batch_good == null)
            {
                return HttpNotFound();
            }
            return View(batch_good);
        }

        // POST: batch_good/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "batch_id,good_id,purchase_price,purchase_quantity,surplus")] batch_good batch_good)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batch_good).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(batch_good);
        }

        // GET: batch_good/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            batch_good batch_good = db.batch_goods.Find(id);
            if (batch_good == null)
            {
                return HttpNotFound();
            }
            return View(batch_good);
        }

        // POST: batch_good/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            batch_good batch_good = db.batch_goods.Find(id);
            db.batch_goods.Remove(batch_good);
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
