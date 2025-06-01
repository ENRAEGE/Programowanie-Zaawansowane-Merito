using System;

namespace Zadanie7
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal kwota = 10000m;

            IPodatekStrategia podatekPolska = new PodatekPolska();
            IPodatekStrategia podatekNiemcy = new PodatekNiemcy();

            KalkulatorPodatku kalkulatorPolska = new KalkulatorPodatku(podatekPolska);
            KalkulatorPodatku kalkulatorNiemcy = new KalkulatorPodatku(podatekNiemcy);

            Console.WriteLine($"Podatek w Polsce od kwoty {kwota} wynosi: {kalkulatorPolska.ObliczPodatek(kwota)}");
            Console.WriteLine($"Podatek w Niemczech od kwoty {kwota} wynosi: {kalkulatorNiemcy.ObliczPodatek(kwota)}");
        }
    }

    public interface IPodatekStrategia
    {
        decimal ObliczPodatek(decimal kwota);
    }

    public class PodatekPolska : IPodatekStrategia
    {
        decimal IPodatekStrategia.ObliczPodatek(decimal kwota)
        {
            decimal kwotaPodatku = kwota * 0.23M;
            return kwotaPodatku;
        }
    }

    public class PodatekNiemcy : IPodatekStrategia
    {
        decimal IPodatekStrategia.ObliczPodatek(decimal kwota)
        {
            decimal kwotaPodatku = kwota * 0.19M;
            return kwotaPodatku;
        }
    }

    public class KalkulatorPodatku
    {
        private readonly IPodatekStrategia _strategia;

        public KalkulatorPodatku(IPodatekStrategia strategia)
        {
            _strategia = strategia;
        }

        public decimal ObliczPodatek(decimal kwota)
        {
            return _strategia.ObliczPodatek(kwota);
        }
    }
}
