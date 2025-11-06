using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    internal class Geladeira : Dispositivo, IRegulavel {
        private int _potencia;

        public Geladeira(string nome) : base(nome) {
            Regular(60);
            SetEstado($"Geladeira ligada com potência {_potencia}.");
        }

        public void Regular(int potencia) {
            if (potencia > 0 && potencia < 101)
                _potencia = potencia;
            SetEstado($"Geladeira ligada com potência {_potencia}.");
        }

    }
}
