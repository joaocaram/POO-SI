/** 
 * MIT License
 *
 * Copyright(c) 2025 João Caram <caram@pucminas.br>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
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

        public override string Relatorio()
        {
            return $"{_nome} ({_id}) - Carga horária de {_cargaHoraria}h no semestre. Mensalidade: {ValorMensalidade():C2}";
        }
    }

}
