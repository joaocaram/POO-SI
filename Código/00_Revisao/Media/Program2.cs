namespace Media {
    internal class Program2 {

        static int definirTamanho() {
            Console.Clear();
            Console.Write("Qual o tamanho do vetor a ser lido? ");
            return int.Parse(Console.ReadLine());
        }

        static void lerVetorFloat(float[] vet) {
            for (int i = 0; i < vet.Length; i++) {
                Console.Write("Digite o valor " + (i + 1) + ": ");
                vet[i] = float.Parse(Console.ReadLine());
            }
        }

        static float somaVetorFloat(float[] vet) {
            float soma = 0;
            for (int i = 0; i < vet.Length; i++) {
                soma += vet[i];
            }
            return soma;
        }
        
        static float mediaVetorFloat(float[] vet) {
            float soma = somaVetorFloat(vet);
            return soma / vet.Length;
        }

        static void Main(string[] args) {

            float[] vetor;
            float media;

            vetor = new float[definirTamanho()];
            lerVetorFloat(vetor);
            media = mediaVetorFloat(vetor) ;

            Console.WriteLine("A média dos valores do vetor é de " + media.ToString("0.##"));
            Console.ReadKey();

        }
    }
}
