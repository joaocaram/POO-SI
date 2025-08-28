namespace XulambsFoods_2025_1.src {
    internal class XulambsPizza {
        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA v0.1 \n====================");
        }

        static void Pausa() {
            Console.WriteLine("Digite enter para continuar...");
            Console.ReadLine();
        }

        static int ExibirMenuPrincipal() {
            Cabecalho();
            Console.WriteLine("1 - Comprar pizza");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static int ExibirMenuIngredientes(Pizza pizza) {
            Cabecalho();
            Console.WriteLine("Personalizar a Pizza\n");
            MostrarNota(pizza);
            Console.WriteLine("\n1 - Acrescentar ingredientes");         
            Console.WriteLine("0 - Não quero alterar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static void ComprarPizza() {
            Cabecalho();
            Console.WriteLine("Comprando uma nova pizza:");
            Pizza novaPizza = new Pizza();
            EscolherIngredientes(novaPizza);
            Console.WriteLine();
            MostrarNota(novaPizza);
        }

        static void EscolherIngredientes(Pizza pizza) {
            int opcao = ExibirMenuIngredientes(pizza);
            if (opcao != 0) { 
                Console.Write("Quantos ingredientes? ");
                int adicionais = int.Parse(Console.ReadLine());
                pizza.AdicionarIngredientes(adicionais);
                Console.WriteLine();
            }
        }

        static void MostrarNota(Pizza pizza) {
            Console.WriteLine("Comprando: ");
            Console.WriteLine(pizza.NotaDeCompra());

        }

        static void Main(string[] args) {
            int opcao = -1;
            do {
                opcao = ExibirMenuPrincipal();
                switch (opcao) {
                    case 1:
                        ComprarPizza();
                        break;
                    case 0: Console.WriteLine("FLW VLW OBG VLT SMP.");
                        break;
                }
                Pausa();
            } while (opcao != 0);
        }
    }
}
