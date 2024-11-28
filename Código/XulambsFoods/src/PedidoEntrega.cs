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
        private List<Comida> _comidas;

        public PedidoEntrega(double distancia) 
        {

            if (distancia < 0.1) distancia = 0.1;
            _distanciaEntrega = distancia;
            _comidas = new List<Comida>(MaxEntrega);
        }
        
        public int Adicionar(Comida comida)
        {
            if (_comidas.Count == MaxEntrega)
                throw new PedidoLotadoException($"Máximo de {MaxEntrega} comidas em pedido para entrega.");
            _comidas.Add(comida);
            return _comidas.Count;
        }
       
        
        public double ValorItens() {
            return _comidas.Sum(c => c.ValorFinal());
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
            Ordenador qs = new Ordenador(_comidas.ToArray());
            foreach (Comida comida in qs.ordenar()) {
                sb.AppendLine($"{(i):D2} - {comida.NotaDeCompra()}\n");
                i++;
            }
            sb.AppendLine($"TAXA ENTREGA : {ValorTaxa():C2}");
            return sb.ToString();
        }
    }
}
