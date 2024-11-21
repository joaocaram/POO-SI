namespace PoliFiguras {
    internal class Program {
        static Random aleatorio = new Random(42);

        static int menuPrincipal() {
            //TODO: refatorar menu
            Console.Clear();
            Console.WriteLine("FORMAS E MAIS FORMAS");
            Console.WriteLine("1 - Criar novo conjunto");
            Console.WriteLine("2 - Adicionar forma fixa");
            Console.WriteLine("3 - Listar todas as formas");
            Console.WriteLine("4 - Pegar um elemento");
            Console.WriteLine("5 - Todas as formas com filtro de área");
            Console.WriteLine("6 - Nomes e áreas com filtro de área");
            Console.WriteLine("7 - Soma das áreas");

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
            Dictionary<int, FormaGeometrica> tabHash = new Dictionary<int, FormaGeometrica>();
            SortedDictionary<string, FormaGeometrica> arvore = new SortedDictionary<string, FormaGeometrica>();
            while (opcao != 0) {

                //TODO: organizar exemplos e revisar código
                switch (opcao) {
                    case 1:
                        Console.Write("Tamanho do conjunto: ");
                        quantidade = int.Parse(Console.ReadLine());
                        formas = new LinkedList<FormaGeometrica>(gerarConjunto(quantidade));
                        foreach (FormaGeometrica f in formas) {
                            tabHash.Add(f.GetHashCode(), f);
                            arvore.Add(f.ToString(), f);

                        }
                        break;
                    case 2:
                        FormaGeometrica quadradinhoDe8 = new Quadrado(8);
                        formas.AddLast(quadradinhoDe8);
                        formas.AddFirst(quadradinhoDe8);
                        FormaGeometrica quem = new Quadrado(3.8532488206649429);
                        LinkedListNode<FormaGeometrica>? posicao = formas.Find(quem);
                        if(posicao!=null)
                        {
                            formas.AddAfter(posicao, quadradinhoDe8);
                        }
                        else
                            formas.AddLast(quadradinhoDe8); 
                        break;
                    case 3:
                        foreach (FormaGeometrica f in arvore.Values)
                            Console.WriteLine(f);
                        Console.WriteLine("===================");
                        foreach (FormaGeometrica f in tabHash.Values)
                            Console.WriteLine(f);

                        break;
                    case 4:
                        Console.Write($"Escolha o identificador: ");
                        int ident = int.Parse(Console.ReadLine());
                        FormaGeometrica? escolhida = tabHash.GetValueOrDefault(ident);

                        Console.WriteLine(escolhida);
                        break;
                    case 5:
                        Console.Write("Qual é a área mínima? ");
                        double minimo = double.Parse(Console.ReadLine());
                        IEnumerable<string> maiores =
                                formas.Where(f => f.area() >= minimo)
                                      .Select(f => f.nome())
                                      .Distinct();
                        foreach (string nome in maiores)
                            Console.WriteLine(nome);
                        break;
                    case 6:
                        Console.Write("Qual é a área mínima? ");
                        double min = double.Parse(Console.ReadLine());
                        IEnumerable<object> maioresComNome =
                                formas.Where(f => f.area() >= min)
                                      .Select(f => new { nome = f.nome(), area = f.area() });
                                      
                        foreach (object nome in maioresComNome)
                            Console.WriteLine(nome);
                        break;
                    case 7:
                        double somaDasAreas = formas.Select(f => f.area())
                                                    .Sum();
                        Console.WriteLine("Soma = " + somaDasAreas);
                        break;
                    case 8:
                        double mediaDosPerimetros = formas.Select(f => f.perimetro())
                                                    .Average();
                        Console.WriteLine("Media = " + mediaDosPerimetros);
                        break;
                    case 9:
                        Console.Write("Qual o tipo de forma desejada? ");
                        string? qualTipo = Console.ReadLine()?.ToLower();
                        int numero = 1;
                        Comparer<FormaGeometrica> compArea =
                            Comparer<FormaGeometrica>.Create(
                                    (f1, f2) => f1.area() > f2.area() ? 1 : -1);
                        Func<FormaGeometrica, bool> funcaoNome =
                            f => f.nome().ToLower().Equals(qualTipo);

                        Func<FormaGeometrica, bool> funcaoId=
                            f => f.GetHashCode().Equals(numero);

                        FormaGeometrica? maiorDeTodas = 
                                formas.Where( funcaoId)
                                      .Max(compArea);

                        Console.WriteLine(maiorDeTodas);
                        break;
                    case 10:
                        string todas = formas.Where(f => f.nome().Equals("Quadrado"))
                              .Select(f => f.ToString())
                              .Aggregate((f1, f2) => f1+"\n"+ f2);
                        Console.WriteLine(todas);
                        break;
                }
                Console.ReadKey();
                opcao = menuPrincipal();

            }
        }
    }
}
