using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    public sealed class Retangulo : PoligonoReto {

        public Retangulo(double b, double h, int posX, int posY):
            base("Retângulo", b, h, posX, posY) {

        }

        public override double Area() {
            return _basePoligono * _alturaPoligono;
        }

        public override double Perimetro() {
            return 2 * _basePoligono + 2 * _alturaPoligono;
        }

        public override string ToString() {
            return $"{base.ToString()}  | Base: {_basePoligono:F2} | Altura: {_alturaPoligono:F2}";
        }
    }
}
