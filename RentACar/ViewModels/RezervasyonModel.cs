using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.ViewModels
{
    public class RezervasyonModel
    {
        public string RezId { get; set; }
        public DateTime RezTarih { get; set; }
        public DateTime RezBaslangic { get; set; }
        public DateTime RezBitis { get; set; }
        public string RezAracId { get; set; }   
        public string RezMusteriId { get; set; }
        public int RezIslemYapan { get; set; }
        public int RezGunSayisi { get; set; }
        public decimal RezTutar { get; set; }

        public MusterilerModel musteriBilgi { get; set; }
        public AraclarModel aracBilgi { get; set; }
        public UyeModel uyeBilgi { get; set; }
    }
}