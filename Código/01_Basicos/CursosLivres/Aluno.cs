
using System.Text;

namespace CursosLivres {
    /** 
    * MIT License
    *
    * Copyright(c) 2026 João Caram <caram@pucminas.br>
    *
    * Permission is hereby granted, free of charge, to any person obtaining a copy
    * of this software and associated documentation files (the "Software"), to deal
    * in the Software without restriction, including without limitation the rights
    * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    * copies of the Software, and to permit persons to whom the Software is
    * furnished to do so, subject to the following conditions:
    *
    * The above copyright notice and this permission notice shall be included in all
    * copies or substantial portions of the Software.
    *
    * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    * SOFTWARE.
*/

    ///
    ///Classe simples Aluno para demonstração de conceitos básicos de POO
    ///
    public class Aluno {
        
        private const int QuantAvaliacoes = 4;
        private const double NotaAprovacao = 60d;
        private const double FreqMinima = 0.75;

        private static int s_proxMatricula = 10_000;

        //#region atributos
        private string _nome;
        private int _matricula;
        private Curso _curso;
        private List<double> _notas;
        private int _faltas;
        //#endregion

        /// <summary>
        /// Cria um novo aluno com o nome definido pelo usuário(nome sem validação). 
        /// A matrícula é atribuída sequencial e automaticamente utilizando-se o atributo
        /// static definido para a classe(início em 10.000)
        /// </summary>
        /// <param name="nome">Nome do aluno(sem validação)</param>
        public Aluno(string nome) {
            _nome = nome;
            _matricula = s_proxMatricula++; ;
            _notas = new List<double>(QuantAvaliacoes);
            _faltas = 0;
            _curso = null;
        }

        public bool Matricular(Curso curso) {
            bool matriculou = false;
            if(curso!= null) {
                if (_curso != null) {
                    _faltas = 0;
                    _notas = new List<double>(QuantAvaliacoes);
                }
                _curso = curso;
                matriculou = true;
            }
            return matriculou;
            
        }
        
        /// <summary>
        /// Tenta lançar uma nota de uma atividade para o aluno. Em caso de inválida ou todas as atividades já terem sido feitas, ignora a operação. Retorna a nota final até o momento. 
        /// </summary>
        /// <param name="valor">Nota da atividade(double >=0)</param>
        /// <returns>Soma das notas até o momento </returns>
        public double LancarNota(double valor) {
            if (_curso != null && _notas.Count < QuantAvaliacoes && valor >= 0)
                _notas.Add(valor);
            
            return NotaFinal();
        }

        /// <summary>
        /// Lança uma falta para o aluno, respeitando o máximo de aulas do curso.
        /// </summary>
        /// <returns>Total de faltas do aluno, incluindo esta</returns>
        public int LancarFalta() {
            if (_curso != null && _faltas < _curso.QuantidadeAulas())
                _faltas++;

            return _faltas;
        }

        /// <summary>
        /// Soma de todas as notas do aluno(nota total do aluno até o momento)
        /// </summary>
        /// <returns>Double com a soma das notas do aluno(>=0)</returns>
        public double NotaFinal() {
            double soma = 0d;
            foreach (double nota in _notas) {
                soma += nota;
            }
            return soma;
        }

        /// <summary>
        /// Calcula a frequência do aluno no curso atual (em %).
        /// Se o aluno não estiver matriculado, a resposta é 0.
        /// </summary>
        /// <returns>Double com a frequência do aluno, entre 0 e 1 (100%)</returns>
        public double Frequencia() {
            double resposta = 0d;
            if(_curso != null) {
                resposta = 1 - ((double)_faltas / _curso.QuantidadeAulas());
            }
                          
            return resposta;
        }

        /// <summary>
        /// Indica se o aluno já obteve nota acima do mínimo para aprovação.
        /// </summary>
        /// <returns>TRUE se a nota total atual ultrapassa o mínimo para aprovação, FALSE caso contrário</returns>
        public bool Aprovado() {
            return NotaFinal() >= NotaAprovacao && Frequencia() >= FreqMinima;
        }

        /// <summary>
        /// Método de acesso para a matrícula
        /// </summary>
        /// <returns>Int com a matrícula do aluno</returns>
        public int GetMatricula() {
            return _matricula;
        }

        /// <summary>
        /// Produz um relatório para o aluno com nome, matrícula e, se estiver em um curso, nota, frequência e aprovação. 
        /// </summary>
        /// <returns>String com os dados descritos acima, incluindo quebras de linha intermediárias.</returns>
        public override string ToString() {

            string situacao = "não aprovado.";
            
            StringBuilder relat = new StringBuilder($"Aluno {_nome} ({_matricula})\n");
            if(_curso != null) {
                relat.AppendLine($"Nota: {NotaFinal():F2}");
                relat.AppendLine($"Frequência: {Frequencia()*100:F2}%");
                if (Aprovado())
                    situacao = "aprovado.";
                relat.Append("Situação atual: " + situacao);
            }
            
            return relat.ToString();
        }
    }
}

