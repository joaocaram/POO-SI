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
            Console.WriteLine("4 - Pegar um elemento");
            Console.WriteLine(linha);
            Console.WriteLine("FILTROS ");
            Console.WriteLine("5 - Todas as formas com filtro de área");
            Console.WriteLine("6 - Nomes e áreas com filtro de área");
            Console.WriteLine("7 - Filtro por tamanho e pelo tipo de figura");
            Console.WriteLine("8 - Tipos de formas com área mínima");
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

        static FormaGeometrica GerarForma() {
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

        static IEnumerable<FormaGeometrica> GerarConjunto(int quantas) {
            IEnumerable<FormaGeometrica> formas = new List<FormaGeometrica>(quantas);
            for (int i = 0; i < quantas; i++) {
                FormaGeometrica n = GerarForma();
                formas = formas.Append(n);
            }
            return formas;

        }
        static void TesteConjuntoFixo() {
            ConjuntoGeometrico conj = new ConjuntoGeometrico(10);

            conj.AddForma(new Circulo(3));
            conj.AddForma(new Retangulo(3, 4));
            conj.AddForma(new TrianguloRetangulo(3, 4));
            conj.AddForma(new Circulo(2.5));
            conj.AddForma(new Retangulo(4, 8));

            Console.WriteLine(conj);
            Console.ReadKey();
            FormaGeometrica procura = new Circulo(2.5);
            if (conj.Buscar(procura) != null) {
                Console.WriteLine("Existe");
            }
            else
                Console.WriteLine("Não existe.");


        }
        static IEnumerable<FormaGeometrica> GerarNovoConjunto() {
            Console.Write("Tamanho do conjunto: ");
            int quantidade = int.Parse(Console.ReadLine());
            List<FormaGeometrica> lista = new List<FormaGeometrica>(GerarConjunto(quantidade));
            return lista;
        }

        static void AdicionarFormaFixa(IEnumerable<FormaGeometrica> conjunto) {
            if (conjunto == null)
                conjunto = GerarConjunto(10);
            FormaGeometrica quadradinhoDe8 = new Quadrado(8);
            conjunto.Append(quadradinhoDe8);
            conjunto.Prepend(quadradinhoDe8);
        }

        static string Relatorio(IEnumerable<FormaGeometrica> conjunto) {
            return conjunto.Select(f => f.ToString())
                            .Aggregate((s1, s2) => $"{s1}\n{s2}");
        }

        static void LocalizarElemento(IEnumerable<FormaGeometrica> formas) {
            Console.Write("Escolha o identificador ou posição: ");
            int ident = int.Parse(Console.ReadLine());

            FormaGeometrica? escolhida = formas.FirstOrDefault(f => f.GetHashCode() == ident);

            if (escolhida != null)
                Console.WriteLine($"Pelo id: {escolhida}");
            else
                Console.WriteLine("Sem forma com este id.");

            try {
                escolhida = formas.ElementAt(ident);
                Console.WriteLine($"Pela posição: {escolhida}");
            }
            catch (ArgumentOutOfRangeException ex) {
                Console.WriteLine("Posição inexistente");
            }
        }

        static void FiltroDeFormas(IEnumerable<FormaGeometrica> formas) {
            Console.Write("Qual é a área mínima para o filtro? ");
            double minimo = double.Parse(Console.ReadLine());

            //for(int i=0; i<formas.Count; i++) {
            //    FormaGeometrica formaG = formas.ElementAt(i);
            //    if (formaG.Area() >= minimo)
            //        Console.WriteLine(formaG);
            //}

            //foreach(FormaGeometrica f in formas) {
            //    if (f.Area() >= minimo)
            //        Console.WriteLine(f);
            //}

            //Console.WriteLine(                      //LINQ
            //        from f in formas
            //        where f.Area() >= minimo
            //        select f.ToString()
            //);

            //LINQ Method
            IEnumerable<string> formasMaiores =
                                    formas.Where(f => f.Area() >= minimo)
                                          .Select(f => f.ToString());

            foreach (string f in formasMaiores)
                Console.WriteLine(f);

        }

        static void Somar(ConjuntoGeometrico conjunto) {
            Func<FormaGeometrica, double> quemEuQuero;
            Console.WriteLine("1 - area");
            Console.WriteLine("2 - perimetro");
            int opcao = int.Parse(Console.ReadLine());
            quemEuQuero = opcao switch {
                1 => (f =>f.Area()),
                2 => (f => f.Perimetro())
            };
            Console.WriteLine(conjunto.Somar(quemEuQuero));
        }

        static void Maior(ConjuntoGeometrico conjunto) {
            Comparison<FormaGeometrica> comparacao;
            Console.WriteLine("1 - area");
            Console.WriteLine("2 - perimetro");
            int opcao = int.Parse(Console.ReadLine());
            comparacao= opcao switch {
                1 => ((f1, f2) => f1.Area() > f2.Area() ? 1 : -1),
                2 => ((f1, f2) => f1.Perimetro() > f2.Perimetro() ? 1 : -1),
            };
            Console.WriteLine(conjunto.Maior(comparacao));
        }

        static void FiltroComInformacoes(IEnumerable<FormaGeometrica> conjunto) {
            Console.Write("Qual é a área mínima para o filtro? ");
            double min = double.Parse(Console.ReadLine());
            IEnumerable<object> minhasFormasmaiores =
                                    conjunto.Where(f => f.Area() >= min)
                                            .Select(f => new { nome = f.Nome(), area = f.Area() });

            foreach (object o in minhasFormasmaiores)
                Console.WriteLine(o);
        }

        static void FiltroDuplo(IEnumerable<FormaGeometrica> conjunto) {
            Console.Write("Qual é a área mínima para o filtro? ");
            double minArea = double.Parse(Console.ReadLine());
            Console.Write("Qual é o tipo de figura? ");
            string tipo = Console.ReadLine().ToLower();
            IEnumerable<object> formasFiltradas =
                                    conjunto.Where(f => f.Area() >= minArea)
                                          .Where(f => f.ToString().ToLower().Contains(tipo))
                                          .Select(f => f.ToString());

            foreach (object o in formasFiltradas)
                Console.WriteLine(o);
        }

        static void FormasDistintasPorArea(IEnumerable<FormaGeometrica> conjunto) {
            Console.Write("Qual é a área mínima para o filtro? ");
            double minArea2 = double.Parse(Console.ReadLine());


            IEnumerable<object> formasFiltradas2 =
                                    conjunto.Where(f => f.Area() >= minArea2)
                                          .Select(f => f.Nome())
                                          .Distinct();
            foreach (object o in formasFiltradas2)
                Console.WriteLine(o);
        }

        static FormaGeometrica MaiorDeTodas(IEnumerable<FormaGeometrica> conjunto) {
            Comparer<FormaGeometrica> compMaiorArea = Comparer<FormaGeometrica>.Create(
                            (f1, f2) => f1.Area() > f2.Area() ? 1 : -1
                    );
            return conjunto.Max(compMaiorArea);
        }

        static FormaGeometrica MenorPerimetro(IEnumerable<FormaGeometrica> conjunto) {
            Comparer<FormaGeometrica> compMenorPerimetro = Comparer<FormaGeometrica>.Create(
                            (f1, f2) => f1.Perimetro() > f2.Perimetro() ? 1 : -1
            );
            return conjunto.Min(compMenorPerimetro);
        }

        static void FormasOrdenadas(IEnumerable<FormaGeometrica> conjunto) {
            Comparer<FormaGeometrica> compMenorPerimetro = Comparer<FormaGeometrica>.Create(
                            (f1, f2) => f1.Perimetro() > f2.Perimetro() ? 1 : -1
            );

            Console.Write("Qual é o tipo de figura para o filtro? ");
            string tipoFigura = Console.ReadLine().ToLower();

            // map/reduce <--> where/aggregate

            string resultado = conjunto.Where(f => f.ToString().ToLower().Contains(tipoFigura))
                                        .Order(compMenorPerimetro)
                                        .Select(f => f.ToString())
                                        .Aggregate((s1, s2) => $"{s1}\n{s2}");

            Console.WriteLine(resultado);
        }


        static void Main(string[] args) {

            int opcao = MenuPrincipal();
            IEnumerable<FormaGeometrica> formas = null;

            while (opcao != 0) {
                switch (opcao) {
                    case 1:
                        formas = GerarNovoConjunto();
                        break;
                    case 2:
                        AdicionarFormaFixa(formas);
                        break;
                    case 3:
                        Console.WriteLine("Todas as formas:");
                        Console.WriteLine(Relatorio(formas));
                        break;
                    case 4:
                        LocalizarElemento(formas);
                        break;
                    case 5:
                        FiltroDeFormas(formas);
                        break;
                    case 6:
                        FiltroComInformacoes(formas);
                        break;
                    case 7:
                        FiltroDuplo(formas);
                        break;
                    case 8:
                        FormasDistintasPorArea(formas);
                        break;
                    case 9:
                        Console.WriteLine($"Maior pela área: {MaiorDeTodas(formas)}");
                        break;
                    case 10:
                        Console.WriteLine($"Menor perímetro: {MenorPerimetro(formas)}");
                        break;
                    case 11:
                        Console.WriteLine($"Soma = {formas.Sum(f => f.Area())}");
                        break;
                    case 12:
                        Console.WriteLine($"Média = {formas.Average(f => f.Perimetro())}");
                        break;
                    case 13:
                        FormasOrdenadas(formas);
                        break;
                }
                Console.ReadKey();
                opcao = MenuPrincipal();

            }
        }
    }
}