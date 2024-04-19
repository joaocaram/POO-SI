using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_C__Interfaces_Amplexa {
    public abstract class Dispositivo {
        private string _nome;
        private string _estado;
      
        protected Dispositivo(string nome) {
            Nome = nome;
        }

        public string Nome {
            get => _nome;
            set => _nome = value;
        }
        
        protected string Estado {
            set => _estado = value;
        }

        public override bool Equals(object? obj) {
            Dispositivo outro = (Dispositivo)obj;
            return this._nome.Equals(outro._nome);
        }

        public override string ToString() {
            return this._nome+": "+this._estado;
        }
    }
}
