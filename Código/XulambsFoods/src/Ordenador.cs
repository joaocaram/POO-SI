using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src {
    public class Ordenador {

        private IComparable[] _dados;

        public Ordenador(IComparable[] dados) {
            _dados = dados;
        }

        public IComparable[] ordenar() {
            _dados = QuickSort(_dados, 0, _dados.Length-1);
            return _dados;
        }

        private IComparable[] QuickSort(IComparable[] dados, int inicio, int fim) {
            if(fim > inicio) { 
                int particao = Particao(dados, inicio, fim);
                dados = QuickSort(dados, inicio, particao-1);
                dados = QuickSort(dados, particao+1, fim);
            }
            return dados;
        }

        private int Particao(IComparable[] dados, int inicio, int fim) {
            IComparable pivot = dados[fim];
            int particao = inicio - 1;

            for (int i = inicio; i < fim; i++)
            {
                if (dados[i].CompareTo(pivot) < 0) {
                    particao++;
                    IComparable temporario = dados[i];
                    dados[i] = dados[particao];
                    dados[particao] = temporario;
                }
            }
            particao++;
            IComparable tempPivot = dados[fim];
            dados[fim] = dados[particao];
            dados[particao] = tempPivot;
            return particao;
        }
    }
}
