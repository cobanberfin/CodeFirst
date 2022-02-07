using CodeFirstKisiler.Models;
using CodeFirstKisiler.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeFirstKisiler.Controllers
{
    public class AdresController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Adres
        public ActionResult Yeni()
        {//ısmı secıyor ıdsı gelıyro=dropdownlıst mantıgı
            List<SelectListItem> kisilerlist = db.Kisiler.Select(x => new SelectListItem { Text = x.Ad, Value = x.ID.ToString() }).ToList();
            TempData["Kisiler"] = kisilerlist;//yönlendrıme gırsede verıler kaybolmuyor 
            ViewBag.kisiler = kisilerlist;
            return View();
        }
        [HttpPost]
        public ActionResult Yeni(Adres adres)
        {
            Kisiler kisi = db.Kisiler.Where(x => x.ID == adres.KisiId).FirstOrDefault();
            if (kisi != null)
            {
                adres.Kisiler = kisi;
                db.Adresler.Add(adres);
                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres başarıyla eklenmiştir";
                    ViewBag.Status = "Success";
                }
                else
                {

                    ViewBag.Result = "Adres başarıyla eklenmemiştir";
                    ViewBag.Status = "danger";
                }
                ViewBag.kisiler = TempData["Kisiler"];

            }
            return View();

        }
        public ActionResult Duzenle(int? adresid)
        {
            Adres adres = new Adres();
            if (adresid != null)
            {
                List<SelectListItem> kisilerlist = db.Kisiler.Select(x => new SelectListItem()
                {
                    Text=x.Ad,
                    Value=x.ID.ToString()


                }).ToList();
                TempData["Kisiler"] = kisilerlist;
                ViewBag.kisiler = kisilerlist;
                adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
            }
            return View(adres);


        }
        [HttpPost]
        public ActionResult Duzenle(Adres adress, int? adresid)
        {
            Kisiler kisi = db.Kisiler.Where(x => x.ID == adress.KisiId).FirstOrDefault();
            Adres adres = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
            if (kisi != null)
            {
                adres.Kisiler = kisi;
                adres.AdresTanim = adress.AdresTanim;
             
                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres başarıyla güncellenmiştir";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Adres Başarıyla güncellenmemiştir";
                    ViewBag.Status = "danger";
                }
                ViewBag.kisiler = TempData["Kisiler"];
            }
            return View();
        }

        public ActionResult Sil(int? adresid)
        {
            Adres adresx = new Adres();
            if (adresid != null)
            {
                adresx = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
                adresx.Kisiler = db.Kisiler.Where(x => x.ID == adresx.KisiId).FirstOrDefault();
            }
            return View(adresx);

        }
        [HttpPost]
        public ActionResult SilOk(int? adresid)
        {
            Adres adresx = new Adres();
            if (adresid != null)
            {
                adresx = db.Adresler.Where(x => x.ID == adresid).FirstOrDefault();
                adresx.Kisiler = db.Kisiler.Where(x => x.ID == adresx.KisiId).FirstOrDefault();
                db.Adresler.Remove(adresx);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");


        }



    }
}