using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliFiguras {
    public abstract class FormaGeometrica{
        private int _coordX;
        private int _coordY;
        private string _descricao;

        protected FormaGeometrica(string desc, int posX, int posY) {
            _coordX = posX > 0 ? posX : 1;
            _coordY = posY > 0 ? posY : 1;
            _descricao = desc;
            
        }

        
        public bool TemAreaMaiorQue(FormaGeometrica outra) {
            return Area() > outra.Area();
        }

        public override string ToString() {
            return $"{_descricao,10} -> Área: {Area():00.00} | Perímetro: {Perimetro():F2}";
        }

        public override int GetHashCode() {
            return (_descricao+_coordX.ToString()+_coordY.ToString()).GetHashCode(); 
        }

        public override bool Equals(object? obj) {
            FormaGeometrica outro = obj as FormaGeometrica;
            return (Area() == outro.Area()
                    && _descricao.Equals(outro._descricao));
        }

        public abstract double Area();
        public abstract double Perimetro();
    }
}
