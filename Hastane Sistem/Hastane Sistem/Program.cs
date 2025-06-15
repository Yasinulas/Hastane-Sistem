using System.Collections.Generic;
using Hastane_Sistem.Abstract;
using Hastane_Sistem.Entities;
using Hastane_Sistem.Enums;
using Hastane_Sistem.Helpers;

namespace Hastane_Sistem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Doktor doktor1 = new Doktor();
            doktor1.ad = "Onur";
            doktor1.soyad = "Yağız";
            doktor1.sicilNo = "D123";
            doktor1.unvan = "Uzman";
            doktor1.brans = "Kardiyoloji";
            HastaneYonetimi.doktorlar.Add(doktor1);

            Doktor doktor2 = new Doktor();
            doktor2.ad = "Altan Emre";
            doktor2.soyad = "Demirci";
            doktor2.sicilNo = "D124";
            doktor2.unvan = "Doçent";
            doktor2.brans = "Nöroloji";
            HastaneYonetimi.doktorlar.Add(doktor2);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Hastane Yönetim Sistemi ===");
                Console.WriteLine("1. Hasta Kaydı Yap");
                Console.WriteLine("2. Muayene Başlat");
                Console.WriteLine("3. Hasta Aktif İşlemleri");
                Console.WriteLine("4. Taburcu Et");
                Console.WriteLine("5. Günlük Rapor");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçim yapınız: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        HastaKayit();
                        break;
                    case "2":
                        MuayeneBaslat();
                        break;
                    case "3":
                        AktifIslemlerGoster();
                        break;
                    case "4":
                        TaburcuEt();
                        break;
                    case "5":
                        RaporYonetimi.GunlukIslemSayisiYazdir();
                        Bekle();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Hatalı seçim!");
                        Bekle();
                        break;
                }
            }
        }

        static void HastaKayit()
        {
            Hasta hasta = new Hasta();

            Console.Write("Hasta Adı: ");
            hasta.ad = Console.ReadLine();

            Console.Write("Hasta Soyadı: ");
            hasta.soyad = Console.ReadLine();

            Console.Write("Kimlik No (11 haneli): ");
            hasta.kimlikNo = Console.ReadLine();

            Console.Write("Cinsiyet: ");
            hasta.cinsiyet = Console.ReadLine();

            Console.Write("Doğum Tarihi (gg.aa.yyyy): ");
            hasta.dogumTarihi = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture); // System.Globalization.CultureInfo.InvariantCulture :  uygulamanın çalıştığı makinenin yerel ayarlarına bakmaksızın, standart ve evrensel bir biçimde verilerin işlenmesini sağlar.

            Console.Write("Durumu (Acil/Beklemede): ");
            string durum = Console.ReadLine();

            hasta.durum = durum == "Acil" ? HastaDurumu.Acil : HastaDurumu.Beklemede;

            Console.WriteLine("\n--- Doktor Seçimi ---");
            Doktor secilenDoktor = DoktorSec();

            if (secilenDoktor.sicilNo != "")
            {
                Muayene muayene = new Muayene();
                muayene.islemAdi = "İlk Muayene";
                muayene.uygulayanPersonel = secilenDoktor;
                muayene.baslangicZamani = DateTime.Now;
                muayene.aciklama = "Hasta Kaydı - İlk Kontrol";

                hasta.aktifIslemler.Add(muayene);
                HastaneYonetimi.islemKayitlari.Add(muayene);
                HastaneYonetimi.HastaEkle(hasta);

                Console.WriteLine($"Hasta başarıyla kaydedildi. Doktor: Dr. {secilenDoktor.ad} {secilenDoktor.soyad}");
            }
            else
                Console.WriteLine("Doktor seçilmedi, hasta kaydedilemedi!");

            Bekle();
        }

        static void MuayeneBaslat()
        {
            Console.Write("Hasta kimlik no: ");
            Hasta secilenHasta = HastaGetir(Console.ReadLine());

            if (HastaSistemdeKayitliMi(secilenHasta))
            {
                Doktor doktor = DoktorSec();
                if (doktor.sicilNo != "")
                {
                    Muayene muayene = new Muayene();
                    muayene.islemAdi = "Genel Muayene";
                    muayene.uygulayanPersonel = doktor;
                    muayene.baslangicZamani = DateTime.Now;
                    muayene.aciklama = "Rutin Kontrol";

                    muayene.HazirlikYap();
                    secilenHasta.aktifIslemler.Add(muayene);
                    HastaneYonetimi.islemKayitlari.Add(muayene);

                    Console.WriteLine($"{secilenHasta.ad} {secilenHasta.soyad} muayeneye alındı. Doktoru: Dr. {doktor.ad} {doktor.soyad}");

                    Console.Write("Muayene bitti, hasta taburcu edilsin mi? (Evet/Hayır): ");
                    string cevap = Console.ReadLine();

                    if (cevap.ToLower() == "evet")
                    {
                        HastaneYonetimi.taburcuEdilecekler.Push(secilenHasta);
                        Console.WriteLine("Hasta taburcu listesine eklendi.");
                    }
                }
                else
                    Console.WriteLine("Doktor seçilmedi!");
            }
            else
                Console.WriteLine("Hasta bulunamadı!");

            Bekle();
        }


        static void AktifIslemlerGoster()
        {
            Console.Write("Hasta kimlik no: ");
            Hasta hasta = HastaGetir(Console.ReadLine());

            if (HastaSistemdeKayitliMi(hasta))
            {
                Console.WriteLine($"Aktif işlemler ({hasta.ad} {hasta.soyad}):");
                foreach (Islem islem in hasta.aktifIslemler)
                {
                    Console.WriteLine($"- {islem.islemAdi} | Doktor: {islem.uygulayanPersonel.ad} {islem.uygulayanPersonel.soyad} | Başlangıç: {islem.baslangicZamani}");
                }
            }
            else
                Console.WriteLine("Hasta kayıtlı değil!");

            Bekle();
        }

        static void TaburcuEt()
        {
            if (HastaneYonetimi.taburcuEdilecekler.Count > 0)
            {
                Hasta taburcuHasta = (Hasta)HastaneYonetimi.taburcuEdilecekler.Pop();
                taburcuHasta.durum = HastaDurumu.Taburcu;

                foreach (Islem islem in taburcuHasta.aktifIslemler)
                    taburcuHasta.islemGecmisi.Add(islem);

                taburcuHasta.aktifIslemler.Clear();
                HastaneYonetimi.hastalar.Remove(taburcuHasta);

                Console.WriteLine($"{taburcuHasta.ad} {taburcuHasta.soyad} başarıyla taburcu edildi ve işlemleri temizlendi.");
            }
            else
                Console.WriteLine("Taburcu edilecek hasta bulunmamaktadır!");

            Bekle();
        }

        static Hasta HastaGetir(string kimlikNo)
        {
            Hasta bulunanHasta = new Hasta();
            foreach (Hasta h in HastaneYonetimi.hastalar)
                if (h.kimlikNo.Equals(kimlikNo))
                    bulunanHasta = h;

            return bulunanHasta;
        }

        static Doktor DoktorSec()
        {
            Doktor secilen = new Doktor();
            secilen.sicilNo = "";
            Console.WriteLine("Doktorlar:");
            for (int i = 0; i < HastaneYonetimi.doktorlar.Count; i++)
            {
                Doktor d = (Doktor)HastaneYonetimi.doktorlar[i];
                Console.WriteLine($"{i + 1}) Dr.{d.ad} {d.soyad} - {d.brans}");
            }
            Console.Write("Doktor No: ");
            int no = int.Parse(Console.ReadLine());
            if (no > 0 && no <= HastaneYonetimi.doktorlar.Count)
                secilen = (Doktor)HastaneYonetimi.doktorlar[no - 1];
            return secilen;
        }

        static bool HastaSistemdeKayitliMi(Hasta h) => h.kimlikNo != "";

        static void Bekle()
        {
            Console.WriteLine("\nDevam etmek için Enter'a basınız...");
            Console.ReadLine();
        }
    }
}
