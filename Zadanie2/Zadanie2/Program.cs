using System.Collections;
using Zadanie2;

namespace Zadanie2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var planPieczenia = new PlanPieczenia();

            planPieczenia.DodajCiasto(new FabrykaCiastaCzekoladowego());
            planPieczenia.DodajCiasto(new FabrykaCiastaJabłkowego());

            planPieczenia.WyświetlPlan();
        }
    }

    public class Ciasto
    {
        public string Nazwa { get; set; }
        public string Rodzaj { get; set; }
        public List<string> Składniki { get; set; }

        public Ciasto(string nazwa, string rodzaj, List<string> składniki)
        {
            Nazwa = nazwa;
            Rodzaj = rodzaj;
            Składniki = składniki;
        }

        public void WyswietlInformacje()
        {
            Console.WriteLine($"Ciasto: {Nazwa}, Rodzaj: {Rodzaj}");
            Console.WriteLine("Składniki: " + string.Join(", ", Składniki));
            Console.WriteLine();
        }
    }
    public class PlanPieczenia : IEnumerable<Ciasto>
    {
        private List<Ciasto> listaCiast = new List<Ciasto>();
        public void DodajCiasto(IFabrykaCiasta fabryka)
        {
            listaCiast.Add(fabryka.StworzCiasto());
        }
        public void WyświetlPlan()
        {
            foreach (var ciasto in listaCiast)
            {
                ciasto.WyswietlInformacje();
            }
        }
        public IEnumerator<Ciasto> GetEnumerator()
        {
            return listaCiast.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public interface IFabrykaCiasta
    { 
        Ciasto StworzCiasto();
    }




    public class FabrykaCiastaCzekoladowego : IFabrykaCiasta 
    { 
        Ciasto IFabrykaCiasta.StworzCiasto() 
        {
            return new Ciasto("Czekoladowe", "Kruche", new List<string> { "Czekolada", "Mąka", "Jajka", "Masło" });
        }
    }
    public class FabrykaCiastaJabłkowego : IFabrykaCiasta 
    {
        Ciasto IFabrykaCiasta.StworzCiasto()
        {
            return new Ciasto("Jabłkowe", "Drożdżowe", new List<string> { "Jabłka", "Cynamon", "Mąka", "Cukier" });
        }
    }
}
