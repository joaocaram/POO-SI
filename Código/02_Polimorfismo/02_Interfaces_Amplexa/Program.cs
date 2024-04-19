using System.Runtime.CompilerServices;

namespace POO_C__Interfaces_Amplexa {
    internal class Program {
        static int numero = 0;
        
        static Dispositivo? criarDispositivo(string qual) {
            numero++;
            return qual.ToLower() switch {
                "lampada" => new Lampada("L" + numero),
                "cafeteira" => new Cafeteira("C" + numero),
                "geladeira" => new Geladeira("G" + numero),
                _ => null
            };
            
        }

        static void Main(string[] args) {
            Amplexa assistente = new Amplexa("Assistente POO");

            for (int i = 0; i < 10; i++)
            {
                assistente.AddDispositivo(criarDispositivo("lampada"));
            }
            assistente.AddDispositivo(criarDispositivo("cafeteira"));
            assistente.AddDispositivo(criarDispositivo("geladeira"));
            assistente.AddDispositivo(criarDispositivo("geladeira"));

            Console.WriteLine(assistente);
            Console.ReadKey();
            Console.Clear();

            assistente.Ligar("L7");
            assistente.Ligar("C11");
            assistente.Regular("G12", 77);

            Console.WriteLine(assistente);
            Console.ReadKey();
            Console.Clear();

            assistente.Regular("C11", 93);
            Console.WriteLine(assistente);
        }
    }
}
