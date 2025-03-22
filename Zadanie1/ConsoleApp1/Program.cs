namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFabrykaNPC fabryka;
            switch (new Random().Next(3))
            {
                case 0:
                    fabryka = new FabrykaMaga();
                    break;

                case 1:
                    fabryka = new FabrykaWojownika();
                    break;

                default:
                    fabryka = new FabrykaZlodzieja();
                    break;
            }
            INPC npc = fabryka.CreateNPC();
            npc.Przedstawsie();
        }
    }

    public interface INPC
    {
        void Przedstawsie();
    }

    public interface IFabrykaNPC
    {
        INPC CreateNPC();
    }
}
