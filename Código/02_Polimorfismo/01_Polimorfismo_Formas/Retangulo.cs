using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class Retangulo : Poligono {

        public Retangulo(double b, double h) : base("Retângulo", b, h) {

        }

        public override double area() {
            return basePoligono * alturaPoligono;
        }

        public override double perimetro() {
            return 2 * basePoligono + 2 * alturaPoligono;
        }
    }
}
