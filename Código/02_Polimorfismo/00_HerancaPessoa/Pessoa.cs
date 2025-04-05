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
        /// Cria uma pessoa com nome, data de nascimento, documento e email. 
        /// Como trata-se de uma demonstração, sem requisitos envolvidos,
        /// nenhum dado é validado.
        /// </summary>
        /// <param name="nome">Nome da pessoa</param>
        /// <param name="nascimento">Data de nascimento</param>
        /// <param name="documento">Um documento identificador</param>
        /// <param name="email">O email de contato da pessoa</param>
        public Pessoa(string nome, DateOnly nascimento, string documento, string email)
        {
            _nome = nome;
            _dataNasc = nascimento;
            _documento = documento;
            _id = s_sorteio.Next() % 1_000_000;
            _email = email;
        }

        /// <summary>
        /// Aloca uma carga horária para a pessoa no âmbito da Universidade.
        /// A carga precisa ser maior que 0.
        /// </summary>
        /// <param name="cargaHoraria">Valor da carga horária atual (> 0)</param>
        public void SetCargaHoraria(int cargaHoraria)
        {
            if (cargaHoraria > 0)
                _cargaHoraria = cargaHoraria;
        }


        /// <summary>
        /// Retorna a idade da pessoa. 
        /// </summary>
        /// <returns>Int com a idade da pessoa (valor não negativo)</returns>
        public int Idade()
        {
            DateOnly hoje = DateOnly.FromDateTime(DateTime.Now);
            int idade = (hoje.Year - _dataNasc.Year);
            if (hoje.DayOfYear > _dataNasc.DayOfYear)
                idade--;
            return idade;
        }

        /// <summary>
        /// <i>Stub</i> para simular o envio de um email 
        /// </summary>
        /// <param name="texto"></param>
        public void EnviarEmail(string texto)
        {
            //fingindo que estou usando um serviço bacanudo para enviar emails
        }

        /// <summary>
        /// Cria uma string com nome e idade da pessoa.
        /// </summary>
        /// <returns>String com nome e idade (em anos) da pessoa</returns>
        public virtual string Relatorio()
        {
            return $"{_nome}: {Idade()} anos.";
        }
    }

}
