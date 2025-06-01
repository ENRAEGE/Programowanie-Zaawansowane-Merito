using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> clothes = new List<string>
            {
                "Koszulka",
                "Spodnie dresowe",
                "Skarpetki",
                "Bluza z kapturem",
                "Czapka zimowa"
            };

            Console.WriteLine("=== Ręczne pranie ===");
            var manualWasher = new ManualWasher();
            var adapter = new ManualWasherAdapter(manualWasher);
            var laundryManual = new LaundryService(adapter);
            laundryManual.WashAll(clothes);

            // 2. Pranie automatyczne w osobnym wątku
            Console.WriteLine("\n=== Pranie automatyczne w osobnym wątku ===");
            var washingMachine = new WashingMachine();
            var laundryMachine = new LaundryService(washingMachine);

            Thread thread = new Thread(() => laundryMachine.WashAll(clothes));
            thread.Start();
            thread.Join();
        }

        public interface IWasher
        {
            void Wash(string cloth);
        }

        public class ManualWasherAdapter : IWasher
        {
            private ManualWasher _manualWasher;

            public ManualWasherAdapter(ManualWasher manualWasher)
            {
                _manualWasher = manualWasher;
            }

            public void Wash(string cloth)
            {
                _manualWasher.ScrubWithBoard(cloth);
            }
        }

        public class ManualWasher
        {
            public void ScrubWithBoard(string clothes)
            {
                string time = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"[RĘCZNE] [{time}] Szoruję ubranie: {clothes}");
            }
        }

        public class WashingMachine : IWasher
        {
            public void Wash(string cloth)
            {
                string start = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"[PRALKA] [{start}] Rozpoczynam pranie: {cloth}");

                Thread.Sleep(1000); // symulacja prania

                string end = DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine($"[PRALKA] [{end}] Zakończono pranie: {cloth}");
            }
        }

    }
}
