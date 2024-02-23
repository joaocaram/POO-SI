namespace Hora {
    
    /**
     * App principal apenas para teste da classe Hora
     */
    internal class Program {
        static void testeBasico() {
            Hora minhaHora = new Hora();

            minhaHora.ajustar(20, 30, 29);  //válido e todos com dois dígitos
            Console.WriteLine("Hora: " + minhaHora.horaFormatada());

            minhaHora.ajustar(20, 30, 9);  //válido e um com um dígito                 
            Console.WriteLine("Hora: " + minhaHora.horaFormatada());

            minhaHora.ajustar(9, 8, 5);    //válido e todos com um dígito
            Console.WriteLine("Hora: " + minhaHora.horaFormatada());

            minhaHora.ajustar(20, 68, 15);      //inválido, deveria ir para 00:00:00
            Console.WriteLine("Hora: " + minhaHora.horaFormatada());
        }

        static void testeIncremento() {
            Hora horaBase = new Hora();
            horaBase.ajustar(19, 54, 00);
            Console.WriteLine("Hora original: " + horaBase.horaFormatada());
            horaBase.incrementar(28, 's');          //19:54:28
            Console.WriteLine("Nova hora: " + horaBase.horaFormatada());
            horaBase.incrementar(44, 's');          //19:55:12 (72->12)
            Console.WriteLine("Nova hora: " + horaBase.horaFormatada());
            horaBase.incrementar(20, 'm');          //20:15:12 (75->15)
            Console.WriteLine("Nova hora: " + horaBase.horaFormatada());
            horaBase.incrementar(6, 'h');           //02:15:12 (26->02_
            Console.WriteLine("Nova hora: " + horaBase.horaFormatada());

            
        }

        static void testeVisibilidade() {
            Hora horaTeste = new Hora();
            horaTeste.ajustar(22, 04, 13);      //não podemos usar os atributos diretamente
            Console.WriteLine("Hora original: " + horaTeste.horaFormatada());
            horaTeste.incrementar(44, 'm');     //não podemos usar os atributos diretamente
            Console.WriteLine("Nova hora: " + horaTeste.horaFormatada());
        }

        static void Main(string[] args) {
            testeBasico();
            Console.Write("Enter para continuar.");
            Console.ReadLine();
            testeIncremento();
            Console.Write("Enter para continuar.");
            Console.ReadLine();
            testeVisibilidade();
        }

    }
}
