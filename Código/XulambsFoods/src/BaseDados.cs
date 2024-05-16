using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods.src {
    internal class BaseDados<K, T> {
        private Dictionary<K, T> dados;
        private List<T> dadosOrdenados;

        public BaseDados(int capacidade) {
            if (capacidade < 16)
                capacidade = 16;
            dados = new Dictionary<K, T>(capacidade);
        }

        public T localizar(K idCli) {
            T quem;
            dados.TryGetValue(idCli, out quem);
            return quem;
        }
        public void adicionar(K chave, T novo) {
            dados.Add(chave, novo);
        }

        public void ordenar(Comparison<T> comparacao) {
            dadosOrdenados = dados.Values.ToList();
            dadosOrdenados.Sort(comparacao);
        }

        public string relatorioResumido() {
            StringBuilder relatorio = new StringBuilder("Relatório resumido: \n");
            foreach (T dado in dadosOrdenados) {
                relatorio.AppendLine($"{dado}\n");
            }
            return relatorio.ToString();
        }

        public double totalizar(Func<T, double> metodo) {
            double total = 0;
            foreach (T dado in dados.Values)
            {
                total += metodo.Invoke(dado);
            }
            return total;
        }

        public void processar(Action<T> metodo) {
            foreach (T dado in dados.Values) {
                metodo.Invoke(dado);
            }
        }
    }
}
