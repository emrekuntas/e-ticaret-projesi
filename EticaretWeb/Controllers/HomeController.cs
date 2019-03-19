using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EticaretWeb.Models;

namespace EticaretWeb.Controllers
{
    public class HomeController : Controller
    {
        eticaretEntities db = new eticaretEntities();

        public ActionResult Index()
        {
            ViewBag.KategoriListesi = db.Kategori.ToList();
            ViewBag.SonUrunler = db.Urunler.OrderByDescending(a => a.UrunId).Skip(0).Take(12).ToList();
            return View();
        }
        public ActionResult Kategori(int id)
        {
            ViewBag.KategoriListesi = db.Kategori.ToList();
            ViewBag.Kategori = db.Kategori.Find(id);
            return View(db.Urunler.Where(a=>a.RefKategoriID==id).OrderBy(a=>a.UrunAdi).ToList());
        }


        public ActionResult Urun(int id)
        {
            ViewBag.KategoriListesi = db.Kategori.ToList();
            return View(db.Urunler.Find(id));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}