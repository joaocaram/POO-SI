using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal abstract class FormaGeometrica{
        
        protected string descricao;

        protected FormaGeometrica(string desc) {
            descricao = desc;
        }

        public override string ToString() {
            return descricao + " com área de " + area().ToString("0.00");
        }

        public abstract double area();
        public abstract double perimetro();
    }
}
