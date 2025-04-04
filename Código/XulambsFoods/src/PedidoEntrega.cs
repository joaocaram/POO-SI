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
    /// Pedido para entrega: há limite de quantidade de pizzas e há uma taxa de
    /// entrega, que pode ser de valor 0.
    /// </summary>
	public class PedidoEntrega : Pedido {
        /// <summary>
        /// Constante: máximo de pizzas em um pedido para entrega
        /// </summary>
        private const int MaxEntrega = 8;
        
        /// <summary>
        /// Vetor 'constante' com os valores da taxa de entrega.
        /// Pergunta: isso pode ser melhor?
        /// </summary>
        private readonly double[] TaxasEntrega = [ 0, 5, 8 ];
        
        /// <summary>
        /// Vetor 'constante' com as distâncias de entrega correspondentes
        /// às taxas. Pergunta: isso pode ser melhor?
        /// </summary>
        private readonly double[] DistanciasEntrega = [4, 8, Double.MaxValue];
       
        /// <summary>
        /// Distância da entrega. Deve ser maior que 0.
        /// </summary>
        private double _distanciaEntrega;

        /// <summary>
        /// Construtor do pedido para entrega. Recebe uma distância, que
        /// deve ser um valor positivo.
        /// </summary>
        /// <param name="distancia">Distância da entrega (double > 0)</param>
        public PedidoEntrega(double distancia): base(MaxEntrega) {
            if (distancia < 0.1)
                distancia = 0.1;
            _distanciaEntrega = distancia;
        }

        /// <summary>
        /// Calcula o valor da taxa de acordo com a distância de entrega
        /// do pedido. O valor é um double não negativo.
        /// </summary>
        /// <returns>Valor da taxa de entrega (double não negativo)</returns>
        private double ValorTaxa() {
            int i = 0;
            while (_distanciaEntrega > DistanciasEntrega[i])
                i++;
            return TaxasEntrega[i];
        }

        /// <summary>
        /// Preço a pagar pelo pedido de entrega: valor de um pedido +
        /// o valor da taxa de entrega.
        /// </summary>
        /// <returns>Valor a ser pago pelo pedido. (double positivo)</returns>
        public override double PrecoAPagar() {
            return base.PrecoAPagar() + ValorTaxa();
        }

        /// <summary>
        /// Relatório do pedido, contendo identificador, data, distância,
        /// detalhamento das pizzas, valor da taxa de entrega e valor do pedido.
        /// </summary>
        /// <returns>String multilinhas com o detalhamento descrito do pedido.</returns>
        public override string ToString() {
            StringBuilder relat = new StringBuilder($"Pedido para Entrega ({_distanciaEntrega:F1}km) {DetalhamentoPedido()}");
            
            relat.AppendLine($"\nValor dos itens: {ValorItens():C2}");
            relat.AppendLine($"Taxa de entrega: {ValorTaxa():C2}");
            relat.Append($"Valor a pagar: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}


