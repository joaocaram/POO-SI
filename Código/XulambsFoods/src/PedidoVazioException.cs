using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class PedidoVazioException : InvalidOperationException {

        public PedidoVazioException():
            base("Não se pode fechar pedido sem pizzas.") {

        }

    }
}
