﻿using kutuphane_otomasyou.Models.table.kitaplar;
using kutuphane_otomasyou.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kutuphane_otomasyou.Models.table;
using kutuphane_otomasyou.Models.table.kisiler;
using System.Drawing;

namespace kutuphane_otomasyou.Controllers
{
    public class EkleSilController : Controller
    {
        // GET: EkleSil,
        //[HttpPost]
        [Authorize]
        public ActionResult ekle(string kitap_adi, string yazar, int? turuId, string ozet, int? sayfa_sayisi, string resimi, int? yili)
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();

                if (TempData["kitapTuru"] == null)
                {
                    List<SelectListItem> turler = (from x in db.kitapTuru.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = x.tur_adi,
                                                        Value = x.Id.ToString()
                                                    }).ToList();
                    TempData["kitapTuru"] = turler;
                }

                if (!string.IsNullOrEmpty(kitap_adi) && !string.IsNullOrEmpty(yazar) && turuId.HasValue && !string.IsNullOrEmpty(ozet) && !string.IsNullOrEmpty(resimi) && sayfa_sayisi.HasValue && yili.HasValue)
                {
                    if (yili.Value > 0 && sayfa_sayisi.Value > 0)
                    {
                        Kitap yeni_kitap = new Kitap
                        {
                            kitap_adi = kitap_adi,
                            yazar = yazar,
                            turuId = turuId.Value,
                            ozet = ozet,
                            resimi = resimi,
                            yili = yili.Value,
                            sayfa_sayisi = sayfa_sayisi.Value
                        };

                        db.kitaptablosu.Add(yeni_kitap);
                        db.SaveChanges();

                        return RedirectToAction("sil", "EkleSil"); // Metodu tamamlayan dönüş ifadesi
                    }

                }
                if (!string.IsNullOrEmpty(kitap_adi) || !string.IsNullOrEmpty(yazar) || turuId.HasValue || !string.IsNullOrEmpty(ozet) || !string.IsNullOrEmpty(resimi) || sayfa_sayisi.HasValue || yili.HasValue)
                {
                    TempData["bos"] = "bos";
                    return View(); // Metodu tamamlayan dönüş ifadesi
                }
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

        [Authorize]
        public ActionResult sil(string Kitapismi)
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                List<Kitap> kitaplistesi = db.kitaptablosu.ToList();
                if (Kitapismi != null)
                {


                    Kitap kitapSil = db.kitaptablosu.Where(x => x.kitap_adi == Kitapismi).FirstOrDefault();
                    db.kitaptablosu.Remove(kitapSil);
                    db.SaveChanges();
                    kitaplistesi = db.kitaptablosu.ToList();



                }
                return View(kitaplistesi);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }

        [Authorize]
        public ActionResult alınanKitaplar()
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                List<AlinanKitaplar> kitaplistesi = db.AlinanKitapTaplosu.ToList();
                return View(kitaplistesi);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }

        [Authorize,HttpGet]
        public ActionResult duzenle(int? kitapID)
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                if (TempData["kitapTuru"] == null)
                {
                    List<SelectListItem> turler = (from x in db.kitapTuru.ToList()
                                                   select new SelectListItem()
                                                   {
                                                       Text = x.tur_adi,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
                    TempData["kitapTuru"] = turler;
                }

                Kitap kitap = null;

                if (kitapID != null)
                {
                    kitap = db.kitaptablosu.Where(x => x.Id == kitapID).FirstOrDefault();
                }

                return View(kitap);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

        [Authorize,HttpPost]
        public ActionResult duzenle(Kitap model,int? kitapID)
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                if (TempData["kitapTuru"] == null)
                {
                    List<SelectListItem> turler = (from x in db.kitapTuru.ToList()
                                                   select new SelectListItem()
                                                   {
                                                       Text = x.tur_adi,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
                    TempData["kitapTuru"] = turler;
                }

                Kitap kitap = null ;

                if (kitapID != null && model != null)
                {
                    kitap = db.kitaptablosu.Where(x => x.Id == kitapID).FirstOrDefault();

                    kitap.kitap_adi = model.kitap_adi;
                    kitap.yazar = model.yazar;
                    kitap.ozet = model.ozet;
                    kitap.turuId = model.turuId;
                    kitap.resimi = model.resimi;
                    kitap.sayfa_sayisi = model.sayfa_sayisi;
                    kitap.yili = model.yili;
                    int result = db.SaveChanges();

                    if (result > 0)
                    {
                        
                    }
                    else
                    {
                        TempData["bos"] = "bos";
                        return View(kitap);
                    }
                }
                else
                {
                    TempData["bos"] = "bos";
                    return View(kitap);
                }

                return RedirectToAction("sil", "EkleSil");
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

        [Authorize]
        public ActionResult cezaliKisiler(string kisiIsmi)
        {if (Session["gizli"] != null) {
                databaseContextcs db = new databaseContextcs();
                List<kisi> kisiler = db.kisitablosu.ToList();
            if (kisiIsmi != null)
            {
                kisi kisiSil = db.kisitablosu.Where(x => x.ad == kisiIsmi).FirstOrDefault();

                int kisi_id = kisiSil.Id;
                List<AlinanKitaplar> kitaplar = db.AlinanKitapTaplosu.Where(x => x.kullanici_ıd == kisiSil.Id).ToList();



                foreach (var kitap in kitaplar)
                {
                    var geri_koy = new Kitap // kitaptablosu'na eklemek için doğru sınıfı kullanmalısınız
                    {
                        kitap_adi = kitap.kitap_adi,
                        yazar = kitap.yazar,
                        turuId = kitap.turuId,
                        ozet = kitap.ozet,
                        resimi = kitap.resimi,
                        yili = kitap.yili,
                        sayfa_sayisi = kitap.sayfa_sayisi,
                        // Diğer alanları da gerekiyorsa burada ayarlayabilirsiniz
                    };

                    db.kitaptablosu.Add(geri_koy);
                    db.AlinanKitapTaplosu.Remove(kitap);
                }

                db.SaveChanges();


                if (kisiSil != null)
                {
                    db.kisitablosu.Remove(kisiSil);
                }
               
               

                var cezalandi = new CezaliKisiler
                {
                    ad = kisiSil.ad,
                    soyad = kisiSil.soyad,
                    email = kisiSil.email,
                    sifre = kisiSil.sifre,
            


                };
                db.CezaliKisilertablosu.Add(cezalandi);
                db.SaveChanges();


                return RedirectToAction("cezaliKisiler","EkleSil");

            }
            return View(kisiler);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }
        [Authorize]
        public ActionResult admin_paneli()
        {
            if (Session["gizli"] != null)
            {
                databaseContextcs db = new databaseContextcs();
                int count = db.kisitablosu.Count();
                int count2 = db.CezaliKisilertablosu.Count();
                int count3 = db.kitaptablosu.Count();
                int count4 = db.AlinanKitapTaplosu.Count();
                int count5 = db.kitapTuru.Count();
                TempData["kisi sayisi"] = count.ToString();
                TempData["Cezali Kisiler sayisi"] = count2.ToString();
                TempData["kitap sayisi"] = (count3+ count4).ToString();
                TempData["Alinan Kitap sayisi"] = count4.ToString();
                TempData["Kitap Turu sayisi"] = count5.ToString();
                TempData["kitapliktaki kitap sayisi"] = count3.ToString();

                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

        }

    }
}