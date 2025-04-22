
namespace XulambsFoods_2025_1.src {
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

    /// <summary>
    /// Classe Pizza para a Xulambs Pizza. Uma pizza tem um preço base e pode ter até 8 ingredientes adicionais. Cada ingrediente tem custo fixo.
    /// A pizza deve emitir uma nota de compra com os seus detalhes.
    /// </summary>


    public class Pizza : Comida {

        private const int MaxIngredientes = 8;
        private const string Descricao = "Pizza";
        private const double PrecoBase = 29d;
        private const double ValorAdicional = 5d;
        private const int AdicionaisSemDesconto = 5;
        private const double PctDesconto = 0.5;



        /// <summary>
        /// Inicializador privado da pizza: valida a quantidade de adicionais. Em caso de não validação, a pizza será criada sem adicionais.
        /// </summary>
        /// <param name="quantosAdicionais">Quantos adicionais para iniciar a pizza. Em caso de não validação, a pizza será criada sem adicionais.</param>
        private void Init(int quantosAdicionais) {
            AdicionarIngredientes(quantosAdicionais);
        }

        /// <summary>
        /// Construtor padrão.Cria uma pizza sem adicionais.
        /// </summary>
        public Pizza() :
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            Init(0);
        }

        /// <summary>
        /// Cria uma pizza com a quantidade de adicionais pré-definida.Em caso de valor inválido, a pizza será criada sem adicionais.
        /// </summary>
        /// <param name="quantosAdicionais">Quantidade de adicionais (entre 0 e 8, limites inclusivos)</param>
        public Pizza(int quantosAdicionais)
            : base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            Init(quantosAdicionais);
        }



        /// <summary>
        /// Retorna o valor final da pizza, incluindo seus adicionais.
        /// </summary>
        /// <returns>Double com o valor final da pizza.</returns>
        public override double ValorFinal() {
            return PrecoBase + ValorAdicionais();
        }

        private double DescontoAdicionais() {
            double desconto = 0d;
            int quantos = _quantidadeIngredientes - AdicionaisSemDesconto;
            if (quantos > 0)
                desconto = quantos * ValorAdicional * PctDesconto;
            return desconto;
        }
        protected override double ValorAdicionais() {
            return base.ValorAdicionais() - DescontoAdicionais();
        }

        /// <summary>
        /// Nota simplificada de compra: descrição da pizza, dos ingredientes e do preço.
        /// </summary>
        /// <returns>String no formato "<DESCRICAO> <PRECO> com <QUANTIDADE> ingredientes <PRECO></PRECO>, no valor total de <VALOR>"</returns>
        public override string ToString() {
            return $"{base.ToString()} , no valor total de {ValorFinal():C2}";
        }

        public override bool Equals(object? obj) {
            Pizza outraPizza = (Pizza)obj;
            return this._quantidadeIngredientes == outraPizza._quantidadeIngredientes;
        }
    }

}
