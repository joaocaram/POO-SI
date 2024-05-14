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
    public class Cliente {
        private static int ultimoId = 0;
        private int id;
        private IFidelidade categoria;
        private string nome;
        private Queue<Pedido> pedidos;

        /// <summary>
        /// Construtor do cliente: id automático e fila para até 100 pedidos. Um cliente recém criado será da categoria Xulambs Junior
        /// </summary>
        /// <param name="nome">Nome do cliente. Não deve ser vazio, ou será alterado para "Cliente XX", sendo XX seu identificador</param>
        public Cliente(string nome) {
            this.nome = nome;
            this.pedidos = new Queue<Pedido>(100);
            this.id = ++ultimoId;
            categoria = new XulambsJunior(pedidos);
        }

        /// <summary>
        /// Verifica/atualiza a categoria de fidelidade do cliente (chamada delegada ao objeto da interface IFidelidade.
        /// </summary>
        public void verificarCategoria() {
            categoria = categoria.atualizarCategoria();
        }


        /// <summary>
        /// Registra um pedido para um cliente: coloca-o na fila.
        /// </summary>
        /// <param name="novo">Pedido a ser registrado. Não pode ser nulo, ou a operação será ignorada.</param>
        public void registrarPedido(Pedido novo) {
            if (novo != null)
                pedidos.Enqueue(novo);
        }

        /// <summary>
        /// Relatório completo do cliente, incluindo detalhamento de todos os seus pedidos.
        /// </summary>
        /// <returns>Uma string, com quebras de linha, contendo:
        /// <ol>
        /// <li> Nome, identificador e total gasto em pedidos </li>
        /// <li> Resumo de cada pedido, com descrições dos itens e seus valores, assim como valor do pedido </li>
        /// </ol>
        /// </returns>
        public string resumoPedidos() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.ToString());
            foreach (Pedido pedido in pedidos)
            {
                sb.AppendLine(pedido.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Calcula o valor total pago pelo cliente em todos os seus pedidos.
        /// </summary>
        /// <returns>Double com o valor total pago pelo cliente em todos os seus pedidos</returns>
        private double totalEmPedidos() {
            double valor = 0d;
            foreach (Pedido pedido in pedidos) {
                valor += pedido.precoFinal();
            }
            return valor;
        }

        /// <summary>
        /// Retorna o valor pago pelo cliente para este pedido, considerando os possíveis descontos de sua categoria de fidelidade
        /// </summary>
        /// <param name="pedido">Pedido original (não deve ser nulo)</param>
        /// <returns>Valor a pagar pelo pedido, incluindo o desconto (0 ou mais)</returns>
        public double valorAPagar(Pedido pedido) {
            double precoAPagar = pedido.precoFinal();
            precoAPagar -= categoria.desconto(pedido);
            return precoAPagar;
        }

        /// <summary>
        /// Representação simplificada do cliente em string: id, nome e valor total gasto em pedidos.
        /// </summary>
        /// <returns>String com id, nome e valor total gasto em pedidos.</returns>
        public override string ToString() {
            return $"Cliente nº {id}: {nome}\nTotal de {pedidos.Count} pedidos (R$ {totalEmPedidos().ToString("00.00")}).";
        }

        /// <summary>
        /// Código identificador do cliente (é o código definido na sua criação)
        /// </summary>
        /// <returns>Inteiro com o código do cliente.</returns>
        public override int GetHashCode() {
            return id;
        }


    }
}
