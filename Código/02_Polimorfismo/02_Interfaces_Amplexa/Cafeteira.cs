using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    internal class Cafeteira : Dispositivo, IDesligavel {

        private bool _ligado;

        public Cafeteira(string nome) : base(nome) {
            Desligar();
        }

        public bool Ligar() {
            _ligado = true;
            SetEstado("Cafeteira ligada");
            return _ligado;
        }

        public bool Desligar() {
            _ligado = false;
            SetEstado("Cafeteira desligada");
            return _ligado;
        }
    }
}