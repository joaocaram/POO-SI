using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XulambsFoods_2024_2.src
{
    /// <summary>
    /// Classe Pedido: composição com classe Pizza. Um pedido pode conter diversas pizzas. Elas  podem ser adicionadas desde que o pedido esteja aberto.Um pedido tem um identificador único e armazena sua data.Ele deve calcular o preço a ser pago por ele e emitir um relatório detalhando suas pizzas e o valor a pagar.
    /// </summary>
    public class Pedido : IComparable, IEquatable<int>
    {

        #region static/const
        /// <summary>
        /// Para gerar o id incremental automático
        /// </summary>
        private static int _ultimoPedido;
        #endregion

        #region atributos
        private IPedido modalidade;
        private int _idPedido;
        private  DateOnly _data;
        private List<Comida> _comidas;
        private int _quantComidas;
        private bool _aberto;
        #endregion

        #region construtores
        static Pedido()
        {
            _ultimoPedido = 0;
        }

        /// <summary>
        /// Cria um pedido com a data de hoje. Identificador é gerado automaticamente a partir do último identificador armazenado.
        /// </summary>
        public Pedido(double distancia)
        {
            
            _quantComidas = 0;
            _aberto = true;
            _comidas = new List<Comida>();
            _data = DateOnly.FromDateTime(DateTime.Now);
            _idPedido = ++_ultimoPedido;
            setModalidade(distancia);
        }

        private void setModalidade(double distancia) {
            modalidade = (distancia > 0) ? new PedidoEntrega(_comidas, distancia) : new PedidoLocal(_comidas);
        }
        #endregion

        #region métodos de negócio

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
            if (_aberto && modalidade.PodeAdicionar()) {
                _comidas.Add(comida);
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
            return ValorItens() + modalidade.ValorTaxa();
        }

        public string Relatorio() {
            StringBuilder relat = new StringBuilder("XULAMBS PIZZA - Pedido ");
            relat.Append($"{_idPedido:D2} - {_data.ToShortDateString()}");
            relat.AppendLine(modalidade.Relatorio());
            relat.AppendLine($"TOTAL A PAGAR: {PrecoAPagar():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();

        }

        public override string ToString() {
            return Relatorio();
        }
       
        public override bool Equals(object? obj) {
            Pedido outro = (Pedido)obj;
            return _idPedido == outro._idPedido && _data.Equals(outro._data);
        }

        public bool Equals(int other) {
            return _idPedido == other;
        }

        public override int GetHashCode() {
            return HashCode.Combine(_idPedido, _data);
        }

        public int CompareTo(object? other) {
            Pedido outro = (Pedido)other;
            int resposta = 0;
            double meuValor = PrecoAPagar();
            double valorDoOutro = outro.PrecoAPagar();
            
            double diferenca = meuValor - valorDoOutro;
            if(diferenca < 0) {
                resposta = -1;
            }
            else if(diferenca > 0) {
                resposta = 1;
            }
            return resposta;
        }

        


        #endregion
    }
}