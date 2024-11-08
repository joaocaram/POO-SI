using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    internal class PedidoEntrega : IPedido
    {
        private const int MaxEntrega = 6;
        private readonly double[] TaxasEntrega = { 0, 5, 8 };
        private readonly double[] DistanciasEntrega = { 4, 8, double.MaxValue };

        private double _distanciaEntrega;
        private BaseDados<Comida> _comidas;

        public PedidoEntrega(BaseDados<Comida> comidas, double distancia) 
        {

            if (distancia < 0.1) distancia = 0.1;
            _distanciaEntrega = distancia;
            _comidas = comidas;
        }
        
        public  bool PodeAdicionar()
        {
            return (_comidas.Quantidade() < MaxEntrega);
        }
       
        public double ValorTaxa()
        {
            double taxa = 0d;
            for (int i = DistanciasEntrega.Length - 1; i >= 0; i--)
            {
                if (_distanciaEntrega <= DistanciasEntrega[i])
                    taxa = TaxasEntrega[i];
            }
            return taxa;
        }

        public string Relatorio()
        {
            StringBuilder sb = new StringBuilder(" - ENTREGA\n");
            sb.AppendLine("=============================");
            int i = 1;

            sb.AppendLine(_comidas.RelatorioOrdenado());
            
            sb.AppendLine($"TAXA ENTREGA : {ValorTaxa():C2}");
            return sb.ToString();
        }
    }
}
