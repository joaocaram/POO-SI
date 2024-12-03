using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras
{
    internal class ConjuntoGeometrico
    {
        private FormaGeometrica[] formas;
        private int quantasFormas;
        private int capacidade;

        public ConjuntoGeometrico(int tamanho)
        {
            capacidade = 1;
            if (tamanho > 1)
                capacidade = tamanho;
            formas = new FormaGeometrica[capacidade];
            quantasFormas = 0;
        }

        public void AddForma(FormaGeometrica nova)
        {
            if (nova != null
                && quantasFormas < capacidade)
            {
                formas[quantasFormas] = nova;
                quantasFormas++;
            }
        }

        public FormaGeometrica Buscar(FormaGeometrica outra) 
        {
            bool achou = false;
            int pos = 0;
            FormaGeometrica achada = null;
            while(!achou && pos < quantasFormas)
            {
                if (formas[pos].Equals(outra))
                {
                    achou = true;
                    achada = formas[pos];
                }
                else
                    pos++;
            }
            return achada;
        }


        public override string ToString()
        {
            StringBuilder relat = new StringBuilder("Conjunto com " + quantasFormas + " formas geométricas\n");
            for (int i = 0; i < quantasFormas; i++)
            {
                relat.AppendLine(formas[i].ToString());
            }
            return relat.ToString();
        }

          
    }
}
