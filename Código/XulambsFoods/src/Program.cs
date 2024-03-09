namespace XulambsFoods.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Comida comida = new Comida("Pizza", 3);
            Console.WriteLine(comida.relatorio());
        }
    }
}
