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
    internal interface IFidelidade {
        /// <summary>
        /// Retorna o valor do desconto obtido pelo cliente neste pedido (0 ou mais) de acordo com sua fidelidade.
        /// </summary>
        /// <param name="pedido">Pedido a ser analisado (não deve ser nulo)</param>
        /// <returns>Valor do desconto obtido pelo cliente (0 ou mais) </returns>
        public double desconto(Pedido pedido);
        
        /// <summary>
        /// Deve verificar as condições de fidelidade e retornar a nova categoria do cliente.
        /// </summary>
        /// <returns>A nova categoria de fidelidade do cliente de acordo com seus pedidos.</returns>
        public IFidelidade atualizarCategoria();

    }
}
