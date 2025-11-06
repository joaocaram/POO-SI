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
            this._nome = nome;
            this._dispositivos = new Dictionary<int, Dispositivo>();
        }

        public bool Ligar(string nome) {
            Dispositivo? disp = Localizar(nome);
            IDesligavel desligavel = disp as IDesligavel;
            bool resposta = false;

            if (desligavel != null) {
                resposta = desligavel.Ligar();
            }

            return resposta;
        }

        public bool Desligar(string nome) {
            Dispositivo? disp = Localizar(nome);
            IDesligavel desligavel = disp as IDesligavel;
            bool resposta = false;

            if (desligavel != null) {
                resposta = desligavel.Desligar();
            }

            return resposta;
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
            StringBuilder disp = new StringBuilder(this._nome + " controlando:\n");
            foreach (Dispositivo item in _dispositivos.Values)
            {
                disp.AppendLine(item.ToString());
            }
            return disp.ToString();
        }
    }
}
