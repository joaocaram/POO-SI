using System.Globalization;

namespace XulambsFoods_2025_1.src
{
    public static class ExtensaoBorda
    {
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