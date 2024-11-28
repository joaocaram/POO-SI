
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace XulambsFoods_2024_2.src {
    internal class XulambsFoods {

        static List<Pedido> todosOsPedidos;
        static Dictionary<int, Cliente> todosOsClientes;

        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS");
            Console.WriteLine("=============");
        }

        static int lerInteiro(string mensagem)
        {
            int opcao;
            Console.Write($"{mensagem}: ");
            try
            {
                opcao = int.Parse(Console.ReadLine());
            }
            catch (FormatException fex)
            {
                opcao = -1;
                Console.WriteLine("Favor digitar somente números.");
            }
            return opcao;
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
            Console.WriteLine("5 - Relatório geral de pedidos");
            Console.WriteLine("6 - Relatório geral de clientes");
            Console.WriteLine("0 - Finalizar");

            return  lerInteiro("Digite sua escolha");
            
            
            

        }

        static Pedido EscolherTipoPedido() {
            Pedido novoPedido;
            int escolha;
            Cabecalho();
            
            Console.WriteLine("Abrindo um novo Pedido.");
            Console.WriteLine("1 - Pedido Local.");
            Console.WriteLine("2 - Pedido para Entrega.");

            escolha = lerInteiro("Escolha um tipo de pedido");

            novoPedido = escolha switch {
                -1 => null,
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
                try
                {
                    procurado.Adicionar(novaComida);
                }
                catch (ArgumentNullException anex)
                {
                    Console.WriteLine(anex.Message);
                }
                catch(InvalidOperationException ope) {
                    Console.WriteLine(ope.Message);
                    Pausa();
                    return;
                }
                
                Console.Write("\nDeseja outra comida? (s/n) ");
                escolha = Console.ReadLine();
            } while (escolha.ToLower().Equals("s"));
        }

        private static Comida ComprarComida() {
            Console.WriteLine("Escolha sua opção: ");
            Console.WriteLine("1 - Pizza ");
            Console.WriteLine("2 - Sanduíche");

            int escolha = lerInteiro("Opção");
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

            int adicionais = lerInteiro("Quantos adicionais você deseja?") ;
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
            
            id = lerInteiro("ID do pedido");
            
            foreach (Pedido ped in pedidos)
            {
                if (ped.Relatorio().Contains("Pedido "+ id.ToString("D2")))
                    return ped;
            }
            return null;
        }

        static Cliente LocalizarCliente(Dictionary<int, Cliente> clientes) {
            Cabecalho();
            int id;
            
            id = lerInteiro("ID do Cliente");

            return clientes.GetValueOrDefault(id);
        }

        static void config() {
            gerarClientes();
            gerarPedidos();
        }

        private static void gerarClientes() {
            string[] nomes = File.ReadAllLines("medalhistas.txt"); ;
            foreach (string nome in nomes) {
                Cliente cliente = new Cliente(nome);
                todosOsClientes.Add(cliente.GetHashCode(), cliente);
            }
        }

        private static void gerarPedidos() {
            Random aleat = new Random(42);
            int quantos = todosOsClientes.Count * 13;
            for (int i = 0; i < quantos; i++)
            {
                Pedido novoPedido;
                int tipoPedido = aleat.Next(100)%2;
                
                novoPedido = tipoPedido switch {
                    0 => new Pedido(0),
                    1 => new Pedido(aleat.NextDouble() * 12),
                };
                int quantasComidas = aleat.Next(1, 3);
                for (int j = 0; j < quantasComidas; j++)
                {
                    Comida novaComida;
                    int tipoComida = aleat.Next(1, 4);
                    int ingredientes = aleat.Next(6);
                    novaComida = tipoComida switch {
                        1 => new Pizza(ingredientes),
                        2 => new Sanduiche(ingredientes, true),
                        3 => new Sanduiche(ingredientes, false)
                    };
                    try
                    {
                        novoPedido.Adicionar(novaComida);
                    } catch(ArgumentNullException anex)
                    {
                        Console.WriteLine(anex.Message);
                    }
                }

                novoPedido.FecharPedido();
                int id = aleat.Next(1, todosOsClientes.Count+1);
                todosOsPedidos.Add(novoPedido);
                Cliente cliente = todosOsClientes.GetValueOrDefault(id);
                cliente.RegistrarPedido(novoPedido);
            }           
        }

        static void Main(string[] args) {
            
            todosOsPedidos = new List<Pedido>();
            todosOsClientes = new Dictionary<int, Cliente>();
            config();

            Pedido pedido;
            Cliente cliente;
            int opcao = -1;
            do {
                opcao = ExibirMenu();
                switch (opcao) {
                    case 1:
                        pedido = AbrirPedido();
                        todosOsPedidos.Add(pedido);
                        RelatorioPedido(pedido);
                        Cliente quem = LocalizarCliente(todosOsClientes);
                        String mensagem = "Cliente não existente. Pedido registrado como anônimo";
                        if (quem != null) {
                            quem.RegistrarPedido(pedido);
                            mensagem = $"Pedido registrado para {quem}";
                        }
                        Console.WriteLine(mensagem);
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
                    case 5:
                        IComparable[] pedidosOrd = todosOsPedidos.ToArray();
                        Ordenador qs = new Ordenador(pedidosOrd);
                        pedidosOrd = qs.ordenar();
                        Cabecalho();
                        Console.WriteLine("Pedidos:");
                        foreach (IComparable item in pedidosOrd)
                        {
                            Console.WriteLine(item);
                        }
                        break;
                    case 6:
                        IComparable[] clientesOrd = todosOsClientes.Values.ToArray();
                        Ordenador qsCliente = new Ordenador(clientesOrd);
                        clientesOrd= qsCliente.ordenar();
                        Cabecalho();
                        Console.WriteLine("Clientes:");
                        foreach (IComparable item in clientesOrd) {
                            Console.WriteLine(item);
                        }
                        break;

                }
                Pausa();
            } while (opcao != 0);

        }


    }
}