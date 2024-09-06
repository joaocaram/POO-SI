using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XulambsFoods_2024_2.src {
    /// <summary>
    /// Classe Pedido: composição com classe Pizza. Um pedido pode conter diversas pizzas. Elas  podem ser adicionadas desde que o pedido esteja aberto.Um pedido tem um identificador único e armazena sua data.Ele deve calcular o preço a ser pago por ele e emitir um relatório detalhando suas pizzas e o valor a pagar.
    /// </summary>
    public class Pedido {

        #region static/const
        /// <summary>
        /// Para gerar o id incremental automático
        /// </summary>
        private static int _ultimoPedido;
        /// <summary>
        /// Para criar um vetor de pizzas de tamanho grande
        /// </summary>
        private const int MaxPizzas = 100;
        #endregion

        #region atributos
        private int _idPedido;
        private DateOnly _data;
        private Pizza[] _pizzas;
        private int _quantPizzas;
        private bool _aberto;
        #endregion

        #region construtores
        static Pedido() {
            _ultimoPedido = 0;
        }

        /// <summary>
        /// Cria um pedido com a data de hoje. Identificador é gerado automaticamente a partir do último identificador armazenado.
        /// </summary>
        public Pedido() {
            _quantPizzas = 0;
            _aberto = true;
            _pizzas = new Pizza[MaxPizzas];
            _data = DateOnly.FromDateTime(DateTime.Now);
            _idPedido = ++_ultimoPedido;
        }
        #endregion

        #region métodos de negócio
       /// <summary>
       /// Verifica se uma pizza pode ser adicionada ao pedido, ou seja, se o pedido está aberto e há espaço na memória.
        /// </summary>
        /// <returns>TRUE se puder adicionar, FALSE caso contrário</returns>
        private bool PodeAdicionar() {
            return _aberto;
        }

        /// <summary>
        /// Adiciona uma pizza ao pedido, se for possível. Caso não seja, a operação é ignorada.Retorna a quantidade de pizzas do pedido após a execução. 
        /// </summary>
        /// <param name="pizza">Pizza a ser adicionada</param>
        /// <returns>A quantidade de pizzas do pedido após a execução.</returns>
        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar()) {
                _pizzas[_quantPizzas] = pizza;
                _quantPizzas++;
            }
            return _quantPizzas;
        }

        /// <summary>
        ///  Fecha um pedido, desde que ele contenha pelo menos 1 pizza. Caso esteja vazio, a operação é ignorada.
        /// </summary>
        public void FecharPedido() {
            if(_quantPizzas > 0)
                _aberto = false;
        }

        /// <summary>
        /// Calcula o preço a ser pago pelo pedido (no momento, a soma dos preços de todas as pizzas contidas no pedido)
        /// </summary>
        /// <returns>Double com o valor a ser pago pelo pedido(> 0)</returns>
        public double PrecoAPagar() {
            double preco = 0d;
            for (int i = 0; i < _quantPizzas; i++)
            {
                preco += _pizzas[i].ValorFinal();
            }
            return preco;
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
            StringBuilder relat = new StringBuilder("XULAMBS PIZZA - Pedido ");
            relat.AppendLine($"{_idPedido:D2} - {_data.ToShortDateString()}");
            relat.AppendLine("=============================");

            for (int i = 0; i < _quantPizzas; i++) {
                relat.AppendLine($"{(i + 1):D2} - {_pizzas[i].NotaDeCompra()}");
            }
            relat.AppendLine($"\nTOTAL A PAGAR: {PrecoAPagar():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }
        #endregion
    }
}