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

/*
 * Classe Produto simples para demonstração de atributos e métodos
 */

namespace Produtos {
    internal class Produto {
    
        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string descricao;
        /// <summary>
        /// Preço de venda de uma unidade
        /// </summary>
        public float precoUnitario;

        /// <summary>
        /// Informa o preço de venda de um lote do produto com 'quantidade' unidades. 
        /// Se a quantidade passada for negativa, retorna 0.
        /// </summary>
        /// <param name="quantidade">Tamanho do lote a ser vendido</param>
        /// <returns>Preço do lote com a 'quantidade' de produtos, ou 0 em caso de quantidade negativa (float)</returns>
        public float precoPorLote(int quantidade) {
            float valor = 0f;
            if(quantidade>0)
                valor = precoUnitario * quantidade;
            return valor;
        }

        /// <summary>
        /// Retorna uma string formatada com a descrição e o preço unitário do produto.
        /// Inclui o símbolo de R$ e preço formatado com 2 casas decimais.
        /// </summary>
        /// <returns>Descrição do produto e seu preço com duas casas decimais (string) </returns>
        public string descricaoProduto() {
            return descricao + " - Preço Unitário: R$ " + precoUnitario.ToString("0.00");
        }
    }
}
