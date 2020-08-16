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
    public class storegoodsController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: storegoods
        public ActionResult Index()
        {
            return View(db.storegoods.ToList());
        }

        // GET: storegoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storegood storegood = db.storegoods.Find(id);
            if (storegood == null)
            {
                return HttpNotFound();
            }
            return View(storegood);
        }

        // GET: storegoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: storegoods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "storegood_id,stor_id,good_id")] storegood storegood)
        {
            if (ModelState.IsValid)
            {
                db.storegoods.Add(storegood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(storegood);
        }

        // GET: storegoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storegood storegood = db.storegoods.Find(id);
            if (storegood == null)
            {
                return HttpNotFound();
            }
            return View(storegood);
        }

        // POST: storegoods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "storegood_id,stor_id,good_id")] storegood storegood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(storegood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(storegood);
        }

        // GET: storegoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            storegood storegood = db.storegoods.Find(id);
            if (storegood == null)
            {
                return HttpNotFound();
            }
            return View(storegood);
        }

        // POST: storegoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            storegood storegood = db.storegoods.Find(id);
            db.storegoods.Remove(storegood);
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
