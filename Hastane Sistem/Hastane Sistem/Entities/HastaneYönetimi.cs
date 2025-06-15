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
    public static class HastaneYonetimi
    {
        public static readonly string hastaneAdi = "UBY Hastane";
        public const int MAX_PERSONEL = 100;
        public const int MAX_HASTA = 1000;
        public const int MAX_VARDIYA = 3;

        public static ArrayList doktorlar = new ArrayList();
        public static ArrayList hemsireler = new ArrayList();
        public static ArrayList hastalar = new ArrayList();
        public static ArrayList islemKayitlari = new ArrayList();

        public static Queue bekleyenHastalar = new Queue();
        public static Stack taburcuEdilecekler = new Stack();

        public static void HastaEkle(Hasta hasta)
        {
            hastalar.Add(hasta);
            if (hasta.durum == HastaDurumu.Acil)
            {
                Queue gecici = new Queue();
                gecici.Enqueue(hasta);
                foreach (var has in bekleyenHastalar)
                    gecici.Enqueue(has);
                bekleyenHastalar = gecici;
            }

            else
            {
                bekleyenHastalar.Enqueue(hasta);
            }
        }
    }
    }
