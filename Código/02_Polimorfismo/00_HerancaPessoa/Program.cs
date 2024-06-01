using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerancaPessoa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pessoa[] pessoas = new Pessoa[20];
            pessoas[0] = new Pessoa("Pessoa Muito Feliz", DateOnly.Parse("01-10-2004"), "1");
            pessoas[1] = new Professor("Professor", DateOnly.Parse("05-07-1989"), "Pr1");
            pessoas[2] = new Aluno("Aluno Espantado",
                DateOnly.Parse("04-11-2005"), "Al1");

            foreach(Pessoa p in pessoas)
            {
                if (p != null)
                    Console.WriteLine(p.relatorio());
            }
        }
    }
}
