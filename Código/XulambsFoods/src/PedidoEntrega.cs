using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    internal class PedidoEntrega : Pedido
    {
        private const int MaxEntrega = 6;
        private readonly double[] TaxasEntrega = { 0, 5, 8 };
        private readonly double[] DistanciasEntrega = { 4, 8, double.MaxValue };

        private double _distanciaEntrega;

        public PedidoEntrega(double distancia) : base(MaxEntrega)
        {
            if (distancia < 0.1) distancia = 0.1;
            _distanciaEntrega = distancia;
        }
        protected  override bool PodeAdicionar()
        {
            return _aberto && (_quantPizzas < MaxEntrega);
        }
       
        protected override double ValorTaxa()
        {
            double taxa = 0d;
            for (int i = DistanciasEntrega.Length - 1; i >= 0; i--)
            {
                if (_distanciaEntrega <= DistanciasEntrega[i])
                    taxa = TaxasEntrega[i];
            }
            return taxa;
        }

        public override string Relatorio()
        {
            StringBuilder relat = new StringBuilder("XULAMBS PIZZA - Pedido ");
            relat.AppendLine($"{_idPedido:D2} - {_data.ToShortDateString()} - ENTREGA");
            relat.AppendLine("=============================");

            for (int i = 0; i < _quantPizzas; i++)
            {
                relat.AppendLine($"{(i + 1):D2} - {_pizzas[i].NotaDeCompra()}");
            }
            relat.AppendLine($"\nTAXA ENTREGA : {ValorTaxa():C2}");
            relat.AppendLine($"TOTAL A PAGAR: {PrecoAPagar():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }
    }
}
