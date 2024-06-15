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
            return $"{descricao} com área de {area().ToString("F2")} e perimetro {perimetro().ToString("F2")}";
        }

        public abstract double area();
        public abstract double perimetro();
    }
}
