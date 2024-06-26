﻿using System;
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
    internal class PedidoDelivery : Pedido
    {
        #region constantes
        private const int MAX_COMIDAS = 10;
        private static readonly double[] VALORES_TAXA = { 0d, 5d, 8d };
        private static readonly double[] DISTANCIAS_ENTREGA = { 5d, 8d, double.MaxValue};
        #endregion

        #region atributos
        private double distancia;
        #endregion

        #region construtores
        /// <summary>
        /// Cria um pedido para a entrega. A distância deve ser não negativa, ou reverte para 0.1km
        /// </summary>
        /// <param name="dist">A distância da entrega, em km. Deve ser não negativa</param>
        public PedidoDelivery(double dist) : base("Pedido para entrega")
        {

            this.distancia = 0.1;
            if (dist > 0.1)
                this.distancia = dist;
        }
        #endregion

        #region métodos de negócio
        /// <summary>
        /// Tenta adicionar uma comida ao pedido. A operação será concretizada se o pedido estiver aberto,
        /// não houver atingido o máximo de comidas e o parâmetro não seja nulo. Retorna TRUE ou FALSE conforme
        /// foi possível executar ou não a operação.
        /// </summary>
        /// <param name="nova">A comida a ser inserida no pedido. Não deve ser nula.</param>
        /// <returns>TRUE se a comida foi adicionada, FALSE caso contrário.</returns>
        public override int addComida(Comida nova)
        {
            if (nova != null && podeAdicionarComida())
            {
                itens[quantComidas] = nova;
                quantComidas++;
            }
            return quantComidas;
        }

        /// <summary>
        /// Verifica se pode adicionar uma nova comida no pedido (o pedido deve estar aberto e não ter atingido a
        /// quantidade máxima de comidas)
        /// </summary>
        /// <returns>TRUE se é possível adicionar, FALSE caso contrário.</returns>
        private bool podeAdicionarComida()
        {
            return (aberto && quantComidas < MAX_COMIDAS);
        }

        /// <summary>
        /// Retorna o valor da taxa de entrega do pedido, calculada de acordo com a distância.
        /// </summary>
        /// <returns>Double não negativo com o valor da taxa de entrega</returns>
        public override double taxa()
        {
            int pos = 0;
            while(distancia > DISTANCIAS_ENTREGA[pos])
            {
                pos++;
            }
            return VALORES_TAXA[pos];
        }
        #endregion
    }
}
