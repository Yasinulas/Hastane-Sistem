using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hastane_Sistem.Entities;

namespace Hastane_Sistem.Abstract
{
    public abstract class Personel
    {
        public string ad;
        public string soyad;
        public string sicilNo;
        public string unvan;
        public Vardiya vardiyaBilgisi;

        public virtual void PersonelBilgisiYazdir()
        {
            Console.WriteLine($"{unvan}: {ad} {soyad}, Sicil: {sicilNo}, Vardiya: {vardiyaBilgisi.vardiyaNo}");
        }

    }
}
