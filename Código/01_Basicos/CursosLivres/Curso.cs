using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLivres {
    public class Curso {
        private string _nome;
        private string _codigo;
        private int _quantidadeAulas;
        private DayOfWeek _diaSemana;

        public Curso(string nome, int quantidadeAulas, DayOfWeek dia) {
            _nome = nome;
            _quantidadeAulas = quantidadeAulas;
            _diaSemana = dia;
            if (_quantidadeAulas <= 0)
                _quantidadeAulas = 1;
            _codigo = CriarCodigo();
        }

        private string CriarCodigo() {
            StringBuilder cod = new StringBuilder();
            string[] palavras = _nome.Split(" ");
            foreach (string palavra in palavras)
                cod.Append(palavra.ElementAt(0));
            
            cod.Append((int)_diaSemana + 1);
            return cod.ToString();
        }

        public int QuantidadeAulas() {
            return _quantidadeAulas;
        }

        public override string ToString() {
            return $"{_nome} ({_codigo}) com total de {_quantidadeAulas} aulas.";
        }


    }
}
