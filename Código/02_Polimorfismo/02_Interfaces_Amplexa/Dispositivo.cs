using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    public abstract class Dispositivo {
        private string _nome;
        private string _estado;
        protected bool _ligado;

        protected Dispositivo(string nome) {
            _nome = nome;
        }

        public void SetEstado(string estado) {
            _estado = estado;
        }

        public abstract bool Ligar();
        public abstract bool Desligar();

        public override bool Equals(object? obj) {
            Dispositivo outro = (Dispositivo)obj;
            return _nome.Equals(outro._nome);
        }

        public override string ToString() {
            return _nome+": "+_estado;
        }

        public override int GetHashCode() {
            return _nome.GetHashCode();
        }
    }
}
