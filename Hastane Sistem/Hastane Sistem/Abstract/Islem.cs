using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistem.Abstract
{
    public abstract class Islem
    {
        public string islemAdi;
        public Personel uygulayanPersonel;
        public DateTime baslangicZamani;
        public string aciklama;
        public abstract void HazirlikYap();
    }
}
