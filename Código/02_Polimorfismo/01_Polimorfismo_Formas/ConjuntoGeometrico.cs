using System.Text;

namespace PoliFiguras
{
    internal class ConjuntoGeometrico
    {
        private List<FormaGeometrica> formas;
        private int capacidade;

        public ConjuntoGeometrico(int tamanho)
        {
            capacidade = 1;
            if (tamanho > 1)
                capacidade = tamanho;
            formas = new List<FormaGeometrica>();
        }

        public void AddForma(FormaGeometrica nova)
        {
            if (nova != null && formas.Count < capacidade)
            {
                formas.Add(nova);
            }
        }

        public FormaGeometrica Buscar(FormaGeometrica outra) 
        {
            bool achou = false;
            int pos = 0;
            FormaGeometrica achada = null;
            while(!achou && pos < formas.Count)
            {
                if (formas.ElementAt(pos).Equals(outra)) {
                    achou = true;
                    achada = formas.ElementAt(pos);
                }
                else
                    pos++;
            }
            return achada;
        }

        public FormaGeometrica MaiorDeTodas() {
            FormaGeometrica resposta = null;
            if(formas.Count > 0) {
                resposta = formas.ElementAt(0);
                for(int i=1; i< formas.Count; i++) {
                    if (formas.ElementAt(i).TemAreaMaiorQue(resposta))
                        resposta = formas.ElementAt(i);
                }
            }
            return resposta;

        }

        public override string ToString()
        {
            StringBuilder relat = new StringBuilder("Conjunto com " + formas.Count + " formas geométricas\n");
            foreach(FormaGeometrica forma in formas)
            {
                relat.AppendLine(forma.ToString());
            }
            return relat.ToString();
        }

          
    }
}
