using System;
using System.Collections.Generic;
using System.Text;

namespace XulambsFoods_2025_1.src {
    public class XulambsSenior : IFidelidade {
        private const double PctDesconto = 0.2;
        public const int MinimoPedidos = 20;
        public const double MinimoMedia = 44;
        private const int ComprasCupom = 5;
        private const double ValorCupom = 10;
        private int _contadorPedidos;

        public XulambsSenior() {
            _contadorPedidos = 0;
        }

        public double DescontoPedido(Pedido pedido) {
            if (pedido == null)
                throw new ArgumentNullException("Pedido não foi criado corretamente");
            double valorDesconto = pedido.PrecoAPagar() * PctDesconto;
            if ((_contadorPedidos + 1) % ComprasCupom == 0)
                valorDesconto += ValorCupom;
            pedido.AplicarDesconto(valorDesconto);
            _contadorPedidos++;
            return valorDesconto;
        }

        public override string ToString() {
            return "Cliente Xulambs Senior";
        }
    }
}
