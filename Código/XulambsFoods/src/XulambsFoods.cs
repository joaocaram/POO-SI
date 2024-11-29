
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace XulambsFoods_2024_2.src {
    internal class XulambsFoods {

        static BaseDados<Pedido> todosOsPedidos;
        static BaseDados<Cliente> baseClientes;
        static List<Pedido> pedidosLista;
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
            catch (FormatException)
            {
                opcao = -1;
                Console.WriteLine("Favor digitar somente números.");
            }
            catch (OverflowException) {
                opcao = -1;
                Console.WriteLine("Favor digitar somente valores do menu.");
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
            Console.WriteLine("6 - Relatório geral de clientes (nome)");
            Console.WriteLine("7 - Relatório geral de clientes (gasto)");
            Console.WriteLine("8 - Total vendido em pedidos");
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
                procurado.Adicionar(novaComida);
             
                Console.Write("\nDeseja outra comida? (s/n) ");
                escolha = Console.ReadLine();
            } while (escolha.ToLower().Equals("s"));
        }

        private static Comida? ComprarComida() {
            Console.WriteLine("Escolha sua opção: ");
            Console.WriteLine("1 - Pizza ");
            Console.WriteLine("2 - Sanduíche");

            int escolha = lerInteiro("Opção");
            return escolha switch {
                1 => ComprarPizza(),
                2 => ComprarSanduiche(),
                _ => null
            };
        }

        static Comida ComprarPizza() {
            Console.WriteLine("\nComprando uma nova pizza:");
            return new Pizza();
            
        }

        static Comida ComprarSanduiche() {
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

        static Pedido LocalizarPedido(BaseDados<Pedido> pedidos) {
            Cabecalho();
            int id;
            Console.WriteLine("Localizando um pedido.");
            
            id = lerInteiro("ID do pedido");

            return pedidos.Localizar(id);
        }

        static Cliente LocalizarCliente(BaseDados<Cliente> clientes) {
            Cabecalho();
            int id;
            
            id = lerInteiro("ID do Cliente");

            return clientes.Localizar(id);
        }

        static void config() {
            gerarClientes();
            gerarPedidos();
        }

        private static void gerarClientes() {
            string[] nomes = File.ReadAllLines("medalhistas.txt"); ;
            foreach (string nome in nomes) {
                Cliente cliente = new Cliente(nome);
                baseClientes.Add(cliente);
                
            }
        }

        private static void gerarPedidos() {
            Random aleat = new Random(42);
            int quantos = baseClientes.Quantidade() * 13;
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
                int id = aleat.Next(1, baseClientes.Quantidade()+1);
                todosOsPedidos.Add(novoPedido);
                Cliente cliente = baseClientes.Localizar(id);
                cliente.RegistrarPedido(novoPedido);
            }           
        }

        static void Main(string[] args) {
            
            todosOsPedidos = new BaseDados<Pedido>();
            baseClientes = new BaseDados<Cliente>();
            config();

            Pedido pedido = null;
            Cliente cliente;
            int opcao = -1;
            do {
                opcao = ExibirMenu();
                switch (opcao) {
                    case 1:
                        try {
                            pedido = AbrirPedido();
                            todosOsPedidos.Add(pedido);
                            RelatorioPedido(pedido);
                            Cliente quem = LocalizarCliente(baseClientes);
                            string mensagem = "Cliente não existente. Pedido registrado como anônimo.";
                            if (quem != null) {
                                quem.RegistrarPedido(pedido);
                                mensagem = $"Pedido registrado para {quem}";
                            }
                            Console.WriteLine(mensagem);
                        }
                        catch (ArgumentNullException ae) {
                            Console.WriteLine(ae.Message);
                        }
                        catch (InvalidOperationException ae) {
                            Console.WriteLine(ae.Message);
                        }
                        catch (NullReferenceException ex) {
                            Console.WriteLine("Opção inválida. Pedido não foi criado.");
                        }
            
                        //pedido = AbrirPedido();
                        //todosOsPedidos.Add(pedido);
                        //RelatorioPedido(pedido);
                        //Cliente quem = LocalizarCliente(todosOsClientes);
                        //String mensagem = "Cliente não existente. Pedido registrado como anônimo";
                        //if (quem != null) {
                        //    quem.RegistrarPedido(pedido);
                        //    mensagem = $"Pedido registrado para {quem}";
                        //}
                        //Console.WriteLine(mensagem);
                        break;
                    case 2:
                        try {
                            pedido = LocalizarPedido(todosOsPedidos);
                            AdicionarComidas(pedido);
                        }
                        catch (InvalidOperationException ie) {
                            Console.WriteLine(ie.Message);
                        }
                        catch (NullReferenceException ex) {
                            Console.WriteLine("Pedido não existente.");
                        }
                    //pedido = LocalizarPedido(todosOsPedidos);
                    //if (pedido!= null)
                    //    AdicionarComidas(pedido);
                    //else
                    //    Console.WriteLine("Pedido não existente.");
                    break;
                    case 3:
                        try {
                            pedido = LocalizarPedido(todosOsPedidos);
                            Console.WriteLine(pedido.Relatorio());
                        }
                        catch (NullReferenceException) {
                            Console.WriteLine("Pedido não existente.");
                        }
                        //pedido = LocalizarPedido(todosOsPedidos);
                        //if (pedido != null)
                        //    Console.WriteLine(pedido.Relatorio());
                        //else
                        //    Console.WriteLine("Pedido não existente.");
                        break;
                    case 4:
                        try {
                            pedido = LocalizarPedido(todosOsPedidos);
                            pedido.FecharPedido();
                            Console.WriteLine("Pedido encerrado: ");
                            Console.WriteLine(pedido.Relatorio());
                        }
                        catch (NullReferenceException) {
                            Console.WriteLine("Pedido não existente.");
                        }
    
                        break;
                    //pedido = LocalizarPedido(todosOsPedidos);
                    //if (pedido != null) {
                    //    pedido.FecharPedido();
                    //    Console.WriteLine("Pedido encerrado: ");
                    //    Console.WriteLine(pedido.Relatorio());
                    //}
                    //else
                    //    Console.WriteLine("Pedido não existente.");
                    //break;
                    case 5:
                        Cabecalho();
                        Console.WriteLine(todosOsPedidos.RelatorioOrdenado());
                        break;
                    case 6:
                        Cabecalho();
                        Console.WriteLine(baseClientes.RelatorioOrdenado());
                        break;
                    case 7:
                        Cabecalho();
                        //IComparable
                        /*Comparer
                        ComparadorDeGastos compGasto = new ComparadorDeGastos();
                        Console.WriteLine(baseClientes.RelatorioOrdenado(compGasto));
                        */
                        //Comparison 
                        Comparison<Cliente> comparacao = (cli1, cli2)
                                        => (cli1.TotalGasto()-cli2.TotalGasto()>0) ? 1 : -1 ;

                        Console.WriteLine(baseClientes.RelatorioOrdenado(comparacao));

                        /*FUNÇÃO LAMBDA
                        Console.WriteLine(baseClientes.RelatorioOrdenado(
                                (cli1, cli2) => (cli1.TotalGasto() - cli2.TotalGasto() > 0) ? 1 : -1 
                               )
                          );
                        */
                        break;
                    case 8:
                        Cabecalho();
                        Func<Pedido, double> somaValorPedidos = (ped) => ped.PrecoAPagar();
                        double valorTotal = todosOsPedidos.Totalizador(somaValorPedidos);
                        Console.WriteLine($"Valor total vendido no Xulambs Foods: {valorTotal:C2}");
                        break;
                    case 9:
                        Cabecalho();
                        baseClientes.Processar((cli) => cli.fidelizarCliente("Xulambs"));
                        Console.WriteLine(baseClientes.RelatorioOrdenado());
                        break;
                }
                Pausa();
            } while (opcao != 0);
            
            
        }

       
    }
}