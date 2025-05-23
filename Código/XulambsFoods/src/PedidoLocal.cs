using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class PedidoLocal : Pedido{

        public PedidoLocal() : base(){

        }

        public override double PrecoAPagar() {
            return base.PrecoAPagar();
        }

        /// <summary>
        /// Relatório do pedido, contendo seu identificador, data,
        /// detalhamento das pizzas e preço a pagar.
        /// </summary>
        /// <returns>Uma string, multilinhas, com a informação descrita</returns>
        public override string ToString() {
            StringBuilder relat = new StringBuilder($"Pedido Local {DetalhesPedido()}");
            relat.Append(RodapeNotinha());
            return relat.ToString();
        }
    }
}
