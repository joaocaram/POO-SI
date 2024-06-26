﻿using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;

/** 
        * MIT License
        *
        * Copyright(c) 2022-4 João Caram <caram@pucminas.br>
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


namespace XulambsFoods.src
{
    internal class Program
    {
        static double totalVendido;
        static BaseDados<string, Cliente> clientes;
        static BaseDados<int, Pedido> pedidos;

        static void pausa() {
            Console.Write("\nTecle Enter para continuar.");
            Console.ReadKey();
        }
        static void cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS - v0.52");
            Console.WriteLine("=====================");
        }
        static int MenuPrincipal() {
            int opcao;
            cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Total Vendido Hoje");
            Console.WriteLine("------------------------");
            Console.WriteLine("3 - Novo Cliente");
            Console.WriteLine("4 - Relatório de Cliente");
            Console.WriteLine("5 - Resumo dos Clientes");
            Console.WriteLine("6 - Atualizar Fidelidade");
            Console.WriteLine("------------------------");
            Console.WriteLine("7 - Resumo dos Pedidos");
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
            totalVendido = pedidos.totalizar( ped => ped.precoFinal() ); 
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
                        Console.WriteLine($"\n{novaComida} adicionado ao pedido.");
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
                    Console.WriteLine("\nAdicionando Pizza ao pedido:");
                    novaComida = new Pizza(0,clienteQuerBorda());
                    break;
                case 2:
                    Console.WriteLine("\nAdicionando Sanduíche ao pedido:");
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
            String nome;
            Cliente quem;
            cabecalho();
            Console.Write("Digite o nome do cliente: ");
            nome = Console.ReadLine();
            quem = clientes.localizar(nome);
            return quem;

        }

        private static Cliente cadastrarCliente() {
            string nome;
            Cliente novo; 
            cabecalho();
            Console.Write("Qual é o nome do novo cliente? ");
            nome = Console.ReadLine();
            novo = new Cliente(nome);
            clientes.adicionar(nome, novo);
            Console.WriteLine($"\nCliente cadastrado:\n {novo.ToString()}");
            pausa();
            return novo;
        }

        static void registrarPedido(Cliente clienteAtual, Pedido pedidoAtual) {
            clienteAtual.registrarPedido(pedidoAtual);
            pedidos.adicionar(pedidoAtual.GetHashCode(), pedidoAtual);
            double valorAPagar = clienteAtual.valorAPagar(pedidoAtual);
           
            Console.WriteLine("\nPedido fechado: ");
            Console.WriteLine(pedidoAtual.ToString());
            Console.WriteLine("Cliente pagou R$ " + valorAPagar.ToString("0.00"));
            Console.WriteLine("Registrado para "+clienteAtual);
            
        }

        static void gerarClientes() {
            String[] nomes = {"Everson", "Renzo", "Rodrigo", "Jemerson", "Guilherme",
                               "Otávio", "Givanildo", "Matías", "Gustavo",
                                "Paulinho", "Alan" };
            foreach(String nome in nomes) {
                Cliente novo = new Cliente(nome);
                clientes.adicionar(nome, novo);
            }
        }

        private static int MenuComparadoresClientes() {
            int opcao;
            cabecalho();
            Console.WriteLine("1 - Ordem alfabética (padrão)");
            Console.WriteLine("2 - Ordem de identificador");
            Console.WriteLine("3 - Ordenação decrescente por gastos");
            Console.Write("Digite sua opção: ");
            int.TryParse(Console.ReadLine(), out opcao);
            return opcao;
        }

        static Comparison<Cliente>? escolherComparadorCliente() {
            int opcaoComparador = MenuComparadoresClientes();
            Comparison<Cliente> comparadorEscolhido;
            Comparison<Cliente> compPedidos = (
                                                (cli1, cli2) =>
                                                 cli1.totalEmPedidos() >= cli2.totalEmPedidos() ? -1 : 1
                                               );
            comparadorEscolhido = opcaoComparador switch {
                2 => new ComparadorClienteId().Compare,
                3 => compPedidos,
                _ => Comparer<Cliente>.Default.Compare
            };
            return comparadorEscolhido;

        }

        static Comparison<Pedido> escolherComparadorPedido() {
            int opcaoComparador = MenuComparadoresPedidos();
            Comparison<Pedido> comparadorEscolhido; 
            comparadorEscolhido = opcaoComparador switch {
                2 => (ped1, ped2) => ped1.precoFinal() >= ped2.precoFinal() ? -1 : 1,
                _ => (ped1, ped2) => ped1.GetHashCode() - ped2.GetHashCode(),
            }; 
            return comparadorEscolhido;
           
        }

        private static int MenuComparadoresPedidos() {
            int opcao;
            cabecalho();
            Console.WriteLine("1 - Ordem de identificador (padrão)");
            Console.WriteLine("2 - Ordem de decrescente de valor");
            Console.Write("Digite sua opção: ");
            int.TryParse(Console.ReadLine(), out opcao);
            return opcao;
        }

        static void relatorioResumido<K,T>(BaseDados<K,T> dados, Comparison<T> comparador) {
            dados.ordenar(comparador);
            Console.WriteLine(dados.relatorioResumido());
        }
        

        static void Main(string[] args)
        {
            clientes = new BaseDados<string, Cliente>(100);
            pedidos = new BaseDados<int, Pedido>(1000);
            totalVendido = 0d;
            gerarClientes();
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
                            Console.WriteLine($"\n{clienteAtual.resumoPedidos()}");
                        }
                        else {
                            Console.WriteLine("Cliente não encontrado.");
                        }
                        pausa();
                        break;
                    case 5:
                        cabecalho();
                        Comparison<Cliente> comparador = escolherComparadorCliente();
                        relatorioResumido(clientes, comparador);
                        pausa();
                        break;
                    case 6:
                        cabecalho();
                        clientes.processar( cli => cli.verificarCategoria() );
                        pausa();
                        break;
                    case 7:
                        cabecalho();
                        Comparison<Pedido> compPedido = escolherComparadorPedido();
                        relatorioResumido(pedidos, compPedido);
                        pausa();
                        break;
                }
            } while (opcao != 0);
        }

       
    }
}
