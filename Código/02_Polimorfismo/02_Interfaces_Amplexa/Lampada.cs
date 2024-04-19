using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    public class Lampada : Dispositivo, IDesligavel{

        private bool _ligado;

        public Lampada(string nome) : base(nome) {
            Estado = "Lâmpada desligada.";
        }

        public  bool Ligar() {
            _ligado = true;
            Estado = "Lâmpada ligada.";
            return _ligado;
        }

        public bool Desligar() {
            _ligado = false;
            Estado = "Lâmpada desligada.";
            return _ligado;
        }
    }
}
