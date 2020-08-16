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
    public class salepricesController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: saleprices
        public ActionResult Index()
        {
            return View(db.saleprices.ToList());
        }

        // GET: saleprices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            saleprice saleprice = db.saleprices.Find(id);
            if (saleprice == null)
            {
                return HttpNotFound();
            }
            return View(saleprice);
        }

        // GET: saleprices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: saleprices/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "storegood_id,sale_date,sale_price")] saleprice saleprice)
        {
            if (ModelState.IsValid)
            {
                db.saleprices.Add(saleprice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(saleprice);
        }

        // GET: saleprices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            saleprice saleprice = db.saleprices.Find(id);
            if (saleprice == null)
            {
                return HttpNotFound();
            }
            return View(saleprice);
        }

        // POST: saleprices/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "storegood_id,sale_date,sale_price")] saleprice saleprice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saleprice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saleprice);
        }

        // GET: saleprices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            saleprice saleprice = db.saleprices.Find(id);
            if (saleprice == null)
            {
                return HttpNotFound();
            }
            return View(saleprice);
        }

        // POST: saleprices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            saleprice saleprice = db.saleprices.Find(id);
            db.saleprices.Remove(saleprice);
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
