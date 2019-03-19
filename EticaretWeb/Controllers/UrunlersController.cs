using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EticaretWeb.Models;

namespace EticaretWeb.Controllers
{
    
    public class UrunlersController : Controller
    {
        private eticaretEntities db = new eticaretEntities();

        // GET: Urunlers
        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Kategori);
            return View(urunler.ToList());
        }

        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            ViewBag.RefKategoriID = new SelectList(db.Kategori, "KategoriId", "KategoriAdi");
            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunId,UrunAdi,RefKategoriID,UrunAciklama,UrunFiyat")] Urunler urunler,HttpPostedFileBase UrunResim)
        {
            if (ModelState.IsValid)
            {
               
                db.Urunler.Add(urunler);
                db.SaveChanges();
                if (UrunResim!=null && UrunResim.ContentLength>0)
                {
                    string aa = Path.Combine(Server.MapPath("~/Resim"),urunler.UrunId+".jpg");
                    UrunResim.SaveAs(aa);
                }
                return RedirectToAction("Index");
            }

            ViewBag.RefKategoriID = new SelectList(db.Kategori, "KategoriId", "KategoriAdi", urunler.RefKategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefKategoriID = new SelectList(db.Kategori, "KategoriId", "KategoriAdi", urunler.RefKategoriID);
            return View(urunler);
        }

        // POST: Urunlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunId,UrunAdi,RefKategoriID,UrunAciklama,UrunFiyat")] Urunler urunler,HttpPostedFileBase UrunResim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                if (UrunResim != null && UrunResim.ContentLength > 0)
                {
                    string aa = Path.Combine(Server.MapPath("~/Resim"), urunler.UrunId + ".jpg");
                    UrunResim.SaveAs(aa);
                }
                return RedirectToAction("Index");
            }
            ViewBag.RefKategoriID = new SelectList(db.Kategori, "KategoriId", "KategoriAdi", urunler.RefKategoriID);
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
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
