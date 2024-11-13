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
            return $"{descricao,19} -> Área: {area():00.00} | Perímetro: {perimetro():F2}";
        }

        public abstract double area();
        public abstract double perimetro();
    }
}
