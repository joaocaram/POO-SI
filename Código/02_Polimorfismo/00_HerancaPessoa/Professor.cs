using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerancaPessoa
{
    public class Professor : Pessoa
    {

        private static double s_horaAula = 30.35;

        /// <summary>
        /// Cria um professor com nome, data de nascimento, documento e email.
        /// A carga horária padrão é de 20h e pode ser alterada por meio do 
        /// método herdado de <i>Pessoa</i>.
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="nascimento">Data de nascimento</param>
        /// <param name="documento">Um documento identificador</param>
        /// <param name="email">O email de contato da pessoa</param>
        public Professor(string nome, DateOnly nascimento, string documento, string email) :
                base(nome, nascimento, documento, email)
        {
            _cargaHoraria = 20;
        }

        /// <summary>
        /// Salário bruto do professor, que inclui 20% de extra classe.
        /// </summary>
        /// <returns>Double positivo com o valor do salário bruto do professor.</returns>
        public double SalarioBruto()
        {
            return s_horaAula * (_cargaHoraria*4.5) * 1.20;
        }

        /// <summary>
        /// Resumo do professor: nome, idade (vindo da classe mãe) e salário bruto.
        /// </summary>
        /// <returns>String de uma linha com as informações acima</returns>
        public override string Relatorio()
        {
            return $"{base.Relatorio()} Salário {SalarioBruto():C2}.";
        }

    }
}
