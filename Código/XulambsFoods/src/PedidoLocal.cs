using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    public class PedidoLocal : IPedido
    {
        /// <summary>
        /// Para criar um vetor de pizzas de tamanho grande
        /// </summary>
        private const int MaxPizzas = 100;
        private const double TaxaServico = 0.1;
        private List<Comida> _comidas;
        public PedidoLocal(List<Comida> comidas) 
        {
            _comidas = comidas;
        }
        
        private double ValorItens() {
            double preco = 0d;
            foreach(Comida comida in _comidas)
                preco +=  comida.ValorFinal();
            return preco;
        }

        public double ValorTaxa() {
            return ValorItens() * TaxaServico;
        }

        /// <summary>
        /// Verifica se uma pizza pode ser adicionada ao pedido, ou seja, se o pedido está aberto e há espaço na memória.
        /// </summary>
        /// <returns>TRUE se puder adicionar, FALSE caso contrário</returns>
        public  bool PodeAdicionar() {
            return true;
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
        public string Relatorio() {
            StringBuilder sb = new StringBuilder(" - LOCAL\n");
            sb.AppendLine("=============================");
            int i = 1;
            Ordenador qs = new Ordenador(_comidas.ToArray());
            foreach (Comida comida in (Comida[])qs.ordenar()) {
                sb.AppendLine($"{(i):D2} - {comida.NotaDeCompra()}\n");
                i++;
            }
            sb.AppendLine($"TAXA SERVIÇO : {ValorTaxa():C2}");
            return sb.ToString();
        }

    }
}
