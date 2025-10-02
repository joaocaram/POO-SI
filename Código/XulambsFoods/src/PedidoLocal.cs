using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class PedidoLocal : Pedido {
        private const double PctServico = 0.1;

        public PedidoLocal() : base() { }

        private double TaxaServico() {
            return ValorItens() * PctServico;
        }

        public override double PrecoAPagar() {
            return ValorItens() + TaxaServico();
        }

        /// <summary>
        /// Relatório do pedido, contendo seu identificador, data,
        /// detalhamento das pizzas e preço a pagar.
        /// </summary>
        /// <returns>Uma string, multilinhas, com a informação descrita</returns>
        public override string ToString() {
            return $"Pedido Local {DetalhamentoNota()}Serviço: {TaxaServico():C2}\n{RodapeNota()}";
        }
    }
}
