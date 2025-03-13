using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class Pedido {
        private static int s_ultimoPedido = 0;
        private const int MaxPizzas = 100;

        private int _idPedido;
        private DateOnly _data;
        private Pizza[] _pizzas;
        private int _quantPizzas;
        private bool _aberto;

        public Pedido() {
            s_ultimoPedido++;
            _idPedido = s_ultimoPedido;
            _data = DateOnly.FromDateTime(DateTime.Now);
            _pizzas = new Pizza[MaxPizzas];
            _quantPizzas = 0;
            _aberto = true;
        }

        private bool PodeAdicionar() {
            return _aberto;
        }

        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar()) {
                _pizzas[_quantPizzas] = pizza;
                _quantPizzas++;
            }
            return _quantPizzas;
        }

        public void FecharPedido() {
            _aberto = false;
        }

        public double PrecoAPagar() {
            double preco = 0d;
            for (int i = 0; i < _quantPizzas; i++) {
                preco += _pizzas[i].ValorFinal();
            }
            return preco;
        }

        public string Relatorio() {
            StringBuilder relat = new StringBuilder($"Pedido {_idPedido} - {_data}\n");
            relat.AppendLine("==============================");
            for (int i = 0; i < _quantPizzas; i++) {
                relat.AppendLine(_pizzas[i].NotaDeCompra());
            }
            relat.Append($"Valor a pagar: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}
