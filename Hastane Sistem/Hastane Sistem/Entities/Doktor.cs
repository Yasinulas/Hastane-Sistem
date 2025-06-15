using Hastane_Sistem.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistem.Entities
{
    public class Doktor : Personel, IHareketEdebilir
    {
        public string brans;
        public void ServiseGit(string servisAdi)
        {
            Console.WriteLine($"{unvan} Dr. {ad} {soyad}, {servisAdi} servisine gitti.");
        }

        public override void PersonelBilgisiYazdir()
        {
            base.PersonelBilgisiYazdir();
            Console.WriteLine($"Branş: {brans}");
        }
    }
}
