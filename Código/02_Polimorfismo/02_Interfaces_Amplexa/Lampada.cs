using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    public class Lampada : Dispositivo, IDesligavel{

        private bool _ligado;

        public Lampada(string nome) : base(nome) {
            Desligar();
        }

        public bool Ligar() {
            _ligado = true;
            SetEstado("Lâmpada acesa.");
            return _ligado;
        }

        public  bool Desligar() {
            _ligado = false;
            SetEstado("Lâmpada apagada.");
            return _ligado;
        }
    }
}
