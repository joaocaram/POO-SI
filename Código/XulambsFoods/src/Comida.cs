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
    /// Classe Comida: POO básica
    /// Usando: encapsulamento, construtores, static
    ///
    public class Comida {

        //#region constantes e controle
        private static int MAX_ADICIONAIS_PIZZA = 8;
        private static int MAX_ADICIONAIS_SANDUICHE = 5;
        private static double VALOR_ADICIONAL_PIZZA = 4d;
        private static double VALOR_ADICIONAL_SANDUICHE = 2d;
        private static double PRECO_BASE_PIZZA = 29d;
        private static double PRECO_BASE_SANDUICHE = 15d;
        //#endregion

        #region atributos
        private string _descricao;
        private double _precoBase;
        private double _valorPorAdicional;
        private int _maxAdicionais;
        private int _qtdAdicionais;
        #endregion

        #region construtores
        /// <summary>
        /// Cria uma comida Pizza ou Sanduíche. Se for passado uma descricao inexistente,
        /// cria uma pizza por padrão.
        /// </summary>
        /// <param name="descricao">"pizza" ou "sanduiche", sem importar maiúsculas ou minúsculas. 
        /// Outros valores irão gerar uma pizza.</param>
        public Comida(string descricao) {
            init(descricao, 0);
        }

        /// <summary>
        /// Cria uma comida Pizza ou Sanduíche com uma quantidade de ingredientes adicionais já definida. Se for passado uma descricao inexistente, cria uma pizza por padrão. Quantidades inválidas resultam em 0 adicionais.
        /// </summary>
        /// <param name="descricao">"pizza" ou "sanduiche", sem importar maiúsculas ou minúsculas. 
        /// Outros valores irão gerar uma pizza.</param>
        /// <param name="qtdExtras">Quantidade de adicionais (valor não negativo)</param>
        public Comida(string descricao, int qtdExtras) {
            init(descricao, qtdExtras);
        }

        /// <summary>
        /// Construtor 'padrão': cria uma pizza com 0 adicionais.
        /// </summary>
        public Comida() {
            init("pizza", 0);
        }

        private void init(string descricao, int qtdExtras) {
            string tipoComida = descricao.ToLower();
            switch (tipoComida) {
                case "sanduiche":
                    _descricao = "Sanduíche";
                    _precoBase = PRECO_BASE_SANDUICHE;
                    _valorPorAdicional = VALOR_ADICIONAL_SANDUICHE;
                    _maxAdicionais = MAX_ADICIONAIS_SANDUICHE;
                    break;
                case "pizza":
                default:
                    _descricao = "Pizza";
                    _precoBase = PRECO_BASE_PIZZA;
                    _valorPorAdicional = VALOR_ADICIONAL_PIZZA;
                    _maxAdicionais = MAX_ADICIONAIS_PIZZA;
                    break;
            }
            adicionarIngredientes(qtdExtras);

        }
        #endregion

        #region métodos de negócio
        /// <summary>
        /// Calcura o valor dos ingredientes adicionais.
        /// </summary>
        /// <returns>Double com o valor dos adicionais da comida</returns>
        private double valorAdicionais() {
            return _qtdAdicionais * _valorPorAdicional;
        }

        /// <summary>
        /// Verifica se pode adicionar esta nova quantidade de ingredientes.
        /// </summary>
        /// <param name="quantos">Quantos ingredientes a serem adicionados (>0)</param>
        /// <returns>TRUE se é possível adicionar, FALSE caso contrário</returns>
        private bool podeAdicionarIngredientes(int quantos) {
            return (quantos>0 && _maxAdicionais >= (_qtdAdicionais + quantos));
        }

        /// <summary>
        /// Calcula o preço final da comida, considerando os seus adicionais.
        /// </summary>
        /// <returns>Preço final da comida (double não negativo) </returns>
        public double precoFinal() {
            return _precoBase + valorAdicionais();
        }
                
        /// <summary>
        /// Adiciona ingredientes à comida. O método faz a verificação se é possível adicionar esta quantidade
        /// de ingredientes e, em caso de verificação falha, ignora a operação. A quantidade de ingredientes da comida
        /// após a operação é retornada.
        /// </summary>
        /// <param name="quantos">Quantidade de ingredientes a serem adicionados (>0)</param>
        /// <returns>Quantidade de ingredientes da comida depois da execução da operação</returns>
        public int adicionarIngredientes(int quantos) {
            if (podeAdicionarIngredientes(quantos))
                _qtdAdicionais += quantos;
            return _qtdAdicionais;
        }

        /// <summary>
        /// Relatório descritivo da comida (futuro ToString). 
        /// Descreve seu tipo, seu preço base, quantidade e preço dos adicionais e preço final.
        /// </summary>
        /// <returns>String/relatório com tipo, preço base, quantidade e preço dos adicionais e preço final formatado em duas casas decimais..</returns>
        public string relatorio() {
            return _descricao + " (preço base: R$ " + _precoBase + ") com " + _qtdAdicionais
                                + " ingredientes (R$ " + valorAdicionais() + "): \n TOTAL: R$ "
                                + precoFinal().ToString("0.00");
        }
        #endregion

    }
}
