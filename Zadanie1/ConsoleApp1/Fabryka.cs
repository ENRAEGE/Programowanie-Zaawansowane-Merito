using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class FabrykaWojownika : IFabrykaNPC
    {
        INPC IFabrykaNPC.CreateNPC()
        {
            INPC wojownik = new Wojownik();
            return wojownik;
        }
    }

    internal class FabrykaMaga : IFabrykaNPC
    {
        INPC IFabrykaNPC.CreateNPC()
        {
            INPC mag = new Mag();
            return mag;
        }
    }

    internal class FabrykaZlodzieja : IFabrykaNPC
    {
        INPC IFabrykaNPC.CreateNPC()
        {
            INPC zlodziej = new Zlodziej();
            return zlodziej;
        }
    }
}
