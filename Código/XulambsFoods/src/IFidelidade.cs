using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace XulambsFoods_2025_1.src {
    public interface IFidelidade {
        double DescontoPedido(Pedido pedido);

        //MÉTODO DE EXTENSÃO
        static IFidelidade DefinirCategoria(ICollection pedidos) {
            IFidelidade categoria = new XulambsJunior();
            int quantidade = pedidos.Count;
            double valorMedio = 0d;

            foreach (Pedido item in pedidos) {
                valorMedio += item.PrecoAPagar();
            }
            valorMedio /= quantidade;

            if (quantidade >= XulambsSenior.MinimoPedidos && valorMedio >= XulambsSenior.MinimoMedia)
                categoria = new XulambsSenior();
            else if (quantidade >= XulambsPleno.MinimoPedidos && valorMedio >= XulambsPleno.MinimoMedia)
                categoria = new XulambsPleno();
            return categoria;
        }
    }
}