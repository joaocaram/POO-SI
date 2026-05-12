using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retangulo_POO_M {
    internal class Retangulo {
        int _altura;
        int _largura;
       
        public Retangulo(int altura, int largura) {
            _altura = altura;
            _largura = largura;
        }

        public string LinhaCheia() {
            string linha = "";
            for (int i = 0; i < _largura; i++) {
                linha += "X";
            }
            return linha + "\n";
        }

        public string LinhaVazia() {
            string linha = "X";

            for (int i = 0; i < _largura - 2; i++) {
                linha += " ";
            }

            return linha + "X\n";
        }

        public string DesenharRetangulo() {
            string linhaCheia = LinhaCheia();
            string linhaVazia = LinhaVazia();

            string retangulo = linhaCheia;

            for (int i = 0; i < _altura - 2; i++) {
                retangulo += linhaVazia;
            }

            retangulo += linhaCheia;
            return retangulo;
        }
    }
}
