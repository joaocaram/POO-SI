using System;
using System.Collections.Generic;
using System.Text;

namespace XulambsFoods_2025_1.src {
    public class XulambsJunior : IFidelidade {
        private const double PctDesconto = 0.0;

        public double DescontoPedido(Pedido pedido) {
            if (pedido == null)
                throw new ArgumentNullException("Pedido não foi criado corretamente");
            double desc = pedido.PrecoAPagar() * PctDesconto;
            pedido.AplicarDesconto(desc);
            return desc;
        }

        public override string ToString() {
            return "Cliente Xulambs Junior";
        }
    }
}