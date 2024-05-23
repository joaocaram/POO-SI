using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XulambsFoods.src;

namespace XulambsFoods.src
{
    internal class BaseClientes
    {
        private Dictionary<int, Cliente> clientes;
        private List<Cliente> clientesOrdenados;

        public BaseClientes(int capacidade)
        {
            if (capacidade < 16)
                capacidade = 16;
            clientes = new Dictionary<int, Cliente>(capacidade);
        }

        public Cliente localizar(int idCli)
        {
            Cliente quem;
            clientes.TryGetValue(idCli, out quem);
            return quem;
        }
        public void adicionar(Cliente novo)
        {
            clientes.Add(novo.GetHashCode(), novo);
        }

        public void ordenar(Comparison<Cliente> comparacao)
        {
            clientesOrdenados = clientes.Values.ToList();
            clientesOrdenados.Sort(comparacao);
        }

        public string relatorioResumido()
        {
            StringBuilder relatorio = new StringBuilder("Relatório resumido de clientes: \n");
            foreach (Cliente cliente in clientesOrdenados)
            {
                relatorio.AppendLine($"{cliente}\n");
            }
            return relatorio.ToString();
        }

        public void processar(Action<Cliente> metodo) {
            foreach (Cliente cliente in clientes.Values) {
                metodo.Invoke(cliente);
            }
        }
    }
}
