using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerancaPessoa
{
    public class Pessoa
    {
        static Random sorteio = new Random(42);
        protected int id;
        protected string nome;
        protected string documento;
        protected DateOnly dataNasc;
        protected string email;
        protected int cargaHoraria;

        public Pessoa(string nome, DateOnly nascimento, string documento)
        {
            this.nome = nome;
            this.dataNasc = nascimento;
            this.documento = documento;
            this.id = sorteio.Next() % 1_000_000;
        }
        public void setCargaHoraria(int cargaHoraria) 
        {
            if (cargaHoraria > 0)
                this.cargaHoraria = cargaHoraria;
        }
        public int idade()
        {
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
            return (int)((hoje.DayNumber - dataNasc.DayNumber) / 365.25);
        }

        public void enviarEmail(string texto)
        {
            //fingindo que estou usando um serviço bacanudo para enviar emails
        }

        public virtual string relatorio()
        {
            return nome + ": " + idade() + " anos.";
        }
    }

}
