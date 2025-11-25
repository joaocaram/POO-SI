namespace XulambsFoods_2025_1.src {
    using System.ComponentModel.Design;
    using System.Linq;
    using System.Threading.Channels;

    namespace XulambsFoods_2025_1.src {

        /** 
        * MIT License
        *
        * Copyright(c) 2025 João Caram <caram@pucminas.br>
        *
        * Permission is hereby granted, free of charge, to any person obtaining a copy
        * of this software and associated documentation files (the "Software"), to deal
        * in the Software without restriction, including without limitation the rights
        * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
        * copies of the Software, and to permit persons to whom the Software is
        * furnished to do so, subject to the following conditions:
        *
        * The above copyright notice and this permission notice shall be included in all
        * copies or substantial portions of the Software.
        *
        * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
        * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
        * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
        * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
        * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
        * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
        * SOFTWARE.
        */

        public class XulambsPizza {

            static BaseDados<Pedido> pedidos = new BaseDados<Pedido>(5000);
            static BaseDados<Cliente> clientes = new BaseDados<Cliente>(80);

            static void Cabecalho() {
                Console.Clear();
                Console.WriteLine("XULAMBS PIZZA v0.8\n~~~~~~~~~~~~~~~~~~");
            }

            static void Pausa() {
                Console.WriteLine("Digite enter para continuar...");
                Console.ReadLine();
            }

            static int lerInteiro(string mensagem) {
                int opcao;
                try {
                    Console.Write(mensagem+": ");
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (FormatException) {
                    Console.WriteLine("Opção inválida");
                    Pausa();
                    opcao = -1;
                }
                return opcao;
            }

            static void gerarClientes() {
                clientes.Add(new Cliente(0, "Anônimo"));
                string[] nomes = File.ReadAllLines("medalhistas.txt");
                int doc = 1;
                foreach (string nome in nomes) {
                    Cliente novo = new Cliente(doc, nome);
                    clientes.Add(novo);
                    doc++;
                }
            }

            static void gerarPedidos(int quantCli) {
                Random aleat = new Random(42);
                Pedido pedido;
                IProduto comida;
                for (int i = 0; i < quantCli * 16; i++) {
                    int tipo = aleat.Next() % 2;
                    if (tipo == 0) pedido = new PedidoLocal();
                    else pedido = new PedidoEntrega(aleat.Next(14));

                    int quantComidas = aleat.Next(1000);
                    if (quantComidas > 950)
                        quantComidas = 4;
                    else if (quantComidas > 750)
                        quantComidas = 3;
                    else if (quantComidas > 500)
                        quantComidas = 2;
                    else quantComidas = 1;

                    for (int j = 0; j < quantComidas; j++) {
                        tipo = aleat.Next() % 2;
                        int quantAdic = aleat.Next(5);
                        if (tipo == 0) comida = new Pizza(quantAdic);           
                        else comida = new Sobremesa(Enum.GetValues<ESobremesa>()[aleat.Next(3)]);
                        pedido.Adicionar(comida);
                    }
                    Cliente quem = clientes.Get(aleat.Next(quantCli));
                    quem.RegistrarPedido(pedido);
                    pedido.FecharPedido();
                    pedidos.Add(pedido);
                }
            }

            static void config() {
                gerarClientes();
                gerarPedidos(clientes.Size());
            }

            static int ExibirMenuPrincipal() {
                Cabecalho();
                Console.WriteLine("====== VENDAS ======");
                Console.WriteLine("1 - Abrir Pedido");
                Console.WriteLine("2 - Alterar Pedido");
                Console.WriteLine("3 - Relatório de Pedido");
                Console.WriteLine("4 - Fechar Pedido");
                Console.WriteLine("5 - Valor do último pedido");
                Console.WriteLine("====== CLIENTES ======");
                Console.WriteLine("6 - Relatório simplificado de clientes");
                Console.WriteLine("7 - Relatório ordenado de clientes (padrão)");
                Console.WriteLine("8 - Relatório ordenado de clientes (escolhido)");
                Console.WriteLine("9 - Clientes filtrado por gastos");
                Console.WriteLine("10 - Total gasto por clientes");
                Console.WriteLine("11 - Atualizar fidelidades");
                Console.WriteLine("====== PEDIDOS ======");
                Console.WriteLine("12 - Relatório simplificado de pedidos");
                Console.WriteLine("13 - Valor médio de pedido");
                

                Console.WriteLine("\n0 - Finalizar");
                return lerInteiro("Digite sua escolha");
            }

            static int ExibirMenuSobremesa()
            {
                Cabecalho();
                int i = 1;
                Console.WriteLine("Escolha sua sobremesa: ");
                foreach (ESobremesa nome in Enum.GetValues<ESobremesa>())
                {
                    Console.WriteLine($"{i} - {nome.ToString()}");
                    i++;
                }

                return lerInteiro("Digite sua escolha");
            }

            static Sobremesa ComprarSobremesa()
            {
                
                int opcao = ExibirMenuSobremesa();
                ESobremesa sobre = ESobremesa.Brigadeiro;
                try
                {
                    sobre = Enum.GetValues<ESobremesa>().ElementAt(opcao - 1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Opção inválida. Escolha uma do menu.");
                    Pausa();
                    ComprarSobremesa();
                }
                return new Sobremesa(sobre);
            }
           
            static IProduto EscolherProduto()
            {
                Cabecalho();
                Console.WriteLine("1 - Pizza");
                Console.WriteLine("2 - Sobremesa");
                int opcao = lerInteiro("Digite sua escolha");
                return opcao switch { 
                    1 => ComprarPizza(),
                    2 => ComprarSobremesa()
                };
            }
            
            static void AdicionarProdutos(Pedido pedido) {
                string conf;
                do {
                    IProduto produto = EscolherProduto();
                    
                    pedido.Adicionar(produto);
                    Console.Write("\nQuer algo mais(S/N)? ");
                    conf = Console.ReadLine().ToUpper();
                } while (conf.Equals("S"));
            }

            static Pedido AbrirPedido() {
                Pedido novoPedido = EscolherTipoPedido();
                AdicionarProdutos(novoPedido);
                MostrarPedido(novoPedido);
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
                return lerInteiro("Sua opção");
            }

            static Pedido CriarPedidoLocal() {
                return new PedidoLocal();
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
                Console.WriteLine("0 - Não quero alterar");

                return lerInteiro("Digite sua escolha");
            }

            static int ExibirMenuBordas() {
                Cabecalho();
                int i = 1;
                Console.WriteLine("Escolha sua borda: ");
                foreach (string nome in Enum.GetNames<EBorda>()) {
                    Console.WriteLine($"{i} - {nome}");
                    i++;
                }
                
                return lerInteiro("Digite sua escolha");
            }

            static void EscolherBorda(Pizza pizza) {
                int opcao = ExibirMenuBordas();
                try {
                    EBorda borda = Enum.GetValues<EBorda>().ElementAt(opcao - 1);
                    pizza.AdicionarBorda(borda);
                }
                catch (ArgumentNullException) {
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                    Pausa();
                    EscolherBorda(pizza);
                }
                catch (ArgumentOutOfRangeException) {
                    Console.WriteLine("Opção inválida. Escolha uma do menu.");
                    Pausa();
                    EscolherBorda(pizza);
                }    
            }

            static Pizza ComprarPizza() {
                Cabecalho();
                Console.WriteLine("Comprando uma nova pizza:");
                Pizza novaPizza = new Pizza();
                EscolherBorda(novaPizza);
                EscolherIngredientes(novaPizza);
                Console.WriteLine();
                MostrarNota(novaPizza);
                return novaPizza;
            }

            static void EscolherIngredientes(Pizza pizza) {
                int opcao = ExibirMenuIngredientes(pizza);
                if (opcao == 1) {
                    
                    int adicionais = lerInteiro("Digite a quantidade de adicionais");
                    try {
                        pizza.AdicionarIngredientes(adicionais);
                    }
                    catch (ArgumentOutOfRangeException) {
                        //lógica para ler novamente um número positivo.
                    }
                    catch (InvalidOperationException) {
                        //lógica para deixar a pizza com o máximo de ingredientes.
                    }
                    
                }
            }

            static void MostrarNota(Pizza pizza) {
                Console.WriteLine("Comprando: ");
                Console.WriteLine(pizza);
            }

            static void MostrarPedido(Pedido pedido) {
                Cabecalho();
                Console.WriteLine(pedido);
            }

            static void ArmazenarPedido(Pedido pedido) {
                pedidos.Add(pedido);
            }

            static T LocalizarDado<T>(BaseDados<T> dados, string classe) where T : IComparable<T> { 
                Cabecalho();
                Console.WriteLine($"Localizando um {classe}");
                int numero = lerInteiro("Digite o número identificador");
                return dados.Get(numero);
            }

            static void AlterarPedido() {
                Pedido pedidoParaAlteracao = LocalizarDado(pedidos, nameof(Pedido));
                if (pedidoParaAlteracao != null) {
                    AdicionarProdutos(pedidoParaAlteracao);
                    MostrarPedido(pedidoParaAlteracao);
                }
                else
                    Console.WriteLine("Pedido não encontrado");
            }

            static void FecharPedido() {
                Pedido pedidoParaFechar = LocalizarDado(pedidos, nameof(Pedido));
                if (pedidoParaFechar != null) {
                    try {
                        Cliente cliente = LocalizarDado(clientes, nameof(Cliente));
                        cliente.RegistrarPedido(pedidoParaFechar);
                        pedidoParaFechar.FecharPedido();
                        MostrarPedido(pedidoParaFechar);
                        Console.WriteLine($"\nRegistrado para {cliente.ToString()}\n");
                    }
                    catch (PedidoVazioException excecao) {
                        Console.WriteLine(excecao.Message);
                    }
                }
                else
                    Console.WriteLine("Pedido não encontrado");
            }

            static void RelatorioDePedido() {
                Pedido localizado = LocalizarDado(pedidos, nameof(Pedido));
                if (localizado != null)
                    MostrarPedido(localizado);
                else
                    Console.WriteLine("Pedido não existente");
            }

            private static void RelatorioSimplificado<T>(BaseDados<T> dados) where T : IComparable<T> {
                Cabecalho();
                Console.WriteLine(dados.SimpleReport());
            }

            private static void RelatorioOrdenado<T>(BaseDados<T> dados) where T : IComparable<T> {
                Cabecalho();
                Console.WriteLine(dados.SortedReport());
            }
            static void ValorUltimoPedido() {
                Cabecalho();
                //Pedido ultimo = pedidos.Last();
                //Console.WriteLine($"Valor do último pedido: {ultimo.PrecoAPagar():C2}");
            }

            static void Main(string[] args) {
                config();
                int opcao = -1;
                do {
                    opcao = ExibirMenuPrincipal();
                    switch (opcao) {
                        case 1:
                            Pedido novoPedido = AbrirPedido();
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
                        case 5:
                            ValorUltimoPedido();
                            break;
                        case 6:
                            RelatorioSimplificado(clientes);
                            break;
                        case 7:
                            RelatorioOrdenado(clientes);
                            break;
                        case 8:
                            
                            break;
                        case 9:

                            break;
                        case 10:

                            break;
                        case 11: 

                            break;
                        case 12:
                            RelatorioSimplificado(pedidos);
                            break;
                        case 13:
                            RelatorioSimplificado(pedidos);
                            break;
                        case 0:
                            Console.WriteLine("FLW VLW OBG VLT SMP.");
                            break;
                    }
                    Pausa();
                } while (opcao != 0);
            }

            
        }
    }
}