using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class TrianguloRetangulo : PoligonoReto{

        public TrianguloRetangulo(double cateto1, double cateto2):
                base("Triângulo Retângulo", cateto1, cateto2) {

        }

        public override double area() {
            return _basePoligono * _alturaPoligono / 2;
        }

        public override double perimetro() {
            return _basePoligono + _alturaPoligono + hipotenusa();
        }

        private double hipotenusa() {
            return Math.Sqrt(Math.Pow(_basePoligono, 2) + Math.Pow(_alturaPoligono, 2));
        }

        public override string ToString() {
            return $"{base.ToString()}  | Base: {_basePoligono:F2} | Altura: {_alturaPoligono:F2} | Hipotenusa: {hipotenusa():F2}";
        }
    }
}
