using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class BaseDados<T>{
        private Dictionary<int, T> _dados;
       
        public BaseDados(int tamanho) {
            if (tamanho < 1)
                throw new ArgumentOutOfRangeException("Não se pode ter bases de tamanho negativo.");
            _dados = new Dictionary<int, T>(tamanho);     
        }

        public int Add(T newItem) {
            _dados.Add(newItem.GetHashCode(), newItem);
            return _dados.Count;
        }

        public T Get(int key) {
            T quem = default(T);
            _dados.TryGetValue(key, out quem);
            return quem;
        }

        public int Size() {
            return _dados.Count;
        }

        public int Size(Predicate<T> pred) {
           return  _dados.Values.Where(dado => pred.Invoke(dado))
                         .Count();
        }

        public string SortedReport(Comparison<T> comparacao) {
            List<T> dadosOrdenados = _dados.Values.ToList();
            dadosOrdenados.Sort();
            
            StringBuilder sb = new StringBuilder($"Relatório ordenado de {nameof(T)}:\n");
            foreach (T dado in dadosOrdenados)
                sb.AppendLine(dado.ToString() + "\n");
            return sb.ToString();
        }

        public string SimpleReport() {
            StringBuilder sb = new StringBuilder($"Relatório simplificado de {nameof(T)}:\n");
            foreach (T dado in _dados.Values)
                sb.AppendLine(dado + "\n");
            return sb.ToString();
        }

        public void Update(Action<T> acao)
        {
            foreach (T dado in _dados.Values)
                acao.Invoke(dado);
        }

        public double Sum(Func<T, double> function)
        {
            double resultado = 0;
            foreach (T dado in _dados.Values)
                resultado += function.Invoke(dado);
            return resultado;
        }

        public double Sum(Func<T, double> function, Predicate<T> cond) {
            return _dados.Values.Where(dado => cond.Invoke(dado))
                                .Select(dado => function.Invoke(dado))
                                .Sum();
        }

        public double Average(Func<T, double> function) {
            return _dados.Values.Select(dado => function.Invoke(dado))
                                .Average();
        }

        public double Average(Func<T, double> function, Predicate<T> cond) {
            return _dados.Values.Where(dado => cond.Invoke(dado))
                                .Select(dado => function.Invoke(dado))
                                .Average(); 
        }

        public string FilteredReport(Predicate<T> condicao) {
            StringBuilder sb = new StringBuilder($"Relatório filtrado de {nameof(T)}:\n");
            foreach (T dado in _dados.Values)
                if(condicao.Invoke(dado))
                    sb.AppendLine(dado + "\n");

            return sb.ToString();
        }

        public string FilteredReport(Predicate<T> condicao, Comparison<T> comparacao) {
            IComparer<T> comparador = Comparer<T>.Create(comparacao);
            string? relat = _dados.Values.Where(dado => condicao.Invoke(dado))
                                        .Order(comparador)
                                        .Select(dado => dado.ToString())
                                        .Aggregate((s1, s2) => $"{s1}\n{s2}");
            return $"Relatório filtrado e ordenado:\n" + relat;
        }

        public T Max(Predicate<T> condicao, Comparison<T> comparacao) {
            IComparer<T> comparador = Comparer<T>.Create(comparacao);
            return _dados.Values.Where(d => condicao.Invoke(d))
                         .Max(comparador);
        }

    }
}
