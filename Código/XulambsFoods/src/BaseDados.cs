using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public class BaseDados<Classe> where Classe : IComparable<Classe> {
        private Dictionary<int, Classe> _dados;
       
        public BaseDados(int tamanho) {
            if (tamanho < 1)
                throw new ArgumentOutOfRangeException("Não se pode ter bases de tamanho negativo.");
            _dados = new Dictionary<int, Classe>(tamanho);     
        }

        public int Add(Classe newItem) {
            _dados.Add(newItem.GetHashCode(), newItem);
            return _dados.Count;
        }

        public Classe Get(int key) {
            Classe quem = default(Classe);
            _dados.TryGetValue(key, out quem);
            return quem;
        }

        public int Size() {
            return _dados.Count;
        }

        public string SortedReport() {
            List<Classe> dadosOrdenados = _dados.Values.ToList();
            dadosOrdenados.Sort();
            
            StringBuilder sb = new StringBuilder($"Relatório ordenado de {nameof(Classe)}:\n");
            foreach (Classe dado in dadosOrdenados)
                sb.AppendLine(dado.ToString() + "\n");
            return sb.ToString();
        }

        public string SimpleReport() {
            StringBuilder sb = new StringBuilder($"Relatório simplificado de {nameof(Classe)}:\n");
            foreach (Classe dado in _dados.Values)
                sb.AppendLine(dado.ToString() + "\n");
            return sb.ToString();
        }

        public string Report() {
            StringBuilder sb = new StringBuilder($"Relatório completo de {nameof(Classe)}:\n");
            //TODO
            return sb.ToString();
        }

    }
}
