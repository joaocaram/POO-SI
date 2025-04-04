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
    public class Pedido {
        /// <summary>
        /// Static para geração do id do pedido seguinte.
        /// </summary>
        private static int s_ultimoPedido = 0;
        /// <summary>
        /// Apenas para controle do vetor de pizzas.
        /// </summary>
        private const int MaxPizzas = 100;

        /// <summary>
        /// Máximo de pizzas permitidas(pelo controle de tamanho)
        /// </summary>
        private int _maxPizzas;
        
        /// <summary>
        /// Identificador do pedido (gerado automaticamente) 
        /// </summary>
        private int _idPedido;

        /// <summary>
        /// Data do pedido, para uso futuro
        /// </summary>
        private DateOnly _data;

        /// <summary>
        /// Armazenando as pizzas em um vetor. Pode ser melhorado futuramente.
        /// </summary>
        private Pizza[] _pizzas;

        /// <summary>
        /// Quantidade de pizzas no pedido atualmente.
        /// </summary>
        private int _quantPizzas;

        /// <summary>
        /// Estado do pedido: aberto pode ser modificado, fechado não pode.
        /// </summary>
        private bool _aberto;

        /// <summary>
        /// Inicializador privado. Gera o identificador, cria o vetor
        /// e inicializa demais atributos
        /// </summary>
        /// <param name="maxPizzas"></param>
        private void init(int maxPizzas) {
            s_ultimoPedido++;
            _idPedido = s_ultimoPedido;
            _maxPizzas = maxPizzas;
            _data = DateOnly.FromDateTime(DateTime.Now);
            if (_maxPizzas < 1)
                _maxPizzas = 1;
            _pizzas = new Pizza[_maxPizzas];
            _quantPizzas = 0;
            _aberto = true;
        }

        /// <summary>
        /// Construtor para uso de classes derivadas, permitindo
        /// restringir a quantidade de pizzas armazenadas.
        /// </summary>
        /// <param name="maxPizzas">Máximo de pizzas no pedido. Deve ser >= 1.</param>
        protected Pedido(int maxPizzas) {
            init(maxPizzas);
        }

        /// <summary>
        /// Cria um pedido vazio, com a data de hoje e identificador gerado
        /// automaticamente.
        /// </summary>
        public Pedido() {
            init(MaxPizzas);
        }
               
        /// <summary>
        /// Verifica se ainda é possível adicionar pizzas ao pedido, por seu estado
        /// e quantidade de pizzas.
        /// </summary>
        /// <returns>TRUE/FALSE conforme seja permitido adicionar ou não</returns>
        private bool PodeAdicionar() {
            return _aberto && _quantPizzas < _maxPizzas;
        }

        /// <summary>
        /// Encapsula a lógica do cálculo do valor dos itens do pedidos (soma dos valores)
        /// </summary>
        /// <returns>Valor dos itens do pedido (soma dos valores dos itens)</returns>
        protected double ValorItens() {
            double preco = 0d;
            for (int i = 0; i < _quantPizzas; i++) {
                preco += _pizzas[i].ValorFinal();
            }
            return preco;
        }

        /// <summary>
        /// Tenta adicionar uma pizza ao pedido. Se não for possível, ignora a operação.
        /// </summary>
        /// <param name="pizza">A pizza a ser incluída no pedido</param>
        /// <returns>Quantidade de pizzas no pedido após a execução.</returns>
        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar()) {
                _pizzas[_quantPizzas] = pizza;
                _quantPizzas++;
            }
            return _quantPizzas;
        }

        /// <summary>
        /// Fecha o pedido, caso ele tenha pelo menos 1 pizza. Caso contrário,
        /// ignora a operação.
        /// </summary>
        public void FecharPedido() {
            if(_quantPizzas > 0)
                _aberto = false;
        }

        /// <summary>
        /// Retorna o preço a pagar por um pedido (valor double positivo).
        /// </summary>
        /// <returns>Double com o valor a pagar pelo pedido.</returns>
        public virtual double PrecoAPagar() {
            return ValorItens();
        }

        /// <summary>
        /// Detalhamento do pedido (id, data, detalhes das pizzas)
        /// comum a todos os pedidos.
        /// </summary>
        /// <returns>String multilinha com as informações acima</returns>
        protected string DetalhamentoPedido(){
            StringBuilder relat = new StringBuilder($"nº{ _idPedido} - { _data}\n");
            relat.AppendLine("==============================");
            for (int i = 0; i < _quantPizzas; i++)
            {
                relat.AppendLine(_pizzas[i].ToString());
            }
            return relat.ToString();
        }

        /// <summary>
        /// Relatório do pedido, contendo seu identificador, data,
        /// detalhamento das pizzas e preço a pagar.
        /// </summary>
        /// <returns>Uma string, multilinhas, com a informação descrita</returns>
        public override string ToString() {
            StringBuilder relat = new StringBuilder($"Pedido Local {DetalhamentoPedido()}");
            relat.Append($"Valor a pagar: {PrecoAPagar():C2}");
            return relat.ToString();
        }

        public override bool Equals(object? obj) {
            Pedido outroPedido = (Pedido)obj;

            return  this._idPedido == outroPedido._idPedido &&
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
    }
}
