using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace Comercio {
    internal class Produto {
        /// <summary>
        /// Descrição do produto a ser vendido
        /// </summary>
        private string _descricao;

        /// <summary>
        /// Valor de uma unidade do produto. O valor mínimo é 1.
        /// </summary>
        private double _valorUnitario;

        /// <summary>
        /// Variável de acesso para o valor unitário. Retorna o valor e faz a mudança do valor
        /// considerando a validação de preço mínimo.
        /// </summary>
        public double valorUnitario {
            get => _valorUnitario;
            set => validarValor(value);
        }

        /// <summary>
        /// Reajusta o preço de um produto. O valor deve ser acima do mínimo (R$1).
        /// Valores abaixo do mínimo serão ignorados.
        /// </summary>
        /// <param name="valor">Novo valor unitário do produto. Deve ser no mínimo R$1</param>
        public void reajustar(double valor) {
            validarValor(valor);
        }

        /// <summary>
        /// Método privado para encapsular a validação de valor unitário. Se o valor for 
        /// abaixo do mínimo, a ação é ignorada.
        /// </summary>
        /// <param name="valor">Novo valor unitário para o produto, com mínimo de R$1</param>
        private void validarValor(double valor) {
            _valorUnitario = 1.0;
            if (valor > 1)
                _valorUnitario = valor;
        }


        /// <summary>
        /// Registra dados de um produto, sendo estes a descrição (mínimo 2 caracteres) 
        /// e preço unitário (mínimo R$1).
        /// Em caso de erro, a descrição será "Sem descrição" e o preço fica como R$1.
        /// </summary>
        /// <param name="desc">A descrição do produto (mínimo 2 caracteres)</param>
        /// <param name="valor">O preço unitário (mínimo R$ 1)</param>
        public void registrar(string desc, double valor) {
            if (desc.Length >= 2) 
                _descricao = desc;
            else 
                _descricao = "Sem descrição";
            validarValor(valor);
        }

        /// <summary>
        /// Calcula o valor de venda de um lote do produto a partir de uma quantidade passada por parâmetro.
        /// Em caso de quantidade menor que 1, retorna sempre 0.
        /// </summary>
        /// <param name="quant">Quantidade de produtos do lote (mínimo: 1)</param>
        /// <returns>Double com o valor de venda do lote com a quantidade especificada ou 0 em caso de problemas</returns>
        public double valorLote(int quant) {
            double valor = 0.0;
            if(quant>0)
                valor = quant * _valorUnitario;
            return valor;
        }

        /// <summary>
        /// Retorna os dados do Produto em string (a ser melhorado com polimorfismo futuramente).
        /// Os dados incluem descrição e o valor unitário com duas casas decimais.
        /// </summary>
        /// <returns>String com descrição e o valor unitário com duas casas decimais.</returns>
        public override string ToString() {
            return $"{_descricao} - valor unitário: {_valorUnitario:C2}";
        }
    }
}
