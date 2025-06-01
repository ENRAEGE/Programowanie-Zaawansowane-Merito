using System;
using System.Collections.Generic;

namespace Zadanie8
{
    class Program
    {
        static void Main(string[] args)
        {
            var osoby = new List<IOsoba>
            {
                new Uczen("Kasia", new List<int> { 5, 4, 3 }),
                new Uczen("Marek", new List<int>()), // brak ocen
                new Nauczyciel("Jan Kowalski", 142),
                new Administrator("Admin1", new List<string> { "Zalogowano użytkownika",
                "Zmieniono hasło" }), // symulacja logów dowolność dpisywania
                new Administrator("Admin2", new List<string>()), // brak logów
                new Administrator("Admin3") // dynamiczne logi

            };

            var raportVisitor = new RaportVisitor();

            foreach (var osoba in osoby)
            {
                osoba.Accept(raportVisitor);
            }
        }

        public static class Logger
        {
            public static void Info(string message)
            {
                Console.WriteLine(message);
            }

            public static void InfoWithIndent(string message)
            {
                Console.WriteLine($"    {message}");
            }
        }


        public interface IOsobaVisitor
        {
            void Visit(Uczen uczen);
            void Visit(Nauczyciel nauczyciel);
            void Visit(Administrator administrator);
        }
        public interface IOsoba {
            public void Accept(IOsobaVisitor visitor)
            {
            }
        }


        public class Uczen : IOsoba
        {
            public string Imie { get; set; }
            public List<int> Oceny { get; set; }
            
            public Uczen(string imie, List<int> oceny)
            {
                Imie = imie;
                Oceny = oceny;
            }

            public void Accept(IOsobaVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public class Nauczyciel : IOsoba
        {
            public string Imie { get; set; }
            public int LiczbaWystawionychOcen { get; set; }

            public Nauczyciel(string imie, int liczbaOcen)
            {
                Imie = imie;
                LiczbaWystawionychOcen = liczbaOcen;
            }

            public void Accept(IOsobaVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        public class Administrator : IOsoba
        {
            public string Imie { get; set; }
            public List<string> Logi { get; set; }

            // Konstruktor domyślny: tworzy pustą listę logów
            public Administrator(string imie)
            {
                Imie = imie;
                Logi = new List<string>();
            }
            // Konstruktor z gotową listą logów uzupełnianą w main
            public Administrator(string imie, List<string> logi)
            {
                Imie = imie;
                Logi = logi;
            }
            // Konstruktor z metodą(dodaje log wpisany programowo)
            public void DodajUzytkownika()
            {
                Logi.Add("Dodano użytkownika");
            }
            public void Accept(IOsobaVisitor visitor)
            {
                visitor.Visit(this);
            }
        }
        
        public class RaportVisitor : IOsobaVisitor
        {
            public void Visit(Uczen uczen)
            {
                if (uczen.Oceny.Count > 0)
                {
                    double suma = 0;
                    foreach (int ocena in uczen.Oceny)
                    {
                        suma += ocena;
                    }
                    double srednia = suma / uczen.Oceny.Count;
                    Logger.Info($"Uczeń {uczen.Imie} Średnia ocen: {srednia:F2}");
                }
                else
                {
                    Logger.Info($"Uczeń {uczen.Imie} Brak ocen.");
                }
            }

            public void Visit(Nauczyciel nauczyciel)
            {
                Logger.Info($"Nauczyciel {nauczyciel.Imie} wystawił ocen: {nauczyciel.LiczbaWystawionychOcen}");
            }

            public void Visit(Administrator administrator)
            {
                Logger.Info($"Administrator {administrator.Imie} - Logi systemowe:");
                if (administrator.Logi.Count > 0)
                {
                    foreach (var log in administrator.Logi)
                    {
                        Logger.InfoWithIndent($"- {log}");
                    }
                }
                else
                {
                    Logger.InfoWithIndent("- Brak logów systemowych.");
                }
            }
        }
    }
}
