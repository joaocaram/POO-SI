using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    public class Cliente : IComparable
    {
        private static int _ultimoID = 0;
        private int _id;
        private string _nome;
        private Queue<Pedido> _pedidos;

        public Cliente(string nome)
        {
            if (nome.Length == 0)
                nome = "Cliente anônimo";
            _nome = nome;
            _id = ++_ultimoID;
            _pedidos = new Queue<Pedido>();
        }

        public int CompareTo(object? obj)
        {
            int resposta = -1;
            Cliente outro = obj as Cliente; // outro = (Cliente)obj;
            if (outro != null) { 
                double diferencaGastos = TotalGasto() - outro.TotalGasto();
                if (diferencaGastos == 0)
                    resposta = 0;
                else if (diferencaGastos > 0)
                    resposta = 1;
            }
            return resposta;
        }

        public int RegistrarPedido(Pedido novo)
        {
            _pedidos.Enqueue(novo);
            return _pedidos.Count;
        }

        public string RelatorioPedidos()
        {
            StringBuilder relat = new StringBuilder($"Cliente {_nome} - {_id}");
            foreach (Pedido p in _pedidos)
                relat.AppendLine(p.ToString());
            relat.Append($"\nTotal gasto pelo cliente: {TotalGasto():C2}");
            return relat.ToString();
        }

        public double TotalGasto()
        {
            return _pedidos.Sum(ped => ped.PrecoAPagar());
        }
    }
}
