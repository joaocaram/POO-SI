using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerancaPessoa
{
    public class Administrativo : Pessoa
    {


    private static double salarioBase;
    private double valorGratificacao;

    public Administrativo(string nome, DateOnly nascimento, string documento, string email): 
            base(nome, nascimento, documento, email)
    {

    }

    public double salarioBruto()
    {
            return salarioBase + valorGratificacao;
    }

}
}
