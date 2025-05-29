using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    internal class BaseDados<T> {
        private Dictionary<int, T> _dados;
        
        public BaseDados() {
            _dados = new Dictionary<int, T>();
        }

        public BaseDados(int quantidade) {
            _dados = new Dictionary<int, T>(quantidade);
        }

        public int Add(T novoItem) {
            _dados.Add(novoItem.GetHashCode(), novoItem);
            return _dados.Count;
        }

        public T Get(int identificador) {
            return _dados[identificador];   
        }

        public int Size() {
            return _dados.Count;
        }

        public string Report() {
            StringBuilder sb = new StringBuilder("Relatório resumido de clientes:\n");
            foreach (T dado in _dados.Values)
                sb.AppendLine(dado.ToString() + "\n");
            return sb.ToString();
        }

        public string SortedReport(Comparer<T> comparador) {
            List<T> lista = _dados.Values.ToList();
            lista.Sort(comparador);

            StringBuilder sb = new StringBuilder("Relatório resumido de clientes:\n");
            foreach (T dado in lista)
                sb.AppendLine(dado.ToString() + "\n");
            return sb.ToString();
        }
    }
}
