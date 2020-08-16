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
    public class ordergoodsController : Controller
    {
        private HomeworksDBEntities db = new HomeworksDBEntities();

        // GET: ordergoods
        public ActionResult Index()
        {
            return View(db.ordergoods.ToList());
        }

        // GET: ordergoods/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordergood ordergood = db.ordergoods.Find(id);
            if (ordergood == null)
            {
                return HttpNotFound();
            }
            return View(ordergood);
        }

        // GET: ordergoods/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ordergoods/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ordergood_id,order_num,good_id,ordergood_quantity")] ordergood ordergood)
        {
            if (ModelState.IsValid)
            {
                db.ordergoods.Add(ordergood);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ordergood);
        }

        // GET: ordergoods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordergood ordergood = db.ordergoods.Find(id);
            if (ordergood == null)
            {
                return HttpNotFound();
            }
            return View(ordergood);
        }

        // POST: ordergoods/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ordergood_id,order_num,good_id,ordergood_quantity")] ordergood ordergood)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordergood).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ordergood);
        }

        // GET: ordergoods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ordergood ordergood = db.ordergoods.Find(id);
            if (ordergood == null)
            {
                return HttpNotFound();
            }
            return View(ordergood);
        }

        // POST: ordergoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ordergood ordergood = db.ordergoods.Find(id);
            db.ordergoods.Remove(ordergood);
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
