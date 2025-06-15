using Hastane_Sistem.Abstract;
using Hastane_Sistem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistem.Helpers
{
    public static class RaporYonetimi
    {
        public static void GunlukIslemSayisiYazdir()
        {
            Console.WriteLine($"Bugünkü toplam işlem sayısı: {HastaneYonetimi.islemKayitlari.Count}");
        }

        public static void ServisBazliHastaSayisi(string servisAdi)
        {
            int sayi = 0;


            foreach (Hasta hasta in HastaneYonetimi.hastalar)
            {
                foreach (Islem islem in hasta.aktifIslemler)
                {
                    if (islem.aciklama.Contains(servisAdi))
                        sayi++;
                }
            }

            Console.WriteLine($"{servisAdi} servisindeki hasta sayısı: {sayi}");
        }
    }
}
