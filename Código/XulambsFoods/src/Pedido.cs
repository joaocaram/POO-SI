using System.Text;

namespace XulambsFoods.src {
    /** 
    * MIT License
    *
    * Copyright(c) 2022-4 João Caram <caram@pucminas.br>
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



    ///
    /// Classe Pedido: relacionamento entre classes
    /// Um pedido contém até 10 comidas (composição)
    ///
    public class Pedido {

        #region const e static
        private const int MAX_COMIDAS = 10;       //por enquanto, pedido limitado
        private static DateOnly hoje = DateOnly.FromDateTime(DateTime.Now); 
        private static int ultimoPedido = 0;     //para id automático
        #endregion                                         

        #region atributos
        private int idPedido;
        private DateOnly dataPedido;
        private Comida[] itens;
        private bool aberto;
        private int quantComidas;
        #endregion

        #region construtores
        /// <summary>
        /// Cria um pedido sem nenhuma comida, com a data de hoje.
        /// </summary>
        public Pedido() {
            init(null);
        }

        /// <summary>
        /// Cria um pedido já com a sua primeira comida e a data de hoje. Se a comida for nula, criará um pedido vazio.
        /// </summary>
        /// <param name="primeira">A comida a ser inserida no pedido. Não deve ser nula.</param>
        public Pedido(Comida primeira) {
            init(primeira);
        }

        /// <summary>
        /// Inicializador privado. Cria o pedido com seu id automático, a data de hoje e, caso o parâmetro 'primeira'
        /// não seja nulo, inclui uma comida no pedido.
        /// </summary>
        /// <param name="primeira">A comida a ser incluída no pedido, ou null para pedido vazio.</param>
        private void init(Comida primeira) {
            this.idPedido = ++ultimoPedido;
            this.dataPedido = hoje;
            this.itens = new Comida[MAX_COMIDAS];
            this.quantComidas = 0;
            this.aberto = true;
            if (primeira != null)
                addComida(primeira);
        }
        #endregion

        #region métodos de negócio

        /// <summary>
        /// Tenta adicionar uma comida ao pedido. A operação será concretizada se o pedido estiver aberto,
        /// não houver atingido o máximo de comidas e o parâmetro não seja nulo. Retorna TRUE ou FALSE conforme
        /// foi possível executar ou não a operação.
        /// </summary>
        /// <param name="nova">A comida a ser inserida no pedido. Não deve ser nula.</param>
        /// <returns>TRUE se a comida foi adicionada, FALSE caso contrário.</returns>
        public bool addComida(Comida nova) {
            bool resposta = false;
            if (nova!=null && podeAdicionarComida()) {
                itens[quantComidas] = nova;
                quantComidas++;
                resposta = true;
            }
            return resposta;
        }

        /// <summary>
        /// Verifica se pode adicionar uma nova comida no pedido (o pedido deve estar aberto e não ter atingido a
        /// quantidade máxima de comidas)
        /// </summary>
        /// <returns>TRUE se é possível adicionar, FALSE caso contrário.</returns>
        private bool podeAdicionarComida() {
            return (aberto && quantComidas < MAX_COMIDAS);
        }

        /// <summary>
        /// Produz o relatório descritivo do pedido com seu id, sua data, detalhamento das comidas e preço final.
        /// </summary>
        /// <returns>String de múltiplas linhas com o relatório contendo id, data, detalhamento do pedido e preço final.</returns>
        public override string ToString() {
            StringBuilder relat = new StringBuilder();
            
            relat.AppendLine("=====================");
            relat.AppendLine("Pedido nº " + this.idPedido + " - " + this.dataPedido.ToShortDateString());
            int cont = 1;
            for (int i = 0; i < quantComidas; i++) {
                relat.Append(cont.ToString("00") + " - ");
                relat.AppendLine(itens[i].ToString());
                cont++;
            }
            relat.AppendLine("\nTOTAL DO PEDIDO: R$ " + this.precoFinal().ToString("0.00"));
            relat.AppendLine("=====================\n");
            return relat.ToString();
        }

        /// <summary>
        /// Calcula o preço final do pedido (soma dos preços das comidas)
        /// </summary>
        /// <returns>Double com preço final do pedido.</returns>
        public double precoFinal() {
            double valor = 0d;

            for (int i = 0; i < quantComidas; i++) {
                valor += itens[i].precoFinal();
            }

            return valor;
        }

        /// <summary>
        /// Fecha um pedido. Um pedido com 0 itens não poderá ser fechado. Retornará TRUE se o pedido foi fechado ou 
        /// FALSE caso contrário
        /// <returns>TRUE se o pedido está fechado e FALSE se ele está aberto</returns>
        /// </summary>
        public bool fecharPedido() {
            if(quantComidas>0) 
                aberto = false;
            return !aberto;

        }
        #endregion
    }
}
