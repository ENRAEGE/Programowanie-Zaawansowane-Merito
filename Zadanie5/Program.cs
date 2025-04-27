using System.Text.Json;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text.Json.Serialization;
using static Zadanie5.Program;

namespace Zadanie5
{
    public class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Wiek = 22,
                NumerIndeksu = 1312345,
                NumerGrupy = "25_Inf_NW_4"
            };

            Student student2 = new Student
            {
                Imie = "Anna",
                Nazwisko = "Nowak",
                Wiek = 21,
                NumerIndeksu = 1412346,
                NumerGrupy = "23_Inf_NW_4"
            };

            Student student3 = new Student
            {
                Imie = "Piotr",
                Nazwisko = "Wiśniewski",
                Wiek = 23,
                NumerIndeksu = 1312347,
                NumerGrupy = "26_Inf_NW_4"
            };

            Student student4 = new Student
            {
                Imie = "Maria",
                Nazwisko = "Wójcik",
                Wiek = 20,
                NumerIndeksu = 1412348,
                NumerGrupy = "21_Inf_NW_4"
            };

            Osoba osoba1 = new Osoba
            {
                Imie = "Tomasz",
                Nazwisko = "Zieliński",
                Wiek = 24,
            };

            Osoba osoba2 = new Osoba
            {
                Imie = "Katarzyna",
                Nazwisko = "Dąbrowska",
                Wiek = 22,
            };
            Osoba osoba3 = new Osoba
            {
                Imie = "Michał",
                Nazwisko = "Lewandowski",
                Wiek = 25,

            };

            Osoba osoba4 = new Osoba
            {
                Imie = "Aleksandra",
                Nazwisko = "Szymańska",
                Wiek = 19,

            };



            List<Osoba> osoby = new List<Osoba> { student1, student2, student3, student4, osoba1, osoba2, osoba3, osoba4 };
           

            //JSON
            string json = JsonSerializer.Serialize(osoby, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("osoby.json", json);
            Console.WriteLine("Obiekty zostały zserializowane do pliku 'osoby.json'");

            //XML
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Osoba>), new Type[] { typeof(Student) });
            using (FileStream fs = new FileStream("osoby.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, osoby);
                Console.WriteLine("Obiekty zostały zserializowane do pliku 'osoby.xml'");
            }


            //odczyt JSON
            Console.WriteLine("\nDane wczytane z pliku JSON:");
            try
            {
                List<Osoba> osobyJSON = JsonSerializer.Deserialize<List<Osoba>>(json, new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } });

                foreach (var osoba in osobyJSON)
                {
                    if (osoba is Student student)
                    {
                        Console.WriteLine($"Imię: {student.Imie}, Nazwisko {student.Nazwisko}, Wiek: {student.Wiek}, Indeks: {student.NumerIndeksu}, Grupa: {student.NumerGrupy}");
                    }
                    else
                    {
                        Console.WriteLine($"Imię: {osoba.Imie}, Nazwisko: {osoba.Nazwisko}, Wiek: {osoba.Wiek}");
                    }
                }
            }
            catch (Exception ex)
            {
            }

            //odczyt XML
            Console.WriteLine("\nDane wczytane z pliku XML:");
            try
            {
                using (FileStream fs = new FileStream("osoby.xml", FileMode.Open))
                {
                    List<Osoba> osobyXML = (List<Osoba>)xmlSerializer.Deserialize(fs);

                    foreach (var osoba in osobyXML)
                    {
                        if (osoba is Student student)
                        {
                            Console.WriteLine($"Imię: {student.Imie}, Nazwisko {student.Nazwisko}, Wiek: {student.Wiek}, Indeks: {student.NumerIndeksu}, Grupa: {student.NumerGrupy}");
                        }
                        else
                        {
                            Console.WriteLine($"Imię: {osoba.Imie}, Nazwisko: {osoba.Nazwisko}, Wiek: {osoba.Wiek}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        [Serializable]
        [JsonDerivedType(typeof(Student), "Student")]
        public class Osoba
        {
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public int Wiek { get; set; }
        }
        [Serializable]
        public class Student : Osoba
        {
            public int NumerIndeksu { get; set; }
            public string NumerGrupy { get; set; }
        }
    }
}