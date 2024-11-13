using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal class Circulo : FormaGeometrica{
        private double _raio;

        public Circulo(double raio): base("Círculo")
        {
            this._raio = 0.1;
            if (raio > 0.1)
                this._raio = raio;
        }

        public override double area() {
            return Math.PI * Math.Pow(_raio, 2);
        }

        public override double perimetro() {
            return 2 * Math.PI * _raio;
        }

        public override string ToString()
        {
            return $"{base.ToString()}  | Raio: {_raio:F2}";
        }

        public override bool Equals(object? obj)
        {
            try { 
                Circulo outro = (Circulo)obj;
                return (this._raio == outro._raio);
            }catch(InvalidCastException ic)
            {
                return false;
            }
        }
    }
}
