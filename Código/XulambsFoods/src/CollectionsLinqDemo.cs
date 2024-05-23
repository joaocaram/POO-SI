using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods.src {
    internal class CollectionsLinqDemo {

        static List<Cliente> listaClientes = new List<Cliente>(20);
        static Dictionary<int, Cliente> tabelaClientes = new Dictionary<int, Cliente>(20);
        static SortedDictionary<string, Cliente> arvoreClientes = new SortedDictionary<string, Cliente>();
        static PriorityQueue<Pedido, double> pedidos = new PriorityQueue<Pedido, double>(100);
        static BaseDados<int, Cliente> baseClientes = new BaseDados<int, Cliente>(20);

        static void gerarClientes() {
            String[] nomes = {"Everson", "Rodrigo", "Guilherme",
                              "Givanildo", "Matías", "Paulinho", "Alan" };
            foreach (String nome in nomes) {
                Cliente novo = new Cliente(nome);
                listaClientes.Add(novo);
                tabelaClientes.Add(novo.GetHashCode(), novo);
                arvoreClientes.Add(nome, novo);
                baseClientes.adicionar(novo.GetHashCode(), novo);
            }
        }

        static void gerarPedidos() {
            Random aleat = new Random(42);
            Pedido pedido;
            Comida comida;
            for(int i=0; i< listaClientes.Count*10; i++) {
                int tipo = aleat.Next() % 2;
                if (tipo == 0) pedido = new PedidoLocal();
                else pedido = new PedidoDelivery(aleat.Next(14));
                int quantComidas = aleat.Next(4) + 1;
                for (int j = 0; j < quantComidas; j++)
                {
                    tipo = aleat.Next() % 2;
                    int quantAdic = aleat.Next(5);
                    if (tipo == 0) comida = new Pizza(quantAdic);
                    else comida = new Sanduiche(quantAdic);
                    pedido.addComida(comida);
                }
                pedido.fecharPedido();
                int pos = aleat.Next(listaClientes.Count);
                listaClientes[pos].registrarPedido(pedido);
                pedidos.Enqueue(pedido, pedido.precoFinal());
            }
        }


        static void Main(string[] args) {
            
            gerarClientes();
            gerarPedidos();
            Cliente testeCliente;
            Pedido testePedido;

            //================== Localização de dados ==============//
            Console.WriteLine("Localizando dados de cliente por índice na lista: ");
            testeCliente = listaClientes[3];       //por índice na lista
            Console.WriteLine(testeCliente);

            Console.WriteLine("\nLocalizando dados de cliente por busca na lista: ");
            Cliente auxBusca = new Cliente("Paulinho");
            int pos = listaClientes.IndexOf(auxBusca);      //buscando e acessando pela posição
            testeCliente = listaClientes[pos];
            Console.WriteLine(testeCliente);

            Console.WriteLine("\nLocalizando dados de cliente na tabela por ID: ");
            tabelaClientes.TryGetValue(3, out testeCliente);       //buscando na tabela por ID
            Console.WriteLine(testeCliente);

            Console.WriteLine("\nLocalizando dados de cliente na árvore por nome: ");
            arvoreClientes.TryGetValue("Everson", out testeCliente);       //buscando na árvore por nome
            Console.WriteLine(testeCliente);
                       

            Console.WriteLine("\nMostrando o primeiro pedido na fila de prioridades: ");
            testePedido = pedidos.Peek();
            Console.WriteLine(testePedido);

            Console.ReadKey();
            Console.Clear();
           
            //================== Remoção de dados ==============//
            Console.WriteLine("Removendo dados da lista: (Cliente 'Rodrigo'): ");
            testeCliente = new Cliente("Rodrigo");
            listaClientes.Remove(testeCliente);
            foreach (Cliente cli in listaClientes)
                Console.WriteLine(cli + "\n");
            Console.ReadKey();
            Console.Clear();
            
            Console.WriteLine("Removendo dados da lista: (Cliente na posição 0): ");
            listaClientes.RemoveAt(0);
            foreach (Cliente cli in listaClientes)
                Console.WriteLine(cli + "\n");
            Console.ReadKey();
            Console.Clear();
            
            Console.WriteLine("Removendo dados da tabela: (Cliente com id 4): ");
            tabelaClientes.Remove(4, out testeCliente);
            Console.WriteLine("Removido: " + testeCliente);
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Removendo dados da tabela: (Cliente 'Alan'): ");
            arvoreClientes.Remove("Alan", out testeCliente);
            Console.WriteLine("Removido: " + testeCliente);
            Console.WriteLine("Restantes: ");
            foreach (Cliente cli in arvoreClientes.Values)
                Console.WriteLine(cli + "\n");
            Console.ReadKey();
            Console.Clear();
                     
            Console.WriteLine("Desenfileirando da fila de prioridades: ");
            testePedido = pedidos.Dequeue();
            Console.WriteLine(testePedido);
            testePedido= pedidos.Dequeue();
            Console.WriteLine("\n"+testePedido);
            Console.ReadKey();
            Console.Clear();

            //================== Fila de prioridades compDescendente ==============//
            Console.WriteLine("Nova fila de prioridades (descendente): ");

            IComparer<Double> compDescendente = Comparer<Double>.Create((a, b) => b>a ?1:-1);
            PriorityQueue<Pedido, double> filaDescendente = new PriorityQueue<Pedido, double>(pedidos.UnorderedItems, compDescendente);

            Console.WriteLine("Desenfileirando da fila de prioridades descendente: ");
            testePedido = filaDescendente.Dequeue();
            Console.WriteLine(testePedido);
            testePedido = filaDescendente.Dequeue();
            Console.WriteLine("\n" + testePedido);
            Console.ReadKey();
            Console.Clear();

            //================== Cliente: soma e média com LINQ Methods ==============//
            Console.WriteLine("Dados de um cliente usando LINQ Methods: ");
            testeCliente = arvoreClientes["Givanildo"];
            Console.WriteLine($"Cliente {testeCliente}");
            Console.WriteLine($"Gasto total: R$ {testeCliente.totalEmPedidos()}");
            Console.WriteLine($"Média por pedido: R$ {testeCliente.valorMedioPorPedido()}");
            Console.ReadKey();
            Console.Clear();

            
            //================== Clientes: soma e média de todos os clientes direto na lista ==============//
            Console.WriteLine("Soma e média de dados dos clientes direto na lista, usando LINQ Methods: ");
            
            Console.ReadKey();
            Console.Clear();

            //================== Clientes: soma e média de todos os clientes lá na base com LINQ Methods ==============//
            Console.WriteLine("Mesma ação: \nSoma e média de dados dos clientes lá na base, usando LINQ Methods: ");
           
            Console.ReadKey();
            Console.Clear();

            //================== Buscar o cliente com maior gasto na lista ==============//
            Console.WriteLine("LINQ Methods: buscando o cliente com maior gasto na lista:");

           
            Console.ReadKey();
            Console.Clear();

            
            //================== Buscar o cliente com maior gasto lá na base ==============//
            Console.WriteLine("Mesma ação: \nLINQ Methods: buscando o cliente com maior gasto na classe 'BaseDados':");
          
            Console.ReadKey();
            Console.Clear();

            //================== "Consumindo" dados da lista: foreach ==============//
            Console.WriteLine("Clientes ordenados por suas médias de gastos:");
            
            Console.ReadKey();
            Console.Clear();

            //================== "Consumindo" dados da lista: select e aggregate ==============//
            Console.WriteLine("Clientes ordenados por suas médias de gastos:");
           
            
            Console.ReadKey();
            Console.Clear();

            //================== "Filtrando" dados da lista: select e aggregate ==============//
            Console.WriteLine("Clientes ordenados por suas médias de gastos:");
            
            Console.ReadKey();
            Console.Clear();

            //================== "Filtrando" pedidos de um cliente ==============//
            Console.WriteLine("Filtro de pedidos do cliente com maior valor gasto:");
            
            
            Console.ReadKey();
            Console.Clear();

        }

    }
}
