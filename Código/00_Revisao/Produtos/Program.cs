namespace Produtos {
    internal class Program {
        
        static void lerVetorProdutos(Produto[] vet) {
            Console.Clear();
            for (int i = 0; i < vet.Length; i++){
                vet[i] = new Produto();
                Console.Write("Qual a descrição do produto "+(i+1)+" ? ");
                vet[i].descricao = Console.ReadLine();
                Console.Write("Qual o preço unitário do produto " + (i + 1) + " ? ");
                vet[i].precoUnitario = float.Parse(Console.ReadLine());
            }
        }

        static float somaPrecos(Produto[] vet) {
            float soma = 0;
            for (int i = 0; i < vet.Length; i++) {
                soma += vet[i].precoUnitario;
            }
            return soma;
        }

        static float mediaPrecos(Produto[] vet) {
            float soma = somaPrecos(vet);
            return soma / vet.Length;
        }

        static Produto localizarProduto(string desc, Produto[] vetor) {
            foreach (Produto item in vetor){
                if (item.descricao.Equals(desc))
                    return item;
            }
            return null;
        }


        static void Main(string[] args) {
            const int TAM_VETOR = 3;
            float media;
            string opcao;
            string descricao;

            Produto[] produtos = new Produto[TAM_VETOR];

            lerVetorProdutos(produtos);
            media = mediaPrecos(produtos);

            Console.WriteLine("A média de preço dos produtos é de R$ " + media.ToString("0.##"));

            Console.Write("Deseja consultar um produto (S/N)? ");
            opcao = Console.ReadLine().ToLower();
            if (opcao.Equals("s")) {
                Console.Write("Qual é o nome do produto a consultar? ");
                descricao = Console.ReadLine();
                Produto prod = localizarProduto(descricao, produtos);
                if (prod != null) {
                    Console.WriteLine("PRODUTO LOCALIZADO:");
                    Console.WriteLine("Descrição: " + prod.descricao);
                    Console.WriteLine("Preço unitário: R$ " + prod.precoUnitario);
                }
                else
                    Console.WriteLine("Produto não encontrado.");
            }

            
            Console.ReadKey();



        }
    }
}
