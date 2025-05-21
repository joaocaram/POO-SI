using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src
{
    public class Cliente
    {
        private int _id;
        private string _nome;
        private Queue<Pedido> _pedidos;

        public Cliente(int id, string nome)
        {
            if (id < 0 || nome.Length < 2)
                throw new ArgumentOutOfRangeException("Id deve ser positivo e o nome deve ter ao menos 2 letras");
            _id = id;
            _nome = nome;
            _pedidos = new Queue<Pedido>();
        }

        public int RegistrarPedido(Pedido novo)
        {
            if (novo == null)
                throw new ArgumentNullException("Pedido não foi criado corretamente");
            _pedidos.Enqueue(novo);
            return _pedidos.Count;
        }

        public string RelatorioPedidos()
        {
            StringBuilder relat = new StringBuilder($"{_nome} ({_id}) - Relatório de Pedidos\n");
            foreach (Pedido pedido in _pedidos)
            {
                relat.AppendLine("==============");
                relat.AppendLine(pedido.ToString());
            }
            return relat.ToString();
        }

        public double TotalGasto()
        {
            double total = 0d;
            foreach (Pedido pedido in _pedidos)
            {
                total += pedido.PrecoAPagar();
            }
            return total;
        }

        public override string ToString()
        {
            return $"{_nome} - ({_id}). Total gasto: {TotalGasto():C2} em {_pedidos.Count} pedidos.";
        }

        public override bool Equals(object? obj)
        {
            bool resposta = false;
            Cliente outro = obj as Cliente;
            if (outro != null)
                resposta = this._id == outro._id;
            return resposta;
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}
