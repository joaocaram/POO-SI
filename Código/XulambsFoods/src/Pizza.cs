using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XulambsFoods_2024_2.src {
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

    /// <summary>
    /// Classe Pizza para a Xulambs Pizza. Uma pizza tem um preço base e pode ter até 8 ingredientes adicionais. Cada ingrediente tem custo fixo.
    /// A pizza deve emitir uma nota de compra com os seus detalhes.
    /// </summary>
    public class Pizza : Comida{

        private const int MaxIngredientes = 8;
        private const string Descricao = "Pizza";
	    private const double PrecoBase = 29d;
        private const double ValorAdicional = 5d;
        private const int AdicionaisParaDesconto = 5;
        private const double PctDesconto = 0.5;

        
       /// <summary>
       /// Construtor padrão.Cria uma pizza sem adicionais.
       /// </summary>
        public Pizza() : base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) 
        {              
        }

        /// <summary>
        /// Cria uma pizza com a quantidade de adicionais pré-definida.Em caso de valor inválido, a pizza será criada sem adicionais.
        /// </summary>
        /// <param name="quantosAdicionais">Quantidade de adicionais (entre 0 e 8, limites inclusivos)</param>
        public Pizza(int quantosAdicionais) :
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) 
        {
            AdicionarIngredientes(quantosAdicionais);
        }

        private double DescontoAdicionais() {
            double desconto = 0d;
            int quantosTemDesconto = _quantidadeIngredientes - AdicionaisParaDesconto;
            if(quantosTemDesconto > 0) {
                desconto = quantosTemDesconto * ValorAdicional * PctDesconto;
            }
            return desconto;
        }
        
        /// <summary>
        /// Retorna o valor final da pizza, incluindo seus adicionais e descontos.
        /// </summary>
        /// <returns>Double com o valor final da pizza.</returns>
        public override double ValorFinal() {
            return PrecoBase + ValorAdicionais() - DescontoAdicionais();
        }

       
        /// <summary>
        /// Nota simplificada de compra: descrição da pizza, dos ingredientes e do preço.
        /// </summary>
        /// <returns>String no formato "<DESCRICAO> <PRECO> com <QUANTIDADE> ingredientes <PRECO></PRECO>, no valor total de <VALOR>"</returns>
        public override string NotaDeCompra() {
            string notinha = base.NotaDeCompra();
            notinha += $"\n\tDesconto: {DescontoAdicionais():C2}\n";
            notinha += $"VALOR A PAGAR: {ValorFinal():C2}";
            return notinha;
        }

    }

}
