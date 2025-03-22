using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Wojownik : INPC
    {
        void INPC.Przedstawsie()
        {
            Console.WriteLine("Jestem Wojownikiem, walczę mieczem");
        }
    }
}
