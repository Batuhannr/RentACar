using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RentACar.ViewModels;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class ServisController : ApiController
    {
        RentACarDatabaseEntities1 db = new RentACarDatabaseEntities1();
        SonucModel sonuc = new SonucModel();

        //ÇALIŞIYOR
        #region Araclar İşlemleri

        //ARAÇLARIN LİSTELENMESİ
        [HttpGet]
        [Route("api/araclist")]
        public List<AraclarModel> AracListele()
        {
            List<AraclarModel> liste = db.Araclars.Where(s => s.AracKullanilabilir == 1).Select(x => new AraclarModel()
            {
                AracId = x.AracId,
                AracMarka = x.AracMarka,
                AracModel = x.AracModel,
                AracPlaka = x.AracPlaka,
                AracUretimTarihi = x.AracUretimTarihi,
                AracGunlukFiyat = x.AracGunlukFiyat,
                AracKullanilabilir = x.AracKullanilabilir,
            }).ToList();
            foreach (var kayit in liste)
            {
                AracDetayModel aracBilgiList = db.AracDetays.Where(s => s.AracDetayAracId == kayit.AracId).Select(x => new AracDetayModel()
                {
                    AracDetayAracId = x.AracDetayAracId,
                    AracAlimYili = x.AracAlimYili,
                    AracKasaTipi = x.AracKasaTipi,
                    AracKm = x.AracKm,
                    AracKoltukSayisi = x.AracKoltukSayisi,
                    AracMotorGucu = x.AracMotorGucu,
                    AracMotorHacmi = x.AracMotorHacmi,
                    AracRenk = x.AracRenk,
                    AracTuru = x.AracTuru,
                    AracVitesTipi = x.AracVitesTipi,
                    AracYakitTipi = x.AracYakitTipi,
                }
                ).FirstOrDefault();
                kayit.AracDetayBilgi = AracById(aracBilgiList.AracDetayAracId);
            }

            return liste;
        }
        //Araçlarda Plakaya Göre Arama
        public AracDetayModel AracById(string AracId)
        {
            AracDetayModel liste = db.AracDetays.Where(s => s.AracDetayAracId == AracId).Select(x => new AracDetayModel()
            {

                AracDetayAracId = x.AracDetayAracId,
                AracAlimYili = x.AracAlimYili,
                AracKasaTipi = x.AracKasaTipi,
                AracKm = x.AracKm,
                AracKoltukSayisi = x.AracKoltukSayisi,
                AracMotorGucu = x.AracMotorGucu,
                AracMotorHacmi = x.AracMotorHacmi,
                AracRenk = x.AracRenk,
                AracTuru = x.AracTuru,
                AracVitesTipi = x.AracVitesTipi,
                AracYakitTipi = x.AracYakitTipi,

            }).FirstOrDefault();
            return liste;
        }
        [HttpGet]
        [Route("api/araclist/{AracPlakas}")]

        public AraclarModel AracByPlaka(string AracPlakas)
        {
            AraclarModel liste = db.Araclars.Where(s => s.AracPlaka == AracPlakas).Select(x => new AraclarModel()
            {

                AracId = x.AracId,
                AracMarka = x.AracMarka,
                AracModel = x.AracModel,
                AracPlaka = x.AracPlaka,
                AracUretimTarihi = x.AracUretimTarihi,
                AracGunlukFiyat = x.AracGunlukFiyat,
                AracKullanilabilir = x.AracKullanilabilir,

            }).FirstOrDefault();
            AracDetayModel aracBilgiList = db.AracDetays.Where(s => s.AracDetayAracId == liste.AracId).Select(x => new AracDetayModel()
            {
                AracDetayAracId = x.AracDetayAracId,
                AracAlimYili = x.AracAlimYili,
                AracKasaTipi = x.AracKasaTipi,
                AracKm = x.AracKm,
                AracKoltukSayisi = x.AracKoltukSayisi,
                AracMotorGucu = x.AracMotorGucu,
                AracMotorHacmi = x.AracMotorHacmi,
                AracRenk = x.AracRenk,
                AracTuru = x.AracTuru,
                AracVitesTipi = x.AracVitesTipi,
                AracYakitTipi = x.AracYakitTipi,
            }
            ).FirstOrDefault();
            liste.AracDetayBilgi = AracById(aracBilgiList.AracDetayAracId);
            return liste;
        }

        //Araç Ekleme

        [HttpPost]
        [Route("api/aracekle")]
        public SonucModel AracEkleme(AraclarModel model)
        {
            if (db.Araclars.Count(s => s.AracPlaka == model.AracPlaka) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Araç Kayıtlıdır";
                return sonuc;
            }
            Araclar araclar = new Araclar();
            araclar.AracId = Guid.NewGuid().ToString();
            araclar.AracMarka = model.AracMarka;
            araclar.AracModel = model.AracModel;
            araclar.AracPlaka = model.AracPlaka;
            araclar.AracUretimTarihi = model.AracUretimTarihi;
            araclar.AracGunlukFiyat = Convert.ToDecimal(model.AracGunlukFiyat);
            araclar.AracKullanilabilir = Convert.ToInt32(model.AracKullanilabilir);

            AracDetay aracdetay = new AracDetay();
            aracdetay.AracDetayAracId = araclar.AracId;
            aracdetay.AracAlimYili = model.AracDetayBilgi.AracAlimYili;
            aracdetay.AracKasaTipi = model.AracDetayBilgi.AracKasaTipi;
            aracdetay.AracKm = model.AracDetayBilgi.AracKm;
            aracdetay.AracKoltukSayisi = model.AracDetayBilgi.AracKoltukSayisi;
            aracdetay.AracMotorGucu = model.AracDetayBilgi.AracMotorGucu;
            aracdetay.AracMotorHacmi = model.AracDetayBilgi.AracMotorHacmi;
            aracdetay.AracRenk = model.AracDetayBilgi.AracRenk;
            aracdetay.AracTuru = model.AracDetayBilgi.AracTuru;
            aracdetay.AracVitesTipi = model.AracDetayBilgi.AracVitesTipi;
            aracdetay.AracYakitTipi = model.AracDetayBilgi.AracYakitTipi;
            db.Araclars.Add(araclar);
            db.AracDetays.Add(aracdetay);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Araç Başarıyla Eklendi";
            return sonuc;

        }
        //Araç Düzenleme
        [HttpPut]
        [Route("api/aracduzenle")]
        public SonucModel AracDuzenle(AraclarModel model)
        {
            Araclar kayit = db.Araclars.Where(s => s.AracPlaka == model.AracPlaka).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.AracGunlukFiyat = Convert.ToDecimal(model.AracGunlukFiyat);
            kayit.AracKullanilabilir = Convert.ToInt32(model.AracKullanilabilir);
            kayit.AracMarka = model.AracMarka;
            kayit.AracModel = model.AracModel;
            kayit.AracPlaka = model.AracPlaka;
            kayit.AracUretimTarihi = model.AracUretimTarihi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Araç Düzenlendi";
            return sonuc;
        }
        //Araç silme 
        [HttpDelete]
        [Route("api/aracsil/{aracPlaka}")]
        public SonucModel AracSil(string aracPlaka)
        {
            Araclar kayit = db.Araclars.Where(s => s.AracId == aracPlaka).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Araç Bulunamadı";
                return sonuc;
            }
            db.Araclars.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Araç Silindi";
            return sonuc;
        }


        #endregion
        ////ÇALIŞIYOR
        #region Müşteri İşlemleri

        //Müşteri listelenmesi 
        [HttpGet]
        [Route("api/musterilist")]
        public List<MusterilerModel> MusteriList()
        {
            List<MusterilerModel> liste = db.Musteris.Select(x => new MusterilerModel()
            {
                MusteriId = x.MusteriId,
                MusteriAd = x.MusteriAd,
                MusteriSoyad = x.MusteriSoyad,
                MusteriTc = x.MusteriTc,
                MusteriEmail = x.MusteriEmail,
                MusteriTel = x.MusteriTel,
                MusteriAdresIlk = x.MusteriAdresIlk,
                MusteriAdresIkinci = x.MusteriAdresIkinci,
                MusteriRezSay = x.MusteriRezSay,
            }).ToList();
            return liste;
        }
        //Müşteri TC sine göre listeleme
        [HttpGet]
        [Route("api/musterilist/{MusteriTc}")]

        public MusterilerModel MusteriByTc(string MusteriTc)
        {
            MusterilerModel liste = db.Musteris.Where(s => s.MusteriTc == MusteriTc).Select(x => new MusterilerModel()
            {

                MusteriId = x.MusteriId,
                MusteriAd = x.MusteriAd,
                MusteriSoyad = x.MusteriSoyad,
                MusteriTc = x.MusteriTc,
                MusteriEmail = x.MusteriEmail,
                MusteriTel = x.MusteriTel,
                MusteriAdresIlk = x.MusteriAdresIlk,
                MusteriAdresIkinci = x.MusteriAdresIkinci,
                MusteriRezSay = x.MusteriRezSay,

            }).FirstOrDefault();
            return liste;
        }

        //Müşteri Ekleme

        [HttpPost]
        [Route("api/musteriekle")]
        public SonucModel MusteriEkle(MusterilerModel model)
        {
            if (db.Musteris.Count(s => s.MusteriTc == model.MusteriTc) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Müşterinin Kaydı Bulunmaktadır.";
                return sonuc;
            }
            Musteri musteri = new Musteri();
            musteri.MusteriId = Guid.NewGuid().ToString();
            musteri.MusteriAd = model.MusteriAd;
            musteri.MusteriSoyad = model.MusteriSoyad;
            musteri.MusteriTc = model.MusteriTc;
            musteri.MusteriEmail = model.MusteriEmail;
            musteri.MusteriTel = model.MusteriTel;
            musteri.MusteriAdresIlk = model.MusteriAdresIlk;
            musteri.MusteriAdresIkinci = model.MusteriAdresIkinci;
            musteri.MusteriRezSay = 0;
            db.Musteris.Add(musteri);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Müşteri Kaydı Başarıyla Oluşturuldu.";
            return sonuc;

        }

        public SonucModel MusteriRezSayArttir(string id)
        {
            Musteri mus = db.Musteris.Where(s => s.MusteriId == id).FirstOrDefault();
            mus.MusteriRezSay = mus.MusteriRezSay + 1;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yükseldi";
            return sonuc;
        }


        //Müşteri Düzenleme
        [HttpPut]
        [Route("api/musteriduzenle/{musteriTc}")]
        public SonucModel MusteriDuzenle(MusterilerModel model, string musteriTc)
        {
            Musteri kayit = db.Musteris.Where(s => s.MusteriTc == musteriTc).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Müşteri Bulunamadı Müşteri TC Sini Kontrol Ediniz";
                return sonuc;
            }
            else
            {
                kayit.MusteriAd = model.MusteriAd;
                kayit.MusteriSoyad = model.MusteriSoyad;
                kayit.MusteriTel = model.MusteriTel;
                kayit.MusteriTc = model.MusteriTc;
                kayit.MusteriEmail = model.MusteriEmail;
                kayit.MusteriAdresIlk = model.MusteriAdresIkinci;
                kayit.MusteriRezSay = model.MusteriRezSay;
            }
            Musteri kayit1 = db.Musteris.Where(s => s.MusteriTc == kayit.MusteriTc).FirstOrDefault();
            if (kayit1 != null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girmiş Olduğunuz TC De Kayıtlı Bir Müşteri Bulunmaktadır Girmiş Olduğunuz TC: " + kayit.MusteriTc;
                return sonuc;
            }
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Müşteri Düzenlendi";
            return sonuc;
        }
        //Müşteri Sil
        [HttpDelete]
        [Route("api/musteriSil/{musteriTc}")]
        public SonucModel MusteriSil(string musteriTc)
        {
            Musteri kayit = db.Musteris.Where(s => s.MusteriTc == musteriTc).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Müşterinin Kaydı Bulunamadı";
                return sonuc;
            }
            db.Musteris.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Araç Silindi";
            return sonuc;
        }


        #endregion
        //
        #region Rezervasyon İşlemleri

        //Rezervasyon listelenmesi 
        [HttpGet]
        [Route("api/rezlist")]
        public List<RezervasyonModel> RezList()
        {
            List<RezervasyonModel> liste = db.Rezervasyons.Select(x => new RezervasyonModel()
            {
                RezId = x.RezId,
                RezBaslangic = x.RezBaslangic,
                RezBitis = x.RezBitis,
                RezGunSayisi = x.RezGunSayisi,
                RezIslemYapan = x.RezIslemYapan,
                RezMusteriId = x.RezMusteriId,
                RezTarih = x.RezTarih,
                RezAracId = x.RezAracId,
                RezTutar = x.RezTutar
            }).ToList();
            foreach (var kayit in liste)
            {
                AraclarModel aracBilgiList = db.Araclars.Where(s => s.AracId == kayit.RezAracId).Select(x => new AraclarModel()
                {
                    AracId = x.AracId,
                    AracMarka = x.AracMarka,
                    AracModel = x.AracModel,
                    AracPlaka = x.AracPlaka,
                    AracUretimTarihi = x.AracUretimTarihi,
                    AracGunlukFiyat = x.AracGunlukFiyat,
                    AracKullanilabilir = x.AracKullanilabilir,
                }
                ).FirstOrDefault();

                MusterilerModel musteriBilgiList = db.Musteris.Where(s => s.MusteriId == kayit.RezMusteriId).Select(x => new MusterilerModel()
                {
                    MusteriId = x.MusteriId,
                    MusteriAd = x.MusteriAd,
                    MusteriSoyad = x.MusteriSoyad,
                    MusteriTc = x.MusteriTc,
                    MusteriEmail = x.MusteriEmail,
                    MusteriTel = x.MusteriTel,
                    MusteriAdresIlk = x.MusteriAdresIlk,
                    MusteriAdresIkinci = x.MusteriAdresIkinci,
                    MusteriRezSay = x.MusteriRezSay,
                }
                ).FirstOrDefault();
                UyeModel uyeBilgiList = db.Uyelers.Where(s => s.uyeId == kayit.RezIslemYapan).Select(x => new UyeModel()
                {
                    uyeId = x.uyeId,
                    AdSoyad = x.AdSoyad,
                    KullaniciAdi = x.KullaniciAdi,
                    Email = x.Email,
                    Sifre = x.Sifre,
                    UyeAdmin = x.UyeAdmin,
                }
                ).FirstOrDefault();

                kayit.aracBilgi = AracByPlaka(aracBilgiList.AracPlaka);
                kayit.musteriBilgi = MusteriByTc(musteriBilgiList.MusteriTc);
                kayit.uyeBilgi = UyeByKullaniciAdi(uyeBilgiList.KullaniciAdi);
            }
            return liste;
        }
        //Rezervasyonda Müşteriye Göre Arama
        [HttpGet]
        [Route("api/rezlist/{MusteriTc}")]
        public List<RezervasyonModel> RezByMusteriTc(string MusteriTc)
        {
            List<RezervasyonModel> liste = db.Rezervasyons.Where(s => s.Musteri.MusteriTc == MusteriTc).Select(x => new RezervasyonModel()
            {

                RezId = x.RezId,
                RezBaslangic = x.RezBaslangic,
                RezBitis = x.RezBitis,
                RezGunSayisi = x.RezGunSayisi,
                RezIslemYapan = x.RezIslemYapan,
                RezMusteriId = x.RezMusteriId,
                RezTarih = x.RezTarih,
                RezAracId = x.RezAracId,
                RezTutar = x.RezTutar


            }).ToList();
            foreach (var kayit in liste)
            {
                AraclarModel aracBilgiList = db.Araclars.Where(s => s.AracId == kayit.RezAracId).Select(x => new AraclarModel()
                {
                    AracId = x.AracId,
                    AracMarka = x.AracMarka,
                    AracModel = x.AracModel,
                    AracPlaka = x.AracPlaka,
                    AracUretimTarihi = x.AracUretimTarihi,
                    AracGunlukFiyat = x.AracGunlukFiyat,
                    AracKullanilabilir = x.AracKullanilabilir,
                }
            ).FirstOrDefault();

                MusterilerModel musteriBilgiList = db.Musteris.Where(s => s.MusteriId == kayit.RezMusteriId).Select(x => new MusterilerModel()
                {
                    MusteriId = x.MusteriId,
                    MusteriAd = x.MusteriAd,
                    MusteriSoyad = x.MusteriSoyad,
                    MusteriTc = x.MusteriTc,
                    MusteriEmail = x.MusteriEmail,
                    MusteriTel = x.MusteriTel,
                    MusteriAdresIlk = x.MusteriAdresIlk,
                    MusteriAdresIkinci = x.MusteriAdresIkinci,
                    MusteriRezSay = x.MusteriRezSay,
                }
                ).FirstOrDefault();
                UyeModel uyeBilgiList = db.Uyelers.Where(s => s.uyeId == kayit.RezIslemYapan).Select(x => new UyeModel()
                {
                    uyeId = x.uyeId,
                    AdSoyad = x.AdSoyad,
                    KullaniciAdi = x.KullaniciAdi,
                    Email = x.Email,
                    Sifre = x.Sifre,
                    UyeAdmin = x.UyeAdmin,
                }
                ).FirstOrDefault();
                kayit.aracBilgi = AracByPlaka(aracBilgiList.AracPlaka);
                kayit.musteriBilgi = MusteriByTc(musteriBilgiList.MusteriTc);
                kayit.uyeBilgi = UyeByKullaniciAdi(uyeBilgiList.KullaniciAdi);
            }
            return liste;
        }

        //Rezervasyon Ekleme
        [HttpPost]
        [Route("api/rezekle")]
        public SonucModel RezEkle(RezervasyonModel model)
        {

            Rezervasyon rez = new Rezervasyon();
            rez.RezId = Guid.NewGuid().ToString();
            rez.RezBaslangic = model.RezBaslangic;
            rez.RezBitis = model.RezBitis;
            //Bitiş tarihinde'de ücret alınmak istenir ise gün sayısını 1 arttırmamız gerekiyor.
            rez.RezGunSayisi = Convert.ToInt32((model.RezBitis - model.RezBaslangic).TotalDays);
            rez.RezAracId = model.RezAracId;
            rez.RezMusteriId = model.RezMusteriId;
            rez.RezIslemYapan = model.RezIslemYapan;
            rez.RezTarih = DateTime.Now;
            Araclar arac = db.Araclars.Where(s => s.AracId == model.RezAracId).FirstOrDefault();
            rez.RezTutar = Convert.ToInt32((model.RezBitis - model.RezBaslangic).TotalDays) * arac.AracGunlukFiyat;
            if (arac.AracKullanilabilir == 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu araç şuanda kullanılmakta lütfen başka bir araç seçiniz";
                return sonuc;
            }
            if (db.Rezervasyons.Count(s => s.RezAracId == model.RezAracId && s.RezBaslangic >= model.RezBaslangic && s.RezBaslangic >= model.RezBitis || s.RezBitis >= model.RezBaslangic && s.RezBitis >= model.RezBitis) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Araç Girilen Tarihler Arasında Bir Rezervasyona Kayıtlıdır Lütfen Farklı Bir Araç Seçiniz!";
                return sonuc;
            }
            MusteriRezSayArttir(model.RezMusteriId);
            db.Rezervasyons.Add(rez);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Rezervasyon Kaydı Başarıyla Oluşturuldu.";
            return sonuc;

        }
        //Rezervasyon Düzenleme
        [HttpPut]
        [Route("api/rezduzenle")]
        public SonucModel RezDuzenle(RezervasyonModel model)
        {
            Rezervasyon kayit = db.Rezervasyons.Where(s => s.RezId == model.RezId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Rezervasyon Bulunamadı";
                return sonuc;
            }

            kayit.RezBaslangic = model.RezBaslangic;
            kayit.RezBitis = model.RezBitis;
            kayit.RezGunSayisi = Convert.ToInt32((model.RezBitis - model.RezBaslangic).TotalDays);
            kayit.RezIslemYapan = model.RezIslemYapan;
            kayit.RezMusteriId = model.RezMusteriId;
            kayit.RezTarih = model.RezTarih;
            kayit.RezTutar = model.RezTutar;
            Araclar arac = db.Araclars.Where(s => s.AracId == model.RezAracId).FirstOrDefault();
            kayit.RezTutar = Convert.ToInt32((model.RezBitis - model.RezBaslangic).TotalDays) * arac.AracGunlukFiyat;


            if (arac.AracKullanilabilir == 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu araç şuanda kullanılmakta lütfen başka bir araç seçiniz";
                return sonuc;
            }
            if (db.Rezervasyons.Count(s => s.RezAracId == model.RezAracId && s.RezBaslangic >= model.RezBaslangic && s.RezBaslangic >= model.RezBitis || s.RezBitis >= model.RezBaslangic && s.RezBitis >= model.RezBitis) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu Araç Girilen Tarihler Arasında Bir Rezervasyona Kayıtlıdır Lütfen Farklı Bir Araç Seçiniz!";
                return sonuc;
            }
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Rezervasyon Düzenlendi";
            return sonuc;
        }
        //Rezervasyon Silme
        [HttpDelete]
        [Route("api/rezsil/{rezid}")]
        public SonucModel RezSil(string rezid)
        {
            Rezervasyon kayit = db.Rezervasyons.Where(s => s.RezId == rezid).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Rezervasyon Kaydı Bulunamadı";
                return sonuc;
            }
            db.Rezervasyons.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Rezervasyon Silindi";
            return sonuc;
        }

        #endregion
        //
        #region Üye İşlemleri
        //Kullanıcıların listelenmesi
        [HttpGet]
        [Route("api/kullaniciliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uyelers.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                AdSoyad = x.AdSoyad,
                KullaniciAdi = x.KullaniciAdi,
                Email = x.Email,
                Sifre = x.Sifre,
                UyeAdmin = x.UyeAdmin,
            }).ToList();
            return liste;
        }
        //kullanıcılarda kullanıcı adına göre liste
        [HttpGet]
        [Route("api/kullaniciliste/{kullaniciAdi}")]

        public UyeModel UyeByKullaniciAdi(string kullaniciAdi)
        {
            UyeModel liste = db.Uyelers.Where(s => s.KullaniciAdi == kullaniciAdi).Select(x => new UyeModel()
            {

                uyeId = x.uyeId,
                AdSoyad = x.AdSoyad,
                KullaniciAdi = x.KullaniciAdi,
                Email = x.Email,
                Sifre = x.Sifre,
                UyeAdmin = x.UyeAdmin,

            }).FirstOrDefault();
            return liste;
        }

        //Kullanıcı Ekleme

        [HttpPost]
        [Route("api/kullaniciekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            List<UyeModel> liste = db.Uyelers.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
            }).ToList();
            if (db.Uyelers.Count(s => s.KullaniciAdi == model.KullaniciAdi || s.Email == model.Email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Kayıtlıdır";
                return sonuc;
            }
            int counts = liste.Count();
            Uyeler uye = new Uyeler();
            uye.uyeId = liste[counts - 1].uyeId + 1;
            uye.AdSoyad = model.AdSoyad;
            uye.KullaniciAdi = model.KullaniciAdi;
            uye.Email = model.Email;
            uye.Sifre = model.Sifre;
            uye.UyeAdmin = 0;
            db.Uyelers.Add(uye);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Başarıyla Eklendi";
            return sonuc;

        }
        //Kullanıcı Düzenleme
        [HttpPut]
        [Route("api/kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(UyeModel model)
        {
            Uyeler kayit = db.Uyelers.Where(s => s.uyeId == model.uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.KullaniciAdi = model.KullaniciAdi;
            kayit.AdSoyad = model.AdSoyad;
            kayit.Email = model.Email;
            kayit.Sifre = model.Sifre;
            kayit.UyeAdmin = model.UyeAdmin;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Bilgiler Düzenlendi";
            return sonuc;
        }
        //Kullanıcı silme 
        [HttpDelete]
        [Route("api/kullanicisil/{kullaniciId}")]
        public SonucModel KullaniciSil(int kullaniciId)
        {
            Uyeler kayit = db.Uyelers.Where(s => s.uyeId == kullaniciId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı";
                return sonuc;
            }
            db.Uyelers.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Uye Silindi";
            return sonuc;
        }
        #endregion


    }
}