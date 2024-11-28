using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    //LINQ - Language-Integrated Query 

    public class BaseDados<T> 
    {
        private Dictionary<int, T> dados;
      
        public BaseDados()
        {
            dados = new Dictionary<int, T>();
        }

        public int Quantidade()
        {
            return dados.Count;
        }

        public void Add(T novoDado)
        {
            dados.Add(novoDado.GetHashCode(), novoDado);
        }

        public T Localizar(int id)
        {
            return dados.GetValueOrDefault(id);
        }

        public string RelatorioOrdenado() {
            return RelatorioOrdenado(Comparer<T>.Default);
        }

        public string RelatorioOrdenado(Comparison<T> comparison) {
            return RelatorioOrdenado(Comparer<T>.Create(comparison));
        }

        public string RelatorioOrdenado(Comparer<T> comparador) {
            StringBuilder relat = new StringBuilder();
            T[] dadosOrd = new T[dados.Count];

            Array.Copy(dados.Values.ToArray(), dadosOrd, dados.Count);
            Ordenador<T> qSort = new Ordenador<T>(dadosOrd, comparador);
            dadosOrd = qSort.ordenar();

            foreach (T item in dadosOrd) {
                relat.AppendLine(item.ToString());
            }
            relat.Append("==================");
            return relat.ToString();
        }
    
        public double Totalizador(Func<T, double> funcaoSomadora) {
            double valor = 0d;
            foreach(T dado in dados.Values) {
                valor += funcaoSomadora.Invoke(dado);
            }
            return valor;
        }

        public void Processar(Action<T> acao) {
            foreach (T dado in dados.Values) {
                acao.Invoke(dado);
            }
        }
    }
}
