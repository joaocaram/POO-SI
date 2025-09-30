using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal abstract class PoligonoReto : FormaGeometrica {
       
        protected double _basePoligono;
        protected double _alturaPoligono;

        protected PoligonoReto(string desc, double b, double h, int posX, int posY): 
            base(desc, posX, posY) {
            _basePoligono = _alturaPoligono = 0.1;
            if (b > 0.1)
                _basePoligono = b;
            if (h > 0.1)
                _alturaPoligono = h;
        }

        

    }
}
