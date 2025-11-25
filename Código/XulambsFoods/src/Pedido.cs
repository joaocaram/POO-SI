using System.Text;

/** 
* MIT License
*
* Copyright(c) 2024-25 João Caram <caram@pucminas.br>
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

namespace XulambsFoods_2025_1.src {
    /// <summary>
    /// Um pedido pode agrupar várias pizzas. Deve exibir um relatório descritivo 
    /// com o detalhamento das pizzas e o valor total a pagar.
    /// </summary>
    public abstract class Pedido : IComparable<Pedido>{
        static DateTime dataBase = new DateTime(2025, 02, 01, 12, 0, 0);
        static Random sorteio = new Random(42);

        /// <summary>
        /// Static para geração do id do pedido seguinte.
        /// </summary>
        private static int s_ultimoPedido = 0;

        /// <summary>
        /// Identificador do pedido (gerado automaticamente) 
        /// </summary>
        private int _idPedido;

        /// <summary>
        /// Data do pedido, para uso futuro
        /// </summary>
        private DateOnly _data;

        /// <summary>
        /// Estado do pedido: aberto pode ser modificado, fechado não pode.
        /// </summary>
        private bool _aberto;

        /// <summary>
        /// Valor de um possível desconto para o pedido. Só pode ser aplicado o desconto uma vez.
        /// </summary>
        private double _desconto;

        /// <summary>
        /// Armazenando as comidas em uma lista encadeada (dinâmica). 
        /// </summary>
        protected LinkedList<IProduto> _itens;


        /// <summary>
        /// Inicializador privado. Gera o identificador, cria o vetor
        /// e inicializa demais atributos
        /// </summary>
        /// <param name="maxPizzas"></param>
        private void init(DateOnly? data) {
            s_ultimoPedido++;
            _idPedido = s_ultimoPedido;

            if (data != null)
                _data = (DateOnly)data;
            else {
                dataBase = dataBase.AddMinutes(sorteio.NextInt64(12, 50) + 1);
                if (dataBase.Hour < 12)
                    dataBase.AddHours(13 - dataBase.Hour);

                _data = DateOnly.FromDateTime(dataBase);
            }
            _itens = new LinkedList<IProduto>();
            _desconto = 0d;
            _aberto = true;
        }

        /// <summary>
        /// Cria um pedido vazio, com a data de hoje e identificador gerado
        /// automaticamente.
        /// </summary>
        protected Pedido(DateOnly? data) {
            init(data);
        }

        protected Pedido() {
            init(DateOnly.FromDateTime(DateTime.Now));
        }
        /// <summary>
        /// Verifica se ainda é possível adicionar pizzas ao pedido, por seu estado
        /// e quantidade de pizzas.
        /// </summary>
        /// <returns>TRUE/FALSE conforme seja permitido adicionar ou não</returns>
        protected virtual bool PodeAdicionar() {
            return _aberto;
        }

        /// <summary>
        /// Detalhamento do pedido (id, data, detalhes das pizzas)
        /// comum a todos os pedidos.
        /// </summary>
        /// <returns>String multilinha com as informações acima</returns>
        protected string DetalhesPedido() {
            StringBuilder relat = new StringBuilder($"nº{_idPedido} - {_data}\n");
            relat.AppendLine("==============================");
            foreach (IProduto item in _itens) {
                relat.AppendLine(item.ToString());
            }
            relat.AppendLine($"\nValor dos itens: {ValorItens():C2}");
            return relat.ToString();
        }

        /// <summary>
        /// Rodapé da nota de compra, com o desconto e o valor a pagar.
        /// </summary>
        /// <returns>String multilinha com as informações acima</returns>
        protected string RodapeNotinha() {
            StringBuilder relat = new StringBuilder();
            relat.AppendLine($"Desconto do cliente: {_desconto:C2}");
            relat.Append($"Valor a pagar: {PrecoAPagar():C2}");
            return relat.ToString();
        }

        /// <summary>
        /// Encapsula a lógica do cálculo do valor dos itens do pedidos (soma dos valores)
        /// </summary>
        /// <returns>Valor dos itens do pedido (soma dos valores dos itens)</returns>
        protected double ValorItens() {
            double preco = 0d;
            foreach (IProduto item in _itens) {
                preco += item.ValorFinal();
            }
            return preco;
        }

        /// <summary>
        /// Tenta adicionar uma pizza ao pedido. Se não for possível, ignora a operação.
        /// </summary>
        /// <param name="item">A pizza a ser incluída no pedido</param>
        /// <returns>Quantidade de pizzas no pedido após a execução.</returns>
        /// <exception cref="InvalidOperationException">InvalidOperationException em caso de 
        /// tentativa de alteração de pedido fechado</exception>
        public int Adicionar(IProduto item) {
            if (!PodeAdicionar()) {
                throw new InvalidOperationException("Pedido fechado: não pode ser alterado.");
            }
            if (item == null)
                throw new ArgumentNullException("Comida não foi criada corretamente");
            _itens.AddLast(item);
            return _itens.Count;
        }

        /// <summary>
        /// Fecha o pedido, caso ele tenha pelo menos 1 pizza. Caso contrário,
        /// ignora a operação.
        /// </summary>
        public void FecharPedido() {
            if (_itens.Count > 0)
                _aberto = false;
            else
                throw new InvalidOperationException("O pedido não pode ser fechado sem comidas");
        }

        /// <summary>
        /// Retorna o preço a pagar por um pedido (valor double positivo).
        /// </summary>
        /// <returns>Double com o valor a pagar pelo pedido.</returns>
        public virtual double PrecoAPagar() {
            return ValorItens() - _desconto;
        }

        public double AplicarDesconto(double valor) {
            if (valor < 0)
                throw new ArgumentOutOfRangeException("Valor não pode ser negativo");
            if (_desconto != 0)
                throw new InvalidOperationException("Desconto já foi aplicado neste pedido");
            _desconto = valor;
            return _desconto;
        }

        public DateOnly Data() {
            return _data;
        }

        public override bool Equals(object? obj) {
            Pedido outroPedido = (Pedido)obj;

            return this._idPedido == outroPedido._idPedido &&
                    this._data.Equals(outroPedido._data);
        }

        

        /// <summary>
        /// Retorna o id do pedido, para fins de comparação 
        /// (faremos melhor que isso em breve)
        /// </summary>
        /// <returns>Identificador do pedido (inteiro positivo)</returns>
        public override int GetHashCode() {
            return _idPedido;
        }

        public int CompareTo(Pedido? other) {
            int resposta = 0;
            double diferenca = this.PrecoAPagar() - other.PrecoAPagar();
            if (diferenca > 0 )
                resposta = 1;
            else if (diferenca < 0)
                resposta = -1;
            return resposta;

        }
    }
}
