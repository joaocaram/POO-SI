using System.Text;

namespace PoliFiguras
{
    public class ConjuntoGeometrico
    {
        private List<FormaGeometrica> _formas;
        private int _capacidade;

        /// <summary>
        /// Cria um conjunto de formas geométricas com capacidade máxima.
        /// </summary>
        /// <param name="tamanho">Capacidade do conjunto (1 ou mais)</param>
        public ConjuntoGeometrico(int tamanho)
        {
            _capacidade = 1;
            if (tamanho > 1)
                _capacidade = tamanho;
            _formas = new List<FormaGeometrica>(_capacidade);
        }

        public int AddForma(FormaGeometrica nova)
        {
            if (nova != null && _formas.Count < _capacidade)
            {
                _formas.Add(nova);
            }
            return _formas.Count;
        }

        public FormaGeometrica Buscar(FormaGeometrica outra) 
        {
            int pos = 0;
            FormaGeometrica achada = null;
            while(achada == null && pos < _formas.Count)
            {
                if (_formas.ElementAt(pos).Equals(outra)) {
                    achada = _formas.ElementAt(pos);
                }
                pos++;
            }
            return achada;
        }

        public int Remover(FormaGeometrica qual) {
            _formas.Remove(qual);
            return _formas.Count;
        }

        public FormaGeometrica MaiorDeTodas() {
            FormaGeometrica maior = null;
            if(_formas.Count > 0) {
                maior = _formas.ElementAt(0);
                for(int i=1; i< _formas.Count; i++) {
                    FormaGeometrica candidata = _formas.ElementAt(i);
                    if (candidata.TemAreaMaiorQue(maior))
                        maior = candidata;
                }
            }
            return maior;
        }

        public override string ToString()
        {
            StringBuilder relatorio = new StringBuilder($"Conjunto com {_formas.Count} formas geométricas\n");
            foreach(FormaGeometrica forma in _formas)
            {
                relatorio.AppendLine($"{forma}");
            }
            return relatorio.ToString();
        }

          
    }
}
