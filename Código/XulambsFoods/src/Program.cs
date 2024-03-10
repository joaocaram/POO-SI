namespace XulambsFoods.src
{
    internal class Program
    {
        static void testeComida() {
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

        static void testePedido() {
            Pedido ped = new Pedido();
            Comida pizza = new Comida("PizzA", 3);

            ped.addComida(pizza);

            for(int i=0; i<12; i++) {
                pizza = new Comida("PIZZa", i / 2);
                ped.addComida(pizza);
            }

            Console.WriteLine(ped.relatorio());     // Pedido nº1 com 10 pizzas (limite de comidas)

            ped = new Pedido();
            ped.addComida(pizza);
            ped.fecharPedido();
            ped.addComida(pizza);

            Console.WriteLine(ped.relatorio());     // Pedido nº2 com 1 pizza, pois estava fechado

        }

        static void Main(string[] args)
        {

            testePedido();
            Console.ReadKey();

        }
    }
}
