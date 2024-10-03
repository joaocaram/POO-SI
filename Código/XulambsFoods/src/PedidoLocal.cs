using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    public class PedidoLocal : Pedido
    {
        /// <summary>
        /// Para criar um vetor de pizzas de tamanho grande
        /// </summary>
        private const int MaxPizzas = 100;
        private const double TaxaServico = 0.1;

        public PedidoLocal() : base(MaxPizzas)
        { }
        
        

        protected override double ValorTaxa() {
            return ValorItens() * TaxaServico;
        }

        /// <summary>
        /// Verifica se uma pizza pode ser adicionada ao pedido, ou seja, se o pedido está aberto e há espaço na memória.
        /// </summary>
        /// <returns>TRUE se puder adicionar, FALSE caso contrário</returns>
        protected override bool PodeAdicionar() {
            return (_aberto);
        }

        /// <summary>
        /// Cria um relatório para o pedido, contendo seu número, sua data(DD/MM/AAAA), detalhamento de cada pizza e o preço final a ser pago.
        /// </summary>
        /// <returns>String com os detalhes especificados: 
	    ///<br/><pre>
	    ///PEDIDO - NÚMERO - DD/MM/AAAA
	    ///01 - DESCRICAO DA PIZZA
	    ///02 - DESCRICAO DA PIZZA
	    ///03 - DESCRICAO DA PIZZA
        ///
	    ///TOTAL A PAGAR: R$ VALOR
	    ///</pre></returns>
        public override string Relatorio() {
            StringBuilder relat = new StringBuilder("XULAMBS PIZZA - Pedido ");
            relat.AppendLine($"{_idPedido:D2} - {_data.ToShortDateString()}");
            relat.AppendLine("=============================");

            for (int i = 0; i < _quantPizzas; i++) {
                relat.AppendLine($"{(i + 1):D2} - {_pizzas[i].NotaDeCompra()}");
            }
            relat.AppendLine($"\nTAXA SERVIÇO : {ValorTaxa():C2}");
            relat.AppendLine($"TOTAL A PAGAR: {PrecoAPagar():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }

    }
}
