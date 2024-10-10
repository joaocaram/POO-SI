using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src {
    internal interface IPedido {
        public bool PodeAdicionar();
        public double ValorTaxa();
        public string Relatorio();
    }
}
