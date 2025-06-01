using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            IOperacjaFinansowa wplata = new Wplata();
            IOperacjaFinansowa wyplata = new Wyplata();

            bank.RealizujOperacje(wplata);
            bank.RealizujOperacje(wyplata);
        }
    }

    public interface IWyplacanie { }
    public interface IWplacanie { }

    public interface IOperacjaFinansowa
    {
        IMediator Mediator { get; set; }
        void Realizuj();
    }
    public interface IMediator
    {
        public void RealizujOperacje(IOperacjaFinansowa operacja);
    }

    public class Bank : IMediator
    {
        public void RealizujOperacje(IOperacjaFinansowa operacja)
        {
            operacja.Mediator = this;
            operacja.Realizuj();
            ZapiszDoPliku(operacja.GetType().Name);
        }

        public void ZapiszDoPliku(string operacja)
        {
            using (FileStream fs = new FileStream("operacje.txt", FileMode.Append, FileAccess.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, operacja);
            }
        }
    }

    public class Wplata : IOperacjaFinansowa, IWplacanie
    {
        public IMediator Mediator { get; set; }
        public void Realizuj()
        {
            Console.WriteLine("Wykonano operacje wplaty");
        }
    }

    public class Wyplata : IOperacjaFinansowa, IWyplacanie
    {
        public IMediator Mediator { get; set; }
        public void Realizuj()
        { 
            Console.WriteLine("Wykonano operacje wyplaty");
        }
    }
}
