using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras
{
    public class ConjuntoGeometrico {
        private ICollection<FormaGeometrica> formas;
        private int capacidade;

        public ConjuntoGeometrico() {
            capacidade = Int32.MaxValue;
            formas = new LinkedList<FormaGeometrica>();
        }

        public ConjuntoGeometrico(int tamanho)
        {
            capacidade = 1;
            if (tamanho > 1)
                capacidade = tamanho;
            formas = new List<FormaGeometrica>(tamanho);
        }


        public void AddForma(FormaGeometrica nova)
        {
            if (nova != null && formas.Count < capacidade)
            {
                formas.Add(nova);
            }
        }

        public FormaGeometrica Buscar(FormaGeometrica outra) {
            foreach (FormaGeometrica f in formas) {
                if (f.Equals(outra))
                    return f;
            }
            return null;
        }

        public double Somar(Func<FormaGeometrica, double> oQue) {
            return formas.Select(oQue)
                         .Sum();
        }

        public double Media(Func<FormaGeometrica, double> oQue) {
            return formas.Select(oQue)
                         .Average();
        }

        public FormaGeometrica Maior(Comparison<FormaGeometrica> comparacao) {
            Comparer<FormaGeometrica> comp =
                Comparer<FormaGeometrica>.Create(comparacao);
            return formas.Max(comp);
        }


        public FormaGeometrica Buscar(int posicao) {
            if (posicao < 0 || posicao >= formas.Count)
                throw new ArgumentOutOfRangeException("Posição inválida.");
            return formas.ElementAt(posicao);
        }

        public override string ToString()
        {
            StringBuilder relat = new StringBuilder($"Conjunto com {formas.Count} formas geométricas\n");
            foreach(FormaGeometrica f in formas){
                relat.AppendLine(f.ToString());
            }
            return relat.ToString();
        }

          
    }
}
