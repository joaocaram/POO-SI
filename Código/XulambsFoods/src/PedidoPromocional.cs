using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src
{
    public class PedidoPromocional : Pedido
    {
        const int  ComidasParaDesconto = 5;

        public PedidoPromocional(): base(null) {

        }
        private double ValorDesconto()
        {
            double desconto = 0d;
            if(_comidas.Count >= ComidasParaDesconto)
            {
                Comida menor = _comidas.First.Value;
                foreach (Comida item in _comidas)
                {
                    if (item.CompareTo(menor) < 0)
                        menor = item;
                }
                desconto = menor.ValorFinal();
            }
            return desconto;
        }
        public override double PrecoAPagar()
        {
            return ValorItens() - ValorDesconto();
        }
    }
}
