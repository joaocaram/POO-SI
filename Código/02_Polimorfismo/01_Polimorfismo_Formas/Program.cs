namespace PoliFiguras {
    internal class Program {
        static Random aleatorio = new Random(42);

        static FormaGeometrica gerarForma() {
            int tipo = aleatorio.Next(1, 4);
            double dimensao1 = 2 + aleatorio.NextDouble() * 7.9;
            double dimensao2 = 2 + aleatorio.NextDouble() * 7.9;
            return tipo switch {
                1 => new Circulo(dimensao1),
                2 => new Retangulo(dimensao1, dimensao2),
                3 or _ => new TrianguloRetangulo(dimensao1, dimensao2)
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
            int quantidade = 20;
            

            foreach (FormaGeometrica forma in gerarConjunto(quantidade)) {
            
            }

            
            


        }
    }
}
