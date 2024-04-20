using System.ComponentModel.Design;

namespace XulambsFoods.src
{
    internal class Program
    {
        static double totalVendido;

        static void pausa() {
            Console.WriteLine("Tecle Enter para continuar.");
            Console.ReadKey();
        }
        static void cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS - v0.21");
            Console.WriteLine("=====================");
        }
        static int MenuPrincipal() {
            int opcao;
            cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Total Vendido Hoje");
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

        static Pedido criarPedido() {
            Pedido novoPedido = new Pedido();
            Comida novaComida = criarComida();

            if (novaComida != null) {
                do {
                    novoPedido.addComida(novaComida);
                    Console.WriteLine(novaComida.relatorio() + " adicionado ao pedido.");
                    pausa();
                    novaComida = criarComida();
                } while (novaComida != null);
                novoPedido.fecharPedido();
            }
            else
                novoPedido = null;

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
                    novaComida = new Pizza(clienteQuerBorda());
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
        static void Main(string[] args)
        {
            totalVendido = 0d;
            int opcao;
            Pedido pedidoAtual;
            do {
                opcao = MenuPrincipal();
                switch (opcao) {
                    case 1: 
                        pedidoAtual = criarPedido();
                        if (pedidoAtual != null) {
                            totalVendido += pedidoAtual.precoFinal();
                            Console.WriteLine("Pedido fechado: ");
                            Console.WriteLine(pedidoAtual.relatorio());
                            pausa();
                        }
                        break;
                    case 2: 
                        relatorioTotalVendido();
                        break;
                }
            } while (opcao != 0);
        }
    }
}
