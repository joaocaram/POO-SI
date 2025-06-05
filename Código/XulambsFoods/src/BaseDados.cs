using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    internal class BaseDados<T> {
        private Dictionary<int, T> _dados;
        private LinkedList<T> _dadosEmLista;
        
        public BaseDados() {
            _dados = new Dictionary<int, T>();
            _dadosEmLista = new LinkedList<T>();
        }

        public BaseDados(int quantidade) {
            _dados = new Dictionary<int, T>(quantidade);
        }

        public int Adicionar(T novoItem) {
            _dados.Add(novoItem.GetHashCode(), novoItem);
            _dadosEmLista.AddLast(novoItem);
            return _dados.Count;
        }

        public T Buscar(int identificador) {
            return _dados[identificador];   
        }

        public int Tamanho() {
            return _dados.Count;
        }

        private string Relatorio(List<T> lista, Comparer<T> comparador) {        
            if(comparador !=null )
                lista.Sort(comparador);

            StringBuilder sb = new StringBuilder("Relatório resumido de clientes:\n");
            foreach (T dado in lista)
                sb.AppendLine(dado.ToString() + "\n");
            return sb.ToString();
        }

        public string Relatorio() {
            return Relatorio(_dadosEmLista.ToList(), null);
        }

        public string RelatorioOrdenado(Comparer<T> comparador) {
            return Relatorio(_dadosEmLista.ToList(), comparador);
        }

        public double Totalizar(Func<T, double> extratora)
        {
            double valor = 0d;
            foreach (T dado in _dadosEmLista)
                valor += extratora.Invoke(dado);
            return valor;
        }

 
        public string RelatorioFiltrado(Predicate<T> condicao)
        {
            StringBuilder filtrados = new StringBuilder(); ;
            foreach (T dado in _dadosEmLista)
                if (condicao.Invoke(dado))
                    filtrados.AppendLine(dado.ToString() + "\n");
            return filtrados.ToString();
        }

        public void Atualizar(Action<T> atualizacao)
        {
            foreach (T dado in _dadosEmLista)
                atualizacao.Invoke(dado);
        }

    }
}
