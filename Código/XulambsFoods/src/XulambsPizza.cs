namespace XulambsFoods_2025_1.src {
    internal class XulambsPizza {
        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA v0.2\n================");
        }

        static void Pausa() {
            Console.WriteLine("Digite enter para continuar...");
            Console.ReadLine();
        }

        static int ExibirMenuPrincipal() {
            Cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }
        static Pedido AbrirPedido() {
            Pedido novo = new Pedido();
            string conf;
            do {
                Pizza novaPizza = ComprarPizza();
                novo.Adicionar(novaPizza);
                Console.Write("\nQuer uma nova pizza (S/N)? ");
                conf = Console.ReadLine().ToUpper();
            } while (conf.Equals("S"));
            return novo;
        }

        static int ExibirMenuIngredientes(Pizza pizza) {
            Cabecalho();
            Console.WriteLine("Personalizar a Pizza\n");
            MostrarNota(pizza);
            Console.WriteLine("\n1 - Acrescentar ingredientes");
            Console.WriteLine("2 - Retirar ingredientes");
            Console.WriteLine("0 - Não quero alterar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static Pizza ComprarPizza() {
            Cabecalho();
            Console.WriteLine("Comprando uma nova pizza:");
            Pizza novaPizza = new Pizza();
            EscolherIngredientes(novaPizza);
            Console.WriteLine();
            MostrarNota(novaPizza);
            return novaPizza;
        }

        static void EscolherIngredientes(Pizza pizza) {
            int opcao = ExibirMenuIngredientes(pizza);
            while(opcao!=0){
                Console.Write("Quantos ingredientes? ");
                int adicionais = int.Parse(Console.ReadLine());
                switch(opcao) {
                    case 1: pizza.AdicionarIngredientes(adicionais);
                        break;
                    case 2: pizza.RetirarIngredientes(adicionais);
                        break;
                };
                Console.WriteLine();
                MostrarNota(pizza);
                Pausa();
                opcao = ExibirMenuIngredientes(pizza);
            } 
            
        }

        static void MostrarNota(Pizza pizza) {
            Console.WriteLine("Comprando: ");
            Console.WriteLine(pizza.NotaDeCompra());

        }

        static void MostrarPedido(Pedido pedido) {
            Cabecalho();
            Console.WriteLine(pedido.Relatorio());
        }

        static void Main(string[] args) {
            int opcao = -1;
            do {
                opcao = ExibirMenuPrincipal();
                switch (opcao) {
                    case 1:
                        Pedido novo = AbrirPedido();
                        MostrarPedido(novo);
                        break;
                    case 0: Console.WriteLine("FLW VLW OBG VLT SMP.");
                        break;
                }
                Console.ReadKey();
            } while (opcao != 0);
        }
    }
}
