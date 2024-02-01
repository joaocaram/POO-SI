using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produtos {
    internal class Produto {
        public string descricao;
        public float precoUnitario;

        public float precoPorLote(int quantidade) {
            return precoUnitario * quantidade;
        }
    }
}
