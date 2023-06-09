﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTripProje.Models.Siniflar;

namespace TravelTripProje.Controllers
{
    public class AdminController : Controller
    {
        Context c = new Context();       
        [Authorize] //Web.config dosyasında 28.satırda yazdığımız kod sayesinde burada "Authorize" keywordünü kullanarak, admin tarafındaki herhangi bir sayfadan projeyi başlattığımızda otomatik olarak login sayfasına yönlendirecektir.
        public ActionResult Index()
        {
            var degerler = c.Blogs.ToList();
            return View(degerler);
        }
        [HttpGet] //sayfa yüklendiğinde
        public ActionResult YeniBlog()
        {
            return View(); //sayfanın boş halini döndür.
        }
        [HttpPost] //sayfada işlem yapıldığında
        public ActionResult YeniBlog(Blog p)
        {
            c.Blogs.Add(p); //contextten ürettiğim c nesnesine bağlı olarak parametreden gelen değerleri(Textboxforlardan gelen)bloglar içerisine ekle.
            c.SaveChanges();
            return RedirectToAction("Index"); //Index aksiyonuna yönlendir.
        }
        public ActionResult BlogSil(int id)
        {
            var b = c.Blogs.Find(id); //b değerinde değişken tanımla, bu da c'nin içindeki bloglarda id yi bulsun.
            c.Blogs.Remove(b); // c'nin içindeki bloglar için tanımladığımız b değeri silinsin.
            c.SaveChanges(); // değişiklikleri kaydet
            return RedirectToAction("Index"); //index sayfasına yönlendir
        }
        //Güncellenecek olan blogun bilgilerini otomatik olarak doldur.
        public ActionResult BlogGetir(int id)
        {
            var bl = c.Blogs.Find(id);
            return View("BlogGetir", bl); // blog getir sayfasını döndür ve bl'den gelen değerleri de getir.
        }
        //Güncelleme İşlemleri
        public ActionResult BlogGuncelle(Blog b)
        {
            var blg = c.Blogs.Find(b.ID);
            blg.Aciklama = b.Aciklama;
            blg.Baslik = b.Baslik;
            blg.BlogImage = b.BlogImage;
            blg.Tarih = b.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        //Yorum Listesi Getirme
        public ActionResult YorumListesi()
        {
            var yorumlar = c.Yorumlars.ToList();
            return View(yorumlar);
        }
        //Yorum Silme
        public ActionResult YorumSil(int id)
        {
            var y = c.Yorumlars.Find(id);
            c.Yorumlars.Remove(y);
            c.SaveChanges();
            return RedirectToAction("YorumListesi");
        }
        public ActionResult YorumGetir(int id)
        {
            var yr = c.Yorumlars.Find(id);
            return View("YorumGetir", yr);
        }
        //Yorum Güncelle
        public ActionResult YorumGuncelle(Yorumlar y)
        {
            var yrm = c.Yorumlars.Find(y.ID);
            yrm.KullaniciAdi = y.KullaniciAdi;
            yrm.Mail = y.Mail;
            yrm.Yorum = y.Yorum;
            c.SaveChanges();
            return RedirectToAction("YorumListesi");
        }
        BlogYorum by = new BlogYorum();
        public ActionResult BlogDetays(int id)
        {
            by.Deger1 = c.Blogs.Where(x=>x.ID==id).ToList();
            by.Deger2 = c.Yorumlars.Where(x=>x.Blogid == id).ToList();
            return View(by);
        }
    }
}