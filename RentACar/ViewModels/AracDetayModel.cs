using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentACar.ViewModels
{
    public class AracDetayModel
    {

        public string AracDetayAracId { get; set; }
        public int AracKm { get; set; }
        public DateTime AracAlimYili { get; set; }
        public int AracKoltukSayisi { get; set; }
        public string AracYakitTipi { get; set; }
        public string AracVitesTipi { get; set; }
        public string AracRenk { get; set; }
        public string AracMotorGucu { get; set; }
        public string AracMotorHacmi { get; set; }
        public string AracKasaTipi { get; set; }
        public string AracTuru { get; set; }
        public AraclarModel aracBilgi { get; set; }
    }
}