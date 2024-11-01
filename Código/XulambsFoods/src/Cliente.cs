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

        public int compararGasto(Cliente obj) {
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

        public int CompareTo(object? obj)
        {
            Cliente outro = obj as Cliente;
            return _nome.CompareTo(outro._nome);   
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
            //double valor = 0d;
            //foreach (Pedido p in _pedidos)
            //    valor += p.PrecoAPagar();
            //return valor;
            return _pedidos.Sum(ped => ped.PrecoAPagar());
        }

        public override int GetHashCode() {
            return _id;
        }

        public override bool Equals(object? obj) {
            bool resposta = false;
            Cliente outro = obj as Cliente;
            if (obj != null)
                resposta = (_id == outro._id);
            return resposta;
        }

        public override string ToString() {
            return $"{_nome} ({_id}) já gastou {TotalGasto():C2} no Xulambs Foods.";
        }
    }
}
