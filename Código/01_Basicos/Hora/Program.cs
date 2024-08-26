namespace Hora {
    
    /**
     * App principal apenas para teste da classe Hora
     */
    internal class Program {
        static void Main(string[] args) {
            Hora hora;
            Hora hora2;
            int horas;
            int minutos;
            int segundos;
            int incremento;
            string horaDoUsuario;

            Console.Write("Por favor, digite um horário válido (HH:MM:SS): ");
            horaDoUsuario = Console.ReadLine();

            string[] detalhesHora = horaDoUsuario.Split(":");
            horas = int.Parse(detalhesHora[0]);
            minutos = int.Parse(detalhesHora[1]);
            segundos = int.Parse(detalhesHora[2]);

            hora = new Hora();
            hora.Ajustar(horas, minutos, segundos);

            Console.Write("Sua hora escolhida: ");
            Console.WriteLine(hora.HoraFormatada());

            Console.Write("\nPor favor, digite um incremento em segundos positivos: ");
            incremento = int.Parse(Console.ReadLine());

            hora2 = hora.Incrementar(incremento);

            Console.WriteLine($"{hora.HoraFormatada()} é sua hora original e {hora2.HoraFormatada()} " +
                $"é sua hora com incremento de {incremento} segundos");
                            

            Console.Write($"{hora.HoraFormatada()} está na frente de {hora2.HoraFormatada()}? ");
            Console.WriteLine(hora.EstahNaFrenteDe(hora2));

            Console.Write($"{hora2.HoraFormatada()} está na frente de {hora.HoraFormatada()}? ");
            Console.WriteLine(hora2.EstahNaFrenteDe(hora));

            Console.Write("Ajustando a hora para 19:08:24: ");
            hora.Ajustar(19, 8, 24);
            Console.WriteLine(hora.HoraFormatada());
        }
    }
}
