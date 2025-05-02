using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src
{
    internal class MaximoDeIngredientesException : InvalidOperationException
    {
        private int quantidade;
        public MaximoDeIngredientesException(int quantos) :
            base("O máximo de ingredientes da comida foi atingido")
        {
            quantidade = quantos;
        }

        public int getQuant()
        {
            return quantidade;
        }
    }
}
