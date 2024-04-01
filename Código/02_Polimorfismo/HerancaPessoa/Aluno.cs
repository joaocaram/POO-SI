using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerancaPessoa
{
    public class Aluno : Pessoa
    {


    private static double horaAula;
    private double valorMatricula;

    public Aluno(string nome, DateOnly nascimento, string documento):
            base(nome, nascimento, documento)
    {

    }

    public double valorMensalidade()
    {
            return ((cargaHoraria * horaAula) - valorMatricula) / 6.0;
    }

}

}
