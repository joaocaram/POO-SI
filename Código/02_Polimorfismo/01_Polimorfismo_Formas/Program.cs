namespace PoliFiguras {
    internal class Program {
        static Random aleatorio = new Random(42);

        static int menuPrincipal() {
            Console.Clear();
            Console.WriteLine("FORMAS E MAIS FORMAS");
            Console.WriteLine("1 - Criar novo conjunto");
            Console.WriteLine("2 - Adicionar forma fixa");
            Console.WriteLine("3 - Listar todas as formas");
            Console.WriteLine("0 - Sair");
            Console.Write("Sua opção: ");
            return int.Parse(Console.ReadLine());
        }

        static FormaGeometrica gerarForma() {
            int tipo = aleatorio.Next(1, 5);
            double dimensao1 = 2 + aleatorio.NextDouble() * 7.9;
            double dimensao2 = 2 + aleatorio.NextDouble() * 7.9;
            return tipo switch {
                1 => new Circulo(dimensao1),
                2 => new Retangulo(dimensao1, dimensao2),
                3 => new TrianguloRetangulo(dimensao1, dimensao2),
                4 or _ => new Quadrado(dimensao1)
            };
        }

        static IEnumerable<FormaGeometrica> gerarConjunto(int quantas) {
            IEnumerable<FormaGeometrica> formas = new LinkedList<FormaGeometrica>();
            for (int i = 0; i < quantas; i++) {
                FormaGeometrica n = gerarForma();
                formas = formas.Append(n);
            }
            return formas;

        }
        static void testeConjuntoFixo()
        {
            ConjuntoGeometrico conj = new ConjuntoGeometrico(10);

            conj.addForma(new Circulo(3));
            conj.addForma(new Retangulo(3, 4));
            conj.addForma(new TrianguloRetangulo(3, 4));
            conj.addForma(new Circulo(2.5));
            conj.addForma(new Retangulo(4, 8));

            Console.WriteLine(conj);
            Console.ReadKey();
            FormaGeometrica procura = new Circulo(2.5);
            if (conj.buscar(procura) != null)
            {
                Console.WriteLine("Existe");
            }
            else
                Console.WriteLine("Não existe.");

         
        }

        static void Main(string[] args) {
            int quantidade;
            int opcao = menuPrincipal();
            List<FormaGeometrica> formas = new List<FormaGeometrica>();

            while (opcao != 0) {
                switch (opcao) {
                    case 1:
                        Console.Write("Tamanho do conjunto: ");
                        quantidade = int.Parse(Console.ReadLine());
                        formas = new List<FormaGeometrica>(gerarConjunto(quantidade));
                        break;
                    case 2:
                        FormaGeometrica quadradinhoDe8 = new Quadrado(8);
                        formas.Add(quadradinhoDe8);
                        break;
                    case 3:

                        break;
                }
            }
        }
    }
}
