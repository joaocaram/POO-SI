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
    public class Sanduiche : Comida {
        private const int MaxIngredientes = 5;
        private const string Descricao = "Sanduiche";
        private const double PrecoBase = 15d;
        private const double ValorAdicional = 3d;
        private const double ValorCombo = 5;
        private bool _comboFritas;

        private void init(int quantosAdicionais, bool combo) {
            AdicionarIngredientes(quantosAdicionais);
            _comboFritas = combo;
        }
        public Sanduiche(): base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            init(0, false);
        }

        public Sanduiche(int quantosAdicionais) : 
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            init(quantosAdicionais, false);
        }

        public Sanduiche(bool combo):
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            init(0, combo);
        }

        public Sanduiche(int quantosAdicionais, bool combo):
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            init(quantosAdicionais, combo);
        }

        public override double ValorFinal() {
            double valor = PrecoBase + ValorAdicionais();
            if (_comboFritas)
                 valor += ValorCombo;
            return valor;
        }

        public override string NotaDeCompra() {
            string notinha = base.NotaDeCompra();
            if(_comboFritas)
                notinha += $"\n\tCombo com fritas: {ValorCombo:C2}";
            notinha += $"\nValor a pagar: {ValorFinal():C2}";
            return notinha;
        }
    }
}
