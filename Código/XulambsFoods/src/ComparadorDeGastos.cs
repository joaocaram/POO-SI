using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src {
    public class ComparadorDeGastos : Comparer<Cliente> {
        public override int Compare(Cliente? x, Cliente? y) {
            return (x.TotalGasto() - y.TotalGasto() > 0) ? 1 : -1;
        }
    }
}
