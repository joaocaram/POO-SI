using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/** 
         * MIT License
         *
         * Copyright(c) 2022-4 João Caram <caram@pucminas.br>
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
namespace XulambsFoods.src
{
    public class PedidoLocal : Pedido
    {
        #region constantes
        private const double TAXA_SERVICO = 0.1;
        #endregion

        #region construtores
        /// <summary>
        /// Cria um pedido para comer no local, vazio.
        /// </summary>
        public PedidoLocal():base("Pedido local ")
        { 
        }

        /// <summary>
        /// Cria um pedido para comer no local, com uma comida.
        /// </summary>
        /// <param name="nova">A comida que abre o pedido. Não deve ser nula</param>
        public PedidoLocal(Comida nova) : base("Pedido local ", nova)
        {

        }
        #endregion

        #region métodos de negócio

        /// <summary>
        /// Tenta adicionar uma comida ao pedido (poderá adicionar se o pedido estiver aberto). Retorna a quantidade de comidas do pedido após a execução.
        /// </summary>
        /// <param name="nova">A comida a ser adicionada. Não deve ser nula</param>
        /// <returns>A quantidade de comidas no pedido após a execução</returns>
        public override int addComida(Comida nova)
        {
            if (aberto && nova!=null)
            {
                itens[quantComidas] = nova;
                quantComidas++;
            }
            return quantComidas;
        }

        /// <summary>
        /// Calcula a taxa de serviço do pedido, correspondente a 10% do valor dos itens
        /// </summary>
        /// <returns>Double com a taxa de serviço do pedido (não negativa)</returns>
        public override double taxa()
        {
            return valorItens() * TAXA_SERVICO;
        }
        #endregion
    }
}
