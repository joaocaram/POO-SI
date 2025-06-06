using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
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
            return TotalizarFiltrado(extratora, (d => true));   
        }

        public double TotalizarFiltrado(Func<T, double> extratora, Predicate<T> condicao) {
            return _dadosEmLista.Where(d => condicao.Invoke(d))
                                .Sum(d => extratora.Invoke(d));
        }

        public double Media(Func<T, double> extratora) {
            return _dadosEmLista.Average(d => extratora.Invoke(d));
        }


        public string RelatorioFiltrado(Predicate<T> condicao)
        {
            //IEnumerable<string> resultado =  from dado in _dadosEmLista
            //                                 where condicao.Invoke(dado)
            //                                 select dado.ToString();
            string separador = "\n~~~~~~~~~~~~~~~~~~~~~~~~\n";
            return _dadosEmLista.Where(d => condicao.Invoke(d))
                                        .Select(e => e.ToString())
                                        .Aggregate((str1, str2) => str1 + separador + str2);               
        }

        public void Atualizar(Action<T> atualizacao)
        {
            foreach (T dado in _dadosEmLista)
                atualizacao.Invoke(dado);
        }

        public T Maior(){
            return _dadosEmLista.Max();
        }

        public T Maior(Comparison<T> comparacao) {
            return _dadosEmLista.Max(Comparer<T>.Create(comparacao));
        }

    }
}
