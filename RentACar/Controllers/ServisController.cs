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
            return liste;
        }
        //Araçlarda Plakaya Göre Arama
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
            db.Araclars.Add(araclar);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Araç Başarıyla Eklendi";
            return sonuc;

        }
        //YONETİCİ Düzenleme
        [HttpPut]
        [Route("api/aracduzenle")]
        public SonucModel AracDuzenle(AraclarModel model)
        {
            Araclar kayit = db.Araclars.Where(s => s.AracId == model.AracId).FirstOrDefault();
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
        //YONETİCİ SİL
        [HttpDelete]
        [Route("api/aracsil/{aracId}")]
        public SonucModel AracSil(string aracId)
        {
            Araclar kayit = db.Araclars.Where(s => s.AracId == aracId).FirstOrDefault();
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
    }
}