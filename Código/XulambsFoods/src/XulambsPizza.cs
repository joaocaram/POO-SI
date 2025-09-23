namespace XulambsFoods_2025_1.src {
    public class XulambsPizza {

        const int MaxPedidos = 100;
        static Pedido[] pedidos = new Pedido[MaxPedidos];
        static int quantPedidos = 0;

        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA v0.3\n================");
        }

        static void Pausa() {
            Console.WriteLine("Digite enter para continuar...");
            Console.ReadLine();
        }

        static int ExibirMenuPrincipal() {
            Cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Alterar Pedido");
            Console.WriteLine("3 - Relatório de Pedido");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static void AdicionarPizzas(Pedido pedido) {
            string conf;
            do {
                Pizza novaPizza = ComprarPizza();
                pedido.Adicionar(novaPizza);
                Console.Write("\nQuer uma nova pizza (S/N)? ");
                conf = Console.ReadLine().ToUpper();
            } while (conf.Equals("S"));
        }

        static Pedido AbrirPedido() {
            Pedido novoPedido = EscolherTipoPedido();
            AdicionarPizzas(novoPedido);
            return novoPedido;
        }

        static Pedido EscolherTipoPedido() {
            int opcao = ExibirMenuTipoPedido();    
            return opcao switch {
                2 => CriarPedidoEntrega(),
                1 or _ => CriarPedidoLocal()
            };
        }

        static int ExibirMenuTipoPedido() {
            Cabecalho();
            Console.WriteLine("Escolha o tipo de pedido:");
            Console.WriteLine("1 - Local (padrão)");
            Console.WriteLine("2 - Pedido para entrega");
            Console.Write("Sua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static Pedido CriarPedidoLocal() {
            return new Pedido();
        }

        static Pedido CriarPedidoEntrega() {
            Console.WriteLine("Pedido para Entrega.");
            Console.Write("Distância: ");
            double dist = double.Parse(Console.ReadLine());
            return new PedidoEntrega(dist);
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
            while (opcao != 0) {
                Console.Write("Quantos ingredientes? ");
                int adicionais = int.Parse(Console.ReadLine());
                switch (opcao) {
                    case 1:
                        pizza.AdicionarIngredientes(adicionais);
                        break;
                    case 2:
                        pizza.RetirarIngredientes(adicionais);
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

        static void ArmazenarPedido(Pedido pedido) {
            if (quantPedidos < MaxPedidos) {
                pedidos[quantPedidos] = pedido;
                quantPedidos++;
            }
        }

        static Pedido LocalizarPedido() {
            Cabecalho();
            Console.WriteLine("Localizando um pedido");
            Console.Write("Digite o número do pedido: ");
            int numero = int.Parse(Console.ReadLine());
            Pedido localizado = null;

            for (int i = 0; i < quantPedidos; i++) {
                if (pedidos[i].GetID() == numero)
                    localizado = pedidos[i];
            }
            return localizado;
        }

        static void AlterarPedido() {
            Pedido pedidoParaAlteracao = LocalizarPedido();
            if (pedidoParaAlteracao != null) {
                AdicionarPizzas(pedidoParaAlteracao);
                MostrarPedido(pedidoParaAlteracao);
            }
            else
                Console.WriteLine("Pedido não encontrado");
        }

        static void FecharPedido()
        {
            Pedido pedidoParaFechar = LocalizarPedido();
            if (pedidoParaFechar != null)
            {
                pedidoParaFechar.FecharPedido();
                Console.WriteLine(pedidoParaFechar.Relatorio());
            }
            else
                Console.WriteLine("Pedido não encontrado");

        }

        static void RelatorioDePedido() {
            Pedido localizado = LocalizarPedido();
            if (localizado != null)
                MostrarPedido(localizado);
            else
                Console.WriteLine("Pedido não existente");
        }


        static void Main(string[] args) {
            int opcao = -1;
            do {
                opcao = ExibirMenuPrincipal();
                switch (opcao) {
                    case 1:
                        Pedido novoPedido = AbrirPedido();
                        MostrarPedido(novoPedido);
                        ArmazenarPedido(novoPedido);
                        break;
                    case 2:
                        AlterarPedido();
                        break;
                    case 3:
                        RelatorioDePedido();
                        break;
                    case 4:
                        FecharPedido();
                        break;
                    case 0:
                        Console.WriteLine("FLW VLW OBG VLT SMP.");
                        break;
                }
                Console.ReadKey();
            } while (opcao != 0);
        }
    }
}
