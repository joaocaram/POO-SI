using System;
using System.Text;

/** 
    * MIT License
    *
    * Copyright(c) 2024 João Caram <caram@pucminas.br>
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

namespace XulambsFoods_2024_2.src {
    public abstract class Comida {
        private int _maxIngredientes;
        private string _descricao;
        private double _precoBase;
        private double _valorAdicional;
        protected int _quantidadeIngredientes;

        protected Comida(string desc, int maxAdicionais, double valorBase, double valorAdicional) {
            _descricao = desc;
            _maxIngredientes = maxAdicionais;
            _precoBase = valorBase;
            _valorAdicional = valorAdicional;
            _quantidadeIngredientes = 0;
        }

        /// <summary>
        /// Calcula o valor dos adicionais para o preço final da pizza. Atualmente o valor dos adicionais é a multiplicação da quantidade de adicionais por seu valor unitário
        /// </summary>
        /// <returns>Double com o valor a ser cobrado pelos adicionais.</returns>
        protected double ValorAdicionais() {
            return _quantidadeIngredientes * _valorAdicional;
        }

        /// <summary>
        ///Faz a verificação de limites para adicionar ingredientes na pizza.Retorna TRUE/FALSE conforme seja possível ou não adicionar 
        ///esta quantidade de ingredientes.
        /// </summary>
        /// <param name="quantos">Quantidade de ingredientes a adicionar.</param>
        /// <returns>TRUE/FALSE conforme seja possível ou não adicionar esta quantidade de ingredientes.</returns>
        protected bool PodeAdicionar(int quantos) {
            return (quantos > 0 && quantos + _quantidadeIngredientes <= _maxIngredientes);
        }

        public abstract double ValorFinal();

        /// <summary>
        /// Tenta adicionar ingredientes na pizza.Caso a adição seja inválida(ultrapassando limites ou com valores negativos), mantém
        /// a quantidade atual de ingredientes.Retorna a quantidade de ingredientes após a execução do método.
        /// </summary>
        /// <param name="quantos">Quantos ingredientes a serem adicionados (>0)</param>
        /// <returns>Quantos ingredientes a pizza tem após a execução</returns>
        public int AdicionarIngredientes(int quantos) {
            if (PodeAdicionar(quantos)) {
                _quantidadeIngredientes += quantos;
            }
            return _quantidadeIngredientes;
        }

        public virtual string NotaDeCompra() {
            return $"{_descricao} ({_precoBase:C2}) com {_quantidadeIngredientes} ingredientes ({ValorAdicionais():C2}).";
        }
    }
}
