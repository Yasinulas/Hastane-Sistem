using Hastane_Sistem.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hastane_Sistem.Entities
{
    public class KanTesti : Islem, IHazirlanabilir
    {
        public override void HazirlikYap()
        {
            Console.WriteLine($"{islemAdi} için laboratuvar hazırlanıyor.");
        }
    }
}
