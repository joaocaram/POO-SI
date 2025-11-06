using System.Runtime.CompilerServices;

namespace POO_C__Interfaces_Amplexa {
    internal class Program {
        static int numero = 1;        //para id dos dispositivos
        

        /**
         * Cria um dispositivo baseado em uma string descritora. (método análogo a uma fábrica)
         * @param qual Qual dispositivo deve ser criado
         * @return Um dispositivo, ou nulo, em caso de string não reconhecida
         */
        public static Dispositivo criarDispositivo(String qual) {
            Dispositivo novo = qual.ToLower() switch {
                "lampada" => new Lampada("L" + numero),
                "cafeteira" => new Cafeteira("C" + numero)
            };
            numero++;
            return novo;
        }




        static void Main(string[] args) {

            Amplexa assistente = new Amplexa("Faz Tudo");
            for (int i = 0; i < 6; i++) {
                Dispositivo lamp = criarDispositivo("lampada");
                assistente.AddDispositivo(lamp);
            }

            Console.WriteLine("=====INICIO=====");
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.WriteLine("=====LIGANDO DISPOSITIVOS=====");
            assistente.Ligar("L5");
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.WriteLine("=====ADICIONANDO E LIGANDO CAFETEIRAS=====");
            assistente.AddDispositivo(criarDispositivo("cafeteira"));
            assistente.AddDispositivo(criarDispositivo("cafeteira"));
            assistente.Ligar("C7");
            Console.WriteLine(assistente);
            Console.ReadLine();

            Console.ReadLine();
        }
    }
}
