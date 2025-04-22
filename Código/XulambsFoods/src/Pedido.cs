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
    public abstract class Pedido {
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
        /// Armazenando as comidas em uma lista encadeada (dinâmica). 
        /// </summary>
        protected LinkedList<Comida> _comidas;

        
        /// <summary>
        /// Inicializador privado. Gera o identificador, cria o vetor
        /// e inicializa demais atributos
        /// </summary>
        /// <param name="maxPizzas"></param>
        private void init() {
            s_ultimoPedido++;
            _idPedido = s_ultimoPedido;
            _data = DateOnly.FromDateTime(DateTime.Now);
            _comidas = new LinkedList<Comida>();
            _aberto = true;
        }

        

        /// <summary>
        /// Cria um pedido vazio, com a data de hoje e identificador gerado
        /// automaticamente.
        /// </summary>
        protected Pedido() {
            init();
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
            foreach(Comida comida in _comidas) {
                relat.AppendLine(comida.ToString());
            }
            return relat.ToString();
        }

        /// <summary>
        /// Encapsula a lógica do cálculo do valor dos itens do pedidos (soma dos valores)
        /// </summary>
        /// <returns>Valor dos itens do pedido (soma dos valores dos itens)</returns>
        protected double ValorItens() {
            double preco = 0d;
            foreach (Comida comida in _comidas) {
                preco += comida.ValorFinal();
            }
            return preco;
        }

        /// <summary>
        /// Tenta adicionar uma pizza ao pedido. Se não for possível, ignora a operação.
        /// </summary>
        /// <param name="comida">A pizza a ser incluída no pedido</param>
        /// <returns>Quantidade de pizzas no pedido após a execução.</returns>
        public int Adicionar(Comida comida) {
            if (PodeAdicionar()) {
                _comidas.AddLast(comida);
            }
            return _comidas.Count;
        }

        /// <summary>
        /// Fecha o pedido, caso ele tenha pelo menos 1 pizza. Caso contrário,
        /// ignora a operação.
        /// </summary>
        public void FecharPedido() {
            if(_comidas.Count > 0)
                _aberto = false;
        }

        /// <summary>
        /// Retorna o preço a pagar por um pedido (valor double positivo).
        /// </summary>
        /// <returns>Double com o valor a pagar pelo pedido.</returns>
        public abstract double PrecoAPagar();

       

        

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
