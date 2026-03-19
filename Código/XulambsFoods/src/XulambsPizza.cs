namespace XulambsFoods {
    public class XulambsPizza {
        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA V0.1");
            Console.WriteLine("==================");
        }

        static int MenuPrincipal() {
            Cabecalho();
            Console.WriteLine("1 - Comprar uma pizza");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Sua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static void ImprimirDadosPizza(Pizza pizza) {
            Console.WriteLine();
            Console.WriteLine("Pizza comprada:\n ");
            Console.WriteLine(pizza.GerarCupom());
            Console.WriteLine("\nDigite enter para continuar.");
            Console.ReadKey();
        }

        static Pizza ComprarPizza() {
            Cabecalho();
            Console.WriteLine("Comprando uma pizza:");
            Console.Write("Quantos ingredientes você deseja (0-8)? ");
            int quantos = int.Parse(Console.ReadLine());
            Pizza novaPizza = new Pizza(quantos);
            ImprimirDadosPizza(novaPizza);
            return novaPizza;
        }

        static void Main(string[] args) {
            int opcao;
            do {
                opcao = MenuPrincipal();
                Pizza? pizza = opcao switch {
                    1 => ComprarPizza(),
                    _ => null
                };
            } while (opcao != 0);
        }
    }

}