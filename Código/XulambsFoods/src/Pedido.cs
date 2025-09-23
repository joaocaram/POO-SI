using System.Text;


namespace XulambsFoods_2025_1.src {
    public class Pedido {
        private static int s_ultimoPedido = 0;
        
        private int _idPedido;
        private DateOnly _data;
        private LinkedList<Pizza> _pizzas;
        private bool _aberto;

        public Pedido() {
            s_ultimoPedido++;
            _idPedido = s_ultimoPedido;
            _data = DateOnly.FromDateTime(DateTime.Now);
            _pizzas = new LinkedList<Pizza>();
            _aberto = true;
        }

        /// <summary>
        /// Retorna id para localização do Pedido. Será melhorado em breve, para evitar método get
        /// </summary>
        /// <returns></returns>
        public int GetID() {
            return _idPedido;
        }

        private bool PodeAdicionar() {
            return _aberto;
        }

        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar()) {
               _pizzas.AddLast(pizza); 
            }
            return _pizzas.Count;
        }

        public void FecharPedido() {
            _aberto = false;
        }

        public double PrecoAPagar() {
            double preco = 0d;
            foreach (Pizza pizza in _pizzas) {
                preco += pizza.ValorFinal();                
            }
            return preco;
        }

        public string Relatorio() {
            StringBuilder relat = new StringBuilder($"Pedido {_idPedido} - {_data}\n");
            relat.AppendLine("==============================");
            foreach (Pizza pizza in _pizzas) {
                relat.AppendLine(pizza.NotaDeCompra());
            }
            relat.Append($"Valor a pagar: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}
