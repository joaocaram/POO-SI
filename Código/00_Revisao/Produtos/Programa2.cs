namespace Produtos {

    public class ProgramaDeTeste1 {
        static void Mainzinho(string[] args) {
            Produto produto1 = new Produto();
            int quantidade;

            produto1.descricao = "Chá mate com gás";
            produto1.precoUnitario = 2.99f;

            Console.WriteLine("Produto disponínel: " + produto1.descricaoProduto());

            Console.Write("Quer comprar quantos? ");
            quantidade = int.Parse(Console.ReadLine());
            Console.WriteLine(quantidade + " " + produto1.descricao + " vão te custar R$ " + 
                produto1.precoPorLote(quantidade).ToString("0.00"));
        }
    }
}
