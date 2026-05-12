using System;

namespace PoliFiguras {
    internal class PolimorfismoFormas {
        static Random aleatorio = new Random(42);
        static ConjuntoGeometrico formas = null;
        static int MenuPrincipal() {
            string linha = "=========================";
            //TODO: refatorar menu
            Console.Clear();
            Console.WriteLine("FORMAS E MAIS FORMAS");
            Console.WriteLine(linha);
            Console.WriteLine("CRIAR / LISTAR / LOCALIZAR");
            Console.WriteLine("1 - Criar novo conjunto aleatoriamente");
            Console.WriteLine("2 - Adicionar forma");
            Console.WriteLine("3 - Listar todas as formas");
            Console.WriteLine("4 - Pegar/Localizar um elemento");
            Console.WriteLine("5 - Remover uma forma");
            Console.WriteLine("6 - Forma com a maior área");
            Console.WriteLine(linha);
            Console.WriteLine("0 - Sair");
            Console.Write("\nSua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static FormaGeometrica GerarFormaAleatoria() {
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

        static void GerarConjunto() {
            Console.Write("Quantas formas geométricas você quer gerar? ");
            int quantas = int.Parse(Console.ReadLine());
            if (quantas < 1)
                quantas = 1;
            formas = new ConjuntoGeometrico(quantas+5);
            for (int i = 0; i < quantas; i++) {
                FormaGeometrica novaForma = GerarFormaAleatoria();
                formas.AddForma(novaForma);
            }
        }
                

        static void AdicionarForma() {
            if (formas == null)
                 GerarConjunto();
            FormaGeometrica nova = CriarForma();
            formas.AddForma(nova);
        }

        static void Relatorio() {
            Console.Clear();
            Console.WriteLine($"Conjunto das formas geométricas:\n{formas}");
        }

        static FormaGeometrica CriarForma() {
            Console.Clear();
            Console.WriteLine("1 - Círculo");
            Console.WriteLine("2 - Quadrado");
            Console.WriteLine("3 - Retângulo");
            Console.WriteLine("4 - Triângulo Retângulo");
            Console.Write("Escolha a forma: ");
            int opcao = int.Parse(Console.ReadLine());
            Console.Write("Qual a primeira dimensão? ");
            double dimensao1 = double.Parse(Console.ReadLine());
            double dimensao2 = 0d;
            if (opcao > 2) {
                Console.Write("Qual a segunda dimensão? ");
                dimensao2 = double.Parse(Console.ReadLine());
            }

            FormaGeometrica forma = opcao switch {
                1 => new Circulo(dimensao1, 1, 1),
                2 => new Quadrado(dimensao1, 1, 1),
                3 => new Retangulo(dimensao1, dimensao2, 1, 1),
                4 or _ => new TrianguloRetangulo(dimensao1, dimensao2, 1, 1)
            };
            return forma;
        }

        static void LocalizarElemento() {
            FormaGeometrica qual = CriarForma();
            FormaGeometrica procurada = formas.Buscar(qual);

            if (procurada != null)
                Console.WriteLine($"Achei {procurada}");
            else
                Console.WriteLine($"Sem forma com estas características: {qual}.");
        }


        
        static void Remover() {
            FormaGeometrica forma = CriarForma();
            int quantos = formas.Remover(forma);
            Console.WriteLine($"Agora o conjunto tem {quantos} formas.");
        }
        


        static void Main(string[] args) {

            int opcao = MenuPrincipal();
            
            Action escolha;
            while (opcao != 0) {
                escolha = opcao switch {
                    1 => () => GerarConjunto(),
                    2 => () => AdicionarForma(),
                    3 => () => Relatorio(),
                    4 => () => LocalizarElemento(),
                    5 => () => Remover(),
                    6 => () => Console.WriteLine($"Maior pela área: {formas.MaiorDeTodas()}")
                };
                escolha.Invoke();
                Console.WriteLine("Enter para continuar. . .");
                Console.ReadKey();
                opcao = MenuPrincipal();

            }
        }
    }
}