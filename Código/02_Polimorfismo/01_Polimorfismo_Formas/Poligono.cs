using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal abstract class Poligono : FormaGeometrica {
       
        protected double basePoligono;
        protected double alturaPoligono;

        protected Poligono(string desc, double b, double h): base(desc) {
            basePoligono = alturaPoligono = 0.1;
            if (b > 0.1)
                basePoligono = b;
            if (h > 0.1)
                alturaPoligono = h;
        }

        public override bool Equals(object? obj)
        {
            try { 
            Poligono outro = (Poligono)obj;
            return (this.basePoligono == outro.basePoligono
                    && this.alturaPoligono == outro.alturaPoligono
                    && this.descricao.Equals(outro.descricao));
            }
            catch(InvalidCastException ic)
            {
                return false;
            }
         }

    }
}
