using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src {
    public class PedidoLotadoException : InvalidOperationException {
    
        public PedidoLotadoException() : base() {

        }

        public PedidoLotadoException(string msg) : base(msg) {

        }
    }

}
