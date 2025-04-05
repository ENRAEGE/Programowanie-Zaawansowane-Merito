using System;

namespace Kurierens
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Paczki dostarczane do Polski:\n");
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie(ZarządzaniePrzesyłkami.Lokalizacja.Polska);
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie(ZarządzaniePrzesyłkami.Lokalizacja.Polska);
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie(ZarządzaniePrzesyłkami.Lokalizacja.Polska);

            Console.WriteLine("\nPaczki dostarczane do USA:\n");
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie(ZarządzaniePrzesyłkami.Lokalizacja.USA);
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie(ZarządzaniePrzesyłkami.Lokalizacja.USA);
            ZarządzaniePrzesyłkami.Instancja.PrzyjmijZamówienie(ZarządzaniePrzesyłkami.Lokalizacja.USA);
        }
        public interface IPaczka
        {
            void Spakuj();
        }

        public interface IKurier
        {
            void Dostarcz();
        }
        
        class MałaPaczka : IPaczka
        {
            public void Spakuj()
            {
                Console.WriteLine("Spakowano małą paczkę.");
            }
        }

        class DużaPaczka : IPaczka
        {
            public void Spakuj()
            {
                Console.WriteLine("Spakowano dużą paczkę.");
            }
        }

        class DHLKurier : IKurier
        {
            public void Dostarcz()
            {
                Console.WriteLine("Dostarczono przez kuriera DHL.");
            }
        }

        class UPSKurier : IKurier
        {
            public void Dostarcz()
            {
                Console.WriteLine("Dostarczono przez kuriera UPS.");
            }
        }
//Poniżej użyty jest wzorzec Factory, używamy go do "stworzenia" logistyk w krajach.
        interface IFabrykaLogistyki
        {
            IKurier UtworzKuriera();
            IPaczka UtworzPaczke();
        }

        class FabrykaLogistykiPolska : IFabrykaLogistyki
        {
            public IKurier UtworzKuriera()
            {
                return new DHLKurier();
            }

            public IPaczka UtworzPaczke()
            {
                Random rand = new Random();
                int paka = rand.Next(2);
                switch (paka)
                {
                    case 0:
                        return new DużaPaczka();
                    case 1:
                        return new MałaPaczka();
                    default:
                        throw new Exception();
                }
            }
        }

        class FabrykaLogistykiUSA : IFabrykaLogistyki
        {
            public IKurier UtworzKuriera()
            {
                return new UPSKurier();
            }

            public IPaczka UtworzPaczke()
            {
                Random rand = new Random();
                int paka = rand.Next(2);
                switch (paka)
                {
                    case 0:
                        return new DużaPaczka();
                    case 1:
                        return new MałaPaczka();
                    default:
                        throw new Exception();
                }
            }
        }

        class ZarządzaniePrzesyłkami
        {
            private IFabrykaLogistyki fabrykaLogistyki;
            private static ZarządzaniePrzesyłkami _instancja;
            private ZarządzaniePrzesyłkami() { }
            public static ZarządzaniePrzesyłkami Instancja
            {
                get
                {
                    if (_instancja == null)
                    {
                        _instancja = new ZarządzaniePrzesyłkami();
                    }
                    return _instancja;
                }
            }

            public enum Lokalizacja
            {
                Polska,
                USA
            }
            public void PrzyjmijZamówienie(Lokalizacja lokalizacja)
            {
                switch (lokalizacja)
                {
                    case Lokalizacja.Polska:
                        fabrykaLogistyki = new FabrykaLogistykiPolska();
                        break;
                    case Lokalizacja.USA:
                        fabrykaLogistyki = new FabrykaLogistykiUSA();
                        break;
                    default:
                        throw new ArgumentException("Nieobsługiwana lokalizacja.");
                }
                var paczka = fabrykaLogistyki.UtworzPaczke();
                var kurier = fabrykaLogistyki.UtworzKuriera();
                paczka.Spakuj();
                kurier.Dostarcz();
            }
        }
    }
}
