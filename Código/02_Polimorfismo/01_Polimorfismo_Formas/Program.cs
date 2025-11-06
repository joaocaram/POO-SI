using System;

namespace PoliFiguras {
    internal class Program {
        static Random aleatorio = new Random(42);

        static int MenuPrincipal() {
            string linha = "=========================";
            //TODO: refatorar menu
            Console.Clear();
            Console.WriteLine("FORMAS E MAIS FORMAS");
            Console.WriteLine(linha);
            Console.WriteLine("CRIAR / LISTAR / LOCALIZAR");
            Console.WriteLine("1 - Criar novo conjunto");
            Console.WriteLine("2 - Adicionar forma fixa");
            Console.WriteLine("3 - Listar todas as formas");
            Console.WriteLine("4 - Pegar/Localizar um elemento");
            Console.WriteLine("5 - Maior área");
            Console.WriteLine(linha);
            Console.WriteLine("0 - Sair");
            Console.Write("\nSua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static FormaGeometrica GerarForma() {
            int tipo = aleatorio.Next(1, 5);
            double dimensao1 = Math.Round((2 + aleatorio.NextDouble() * 7.9),2);
            double dimensao2 = Math.Round((2 + aleatorio.NextDouble() * 7.9), 2);
            int posX = 1 + aleatorio.Next(80);
            int posY = 1 + aleatorio.Next(80);
            return tipo switch {
                1 => new Circulo(dimensao1, posX, posY),
                2 => new Retangulo(dimensao1, dimensao2, posX, posY),
                3 => new TrianguloRetangulo(dimensao1, dimensao2, posX, posY),
                4 or _ => new Quadrado(dimensao1, posX, posY)
            };
        }

        static ConjuntoGeometrico GerarConjunto() {
            Console.Write("Quantas formas geométricas você quer gerar? ");
            int quantas = int.Parse(Console.ReadLine());
            if (quantas < 1)
                quantas = 1;
            ConjuntoGeometrico formas = new ConjuntoGeometrico(quantas+5);
            for (int i = 0; i < quantas; i++) {
                FormaGeometrica novaForma = GerarForma();
                formas.AddForma(novaForma);
            }
            return formas;

        }
                

        static void AdicionarFormaFixa(ConjuntoGeometrico conjunto) {
            if (conjunto == null)
                conjunto = GerarConjunto();
            FormaGeometrica quadradinhoDe8 = new Quadrado(8, 42, 24);
            conjunto.AddForma(quadradinhoDe8);
        }

        static void Relatorio(ConjuntoGeometrico conjunto) {
            Console.Clear();
            Console.WriteLine($"Conjunto das formas geométricas: {conjunto.ToString()}");
        }

        static void LocalizarElemento(ConjuntoGeometrico conjunto) {
            Console.Clear();
            Console.WriteLine("1 - Círculo");
            Console.WriteLine("2 - Quadrado");
            Console.WriteLine("3 - Retângulo");
            Console.WriteLine("4 - Triângulo Retângulo");
            Console.Write("Escolha a forma: ");
            int forma = int.Parse(Console.ReadLine());
            Console.Write("Qual a primeira dimensão? ");
            double dimensao1 = double.Parse(Console.ReadLine());
            double dimensao2 = 0d;
            if(forma > 2) {
                Console.Write("Qual a segunda dimensão? ");
                dimensao2 = double.Parse(Console.ReadLine());
            }

            FormaGeometrica f = forma switch {
                1 => new Circulo(dimensao1, 1, 1),
                2 => new Quadrado(dimensao1, 1, 1),
                3 => new Retangulo(dimensao1, dimensao2, 1, 1),
                4 or _ => new TrianguloRetangulo(dimensao1, dimensao2, 1, 1)
            };

            FormaGeometrica procurada = conjunto.Buscar(f);

            if (procurada != null)
                Console.WriteLine($"Achei {procurada}");
            else
                Console.WriteLine($"Sem forma com estas características: {f}.");
        }

        

        


        static void Main(string[] args) {

            int opcao = MenuPrincipal();
            ConjuntoGeometrico formas = null;

            while (opcao != 0) {
                switch (opcao) {
                    case 1:
                        formas = GerarConjunto();
                        break;
                    case 2:
                        AdicionarFormaFixa(formas);
                        break;
                    case 3:
                        Relatorio(formas);
                        break;
                    case 4:
                        LocalizarElemento(formas);
                        break;
                    case 5:
                        Console.WriteLine($"Maior pela área: {formas.MaiorDeTodas()}");
                        break;
                    
                }
                Console.WriteLine("Enter para continuar. . .");
                Console.ReadKey();
                opcao = MenuPrincipal();

            }
        }
    }
}