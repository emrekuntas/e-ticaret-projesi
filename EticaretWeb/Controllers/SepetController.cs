using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EticaretWeb.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;

namespace EticaretWeb.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        eticaretEntities db = new eticaretEntities();
        // GET: Sepet
        public ActionResult Index()
        {
            //string userId = User.Identity.GetUserId();
            var sepets = db.Sepet.Include(s => s.Urunler);
            return View(sepets.ToList());
        }
        public ActionResult SepeteEkle(int? adet, int id)
        {
            string userId = User.Identity.GetUserId();
            Sepet sepettekiUrun = db.Sepet.FirstOrDefault(a => a.RefAspNetUserId == userId && a.refUrunId == id);
            Urunler urun = db.Urunler.Find(id);

            if (sepettekiUrun == null)
            {
                Sepet yeniurun = new Sepet()
                {
                    RefAspNetUserId = userId,
                    refUrunId = id,
                    Adet = adet ?? 1,
                    Tutar = (adet ?? 1) * urun.UrunFiyat
                };
                db.Sepet.Add(yeniurun);
            }
            else
            {

                sepettekiUrun.Adet = sepettekiUrun.Adet + (adet ?? 1);

                sepettekiUrun.Tutar = sepettekiUrun.Adet * urun.UrunFiyat;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SepetGuncelle(int? adet, int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sepet sepet = db.Sepet.Find(id);
            if (sepet == null)
            {

                return HttpNotFound();
            }
            Urunler urun = db.Urunler.Find(sepet.refUrunId);
            sepet.Adet = adet?? 1;
            sepet.Tutar = sepet.Adet * urun.UrunFiyat;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            db.Sepet.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}