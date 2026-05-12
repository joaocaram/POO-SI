/** 
 * MIT License
 *
 * Copyright(c) 2025 João Caram <caram@pucminas.br>
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
namespace HerancaPessoa {
    public class Administrativo : Pessoa {

        private static double _salarioBase;
        private double _valorGratificacao;

        public Administrativo(string nome, DateOnly nascimento, string documento, string email, double salario) :
                base(nome, nascimento, documento, email) {
            if (salario > 0)
                _salarioBase = salario;
        }

        public double Salario() {
            return _salarioBase + _valorGratificacao;
        }

        /// <summary>
        /// Resumo do administrativo: nome, idade (vindo da classe mãe) e salário.
        /// </summary>
        /// <returns>String de uma linha com as informações acima</returns>
        public override string Relatorio() {
            return $"{base.Relatorio()}, com salário {Salario():C2} ({_salarioBase:C2} + {_valorGratificacao:C2}).";
        }

    }
}
