using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    public class Lampada : Dispositivo{

        private bool _ligado;

        public Lampada(string nome) : base(nome) {
            SetEstado("Lâmpada desligada.");
        }

        public override bool Ligar() {
            _ligado = true;
            SetEstado("Lâmpada ligada.");
            return _ligado;
        }

        public override bool Desligar() {
            _ligado = false;
            SetEstado("Lâmpada desligada.");
            return _ligado;
        }
    }
}
