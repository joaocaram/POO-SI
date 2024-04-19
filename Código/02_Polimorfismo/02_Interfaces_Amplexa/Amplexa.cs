using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    internal class Amplexa {
        private Dictionary<string, Dispositivo> _dispositivos;
        private string _nome;

        public Amplexa(string nome) {
            this._nome = nome;
            this._dispositivos = new Dictionary<string, Dispositivo>();
        }

        public bool Ligar(string nome) {
            IDesligavel? disp = (IDesligavel?)Localizar(nome);

            bool resposta = false;

            if (disp != null) {
                resposta = disp.Ligar();
            }

            return resposta;
        }

        public bool Desligar(string nome) {
            IDesligavel? disp = (IDesligavel?)Localizar(nome);

            bool resposta = false;

            if (disp != null) {
                resposta = disp.Desligar();
            }

            return resposta;
        }

        public void Regular(string nome, int potencia) {
            IRegulavel? disp = (IRegulavel?)Localizar(nome);

            if (disp != null) {
                disp.Regular(potencia);
            }


        }

        public void AddDispositivo(Dispositivo novo) {
            _dispositivos.Add(novo.getNome(), novo);
        }

        private Dispositivo? Localizar(string nome) {
            Dispositivo? disp;
            _dispositivos.TryGetValue(nome, out disp);
            return disp;
        }
        
        public override string ToString() {
            StringBuilder disp = new StringBuilder(this._nome + " controlando:\n");
            foreach (Dispositivo item in _dispositivos.Values)
            {
                disp.AppendLine(item.ToString());
            }
            return disp.ToString();
        }
    }
}
