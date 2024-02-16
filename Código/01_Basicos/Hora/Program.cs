namespace Hora {
    
    /**
     * App principal apenas para teste da classe Hora
     */
    internal class Program {
        static void Main(string[] args) {
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
    }
}
