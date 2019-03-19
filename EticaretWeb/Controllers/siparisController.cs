using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EticaretWeb.Models;
using Microsoft.AspNet.Identity;



namespace EticaretWeb.Controllers
{
    public class siparisController : Controller
    {
        // GET: siparis
        private eticaretEntities db = new eticaretEntities();
        public ActionResult Index()
        {
            var sipariss = db.Siparis.ToList();
            return View(sipariss.ToList());
        }
        public ActionResult SiparisDetay(int siparisID)
        {
            var siparisDetay = db.SiparisKalem.Where(a => a.refSiparisId == siparisID).ToList();
            return View(siparisDetay.ToList());
          
        }
        public ActionResult siparisTamamla()
        {
            string userID = User.Identity.GetUserId();
            IEnumerable<Sepet> sepetUrunleri = db.Sepet.Where(a => a.RefAspNetUserId == userID).ToList();

            string ClientId = "100300000"; // Bankadan aldığınız mağaza kodu
            string Amount = sepetUrunleri.Sum(a => a.Tutar).ToString(); // sepettteki ürünlerin toplam fiyatı
            string Oid = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now); // sipariş id oluşturuyoruz. her sipariş için farklı olmak zorunda
            string OnayURL = "http://localhost:2335/Siparis/Tamamlandi"; // Ödeme tamamlandığında bankadan verilerin geleceği url
            string HataURL = "http://localhost:2335/Siparis/Hatali"; // Ödeme hata verdiğinde bankadan gelen verilerin gideceği url
            string RDN = "asdf"; // hash karşılaştırması için eklenen rast gele dizedir
            string StoreKey = "123456"; // Güvenlik anahtarı bankanın sanal pos sayfasından alıyoruz


            string TransActionType = "Auth"; // bu bölüm sabit değişmiyor
            string Instalment = "";
            string HashStr = ClientId + Oid + Amount + OnayURL + HataURL + TransActionType + Instalment + RDN + StoreKey; // Hash oluşturmak için bankanın bizden istediği stringleri birleştiriyoruz

            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] HashBytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(HashStr);
            byte[] InputBytes = sha.ComputeHash(HashBytes);
            string Hash = Convert.ToBase64String(InputBytes);

            ViewBag.ClientId = ClientId;
            ViewBag.Oid = Oid;
            ViewBag.okUrl = OnayURL;
            ViewBag.failUrl = HataURL;
            ViewBag.TransActionType = TransActionType;
            ViewBag.RDN = RDN;
            ViewBag.Hash = Hash;
            ViewBag.Amount = Amount;
            ViewBag.StoreType = "3d_pay_hosting"; // Ödeme modelimiz biz buna göre anlatıyoruz 
            ViewBag.Description = "";
            ViewBag.XID = "";
            ViewBag.Lang = "tr";
            ViewBag.EMail = "destek@abc.com";
            ViewBag.UserID = "AliVeli"; // bu id yi bankanın sanala pos ekranında biz oluşturuyoruz.
            ViewBag.PostURL = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate";


            return View();
        }

        public ActionResult Tamamlandi()
        {
            string userID = User.Identity.GetUserId();

            Siparis siparis = new Siparis()
            {
                Ad = Request.Form.Get("Ad"),
                Soyad = Request.Form.Get("Soyad"),
                //Adres = Request.Form.Get("Adres"),
                trh = DateTime.Now,
                TcKimlik = Request.Form.Get("TCKimlikNo"),
                Telefon = Request.Form.Get("Telefon"),
                refAspNetUserId = userID
            };

            IEnumerable<Sepet> sepettekiUrunler = db.Sepet.Where(a => a.RefAspNetUserId == userID).ToList();

            foreach (Sepet sepetUrunu in sepettekiUrunler)
            {
                SiparisKalem yeniKalem = new SiparisKalem()
                {
                    Adet = sepetUrunu.Adet,
                    Tutar = sepetUrunu.Tutar,
                    refUrunId = sepetUrunu.refUrunId
                };

                siparis.SiparisKalem.Add(yeniKalem);

                db.Sepet.Remove(sepetUrunu);
            }

            db.Siparis.Add(siparis);
            db.SaveChanges();

            return View();
        }

        public ActionResult Hatali()
        {
            ViewBag.Hata = Request.Form;

            return View();
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