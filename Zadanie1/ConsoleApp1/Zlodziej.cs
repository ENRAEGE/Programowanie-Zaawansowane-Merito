using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Zlodziej : INPC
    {
        void INPC.Przedstawsie()
        {
            Console.WriteLine("Jestem Złodziejem, nie mam atrybutów");
        }
    }
}
