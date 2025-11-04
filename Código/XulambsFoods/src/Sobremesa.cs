using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src
{
    public class Sobremesa : IProduto
    {
        private ESobremesa _tipo;

        public Sobremesa(ESobremesa tipo)
        {
            _tipo = tipo;
        }
        
        public double ValorFinal()
        {
            return _tipo.Preco();
        }

        public override string ToString()
        {
            return $"{_tipo.ToString().Replace("_", " ")}: " +
                $"{ValorFinal():C2}";
        }
    }
}
