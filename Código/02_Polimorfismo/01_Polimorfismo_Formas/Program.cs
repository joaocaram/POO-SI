using System;

namespace PoliFiguras {
    internal class Program {
        static Random aleatorio = new Random(42);

        static int menuPrincipal() {
            string linha = "=========================";
            //TODO: refatorar menu
            Console.Clear();
            Console.WriteLine("FORMAS E MAIS FORMAS");
            Console.WriteLine(linha);
            Console.WriteLine("CRIAR / LISTAR / LOCALIZAR");
            Console.WriteLine("1 - Criar novo conjunto");
            Console.WriteLine("2 - Adicionar forma fixa");
            Console.WriteLine("3 - Listar todas as formas");
            Console.WriteLine("4 - Pegar um elemento");
            Console.WriteLine(linha);
            Console.WriteLine("FILTROS ");
            Console.WriteLine("5 - Todas as formas com filtro de área");
            Console.WriteLine("6 - Nomes e áreas com filtro de área");
            Console.WriteLine("7 - Filtro por tamanho e pelo tipo de figura");
            Console.WriteLine("8 - Figuras que atendem uma condição");
            Console.WriteLine(linha);
            Console.WriteLine("TOTALIZADORES ");
            Console.WriteLine("9 - Maior área");
            Console.WriteLine("10 - Menor perímetro");
            Console.WriteLine("11 - Soma das áreas");
            Console.WriteLine("12 - Média dos perímetros");
            Console.WriteLine(linha);
            Console.WriteLine("MAP/REDUCE (WHERE/AGGREGATE) ");
            Console.WriteLine("13 - Figuras ordenadas por perímetro");
            Console.WriteLine(linha);
            Console.WriteLine("0 - Sair");
            Console.Write("\nSua opção: ");
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
            Comparer<FormaGeometrica> compMenorPerimetro = Comparer<FormaGeometrica>.Create(
                                (f1, f2) => f1.perimetro() > f2.perimetro() ? 1 : -1
                        );
            int quantidade;
            int opcao = menuPrincipal();
            LinkedList<FormaGeometrica> formas = new LinkedList<FormaGeometrica>();
            PriorityQueue<FormaGeometrica, double> filaPrio = new PriorityQueue<FormaGeometrica, double>();//heap
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
                            filaPrio.Enqueue(f, f.area());
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
                        tabHash.Add(quadradinhoDe8.GetHashCode(), quadradinhoDe8);
                        arvore.Add(quadradinhoDe8.ToString(), quadradinhoDe8);
                        filaPrio.Enqueue(quadradinhoDe8, quadradinhoDe8.area());
                        break;
                        
                    case 3:
                        Console.WriteLine("Lista:");
                        foreach (FormaGeometrica f in formas)
                            Console.WriteLine(f);
                        Console.WriteLine("===================");
                       
                        Console.WriteLine("Retirando da Fila de prioridades:");
                        while(filaPrio.Count > 0) {
                            FormaGeometrica fg = filaPrio.Dequeue();
                            Console.WriteLine(fg);
                        
                        }
                        Console.WriteLine("===================");
                        
                        Console.WriteLine("Dicionário / hash:");
                        foreach (FormaGeometrica f in tabHash.Values)
                            Console.WriteLine(f);
                        Console.WriteLine("===================");
                        
                        Console.WriteLine("Árvore:");
                        foreach (FormaGeometrica f in arvore.Values)
                            Console.WriteLine(f);

                        break;
                    case 4:
                        Console.Write($"Escolha o identificador ou posição: ");
                        int ident = int.Parse(Console.ReadLine());

                        FormaGeometrica? escolhida = tabHash.GetValueOrDefault(ident);
                        
                        Console.WriteLine("Pelo id (dicionário): "+ escolhida);

                        escolhida = formas.ElementAt(ident);
                        Console.WriteLine("Pela posição (lista): " + escolhida);


                        break;
                    case 5:
                        Console.Write("Qual é a área mínima para o filtro? ");
                        double minimo = double.Parse(Console.ReadLine());

                        //for(int i=0; i<formas.Count; i++) {
                        //    FormaGeometrica formaG = formas.ElementAt(i);
                        //    if (formaG.area() >= minimo)
                        //        Console.WriteLine(formaG);
                        //}

                        //foreach(FormaGeometrica f in formas) {
                        //    if (f.area() >= minimo)
                        //        Console.WriteLine(f);
                        //}

                        //Console.WriteLine(                      //LINQ
                        //        from f in formas
                        //        where f.area() >= minimo
                        //        select f.ToString()
                        //);

                        //LINQ Method
                        IEnumerable<string> formasMaiores =
                                                formas.Where(f => f.area() >= minimo)
                                                      .Select(f => f.ToString());
                        
                        foreach(string f in formasMaiores)
                            Console.WriteLine(f);

                        break;
                    case 6:
                        Console.Write("Qual é a área mínima para o filtro? ");
                        double min = double.Parse(Console.ReadLine());
                        IEnumerable<object> minhasFormasmaiores =
                                                formas.Where(f => f.area() >= min)
                                                      .Select(f => new {nome = f.nome(), area = f.area()});
                        
                        foreach (object o in minhasFormasmaiores)
                            Console.WriteLine(o);

                        break;
                    case 7:
                        Console.Write("Qual é a área mínima para o filtro? ");
                        double minArea = double.Parse(Console.ReadLine());
                        Console.Write("Qual é o tipo de figura? ");
                        string tipo = Console.ReadLine().ToLower();
                        IEnumerable<object> formasFiltradas =
                                                formas.Where(f => f.area() >= minArea)
                                                      .Where(f => f.ToString().ToLower().Contains(tipo))
                                                      .Select(f => f.ToString());

                        foreach (object o in formasFiltradas)
                            Console.WriteLine(o);
                        break;
                    case 8:
                        Console.Write("Qual é a área mínima para o filtro? ");
                        double minArea2 = double.Parse(Console.ReadLine());


                        IEnumerable<object> formasFiltradas2 =
                                                formas.Where(f => f.area() >= minArea2)
                                                      .Select(f => f.nome())
                                                      .Distinct();
                        foreach (object o in formasFiltradas2)
                            Console.WriteLine(o);
                        break;
                    case 9:
                        Comparer<FormaGeometrica> compMaiorArea = Comparer<FormaGeometrica>.Create(
                                (f1, f2) => f1.area() > f2.area() ? 1 : -1
                        );
                        FormaGeometrica maiorDeTodas = formas.Max(compMaiorArea);
                        Console.WriteLine("Maior pela área: " + maiorDeTodas);
                        break;
                    case 10:
                        FormaGeometrica menorPerimetro = formas.Min(compMenorPerimetro);
                        Console.WriteLine("Menor perímetro: " + menorPerimetro);
                        break;
                    case 11:
                        double somaDasAreas = formas.Sum(f => f.area());
                        Console.WriteLine("Soma = " + somaDasAreas);
                        break;
                    case 12:
                        Func<FormaGeometrica, double> perimetros = f => f.perimetro();
                        double somaDosPerimetros = formas.Sum(perimetros);
                        double mediaDosPerimetros = formas.Average(perimetros);
                        Console.WriteLine("Soma = " + somaDosPerimetros);
                        Console.WriteLine("Media = " + mediaDosPerimetros);
                        break;
                    case 13:
                        Console.Write("Qual é o tipo de figura para o filtro? ");
                        string tipoFigura= Console.ReadLine().ToLower();

                        // map/reduce <--> where/aggregate

                        string resultado = formas.Where(f => f.nome().ToLower().Equals(tipoFigura))
                                                  .Order(compMenorPerimetro)
                                                  .Select(f => f.ToString())
                                                  .Aggregate((s1, s2) => s1 + "\n" + s2);

                        Console.WriteLine(resultado);
                        break;
                }
                Console.ReadKey();
                opcao = menuPrincipal();

            }
        }
    }
}
