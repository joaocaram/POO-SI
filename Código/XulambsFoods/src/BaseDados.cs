using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    internal class BaseDados<T> where T : IComparable
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

        public T localizar(int id)
        {
            
            return dados.GetValueOrDefault(id);
        }

        public string relatorioOrdenado()
        {
            StringBuilder relat = new StringBuilder();
            IComparable[] dadosOrd = new IComparable[1];
            Array.Copy(dados.Values.ToArray(), dadosOrd, dados.Count);
            Ordenador qsCliente = new Ordenador(dadosOrd);
            dadosOrd = qsCliente.ordenar();

            relat.AppendLine("Relatório ordenado:");
            foreach (IComparable item in dadosOrd)
            {
                relat.AppendLine(item.ToString());
            }
            relat.Append("==================");
            return relat.ToString();
        }
    }
}
