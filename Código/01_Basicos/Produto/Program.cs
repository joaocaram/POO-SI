//Regra 1: Não Viaje
//Regra 2: Não interessa como ele faz isso

namespace Comercio {

    internal class Program {
        static void Main(string[] args) {
            Produto primeiroProduto = new Produto();
            int quantidade;

            primeiroProduto.registrar("Chá mate com gás", 2.99);

            Console.WriteLine("Produto disponível: "+ primeiroProduto.ToString());
            
            Console.Write("Quantos você quer comprar? ");
            quantidade = int.Parse(Console.ReadLine());

            Console.WriteLine(quantidade + " produtos vão custar R$ " + primeiroProduto.valorLote(quantidade));
        }
    }
}
