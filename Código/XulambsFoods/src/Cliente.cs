using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class Cliente : IComparable<Cliente>{
        private int _id;
        private string _nome;
        private IFidelidade _categoria;
        private Queue<Pedido> _pedidos;

        public Cliente(int id, string nome) {
            if (id < 0 || nome.Length < 2)
                throw new ArgumentOutOfRangeException("Id deve ser positivo e o nome deve ter ao menos 2 letras");
            _id = id;
            _nome = nome;
            _categoria = new XulambsJunior();
            _pedidos = new Queue<Pedido>();
        }

        public int RegistrarPedido(Pedido novo) {
            if (novo == null)
                throw new ArgumentNullException("O pedido não é válido para registro.");
            _categoria.DescontoPedido(novo);
            _pedidos.Enqueue(novo);
            return _pedidos.Count;
        }

        public string RelatorioPedidos() {
            StringBuilder relat = new StringBuilder($"{_nome} ({_id}) - {_categoria}\nRelatório de Pedidos\n");
            int i = 1;
            foreach (Pedido pedido in _pedidos) {
                relat.AppendLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                relat.AppendLine($"{i:D3} - {pedido.ToString()}\n");
                i++;
            }
            relat.AppendLine($"Total gasto pelo cliente: {TotalGasto():C2}");
            return relat.ToString();
        }

        public double TotalGasto() {
            double total = 0d;
            foreach (Pedido pedido in _pedidos) {
                total += pedido.PrecoAPagar();
            }
            return total;
        }

        public void AtualizarCategoria() {
            _categoria = IFidelidade.DefinirCategoria(_pedidos);
        }

        public override string ToString() {
            return $"{_nome} - ({_id}) - {_categoria}.\nTotal gasto: {TotalGasto():C2} em {_pedidos.Count} pedidos.";
        }

        public override bool Equals(object? obj) {
            bool resposta = false;
            Cliente outro = obj as Cliente;
            if (outro != null)
                resposta = this._id == outro._id;
            return resposta;
        }

        public override int GetHashCode() {
            return _id;
        }

        public int CompareTo(Cliente? other) {
            int resultado = 0;
            if (this.TotalGasto() > other.TotalGasto())
                resultado = 1;
            else if (this.TotalGasto() < other.TotalGasto())
                resultado = -1;
            return resultado;
        }
    }
}