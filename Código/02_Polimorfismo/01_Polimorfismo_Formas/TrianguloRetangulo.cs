using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class TrianguloRetangulo : PoligonoReto{

        public TrianguloRetangulo(double cateto1, double cateto2, int posX, int posY):
                base("Triângulo Retângulo", cateto1, cateto2, posX, posY) {

        }

        public override double Area() {
            return _basePoligono * _alturaPoligono / 2;
        }

        public override double Perimetro() {
            return _basePoligono + _alturaPoligono + Hipotenusa();
        }

        private double Hipotenusa() {
            return Math.Sqrt(Math.Pow(_basePoligono, 2) + Math.Pow(_alturaPoligono, 2));
        }

        public override string ToString() {
            return $"{base.ToString()}  | Base: {_basePoligono:F2} | Altura: {_alturaPoligono:F2} | Hipotenusa: {Hipotenusa():F2}";
        }
    }
}
