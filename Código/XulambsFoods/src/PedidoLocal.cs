using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods.src
{
    public class PedidoLocal : Pedido
    {
        #region constantes
        private const double TAXA_SERVICO = 0.1;
        #endregion

        #region construtores
        /// <summary>
        /// Cria um pedido para comer no local, vazio.
        /// </summary>
        public PedidoLocal():base("Pedido local ")
        { 
        }

        /// <summary>
        /// Cria um pedido para comer no local, com uma comida.
        /// </summary>
        /// <param name="nova">A comida que abre o pedido. Não deve ser nula</param>
        public PedidoLocal(Comida nova) : base("Pedido local ", nova)
        {

        }
        #endregion

        #region métodos de negócio

        /// <summary>
        /// Tenta adicionar uma comida ao pedido (poderá adicionar se o pedido estiver aberto). Retorna a quantidade de comidas do pedido após a execução.
        /// </summary>
        /// <param name="nova">A comida a ser adicionada. Não deve ser nula</param>
        /// <returns>A quantidade de comidas no pedido após a execução</returns>
        public override int addComida(Comida nova)
        {
            if (aberto && nova!=null)
            {
                itens[quantComidas] = nova;
                quantComidas++;
            }
            return quantComidas;
        }

        /// <summary>
        /// Calcula a taxa de serviço do pedido, correspondente a 10% do valor dos itens
        /// </summary>
        /// <returns>Double com a taxa de serviço do pedido (não negativa)</returns>
        public override double taxa()
        {
            return valorItens() * TAXA_SERVICO;
        }
        #endregion
    }
}
