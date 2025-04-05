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
