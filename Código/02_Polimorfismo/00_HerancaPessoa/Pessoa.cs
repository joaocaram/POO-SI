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
    /// <summary>
    /// Demonstração básica dos conceitos do polimorfismo universal de inclusão:
    /// <ul>
    /// <li>Acoplamento com o construtor da classe mãe</li>
    /// <li>Herança de membros protegidos e públicos</li>
    /// <li>Métodos virtuais e sobrescritos</li>
    /// <li>Uso de "base" no reúso de código</li>
    /// </ul>
    /// </summary>
    public class Pessoa
    {
        private static Random s_sorteio = new Random(42);
        protected int _id;
        protected string _nome;
        protected string _documento;
        protected DateOnly _dataNasc;
        protected string _email;
        protected int _cargaHoraria;

        /// <summary>
        /// Cria uma pessoa com _nome, data de nascimento e _documento. 
        /// Como trata-se de uma demonstração, sem requisitos envolvidos,
        /// nenhum dado é validado.
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="nascimento">Data de nascimento</param>
        /// <param name="documento">Um _documento identificador</param>
        public Pessoa(string nome, DateOnly nascimento, string documento)
        {
            this._nome = nome;
            this._dataNasc = nascimento;
            this._documento = documento;
            this._id = s_sorteio.Next() % 1_000_000;
        }

        /// <summary>
        /// Aloca uma carga horária para a pessoa no âmbito da Universidade.
        /// A carga precisa ser maior que 0.
        /// </summary>
        /// <param name="cargaHoraria">Valor da carga horária atual (> 0)</param>
        public void setCargaHoraria(int cargaHoraria) 
        {
            if (cargaHoraria > 0)
                this._cargaHoraria = cargaHoraria;
        }


        public int idade()
        {
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
            int idade = (hoje.Year - _dataNasc.Year);
            if (hoje.DayOfYear > _dataNasc.DayOfYear)
                idade--;
            return idade;
        }

        public void enviarEmail(string texto)
        {
            //fingindo que estou usando um serviço bacanudo para enviar emails
        }

        public virtual string relatorio()
        {
            return _nome + ": " + idade() + " anos.";
        }
    }

}
