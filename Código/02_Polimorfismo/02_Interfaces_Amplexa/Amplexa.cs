using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    internal class Amplexa {
        private Dictionary<int, Dispositivo> _dispositivos;
        private string _nome;

        public Amplexa(string nome) {
            _nome = nome;
            _dispositivos = new Dictionary<int, Dispositivo>();
        }

        public bool Ligar(string nome) {
            IDesligavel? disp = Localizar(nome) as IDesligavel;

            bool resposta = false;

            if (disp != null) {
                resposta = disp.Ligar();
            }

            return resposta;
        }

        public bool Desligar(string nome) {
            IDesligavel? disp = Localizar(nome) as IDesligavel;

            bool resposta = false;

            if (disp != null) {
                resposta = disp.Desligar();
            }

            return resposta;
        }

        public void Regular(string nome, int potencia) {
            Dispositivo? d = Localizar(nome);
            IRegulavel? disp = d as IRegulavel;

            if (disp != null) {
                disp.Regular(potencia);
            }
        }

        public void AddDispositivo(Dispositivo novo) {
            _dispositivos.Add(novo.GetHashCode(), novo);
        }

        private Dispositivo? Localizar(string nome) {
            Dispositivo? disp;
            _dispositivos.TryGetValue(nome.GetHashCode(), out disp);
            return disp;
        }
        
        public override string ToString() {
            StringBuilder disp = new StringBuilder(_nome + " controlando:\n");
            foreach (Dispositivo item in _dispositivos.Values)
            {
                disp.AppendLine(item.ToString());
            }
            return disp.ToString();
        }
    }
}
