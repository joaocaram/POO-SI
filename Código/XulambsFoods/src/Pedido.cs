using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XulambsFoods_2024_2.src
{
    /// <summary>
    /// Classe Pedido: composição com classe Pizza. Um pedido pode conter diversas pizzas. Elas  podem ser adicionadas desde que o pedido esteja aberto.Um pedido tem um identificador único e armazena sua data.Ele deve calcular o preço a ser pago por ele e emitir um relatório detalhando suas pizzas e o valor a pagar.
    /// </summary>
    public abstract class Pedido
    {

        #region static/const
        /// <summary>
        /// Para gerar o id incremental automático
        /// </summary>
        private static int _ultimoPedido;
        #endregion

        #region atributos
        protected int _maxComidas;
        protected int _idPedido;
        protected DateOnly _data;
        protected Comida[] _comidas;
        protected int _quantComidas;
        protected bool _aberto;
        #endregion

        #region construtores
        static Pedido()
        {
            _ultimoPedido = 0;
        }

        /// <summary>
        /// Cria um pedido com a data de hoje. Identificador é gerado automaticamente a partir do último identificador armazenado.
        /// </summary>
        protected Pedido(int maxPizzas)
        {
            if (maxPizzas < 1) maxPizzas = 1;
            _maxComidas = maxPizzas;
            _quantComidas = 0;
            _aberto = true;
            _comidas = new Comida[maxPizzas];
            _data = DateOnly.FromDateTime(DateTime.Now);
            _idPedido = ++_ultimoPedido;
        }
        #endregion

        #region métodos de negócio

        protected abstract bool PodeAdicionar();
        protected abstract double ValorTaxa();
        public abstract string Relatorio();

        protected double ValorItens()
        {
            double preco = 0d;
            for (int i = 0; i < _quantComidas; i++)
            {
                preco += _comidas[i].ValorFinal();
            }
            return preco;
        }
        /// <summary>
        /// Adiciona uma pizza ao pedido, se for possível. Caso não seja, a operação é ignorada.Retorna a quantidade de pizzas do pedido após a execução. 
        /// </summary>
        /// <param name="pizza">Pizza a ser adicionada</param>
        /// <returns>A quantidade de pizzas do pedido após a execução.</returns>
        public int Adicionar(Pizza pizza)
        {
            return Adicionar(pizza);
        }

        public int Adicionar(Comida comida) {
            if (PodeAdicionar()) {
                _comidas[_quantComidas] = comida;
                _quantComidas++;
            }
            return _quantComidas;
        }

        /// <summary>
        ///  Fecha um pedido, desde que ele contenha pelo menos 1 pizza. Caso esteja vazio, a operação é ignorada.
        /// </summary>
        public void FecharPedido()
        {
            if (_quantComidas > 0)
                _aberto = false;
        }

        /// <summary>
        /// Calcula o preço a ser pago pelo pedido (no momento, a soma dos preços de todas as pizzas contidas no pedido)
        /// </summary>
        /// <returns>Double com o valor a ser pago pelo pedido(> 0)</returns>
        public double PrecoAPagar()
        {
            return ValorItens() + ValorTaxa();
        }

        
        #endregion
    }
}