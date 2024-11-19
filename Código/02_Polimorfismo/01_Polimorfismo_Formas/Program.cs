namespace PoliFiguras {
    internal class Program {
        static Random aleatorio = new Random(42);

        static int menuPrincipal() {
            Console.Clear();
            Console.WriteLine("FORMAS E MAIS FORMAS");
            Console.WriteLine("1 - Criar novo conjunto");
            Console.WriteLine("2 - Adicionar forma fixa");
            Console.WriteLine("3 - Listar todas as formas");
            Console.WriteLine("4 - Pegar um elemento");
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
            LinkedList<FormaGeometrica> formas = new LinkedList<FormaGeometrica>();
            PriorityQueue<FormaGeometrica, double> filaPrio = new PriorityQueue<FormaGeometrica, double>();

            while (opcao != 0) {
                switch (opcao) {
                    case 1:
                        Console.Write("Tamanho do conjunto: ");
                        quantidade = int.Parse(Console.ReadLine());
                        formas = new LinkedList<FormaGeometrica>(gerarConjunto(quantidade));
                        foreach(FormaGeometrica f in formas) {
                            filaPrio.Enqueue(f, f.area());
                        }
                        break;
                    case 2:
                        FormaGeometrica quadradinhoDe8 = new Quadrado(8);
                        formas.AddLast(quadradinhoDe8);
                        formas.AddFirst(quadradinhoDe8);
                        FormaGeometrica quem = new Quadrado(3.8532488206649429);
                        formas.AddAfter(formas.Find(quem), quadradinhoDe8);
                        break;
                    case 3:
                       foreach (FormaGeometrica f in formas)
                           Console.WriteLine(f);
                        
                        Console.WriteLine("===================");
                        
                        while(filaPrio.Count > 0) {
                            Console.WriteLine(filaPrio.Dequeue());
                        }
                        
                        break;
                    case 4:
                        Console.Write($"Escolha a posição (menor que) {formas.Count}: ");
                        int posicao = int.Parse(Console.ReadLine());
                        FormaGeometrica escolhida = formas.ElementAt(posicao);
                        Console.WriteLine(escolhida);
                        break;
                    case 5:
                        FormaGeometrica forma = new Quadrado(8);
                        formas.Remove(forma);
                        break;
                }
                Console.ReadKey();
                opcao = menuPrincipal();

            }
        }
    }
}
