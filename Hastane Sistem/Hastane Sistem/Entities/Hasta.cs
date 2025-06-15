using Hastane_Sistem.Abstract;
using Hastane_Sistem.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistem.Entities
{
    public class Hasta : IBilgiYazdirilabilir
    {
        public string ad;
        public string soyad;
        public string kimlikNo;
        public DateTime dogumTarihi;
        public string cinsiyet;
        public HastaDurumu durum;
        public ArrayList islemGecmisi = new ArrayList();
        public ArrayList aktifIslemler = new ArrayList();


        public void BilgiYazdir()
        {
            Console.WriteLine($"Hasta: {ad} {soyad}, TC: {kimlikNo}, Durum: {durum}");
        }
    }
}
