using System.Data;
using System.Reflection.Metadata;

namespace Retangulo_POO_M
{
    internal class Program
    {
        /// /////////////////////

        /// Leia a altura e a largura a partir da escolha do usuário.
        /// "Desenhar" um retângulo correspondente.
        /// Sempre é obrigatório desenhar o retângulo inteiro.
        /// EX: altura = 7, largura = 5
        /// XXXXX
        /// X   X
        /// X   X
        /// X   X
        /// X   X
        /// X   X
        /// XXXXX

        /// /////////////////////

        static void Main(string[] args)
        {
            Console.Write("Qual a altura do retângulo? ");
            int altura = int.Parse(Console.ReadLine());
            Console.Write("Qual a largura do retângulo? ");
            int largura = int.Parse(Console.ReadLine());
           
            Retangulo meuRetangulo = new Retangulo(altura, largura);
            Console.WriteLine(meuRetangulo.DesenharRetangulo());          
        }
    }
}
