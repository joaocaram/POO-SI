using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


namespace XulambsFoods.src {
    /// <summary>
    /// Classe Genérica para uma base de dados com capacidade de produzir relatórios (listagens) ordenadas
    /// </summary>
    /// <typeparam name="K">Tipo da chave identificadora dos dados</typeparam>
    /// <typeparam name="T">Tipo de dados a ser armazenado</typeparam>
    internal class BaseDados<K, T> {
        
        #region atributos
        private Dictionary<K, T> dados;
        private List<T> dadosOrdenados;     //para listagens
        #endregion

        #region construtor
        /// <summary>
        /// Cria uma base de dados com a capacidade pedida e o comparador padrão do tipo T.
        /// O tamanho mínimo da base é 16.
        /// </summary>
        /// <param name="capacidade">Capacidade mínima da base de dados</param>
        public BaseDados(int capacidade) {
            if (capacidade < 16)
                capacidade = 16;
            dados = new Dictionary<K, T>(capacidade);
            Comparison<T> comp = Comparer<T>.Default.Compare;
            ordenar(comp);      //ordenação padrão
        }
        #endregion

        #region métodos CRUD
        /// <summary>
        /// Localiza um dado por sua chave
        /// </summary>
        /// <param name="identificador">Identificador do dado a ser buscado</param>
        /// <returns>O dado com aquele identificador, ou nulo se não existir</returns>
        public T localizar(K identificador) {
            T quem = default(T);
            dados.TryGetValue(identificador, out quem);
            return quem;
        }
        /// <summary>
        /// Adiciona um dado do tipo T com a chave K
        /// </summary>
        /// <param name="chave">A chave identificadora do dado</param>
        /// <param name="novo">O dado a ser inserido</param>
        public void adicionar(K chave, T novo) {
            dados.Add(chave, novo);
        }
        #endregion

        #region listagem
        /// <summary>
        /// Gera a lista ordenada dos dados de acordo com a comparação especificada.
        /// </summary>
        /// <param name="comparacao">Comparação válida para os dados do tipo T</param>
        public void ordenar(Comparison<T> comparacao) {
            dadosOrdenados = dados.Values.ToList();
            dadosOrdenados.Sort(comparacao);
        }

        /// <summary>
        /// Retorna um relatório ordenado dos dados armazenados. Usará sempre a lista ordenada da última execução do método "ordenar". 
        /// </summary>
        /// <returns>String contendo um relatório dos dados segundo a última ordenação executada</returns>
        public string relatorioResumido() {
            StringBuilder relatorio = new StringBuilder("Relatório resumido: \n");
            foreach (T dado in dadosOrdenados) {
                relatorio.AppendLine($"{dado}\n");
            }
            return relatorio.ToString();
        }
        #endregion

        #region ações genéricas
        /// <summary>
        /// Retorna um valor double totalizando/sumarizando os objetos armazenados. O valor para totalização deve ser obtido na função passada pelo parâmetro.
        /// </summary>
        /// <param name="metodo">Função delegada que extrai o dado a ser totalizado</param>
        /// <returns>Double com a totalização de todos os objetos armazenados.</returns>
        public double totalizar(Func<T, double> metodo) {
            double total = 0;
            foreach (T dado in dados.Values)
            {
                total += metodo.Invoke(dado);
            }
            return total;
        }

        /// <summary>
        /// Realiza uma ação genérica, passada por parâmetro, para cada objeto armazenado na base.
        /// </summary>
        /// <param name="metodo">Ação a ser executada em todos os objetos.</param>
        public void processar(Action<T> metodo) {
            foreach (T dado in dados.Values) {
                metodo.Invoke(dado);
            }
        }
        #endregion
    }
}
