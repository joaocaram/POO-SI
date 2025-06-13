using System.Collections.ObjectModel;
using System.Net.WebSockets;

namespace XulambsFoods_2025_1.src
{

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

    public class XulambsFoods {

        static BaseDados<Pedido> pedidos = new BaseDados<Pedido>();
     
        static BaseDados<Cliente> clientes = new BaseDados<Cliente>();

        static Dictionary<DateOnly, LinkedList<Pedido>> pedidosPorDia = 
                                                new Dictionary<DateOnly, LinkedList<Pedido>>();

        static void gerarClientes() {
            clientes.Adicionar(new Cliente(0, "Anônimo"));
            string[] nomes = File.ReadAllLines("medalhistas.txt");
            int doc = 1;
            foreach (string nome in nomes) {
                Cliente novo = new Cliente(doc, nome);
                clientes.Adicionar(novo);
                doc++;
            }
        }

        static void gerarPedidos(int quantCli) {
            Random aleat = new Random(42);
            Pedido pedido;
            Comida comida;
            for (int i = 0; i < quantCli * 32; i++) {
                int tipo = aleat.Next() % 2;
                if (tipo == 0) pedido = new PedidoLocal(null);
                else pedido = new PedidoEntrega(null, aleat.Next(14));
                
                int quantComidas = aleat.Next(1000);
                if (quantComidas > 990)
                    quantComidas = 4;
                else if (quantComidas > 880)
                    quantComidas = 3;
                else if (quantComidas > 650)
                    quantComidas = 2;
                else quantComidas = 1;

                for (int j = 0; j < quantComidas; j++) {
                    tipo = aleat.Next(10)+1;
                    int quantAdic = aleat.Next(5);
                    if (tipo > 6) comida = new Pizza(quantAdic);
                    else comida = new Sanduiche(quantAdic);
                    pedido.Adicionar(comida);
                }
                int sorteio = aleat.Next(1000);
                if (sorteio > 400)
                    sorteio = quantCli / 3;
                else if (sorteio > 750)
                    sorteio = quantCli / 2;
                else sorteio = quantCli;
                Cliente quem = clientes.Buscar(aleat.Next(sorteio-1));
                quem.RegistrarPedido(pedido);
                pedido.FecharPedido();
                ArmazenarPedido(pedido);
            }
        }

        static void config()
        {
            gerarClientes();
            gerarPedidos(clientes.Tamanho());
        }
        
        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS v0.7\n================");
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
            Console.WriteLine("4 - Fechar Pedido");
            Console.WriteLine("=============================");
            Console.WriteLine("5 - Menu Gerente");
            Console.WriteLine("0 - Finalizar");
            return lerNumero("Digite sua escolha");
        }
        
        static int ExibirMenuGerente() {
            Cabecalho();
            Console.WriteLine("Funções Gerenciais");
            Console.WriteLine("=================================");
            Console.WriteLine("CLIENTES");
            Console.WriteLine("1 - Atualizar programa de fidelidade");
            Console.WriteLine("2 - Relatório de um cliente");
            Console.WriteLine("3 - Relatório resumido dos clientes");
            Console.WriteLine("4 - Relatório resumido ordenado dos clientes ");
            Console.WriteLine("5 - Relatório de clientes com gasto mínimo");
            Console.WriteLine("=================================");
            Console.WriteLine("PEDIDOS");
            Console.WriteLine("6 - Maior pedido do restaurante");
            Console.WriteLine("7 - Pedidos de um dia");
            Console.WriteLine("8 - Pedidos com um prato");
            Console.WriteLine("9 - Pedidos com um valor mínimo");
            Console.WriteLine("=================================");
            Console.WriteLine("FINANCEIRO");
            Console.WriteLine("10 - Gasto total dos clientes");
            Console.WriteLine("11 - Gasto médio por cliente");
            Console.WriteLine("12 - Gasto médio por pedido");
            Console.WriteLine("13 - Arrecadação em um dia");
                       
            Console.WriteLine("\n0 - Voltar");
            return lerNumero("Digite sua escolha");

        }
       
        static void AdicionarComidas(Pedido pedido) {
            string confirmacao;
            do {
                Comida novaComida = ComprarComida();
                try { 
                    pedido.Adicionar(novaComida);
                    Console.Write("\nQuer adicionar algo mais (S/N)? ");
                    confirmacao = Console.ReadLine().ToUpper();
                }catch(InvalidOperationException iex)
                {
                    Console.WriteLine("Pedido já está fechado!");
                    Pausa();
                    confirmacao = "N";
                }
            } while (confirmacao.Equals("S"));
        }

        static Pedido AbrirPedido() {
            Pedido novoPedido = EscolherTipoPedido();
            AdicionarComidas(novoPedido);
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

        static int ExibirMenuTipoComida() {
            Cabecalho();
            Console.WriteLine("Escolha sua comida:");
            Console.WriteLine("1 - Pizza (padrão)");
            Console.WriteLine("2 - Sanduíche");
            Console.Write("Sua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static Pedido CriarPedidoLocal() {
            return new PedidoLocal(DateOnly.FromDateTime(DateTime.Now));
        }

        static Pedido CriarPedidoEntrega() {
            Console.WriteLine("Pedido para Entrega.");
            Console.Write("Distância: ");
            double dist = double.Parse(Console.ReadLine());
            return new PedidoEntrega(DateOnly.FromDateTime(DateTime.Now), dist);
        }

        /// <summary>
        /// Exibe menu para adicionar ou remover ingredientes.
        /// </summary>
        /// <param name="comida">Comida para mostrar a nota</param>
        /// <returns>Opção válida do menu, ou -1 em caso
        /// de digitação de opção inválida</returns>
        static int ExibirMenuIngredientes(Comida comida) {
            Cabecalho();
            Console.WriteLine("Personalizar a Comida\n");
            MostrarNota(comida);
            Console.WriteLine("\n1 - Acrescentar ingredientes");
            Console.WriteLine("2 - Retirar ingredientes");
            Console.WriteLine("0 - Não quero alterar");
            return lerNumero("Digite sua escolha");
           
            
        }

        static int ExibirMenuComparacoes()
        {
            Cabecalho();
            Console.WriteLine("Escolha uma ordenação para o relatório\n");
            Console.WriteLine("\n1 - Por ordem alfabética");
            Console.WriteLine("2 - Por ordem de gastos");
            return lerNumero("Digite sua escolha");
        }

        static Comida ComprarComida() {
            Cabecalho();
            Console.WriteLine("Incluindo no pedido:");
            Comida novaComida = EscolherComida();
            EscolherIngredientes(novaComida);
            Console.WriteLine();
            MostrarNota(novaComida);
            return novaComida;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static int lerNumero(string msg)
        {
            Console.Write(msg + ": ");
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException fException)
            {
                return -1;
            }

        }
        
        private static Comida EscolherComida() {
            int opcao = ExibirMenuTipoComida();
            return opcao switch {
                2 => ComprarSanduiche(),
                1 or _ => new Pizza()
            };
        }

        private static Comida ComprarSanduiche() {
            Console.Write("Deseja combo com fritas (S/N)? ");
            string querCombo = Console.ReadLine().ToLower();
            return querCombo.Equals("s") ? new Sanduiche(true) : new Sanduiche();
        }

        static void EscolherIngredientes(Comida comida) {
            int opcao = ExibirMenuIngredientes(comida);
            while (opcao != 0) {

                int adicionais;
                switch (opcao) {
                    case -1: Console.WriteLine("Opção inválida. Tente novamente");
                        break;
                    case 1:
                        adicionais = lerNumero("Quantidade de ingredientes");
                        try
                        {
                            comida.AdicionarIngredientes(adicionais);
                        }catch(MaximoDeIngredientesException mex)
                        {
                            Console.WriteLine(mex.Message);
                            Console.WriteLine("Quantidade: " + mex.getQuant());
                            Pausa();
                        }
                        
                        break;
                    case 2:
                        adicionais = lerNumero("Quantidade de ingredientes");
                        comida.RetirarIngredientes(adicionais);
                        break;
                };
                Console.WriteLine();
                MostrarNota(comida);
                Pausa();
                opcao = ExibirMenuIngredientes(comida);
            }

        }

        static void MostrarNota(Comida comida) {
            Console.WriteLine("Comprando: ");
            Console.WriteLine(comida);

        }

        static void MostrarPedido(Pedido pedido) {
            Console.WriteLine(pedido);
        }

        static void ArmazenarPedido(Pedido pedido) {
            LinkedList<Pedido> pedidosHoje;
            pedidosPorDia.TryGetValue(pedido.Data(), out pedidosHoje);
            
            if (pedidosHoje == null) {
                pedidosHoje = new LinkedList<Pedido>();
                pedidosPorDia.Add(pedido.Data(), pedidosHoje);
            }

            pedidosHoje.AddLast(pedido);        // no dicionário por data.
            
            pedidos.Adicionar(pedido);          // na base de todos os pedidos.
        }

        static Pedido LocalizarPedido() {
            Cabecalho();
            Console.WriteLine("Localizando um pedido");
            Console.Write("Digite o número do pedido: ");
            int numero = int.Parse(Console.ReadLine());
            Pedido localizado;
            try {
                localizado = pedidos.Buscar(numero);
            }catch (KeyNotFoundException ke) {
                localizado = null;
            }
            return localizado;
        }

        static void AlterarPedido() {
            Pedido pedidoParaAlteracao = LocalizarPedido();
            if (pedidoParaAlteracao != null) {
                AdicionarComidas(pedidoParaAlteracao);
                MostrarPedido(pedidoParaAlteracao);
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

        static void FecharPedido()
        {
            Pedido localizado = LocalizarPedido();
            if (localizado != null)
            {
                FecharERegistrarPedido(localizado);
            }
                
            else
                Console.WriteLine("Pedido não existente");
        }

        static void ExibirMaior<T>(BaseDados<T> dados) where T: IComparable<T>
        {
            Cabecalho();
            Console.WriteLine("Pedido mais caro do dia:");
            Console.WriteLine(dados.Maior());
        }

        static void RegistrarPedidoParaCliente(Pedido pedido)
        {  
            int id = lerNumero("ID do cliente");
            Cliente cliente = clientes.Buscar(id);
            cliente.RegistrarPedido(pedido);
            Console.WriteLine($"\nPedido registrado para {cliente.ToString()}\n");
        }

        static void CriarPedido()
        {
            Pedido novoPedido = AbrirPedido();
            if (novoPedido != null) {
                MostrarPedido(novoPedido);
                ArmazenarPedido(novoPedido);
            }
            else
                Console.WriteLine("Pedido não foi criado corretamente.");
        }

        static void FecharERegistrarPedido(Pedido pedido) {
            
            try {
                RegistrarPedidoParaCliente(pedido);
            }
            catch (KeyNotFoundException ke) {
                clientes.Buscar(0).RegistrarPedido(pedido);
                Console.WriteLine("Não foi encontrado cliente com este id. Registrado para cliente anônimo");
            }
            pedido.FecharPedido();
            Console.WriteLine("Pedido fechado:");
            MostrarPedido(pedido);
        }
      
        static void RelatorioCliente() {
            Cabecalho();
            Console.WriteLine("RELATÓRIO DETALHADO DE CLIENTE");
            int id = lerNumero("ID do cliente");
            string mensagem = "";
            try {
                Cliente cliente = clientes.Buscar(id);
                mensagem = cliente.RelatorioPedidos();
            }catch(KeyNotFoundException knfex) {
                mensagem = $"Cliente com id {id} não existe.";
            }
            Console.WriteLine($"\n{mensagem}");
        }

        static void AtualizarFidelidade() {
            Cabecalho();
            Action<Cliente> atualizar = (cli) => cli.AtualizarCategoria();
            clientes.Atualizar(atualizar);
            Console.WriteLine("Categorias atualizadas.");
        }

        static void RelatorioResumidoClientes() {
            Cabecalho();
            Console.WriteLine(clientes.Relatorio());
        }

        static void RelatorioResumidoOrdenado() {
            Cabecalho();
            int opcao = ExibirMenuComparacoes();
            Comparison<Cliente> comparacao = null;
            comparacao = opcao switch
            {
                1 => (cli1, cli2) => cli1.ToString().CompareTo(cli2.ToString()),
                2 => (cli1, cli2) => (cli1.TotalGasto() > cli2.TotalGasto() ? 1 : -1)
            };
            Comparer<Cliente> comparador = Comparer<Cliente>.Create(comparacao);

            Console.WriteLine(clientes.RelatorioOrdenado(comparador));
        }
       
        static void TotalGastoPorClientes()
        {
            Cabecalho();
            Console.Write("Total gasto no restaurante: ");
            Console.WriteLine($"{clientes.Totalizar((cli) => cli.TotalGasto()):C2}");
        }        

        static void ClientesComGastoMinimo()
        {
            Cabecalho();
            Console.Write("Digite o valor mínimo para filtro: ");
            double valor = double.Parse(Console.ReadLine());
            Predicate<Cliente> filtro =
                    (cli) => cli.TotalGasto() > valor;

             Console.WriteLine(clientes.RelatorioFiltrado(filtro));
        }
        
        static void GastoMedio<T>(BaseDados<T> basedados, Func<T, double> funcao, string nome) {
            Cabecalho();
            double valorMedio = basedados.Media(funcao);
            Console.WriteLine($"Gasto médio: {valorMedio:C2}, sendo {basedados.Tamanho()} {nome}.");
        }

        static void PedidosDeUmDia() {
            Cabecalho();
            Console.Write("Digite uma data para filtrar (DD/MM/AAAA): ");
            string[] dadosFiltro = Console.ReadLine().Split("/");
            DateOnly data = new DateOnly(int.Parse(dadosFiltro[2]), int.Parse(dadosFiltro[1]), int.Parse(dadosFiltro[0]));
            
            LinkedList<Pedido> pedidos;
            pedidosPorDia.TryGetValue(data, out pedidos);
            if (pedidos != null) { 
                string resposta = pedidos.Select(p => p.ToString())
                                         .Aggregate((s1, s2) => $"\n{s1}\n{s2}\n~~~~~~~~~~~~~~~~\n");
            
                Console.WriteLine(resposta);
            }
            else
                Console.WriteLine($"Sem pedidos para {data}!");
            //Console.WriteLine(pedidos.RelatorioFiltrado( p => p.Data().Equals(data)));

        }
        
        static void ModoGerente() {
            int opcao = -1;
            do {
                opcao = ExibirMenuGerente();
                switch (opcao) {
                    case 1:
                        AtualizarFidelidade();
                        break;
                    case 2:
                        RelatorioCliente();
                        break;
                    case 3:
                        RelatorioResumidoClientes();
                        break;
                    case 4:
                        RelatorioResumidoOrdenado();
                        break;
                    case 5:
                        ClientesComGastoMinimo();
                        break;
                    case 6:
                       ExibirMaior(pedidos);
                        break;
                    case 7:
                        PedidosDeUmDia();
                        break;
                    case 8: 
                   //     PedidosComUmPrato();
                        break;
                    case 9:
                   //     PedidosComValorMinimo();
                        break;
                    case 10:
                        TotalGastoPorClientes();
                        break;
                    case 11:
                        GastoMedio(clientes, cli => cli.TotalGasto(), "clientes");
                        break;
                    case 12:
                        GastoMedio(pedidos, ped => ped.PrecoAPagar(), "pedidos");
                        break;
                    case 13:
                    //    ArrecadacaoDeUmDia();
                        break;
                    case 0: Console.WriteLine("Retornando ao menu principal.");
                        break;
                }
                Pausa();
            } while (opcao != 0);
        
        }
        
        static void Main(string[] args) {
            config();
            int opcao = -1;
            do {
                opcao = ExibirMenuPrincipal();
                switch (opcao) {
                    case 1: CriarPedido();
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
                    case 5: ModoGerente();
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
