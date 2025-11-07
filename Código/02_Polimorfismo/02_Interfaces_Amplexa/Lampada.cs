using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    public class Lampada : Dispositivo, IDesligavel{

        private bool _acesa;

        public Lampada(string nome) : base(nome) {
            Desligar();
        }

        public bool Ligar() {
            _acesa = true;
            SetEstado("Lâmpada acesa.");
            return _acesa;
        }

        public bool Desligar() {
            _acesa = false;
            SetEstado("Lâmpada apagada.");
            return _acesa;
        }
    }
}
