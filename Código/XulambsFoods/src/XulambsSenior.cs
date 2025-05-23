using System;
using System.Collections.Generic;
using System.Text;

namespace XulambsFoods_2025_1.src 
{
	public class XulambsSenior : IFidelidade
	{
		private const double PctDesconto = 0.2;
		public const int MinimoPedidos = 20;
		public const double MinimoMedia = 44;
		private const int ComprasCupom = 5;
		private const double ValorCupom = 10;
		private int _contadorPedidos;

		

        public override string ToString() {
            return "Cliente Xulambs Senior";
        }
    }
}
