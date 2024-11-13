using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal abstract class Poligono : FormaGeometrica {
       
        protected double _basePoligono;
        protected double _alturaPoligono;

        protected Poligono(string desc, double b, double h): base(desc) {
            _basePoligono = _alturaPoligono = 0.1;
            if (b > 0.1)
                _basePoligono = b;
            if (h > 0.1)
                _alturaPoligono = h;
        }

        public override bool Equals(object? obj)
        {
            try { 
            Poligono outro = (Poligono)obj;
            return (this._basePoligono == outro._basePoligono
                    && this._alturaPoligono == outro._alturaPoligono
                    && this._descricao.Equals(outro._descricao));
            }
            catch(InvalidCastException ic)
            {
                return false;
            }
         }

    }
}
