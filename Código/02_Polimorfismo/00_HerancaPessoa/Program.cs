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
namespace HerancaPessoa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Pessoa> pessoas = new List<Pessoa>(20);
            
            Pessoa aluno = new Aluno("Aluno Espantado", DateOnly.Parse("04-11-2006"), "Al1", "aluno@aluno.com", 1250d);
            aluno.SetCargaHoraria(430);

            Pessoa prof = new Professor("Professor", DateOnly.Parse("05-07-1989"), "Pr1", "prof@prof.com");
            prof.SetCargaHoraria(32);

            pessoas.Add(new Pessoa("Pessoa Feliz", DateOnly.Parse("05-04-2004"), "Pe1", "pessoa@pessoa.com"));
            pessoas.Add(aluno);
            pessoas.Add(prof);

            Console.WriteLine("Todo mundo:");
            foreach (Pessoa p in pessoas)
            {
                    Console.WriteLine(p.Relatorio());
            }

            Console.WriteLine("\nMensalidade do aluno: (CUIDADO!!!!)");  
            double valor = ((Aluno)pessoas[1]).ValorMensalidade();
            Console.WriteLine($"{valor:C2}");

        }
    }
}
