using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class Circulo : FormaGeometrica{
        private double _raio;

        public Circulo(double raio, int posX, int posY): base("Círculo", posX, posY)
        {
            this._raio = 0.1;
            if (raio > 0.1)
                this._raio = raio;
        }

        public override double Area() {
            return Math.PI * Math.Pow(_raio, 2);
        }

        public override double Perimetro() {
            return 2 * Math.PI * _raio;
        }

        public override string ToString()
        {
            return $"{base.ToString()}  | Raio: {_raio:F2}";
        }

        
    }
}
