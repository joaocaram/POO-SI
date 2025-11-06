using System.Runtime.CompilerServices;

namespace POO_C__Interfaces_Amplexa {
    internal class Program {
        static int numero = 1;        //para id dos dispositivos


        public static Dispositivo CriarDispositivo(String qual) {
            Dispositivo novo = qual.ToLower() switch {
                "lampada" => new Lampada("L" + numero),
                "cafeteira" => new Cafeteira("C" + numero),
                "geladeira" => new Geladeira("G" + numero),
            };
            numero++;
            return novo;
        }

        static void Main(string[] args) {

            Amplexa assistente = new Amplexa("Faz Tudo");
            for (int i = 0; i < 6; i++) {
                Dispositivo lamp = CriarDispositivo("lampada");
                assistente.AddDispositivo(lamp);
            }

            Console.WriteLine("=====INICIO=====");
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("=====LIGANDO DISPOSITIVOS=====");
            assistente.Ligar("L5");
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("=====ADICIONANDO E LIGANDO CAFETEIRAS=====");
            assistente.AddDispositivo(CriarDispositivo("cafeteira"));
            assistente.AddDispositivo(CriarDispositivo("cafeteira"));
            assistente.Ligar("C7");
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("=====ADICIONANDO GELADEIRA=====");
            assistente.AddDispositivo(CriarDispositivo("GELADEIRA"));
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.Clear();
            Console.WriteLine("=====REGULANDO CAFETEIRAS E GELADEIRAS=====");
            assistente.Ligar("C8");
            assistente.Regular("G9", 77);
            assistente.Regular("C8", 93);
            Console.WriteLine(assistente);
            Console.ReadLine();
        }
    }
}
