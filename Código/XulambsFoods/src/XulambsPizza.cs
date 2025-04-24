

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

        static void AdicionarComidas(Pedido pedido) {
            string conf;
            do {
                Comida novaComida = ComprarComida();
                pedido.Adicionar(novaComida);
                Console.Write("\nQuer uma nova pizza (S/N)? ");
                conf = Console.ReadLine().ToUpper();
            } while (conf.Equals("S"));
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


        static int ExibirMenuIngredientes(Comida comida) {
            Cabecalho();
            Console.WriteLine("Personalizar a Comida\n");
            MostrarNota(comida);
            Console.WriteLine("\n1 - Acrescentar ingredientes");
            Console.WriteLine("2 - Retirar ingredientes");
            Console.WriteLine("0 - Não quero alterar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
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
                Console.Write("Quantos ingredientes? ");
                int adicionais = int.Parse(Console.ReadLine());
                switch (opcao) {
                    case 1:
                        comida.AdicionarIngredientes(adicionais);
                        break;
                    case 2:
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
            Cabecalho();
            Console.WriteLine(pedido);
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
                if (pedidos[i].GetHashCode() == numero)
                    localizado = pedidos[i];
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
                    case 0:
                        Console.WriteLine("FLW VLW OBG VLT SMP.");
                        break;
                }
                Console.ReadKey();
            } while (opcao != 0);
        }
    }
}
