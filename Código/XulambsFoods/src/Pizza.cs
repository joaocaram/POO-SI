using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    /// Classe Pizza: especialização de Comida pela herança.
    /// No momento, classe de dados: sua única responsabilidade é o construtor
    public class Pizza : Comida {

        #region constantes
        private const int MAX_ADICIONAIS = 8;
        private const double VALOR_ADICIONAL = 4d;
        private const double PRECO_BASE = 29d;
        
        private const int MIN_ADICIONAIS_DESC = 5;
        private const double DESC_ADICIONAIS = 0.5;
        private const double VALOR_BORDA = 5.5d;
        #endregion

        #region atributos
        private bool _temBordaRecheada;
        #endregion

        #region construtores

        /// <summary>
        /// Inicializador privado: configura as variáveis com as constantes da classe e chama o método herdado para adicionar ingredientes.
        /// Para pensar: isso poderia ser feito usando o construtor da classe mãe?
        /// </summary>
        /// <param name="qtdExtras">Quantidade de extras da pizza (entre 0 e 8). Valores inválidos são ignorados e a pizza fica com 0 ingredientes</param>
        /// <param name="bordaRecheada">Define se a pizza terá ou não sua borda recheada</param>
        private void init(int qtdExtras, bool bordaRecheada) {
            _descricao = "Pizza";
            _precoBase = PRECO_BASE;
            _temBordaRecheada = bordaRecheada;
            _valorPorAdicional = VALOR_ADICIONAL;
            _maxAdicionais = MAX_ADICIONAIS;
            if (_temBordaRecheada)
            {
                _precoBase += VALOR_BORDA;
                _descricao += " com borda recheada";
            }
            adicionarIngredientes(qtdExtras);
        }

        /// <summary>
        /// Cria uma pizza sem ingredientes adicionais nem borda recheada.
        /// </summary>
        public Pizza() {
            init(0, false);
        }

        /// <summary>
        /// Cria uma pizza com ingredientes adicionais e sem borda recheada.
        /// </summary>
        /// <param name="qtdExtras">Quantidade de extras da pizza (entre 0 e 8). Valores inválidos são ignorados e a pizza fica com 0 ingredientes</param>
        public Pizza(int qtdExtras) {
            init(qtdExtras, false);
        }

        /// <summary>
        /// Cria uma pizza sem ingredientes adicionais e escolhendo ter ou não borda recheada.
        /// </summary>
        /// <param name="bordaRecheada">'true' para borda recheada, 'false' caso contrário</param>
        public Pizza(bool bordaRecheada)
        {
            init(0, bordaRecheada);
        }
        #endregion

        #region métodos de negócio
        /// <summary>
        /// Calcula o valor dos adicionais considerando a regra da pizza: a partir do 6º adicional, cada
        /// um recebe 50% de desconto. Sobrescreve o método da classe mãe.
        /// </summary>
        /// <returns>Valor dos adicionais já considerando o desconto (double não negativo)</returns>
        protected override double valorAdicionais()
        {   
            return base.valorAdicionais() - descontoAdicionais();
        }

        /// <summary>
        /// Calcula o valor do possível desconto aplicado ao valor dos adicionais. Haverá desconto se a quantidade de adicionais passar o mínimo definido pela constante MIN_ADICIONAIS_DESC.
        /// </summary>
        /// <returns>Valor do desconto para os adicionais (double não negativo)</returns>
        private double descontoAdicionais()
        {
            double desc = 0d;
            int ingredComDesconto = _qtdAdicionais - MIN_ADICIONAIS_DESC;

            if(ingredComDesconto > 0)
            {
                desc = ingredComDesconto * VALOR_ADICIONAL * DESC_ADICIONAIS;
            }
            return desc;

        }
        #endregion
    }
}
