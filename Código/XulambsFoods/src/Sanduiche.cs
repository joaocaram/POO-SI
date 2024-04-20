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

    ///
    /// Classe Sanduíche: especialização de Comida com herança
    /// "Classe de dados": não possui nenhuma regra própria no momento
    public class Sanduiche :Comida {
        #region constantes
        private const int MAX_ADICIONAIS = 5;
        private const double VALOR_ADICIONAL = 2.5d;
        private const double PRECO_BASE = 15d;
        #endregion

        #region construtores
        /// <summary>
        /// Inicializador privado: configura as variáveis com as constantes da classe e chama o método herdado para adicionar ingredientes.
        /// Para pensar: isso poderia ser feito usando o construtor da classe mãe?
        /// </summary>
        /// <param name="qtdExtras">Quantidade de extras do sanduíche (entre 0 e 5). Valores inválidos são ignorados e o sanduíche fica com 0 ingredientes</param>
        private void init(int qtdExtras) {
            _descricao = "Sanduíche";
            _precoBase = PRECO_BASE;
            _valorPorAdicional = VALOR_ADICIONAL;
            _maxAdicionais = MAX_ADICIONAIS;
            adicionarIngredientes(qtdExtras);
        }

        /// <summary>
        /// Cria um sanduíche sem ingredientes adicionais.
        /// </summary>
        public Sanduiche() {
            init(0);
        }

        /// <summary>
        /// Cria um sanduíche com ingredientes adicionais.
        /// </summary>
        /// <param name="qtdExtras">Quantidad de adicionais (entre 0 e 5). Valores inválidos serão ignorados e o sanduíche ficará com 0 ingredientes</param>
        public Sanduiche(int qtdExtras) {
            init(qtdExtras);
        }
        #endregion
    }
}
