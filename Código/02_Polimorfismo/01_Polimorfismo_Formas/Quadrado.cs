using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class Quadrado : PoligonoReto {

        public Quadrado(double lado) : base("Quadrado", lado, lado) {

        }

        public override double Area() {
            return Math.Pow(_basePoligono, 2.0);
        }

        public override double Perimetro() {
            return 4 * _basePoligono;
        }

        public override string ToString() {
            return $"{base.ToString()}  | Lado: {_basePoligono:F2}";
        }
    }
}
