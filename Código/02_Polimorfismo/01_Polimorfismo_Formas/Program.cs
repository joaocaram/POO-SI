namespace PoliFiguras {
    internal class Program {
        static void testeConjunto()
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
            testeConjunto();
        }
    }
}
