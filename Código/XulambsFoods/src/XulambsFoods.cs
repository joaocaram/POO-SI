
using System.Reflection.PortableExecutable;

namespace XulambsFoods_2024_2.src {
    internal class XulambsFoods {

        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS");
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
                1 or _ => new Pedido(0)
            }; 

            return novoPedido;

        }

        private static Pedido CriarPedidoEntrega() {
            double dist;
            Console.WriteLine("\nPedido para Entrega.");
            Console.Write("Qual a distância da entrega? ");
            dist = double.Parse(Console.ReadLine());
            return new Pedido(dist);
        }

        static Pedido AbrirPedido() {
            Cabecalho();
            Pedido novoPedido = EscolherTipoPedido();   
            Console.WriteLine("\n"+novoPedido.Relatorio());
            Pausa();
            AdicionarComidas(novoPedido);
            return novoPedido;
        }

        private static void AdicionarComidas(Pedido procurado) {
            string escolha;
            do {
                RelatorioPedido(procurado);
                Comida novaComida = ComprarComida();
                EscolherIngredientes(novaComida);
                MostrarNota(novaComida);
                procurado.Adicionar(novaComida);
                Console.Write("\nDeseja outra comida? (s/n) ");
                escolha = Console.ReadLine();
            } while (escolha.ToLower().Equals("s"));
        }

        private static Comida ComprarComida() {
            Console.WriteLine("Escolha sua opção: ");
            Console.WriteLine("1 - Pizza ");
            Console.WriteLine("2 - Sanduíche");
            Console.Write("Opção: ");
            int escolha = int.Parse(Console.ReadLine());
            return escolha switch {
                1 => ComprarPizza(),
                2 or _ => ComprarSanduiche(),
            };
        }

        static Pizza ComprarPizza() {
            Console.WriteLine("\nComprando uma nova pizza:");
            Pizza novaPizza = new Pizza();
            return novaPizza;
        }

        static Sanduiche ComprarSanduiche() {
            Console.WriteLine("\nComprando um novo sanduíche:");
            Console.Write("Deseja combo com fritas? (s/n) ");
            string escolha = Console.ReadLine();
            bool querCombo = (escolha.ToLower().Equals("s")) ? true : false;
            return new Sanduiche(querCombo);
        }

        static void EscolherIngredientes(Comida comida) {
            Console.Write("Quantos adicionais você deseja? ");
            int adicionais = int.Parse(Console.ReadLine());
            comida.AdicionarIngredientes(adicionais);
        }

        static void MostrarNota(Comida comida) {
            Console.WriteLine("\n"+comida.NotaDeCompra());
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
            Console.OutputEncoding = System.Text.Encoding.Unicode;
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
                            AdicionarComidas(pedido);
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