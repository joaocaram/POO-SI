using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comercio {
    internal class Produto {
        /// <summary>
        /// Descrição do produto a ser vendido
        /// </summary>
        public string descricao;
        /// <summary>
        /// Valor de uma unidade do produto. O valor mínimo é 1.
        /// </summary>
        public double valorUnitario;

        
        /// <summary>
        /// Registra dados de um produto, sendo estes a descrição (mínimo 2 caracteres) e preço unitário (mínimo R$1).
        /// Em caso de erro, a descrição será "Sem descrição" e o preço fica como R$1.
        /// </summary>
        /// <param name="desc">A descrição do produto (mínimo 2 caracteres)</param>
        /// <param name="valor">O preço unitário (mínimo R$ 1)</param>
        public void registrar(string desc, double valor) {
            if (desc.Length >= 2) 
                descricao = desc;
            else 
                descricao = "Sem descrição";
            valorUnitario = 1.0;
            if (valor > 1) 
                valorUnitario = valor;
        }

        /// <summary>
        /// Calcula o valor de venda de um lote do produto a partir de uma quantidade passada por parâmetro.
        /// Em caso de quantidade menor que 1, retorna sempre 0.
        /// </summary>
        /// <param name="quant">Quantidade de produtos do lote (mínimo: 1)</param>
        /// <returns>Double com o valor de venda do lote com a quantidade especificada ou 0 em caso de problemas</returns>
        public double valorLote(int quant) {
            double valor = 0.0;
            if(quant>0)
                valor = quant * valorUnitario;
            return valor;
        }

        /// <summary>
        /// Retorna os dados do Produto em string (a ser melhorado com polimorfismo futuramente).
        /// Os dados incluem descrição e o valor unitário com duas casas decimais.
        /// </summary>
        /// <returns>String com descrição e o valor unitário com duas casas decimais.</returns>
        public string ToString() {
            return descricao + " - valor unitário: R$ " + valorUnitario.ToString("0.00");
        }
    }
}
