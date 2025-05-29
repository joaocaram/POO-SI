using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    internal class BaseClientes {
        private Dictionary<int, Cliente> _clientes;
        
        public BaseClientes() {
            _clientes = new Dictionary<int, Cliente>();
        }

        public BaseClientes(int quantidade) {
            _clientes = new Dictionary<int, Cliente>(quantidade);
        }

        public int Add(Cliente novoCliente) {
            _clientes.Add(novoCliente.GetHashCode(), novoCliente);
            return _clientes.Count;
        }

        public Cliente Get(int identificador) {
            return _clientes[identificador];   
        }

        public int Size() {
            return _clientes.Count;
        }
    }
}
