namespace Media
{
    internal class Program{
        
        static void Main(string[] args){
            
            float[] vetor;
            int tamanhoVetor;
            float soma;
            float media;

            Console.Clear();
            Console.Write("Qual o tamanho do vetor a ser lido? ");
            tamanhoVetor = int.Parse(Console.ReadLine());
            vetor = new float[tamanhoVetor];
            Console.WriteLine();
            
            for (int i = 0; i < tamanhoVetor; i++){
                Console.Write("Digite o valor " + (i + 1) + ": ");
                vetor[i] = float.Parse(Console.ReadLine());
            }

            soma = 0;

            for (int i = 0; i < tamanhoVetor; i++) {
                soma += vetor[i];
            }

            media = soma / tamanhoVetor;

            Console.WriteLine("A média dos valores do vetor é de " + media.ToString("0.##"));
            Console.ReadKey();


        }
    }
}
