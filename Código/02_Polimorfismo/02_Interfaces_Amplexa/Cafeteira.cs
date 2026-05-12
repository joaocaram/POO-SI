using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    internal class Cafeteira : Dispositivo, IDesligavel, IRegulavel {
        private bool _ligado;
        private int _potencia;
        
        public Cafeteira(string nome) : base(nome) {
            Regular(0);
            SetEstado("Cafeteira desligada");
        }

        public bool Ligar() {
            _ligado = true;
            Regular(50);
            return _ligado;
        }

        public bool Desligar() {
            _ligado = false;
            SetEstado("Cafeteira desligada.");
            return _ligado;
        }

        public void Regular(int potencia) {
            if (_ligado) {
                if (potencia >= 0 && potencia < 101)
                    _potencia = potencia;
            }
            SetEstado($"Cafeteira ligada com potência {_potencia}.");
        }
    }
}
