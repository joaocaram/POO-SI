using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class TrianguloRetangulo : Poligono{

        public TrianguloRetangulo(double cateto1, double cateto2):
                base("Triângulo Retângulo", cateto1, cateto2) {

        }

        public override double area() {
            return basePoligono * alturaPoligono / 2;
        }

        public override double perimetro() {
            return basePoligono + alturaPoligono + hipotenusa();
        }

        private double hipotenusa() {
            return Math.Sqrt(Math.Pow(basePoligono, 2) + Math.Pow(alturaPoligono, 2));
        }
    }
}
