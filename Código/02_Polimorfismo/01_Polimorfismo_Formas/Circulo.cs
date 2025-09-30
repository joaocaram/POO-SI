using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    public class Circulo : FormaGeometrica{
        private double _raio;

        public Circulo(double raio, int posX, int posY): 
            base("Círculo", posX, posY)
        {
            _raio = 0.1;
            if (raio > 0.1)
                _raio = raio;
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
