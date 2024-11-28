using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src
{
    internal class PedidoEntrega : IPedido
    {
        private const int MaxEntrega = 2;
        private readonly double[] TaxasEntrega = { 0, 5, 8 };
        private readonly double[] DistanciasEntrega = { 4, 8, double.MaxValue };

        private double _distanciaEntrega;
        private BaseDados<Comida> _comidas;

        
        public PedidoEntrega(double distancia) 
        {

            if (distancia < 0.1) distancia = 0.1;
            _distanciaEntrega = distancia;
            _comidas = new BaseDados<Comida>();
        }
        
        public int Adicionar(Comida comida)
        {
            if (_comidas.Quantidade() == MaxEntrega)
                throw new PedidoLotadoException($"Máximo de {MaxEntrega} comidas em pedido para entrega.");
            _comidas.Add(comida);
            return _comidas.Quantidade();
        }
       
        
        public double ValorItens() {
            return _comidas.Totalizador(c => c.ValorFinal());
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
