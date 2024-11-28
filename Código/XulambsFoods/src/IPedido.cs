using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src {
    public interface IPedido {
        public int Adicionar(Comida comida);
        public double ValorTaxa();
        
        public double ValorItens();
        public string Relatorio();
    }
}
