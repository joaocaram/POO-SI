﻿using System;
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
        private BaseDados<Comida> _comidas;
        
        
        public PedidoLocal() 
        {
            _comidas = new BaseDados<Comida>();
        }
        
        
        public double ValorItens() {
            return _comidas.Totalizador(c => c.ValorFinal());
        }
        

        public double ValorTaxa() {
            return ValorItens() * TaxaServico;
        }

        /// <summary>
        /// Verifica se uma comida pode ser adicionada ao pedido, ou seja, se o pedido está aberto e há espaço na memória. O parâmetro comida não pode ser nulo, ou causará uma exceção.
        /// </summary>
        /// <returns>TRUE se puder adicionar, FALSE caso contrário</returns>
        public int Adicionar(Comida comida) {
            _comidas.Add(comida);
            return _comidas.Quantidade();
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

            sb.AppendLine(_comidas.RelatorioOrdenado());

            sb.AppendLine($"TAXA SERVIÇO : {ValorTaxa():C2}");
            return sb.ToString();
        }

        public override string ToString() {
            return $"Pedido Local com valor de {(ValorItens() + ValorTaxa()):C2}";
        }

    }
}
