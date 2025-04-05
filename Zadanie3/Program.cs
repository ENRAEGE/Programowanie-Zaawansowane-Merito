using System;

namespace Paczkens
{
    class Program
    {
        static void Main(string[] args)
        {
            var zarzadzaniePaczkami = ZarządzaniePaczkami.Instancja;

            
            IFabrykaPaczek fabrykaDużych = new FabrykaDużychPaczek();
            IFabrykaPaczek fabrykaMałych = new FabrykaMałychPaczek();

         
            IPaczka paczkaMała1 = fabrykaMałych.UtwórzPaczkę();
            paczkaMała1.Przygotuj();

            IPaczka paczkaMała2 = fabrykaMałych.UtwórzPaczkę();
            paczkaMała2.Przygotuj();

            
            IPaczka paczkaDuża1 = fabrykaDużych.UtwórzPaczkę();
            paczkaDuża1.Przygotuj();

            IPaczka paczkaDuża2 = fabrykaDużych.UtwórzPaczkę();
            paczkaDuża2.Przygotuj();
        }
    }

    public interface IPaczka
    {
        void Przygotuj();
    }

    class MałaPaczka : IPaczka
    {
        public void Przygotuj()
        {
            Console.WriteLine("Przygotowano małą paczkę.");
        }
    }

    class DużaPaczka : IPaczka
    {
        public void Przygotuj()
        {
            Console.WriteLine("Przygotowano dużą paczkę.");
        }
    }

    class ZarządzaniePaczkami
    {
        private IFabrykaPaczek fabrykaPaczek;
        private static ZarządzaniePaczkami _instancja;
        private ZarządzaniePaczkami() 
        {
        }
        public static ZarządzaniePaczkami Instancja
        {
            get
            {
                if (_instancja == null)
                {
                    _instancja = new ZarządzaniePaczkami();
                }
                return _instancja;
            }
        }
    }

    interface IFabrykaPaczek
    {
        IPaczka UtwórzPaczkę();
    }
//Poniżej użyty jest wzorzec Abstract Factory do tworzenia rodzaju paczek
    class FabrykaDużychPaczek : IFabrykaPaczek
    {
        public IPaczka UtwórzPaczkę()
        {
            return new DużaPaczka();
        }
    }

    class FabrykaMałychPaczek : IFabrykaPaczek
    {
        public IPaczka UtwórzPaczkę()
        {
            return new MałaPaczka();
        }
    }
}
