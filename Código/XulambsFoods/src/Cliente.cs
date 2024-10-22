using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    public class Cliente : IComparable
    {
        private int _id;
        private string _nome;
        private Queue<Pedido> _pedidos;

        public Cliente(string nome, int id)
        {
            _nome = nome;
            _id = id;
        }

        public int CompareTo(object? obj)
        {
            int resposta = 0;
            Cliente outro = (Cliente)obj;
            double dif = TotalGasto() - outro.TotalGasto();
            if (dif < 0)
                resposta = -1;
            else if (dif > 0)
                resposta = 1;

            return resposta;

            
        }

        public int RegistrarPedido(Pedido novo)
        {
            _pedidos.Enqueue(novo);
            return _pedidos.Count;
        }

        public string RelatorioPedidos()
        {
            StringBuilder relat = new StringBuilder(_nome);
            foreach (Pedido p in _pedidos)
                relat.AppendLine(p.ToString());
            relat.AppendLine($"{TotalGasto():C2}");
            return relat.ToString();
        }

        public double TotalGasto()
        {
            return _pedidos.Sum(ped => ped.PrecoAPagar());
        }
    }
}
