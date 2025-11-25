using System;
using System.Collections.Generic;
using System.Text;

namespace XulambsFoods_2025_1.src {
    public class XulambsPleno : IFidelidade {
        private const double PctDesconto = 0.1;
        public const int MinimoPedidos = 10;
        public const double MinimoMedia = 29;



        public override string ToString() {
            return "Cliente Xulambs Pleno";
        }

        public double DescontoPedido(Pedido pedido) {
            if (pedido == null)
                throw new ArgumentNullException("Pedido não foi criado corretamente");
            double desc = pedido.PrecoAPagar() * PctDesconto;
            pedido.AplicarDesconto(desc);
            return desc;
        }
    }
}
