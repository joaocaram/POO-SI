using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XulambsFoods.src;

namespace XulambsFoods_C_.src {
    public class Cliente {
        private static int ultimoId = 0;
        private int id;
        private string nome;
        private Queue<Pedido> pedidos;

        /// <summary>
        /// Construtor do cliente: id automático e fila para até 100 pedidos
        /// </summary>
        /// <param name="nome">Nome do cliente. Não deve ser vazio, ou será alterado para "Cliente XX", sendo XX seu identificador</param>
        public Cliente(string nome) {
            this.nome = nome;
            this.pedidos = new Queue<Pedido>(100);
            this.id = ++ultimoId;
            if (this.nome.Length == 0) this.nome = "Cliente " + this.id;
        }

        /// <summary>
        /// Registra um pedido para um cliente: coloca-o na fila.
        /// </summary>
        /// <param name="novo">Pedido a ser registrado. Não pode ser nulo, ou a operação será ignorada.</param>
        public void registrarPedido(Pedido novo) {
            if(novo!=null)
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
