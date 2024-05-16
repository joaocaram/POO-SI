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


namespace XulambsFoods.src {
    internal class XulambsSenior : IFidelidade {
        private const double PCT_DESCONTO = 0.15;
        private const int PEDIDOS_DESC_ADIC = 5;
        private const double VALOR_DESC_ADIC = 10d;
        private const int MIN_PEDIDOS = 20;
        private const double MEDIA_MIN = 35;
        private Queue<Pedido> pedidos;

        /// <summary>
        /// Cria uma nova fidelidade Senior, com a fila de pedidos a ser observada.
        /// </summary>
        /// <param name="pedidos">Fila de pedidos do cliente com esta fidelidade</param>
        public XulambsSenior(Queue<Pedido> pedidos) {
            this.pedidos = pedidos;
        }

        /// <summary>
        /// Verifica se o cliente mudou de categoria de fidelidade. Retorna a categoria correta depois da análise.
        /// </summary>
        /// <returns>Uma categoria de fidelidade, de acordo com os pedidos já realizados pelo cliente</returns>
        public IFidelidade atualizarCategoria() {
            IFidelidade novaCategoria = new XulambsPleno(pedidos);
            if (pedidos.Count >= MIN_PEDIDOS) {
                double valor = 0d;
                double media;
                foreach (Pedido item in pedidos) {
                    valor += item.precoFinal();
                }
                media = valor / pedidos.Count;
                if (media >= MEDIA_MIN)
                    novaCategoria = this;
            }
            return novaCategoria;
        }

        /// <summary>
        /// Retorna o desconto concedido ao cliente neste pedido, de acordo com a categoria de fidelidade.
        /// </summary>
        /// <param name="pedido">Pedido original</param>
        /// <returns>Desconto obtido pelo cliente</returns>
        public double desconto(Pedido pedido) {
            double precoOriginal = pedido.precoFinal();
            double desconto = PCT_DESCONTO * precoOriginal;
            if (pedidos.Count % PEDIDOS_DESC_ADIC == 0)
                desconto += VALOR_DESC_ADIC;
            desconto = (desconto > precoOriginal) ? precoOriginal : desconto;
            return desconto;
        }
    }
}

