using System.Collections.ObjectModel;

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

        static LinkedList<Pedido> pedidos = new LinkedList<Pedido>();
        static BaseClientes clientes = new BaseClientes();
        
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
            Comida comida;
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
                    else comida = new Sanduiche(quantAdic);
                    pedido.Adicionar(comida);
                }
                Cliente quem = clientes.Get(aleat.Next(quantCli));
                quem.RegistrarPedido(pedido);
                pedido.FecharPedido();
                pedidos.AddLast(pedido);
            }
        }

        static void config()
        {
            gerarClientes();
            gerarPedidos(clientes.Size());
        }

        static T maiorDoConjunto<T>(ICollection<T> dados) where T:IComparable<T>
        {
            T maior = dados.ElementAt(0);
            for (int i = 1; i < dados.Count; i++)
            {
                if (dados.ElementAt(i).CompareTo(maior) > 0)
                    maior = dados.ElementAt(i);
            }
            return maior;
        }

        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS v0.6\n================");
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
            Console.WriteLine("5 - Obter maior Pedido do dia");
            Console.WriteLine("6 - Relatório de cliente");
            Console.WriteLine("7 - Atualizar programa de fidelidade");
            Console.WriteLine("=================================");
            Console.WriteLine("8 - Relatório resumido de clientes");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
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
            return new PedidoLocal();
        }

        static Pedido CriarPedidoEntrega() {
            Console.WriteLine("Pedido para Entrega.");
            Console.Write("Distância: ");
            double dist = double.Parse(Console.ReadLine());
            return new PedidoEntrega(dist);
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
            pedidos.AddLast(pedido);
        }

        static Pedido LocalizarPedido() {
            Cabecalho();
            Console.WriteLine("Localizando um pedido");
            Console.Write("Digite o número do pedido: ");
            int numero = int.Parse(Console.ReadLine());
            Pedido localizado = null;

            for (int i = 0; i < pedidos.Count && localizado == null; i++) {
                if (pedidos.ElementAt(i).GetHashCode() == numero)
                    localizado = pedidos.ElementAt(i);
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

        static void ExibirMaior<T>(ICollection<T> dados) where T: IComparable<T>
        {
            Cabecalho();
            Console.WriteLine("Pedido mais caro do dia:");
            Console.WriteLine(maiorDoConjunto(dados));
        }

        
        static void RegistrarPedidoParaCliente(Pedido pedido)
        {  
            int id = lerNumero("ID do cliente");
            Cliente cliente = clientes.Get(id);
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
                clientes.Get(0).RegistrarPedido(pedido);
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
                Cliente cliente = clientes.Get(id);
                mensagem = cliente.RelatorioPedidos();
            }catch(KeyNotFoundException knfex) {
                mensagem = $"Cliente com id {id} não existe.";
            }
            Console.WriteLine($"\n{mensagem}");
        }

        static void AtualizarFidelidade() {
            //foreach (Cliente cliente in clientes.Values) {
            //    cliente.AtualizarCategoria();
            //}
            //Console.WriteLine("Categorias atualizadas.");
        }

        static void RelatorioResumidoClientes() {
            Cabecalho();
            Console.WriteLine(clientes.Report());
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
                    case 5:
                        ExibirMaior(pedidos);
                        break;
                    case 6:
                        RelatorioCliente();
                        break;
                    case 7:
                        AtualizarFidelidade();
                        break;
                    case 8:
                        RelatorioResumidoClientes();
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
