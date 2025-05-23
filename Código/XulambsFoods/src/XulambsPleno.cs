using System;
using System.Collections.Generic;
using System.Text;

namespace XulambsFoods_2025_1.src 
{
	public class XulambsPleno : IFidelidade
	{
		private const double PctDesconto = 0.1;
		public const int MinimoPedidos = 10;
		public const double MinimoMedia = 29;

		

        public override string ToString() {
            return "Cliente Xulambs Pleno";
        }
    }
}
