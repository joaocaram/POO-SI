using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    public abstract class FormaGeometrica{
        static int ordinal = 1;
        protected string _descricao;
        protected int _ordinal;

        protected FormaGeometrica(string desc) {
            _descricao = desc;
            _ordinal = ordinal++;
        }

        public string Nome() {
            return _descricao;
        }

        public override string ToString() {
            return $"{_descricao,19} -> Área: {Area():00.00} | Perímetro: {Perimetro():F2}";
        }

        public override int GetHashCode() {
            return _ordinal; 
        }

        public override bool Equals(object? obj) {
            bool resposta = false;
            FormaGeometrica outra = obj as FormaGeometrica;
            if(outra != null) {
                resposta = (this._descricao.Equals(outra._descricao) &&
                            this.Area() == outra.Area());
            }
            return resposta;    
        }

        public abstract double Area();
        public abstract double Perimetro();
    }
}
