using Hastane_Sistem.Abstract;
using Hastane_Sistem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistem.Helpers
{
    public static class IslemDenetleyici
    {
        public static bool AmeliyatIcinUygunMu(Hasta hasta)
        {
            foreach (Islem islem in hasta.islemGecmisi)
            {
                if (islem.GetType().Name == "KanTesti")
                    return true;
            }

            return false;
        }
    }
}
