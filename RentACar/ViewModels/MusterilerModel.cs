using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.ViewModels
{
    public class MusterilerModel
    {
        public string MusteriId { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriTc { get; set; }
        public string MusteriTel { get; set; }
        public string MusteriEmail { get; set; }
        public string MusteriAdresIlk { get; set; }
        public string MusteriAdresIkinci { get; set; }
        public int MusteriRezSay { get; set; }
    }
}