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
    public class paytypesController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: paytypes
        public ActionResult Index()
        {
            return View(db.paytypes.ToList());
        }

        // GET: paytypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paytype paytype = db.paytypes.Find(id);
            if (paytype == null)
            {
                return HttpNotFound();
            }
            return View(paytype);
        }

        // GET: paytypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: paytypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "paytype_id,paytype_name")] paytype paytype)
        {
            if (ModelState.IsValid)
            {
                db.paytypes.Add(paytype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paytype);
        }

        // GET: paytypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paytype paytype = db.paytypes.Find(id);
            if (paytype == null)
            {
                return HttpNotFound();
            }
            return View(paytype);
        }

        // POST: paytypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "paytype_id,paytype_name")] paytype paytype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paytype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paytype);
        }

        // GET: paytypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paytype paytype = db.paytypes.Find(id);
            if (paytype == null)
            {
                return HttpNotFound();
            }
            return View(paytype);
        }

        // POST: paytypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paytype paytype = db.paytypes.Find(id);
            db.paytypes.Remove(paytype);
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
