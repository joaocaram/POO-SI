using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class Sanduiche : Comida {
        private const int MaxIngredientes = 5;
        private const string Descricao = "Sanduiche";
        private const double PrecoBase = 15d;
        private const double ValorAdicional = 3d;
        private const double ValorCombo = 5;
        private bool _comboFritas;

        private void Init(int adicionais, bool combo) {
            AdicionarIngredientes(adicionais);
            _comboFritas = combo;
        }

        public Sanduiche():
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            Init(0, false);
        }

        public Sanduiche(int quantosAdicionais):
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            Init(quantosAdicionais, false);
        }

        public Sanduiche(bool combo):
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            Init(0, combo);
        }

        public Sanduiche(int quantosAdicionais, bool combo):
            base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional) {
            Init(quantosAdicionais, combo);
        }

        public override double ValorFinal() {
            double preco = PrecoBase + ValorAdicionais();
            if (_comboFritas)
                preco += ValorCombo;
            return preco;
        }


        public override string ToString() {
            string relat = base.ToString();
            if (_comboFritas)
                relat += $", combo com fritas ({ValorCombo:C2})";
            relat += $", no valor total de {ValorFinal():C2}";
            return relat;
        }
    }
}
