using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class Retangulo : PoligonoReto {

        public Retangulo(double b, double h) : base("Retângulo", b, h) {

        }

        public override double area() {
            return _basePoligono * _alturaPoligono;
        }

        public override double perimetro() {
            return 2 * _basePoligono + 2 * _alturaPoligono;
        }

        public override string ToString() {
            return $"{base.ToString()}  | Base: {_basePoligono:F2} | Altura: {_alturaPoligono:F2}";
        }
    }
}
