using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    internal abstract class FormaGeometrica{
        static int ordinal = 1;
        protected string _descricao;
        protected int _ordinal;

        protected FormaGeometrica(string desc) {
            _descricao = desc;
            _ordinal = ordinal++;
        }

        public string nome() {
            return _descricao;
        }

        public override string ToString() {
            return $"{_descricao,19} -> Área: {area():00.00} | Perímetro: {perimetro():F2}";
        }

        public override int GetHashCode() {
            return _ordinal;
        }

        public abstract double area();
        public abstract double perimetro();
    }
}
