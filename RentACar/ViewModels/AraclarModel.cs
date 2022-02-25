using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.ViewModels
{
    public class AraclarModel
    {
        public string AracId { get; set; }
        public string AracPlaka { get; set; }
        public string AracMarka { get; set; }
        public string AracModel { get; set; }
        public DateTime AracUretimTarihi { get; set; }
        public decimal AracGunlukFiyat { get; set; }
        public int AracKullanilabilir { get; set; }
        public AracDetayModel AracDetayBilgi { get; set; }
    }
}