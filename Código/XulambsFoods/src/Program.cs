namespace XulambsFoods.src
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Comida comida = new Comida("Pizza", 3);
            Console.WriteLine(comida.relatorio());      // 3 ingredientes, R$41
            
            comida.adicionarIngredientes(4);            // 7 ingredientes, R$57 
            Console.WriteLine(comida.relatorio());
            
            comida.adicionarIngredientes(4);        
            Console.WriteLine(comida.relatorio());      // 7 ingredientes, R$57, não pode ultrapassar o limite.

            comida = new Comida("sanDUICHE");
            Console.WriteLine(comida.relatorio());      // 0 ingredientes, R$15

            comida.adicionarIngredientes(4);            // 4 ingredientes, R$23
            Console.WriteLine(comida.relatorio());

            comida.adicionarIngredientes(4);
            Console.WriteLine(comida.relatorio());      //4 ingredientes, R$23, não pode ultrapassar o limite

            comida = new Comida();
            Console.WriteLine(comida.relatorio());      //0 ingredientes, R$29

        }
    }
}
