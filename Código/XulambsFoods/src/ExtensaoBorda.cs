using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {
    public static class ExtensaoBorda {
        public static double Preco(this EBorda borda) {
            return borda switch {
                EBorda.TRADICIONAL => 0,
                EBorda.CHEDDAR => 10,
                EBorda.CHOCOLATE => 8,
                EBorda.REQUEIJAO => 7,
            };
        }

        public static string ToString(this EBorda borda) {
            string nome = borda.ToString().ToLower();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nome);
        }
    }
}
