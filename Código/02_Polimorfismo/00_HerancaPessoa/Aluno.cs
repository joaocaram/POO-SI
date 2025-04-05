using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerancaPessoa
{
    public class Aluno : Pessoa
    {


        private static double s_horaAula;
        private double _valorMatricula;

        static Aluno()
        {
            s_horaAula = 25d;
        }

        /// <summary>
        /// Construtor de um aluno, que recebe nome, data de nascimento,
        /// documento e um email de contato. O aluno também recebe o valor
        /// pago como matrícula (mínimo de R$1000)
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="nascimento">Data de nascimento</param>
        /// <param name="documento">Um documento identificador</param>
        /// <param name="email">O email de contato da pessoa</param>
        /// <param name="valorMatricula">Matrícula paga (>=1000)</param>
        public Aluno(string nome, DateOnly nascimento, string documento, string email,
                        double valorMatricula) :
                base(nome, nascimento, documento, email)
        {
            if (valorMatricula < 1000d)
                valorMatricula = 1000d;
            _valorMatricula = valorMatricula;
        }

        /// <summary>
        /// Valor da mensalidade do aluno: sua carga horária total do semestre,
        /// multiplicado pelo valor da hora aula, descontando-se o valor pago
        /// como matrícula - e tudo isso dividido por 6 meses.
        /// </summary>
        /// <returns>Double com o valor da mensalidade</returns>
        public double ValorMensalidade()
        {
            return ((_cargaHoraria * s_horaAula) - _valorMatricula) / 6.0;
        }

    }

}
