using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HerancaPessoa
{
    public class Professor : Pessoa
    {


    private static double horaAula = 20.0;
    public Professor(string nome, DateOnly nascimento, string documento):
            base (nome, nascimento, documento)
    {
            this.cargaHoraria = 20;
    }

    public double salarioBruto()
    {
            return horaAula * cargaHoraria * 1.20;
    }

    public override string relatorio()
        {
            return nome + ": " + idade() + " anos, salário R$ " + salarioBruto();
        }

}
}
