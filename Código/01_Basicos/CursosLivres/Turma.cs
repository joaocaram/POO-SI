using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLivres {
    public class Turma {
        private const int MaxAlunos = 20;

        private List<Aluno> _alunos;
        private string _nome;
        private Curso _curso;

        public Turma(string nome, Curso curso) {
            _nome = nome;
            _curso = curso;
            _alunos = new List<Aluno>(MaxAlunos);
        }

        public bool Matricular(Aluno aluno) {
            bool resposta = false;
            bool alunoExiste = aluno != null && GetAluno(aluno.GetMatricula()) != null;
            if (TemVagas() && !alunoExiste) {
                resposta = aluno.Matricular(_curso);
                if (resposta)
                    _alunos.Add(aluno);
            }
            return resposta;
        }

        public bool TemVagas() {
            return _alunos.Count < MaxAlunos;
        }

        public Aluno GetAluno(int matricula) {
            Aluno aluno = null;
            for (int i = 0; i < _alunos.Count && aluno == null; i++) {
                Aluno candidato = _alunos.ElementAt(i);
                if (candidato.GetMatricula() == matricula)
                    aluno = candidato;
            }
            return aluno;
        }

        public double PorcentagemAprovados() {
            int totalAprovados = 0;
            foreach (Aluno aluno in _alunos) {
                if (aluno.Aprovado())
                    totalAprovados++;
            }
            return (double)totalAprovados / _alunos.Count;
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder($"Turma {_nome} - ");
            builder.AppendLine(_curso.ToString());
            builder.AppendLine($"Alunos matriculados: {_alunos.Count}");
            builder.Append($"Porcentagem de aprovados: {PorcentagemAprovados():100:F2}%");
            return builder.ToString();

        }

    }
}
