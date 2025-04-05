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
