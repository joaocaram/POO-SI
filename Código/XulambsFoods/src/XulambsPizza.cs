
using System.Reflection.PortableExecutable;

namespace XulambsFoods_2024_2.src {
    internal class XulambsPizza {

        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA");
            Console.WriteLine("=============");
        }

        static void Pausa()
        {
            Console.WriteLine("\nDigite ENTER para continuar.");
            Console.ReadKey();
        }
        static int ExibirMenu() {
            Cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Alterar Pedido");
            Console.WriteLine("3 - Relatório de Pedido");
            Console.WriteLine("4 - Encerrar Pedido");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }
        static Pedido EscolherTipoPedido() {
            Pedido novoPedido;
            int escolha;
            Cabecalho();
            
            Console.WriteLine("Abrindo um novo Pedido.");
            Console.WriteLine("1 - Pedido Local.");
            Console.WriteLine("2 - Pedido para Entrega.");
            Console.Write("Escolha um tipo de pedido: ");
            escolha = int.Parse(Console.ReadLine());

            novoPedido = escolha switch {
                2 => CriarPedidoEntrega(),
                1 or _ => new PedidoLocal()
           };

            return novoPedido;

        }

        private static Pedido CriarPedidoEntrega() {
            double dist;
            Console.WriteLine("\nPedido para Entrega.");
            Console.Write("Qual a distância da entrega? ");
            dist = double.Parse(Console.ReadLine());
            return new PedidoEntrega(dist);
        }

        static Pedido AbrirPedido() {
            Cabecalho();
            Pedido novoPedido = EscolherTipoPedido();   
            Console.WriteLine(novoPedido.Relatorio());
            Pausa();
            AdicionarPizzas(novoPedido);
            return novoPedido;
        }

        private static void AdicionarPizzas(Pedido procurado) {
            String escolha = "n";
            do {
                RelatorioPedido(procurado);
                Comida novaPizza = ComprarPizza();
                procurado.Adicionar(novaPizza);
                Console.Write("\nDeseja outra pizza? (s/n) ");
                escolha = Console.ReadLine();
            } while (escolha.ToLower().Equals("s"));
        }

        static Comida ComprarPizza() {
            Console.WriteLine("\nComprando uma nova pizza:");
            Comida novaPizza = new Pizza();
            EscolherIngredientes(novaPizza);
            MostrarNota(novaPizza);
            
            return novaPizza;
        }

        static void EscolherIngredientes(Comida pizza) {
            Console.Write("Quantos adicionais você deseja? (máx. 8): ");
            int adicionais = int.Parse(Console.ReadLine());
            pizza.AdicionarIngredientes(adicionais);
        }

        static void MostrarNota(Comida pizza) {
            Console.WriteLine("\n"+pizza.NotaDeCompra());
        }
        static void RelatorioPedido(Pedido pedido)
        {
            Cabecalho();
            Console.WriteLine(pedido.Relatorio() + "\n");
        }
        static Pedido LocalizarPedido(List<Pedido> pedidos) {
            Cabecalho();
            int id;
            Console.WriteLine("Localizando um pedido.");
            Console.Write("ID do pedido: ");
            id = int.Parse(Console.ReadLine());
            
            foreach (Pedido ped in pedidos)
            {
                if (ped.Relatorio().Contains("Pedido "+ id.ToString("D2")))
                    return ped;
            }
            return null;
        }



        static void Main(string[] args) {
            List<Pedido> todosOsPedidos = new List<Pedido>();
            Pedido pedido;
            int opcao = -1;
            do {
                opcao = ExibirMenu();
                switch (opcao) {
                    case 1:
                        pedido = AbrirPedido();
                        todosOsPedidos.Add(pedido);
                        RelatorioPedido(pedido);
                        
                        break;
                    case 2:
                        pedido = LocalizarPedido(todosOsPedidos);
                        if (pedido!= null)
                            AdicionarPizzas(pedido);
                        else
                            Console.WriteLine("Pedido não existente.");
                        break;
                    case 3:
                        pedido = LocalizarPedido(todosOsPedidos);
                        if (pedido != null)
                            Console.WriteLine(pedido.Relatorio());
                        else
                            Console.WriteLine("Pedido não existente.");
                        break;
                    case 4:
                        pedido = LocalizarPedido(todosOsPedidos);
                        if (pedido != null) {
                            pedido.FecharPedido();
                            Console.WriteLine("Pedido encerrado: ");
                            Console.WriteLine(pedido.Relatorio());
                        }
                        else
                            Console.WriteLine("Pedido não existente.");
                        break;
                        
                }
                Pausa();
            } while (opcao != 0);

        }


    }
}