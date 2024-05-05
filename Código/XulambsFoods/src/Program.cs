using System.ComponentModel.Design;
using System.Numerics;
using XulambsFoods_C_.src;

namespace XulambsFoods.src
{
    internal class Program
    {
        static double totalVendido;
        static Dictionary<int, Cliente> clientes;

        static void pausa() {
            Console.WriteLine("Tecle Enter para continuar.");
            Console.ReadKey();
        }
        static void cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS - v0.41");
            Console.WriteLine("=====================");
        }
        static int MenuPrincipal() {
            int opcao;
            cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Total Vendido Hoje");
            Console.WriteLine("3 - Novo Cliente");
            Console.WriteLine("4 - Relatório de Cliente");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite sua opção: ");
            int.TryParse(Console.ReadLine(), out opcao);
            return opcao;
        }

        static int MenuComida() {
            int opcao;
            cabecalho();
            Console.WriteLine("1 - Pizza");
            Console.WriteLine("2 - Sanduiche");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite sua opção: ");
            int.TryParse(Console.ReadLine(), out opcao);
            return opcao;
        }

        static void relatorioTotalVendido() {
            cabecalho();
            String hoje = DateOnly.FromDateTime(DateTime.Now).ToShortDateString();
            Console.WriteLine("Total vendido hoje (" + hoje + "): R$ " + totalVendido.ToString("0.00"));
            pausa();
        }

        static Pedido escolherTipoDePedido()
        {
            cabecalho();
            int opcao = 0;
            Pedido novo = null;
            Console.WriteLine("1 - Pedido para comer no local");
            Console.WriteLine("2 - Pedido para entrega");
            Console.WriteLine("0 - Sair");
            Console.Write("Digite sua opção: ");
            int.TryParse(Console.ReadLine(), out opcao);
            switch (opcao)
            {
                case 1: novo = new PedidoLocal();
                    break;
                case 2:
                    double dist;
                    Console.Write("Informe a distância para entrega: ");
                    double.TryParse(Console.ReadLine(), out dist);
                    novo = new PedidoDelivery(dist);
                    break;
            }
            return novo;
        }
        static Pedido criarPedido() {
            Pedido novoPedido = escolherTipoDePedido();
            if (novoPedido != null) { 
                Comida novaComida = criarComida();

                if (novaComida != null) {
                    do {
                        novoPedido.addComida(novaComida);
                        Console.WriteLine(novaComida + " adicionado ao pedido.");
                        pausa();
                        novaComida = criarComida();
                    } while (novaComida != null);
                    novoPedido.fecharPedido();
                }
                else
                    novoPedido = null;
            }
            return novoPedido;
        }

        
        static bool clienteQuerBorda()
        {
            Console.Write("Deseja borda recheada? (s/n) ");
            string borda = Console.ReadLine();
            return borda.ToLower().Equals("s");
        }

        static Comida criarComida() {
            int tipoComida = MenuComida();
            Comida novaComida = null;
            switch (tipoComida) {
                case 1:
                    novaComida = new Pizza(0,clienteQuerBorda());
                    break;
                case 2:
                    novaComida = new Sanduiche();
                    break;
            }
            
            if (novaComida != null) {
                Console.Write("Deseja quantos adicionais? ");
                int adicionais = int.Parse(Console.ReadLine());
                novaComida.adicionarIngredientes(adicionais);
            }

            return novaComida;
        }
        private static Cliente localizarCliente() {
            int idCli;
            Cliente quem;
            cabecalho();
            Console.Write("Digite o id do cliente: ");
            idCli = int.Parse(Console.ReadLine());
            clientes.TryGetValue(idCli, out quem);
            return quem;

        }

        private static Cliente cadastrarCliente() {
            string nome;
            Cliente novo; 
            cabecalho();
            Console.Write("Qual é o nome do novo cliente? ");
            nome = Console.ReadLine();
            novo = new Cliente(nome);
            clientes.Add(novo.GetHashCode(), novo);
            Console.WriteLine($"Cliente cadastrado:\n {novo.ToString()}");
            pausa();
            return novo;
        }

        static void registrarPedido(Cliente clienteAtual, Pedido pedidoAtual) {
            clienteAtual.registrarPedido(pedidoAtual);
            totalVendido += pedidoAtual.precoFinal();
            Console.WriteLine("Pedido fechado: ");
            Console.WriteLine(pedidoAtual.ToString());
        }
        static void Main(string[] args)
        {
            clientes = new Dictionary<int, Cliente>();
            totalVendido = 0d;
            int opcao;
            Pedido pedidoAtual;
            Cliente clienteAtual;
            do {
                opcao = MenuPrincipal();
                switch (opcao) {
                    case 1:
                        clienteAtual = localizarCliente();
                        if (clienteAtual == null) {
                            clienteAtual = cadastrarCliente();
                        }
                        pedidoAtual = criarPedido();
                        if (pedidoAtual != null) {
                            registrarPedido(clienteAtual, pedidoAtual);
                            pausa();
                        }
                        break;
                    case 2: 
                        relatorioTotalVendido();
                        break;
                    case 3:
                        clienteAtual = cadastrarCliente();
                        pedidoAtual = criarPedido();
                        if (pedidoAtual != null) {
                            registrarPedido(clienteAtual, pedidoAtual);
                            pausa();
                        }
                        break;
                    case 4:
                        clienteAtual = localizarCliente();
                        if (clienteAtual != null) {
                            Console.WriteLine(clienteAtual.resumoPedidos());
                        }
                        else {
                            Console.WriteLine("Cliente não encontrado.");
                        }
                        pausa();
                        break;
                }
            } while (opcao != 0);
        }

       
    }
}
