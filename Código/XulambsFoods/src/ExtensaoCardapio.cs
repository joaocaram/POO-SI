using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src
{
    public static class ExtensaoCardapio
    {
        public static double Preco(this ESobremesa sobre)
        {
            return sobre switch
            {
                ESobremesa.Brigadeiro => 8,
                ESobremesa.Pudim => 10,
                ESobremesa.Doce_de_leite => 9
            };
        }
        public static double Preco(this EBorda borda)
        {
            return borda switch
            {
                EBorda.TRADICIONAL => 0,
                EBorda.CHEDDAR => 10,
                EBorda.CHOCOLATE => 8,
                EBorda.REQUEIJAO => 7
            };
        }

        public static string Nome(this EBorda borda)
        {
            string nomeBorda = borda.ToString().ToLower();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomeBorda);
        }
    }
}