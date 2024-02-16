/** 
 * MIT License
 *
 * Copyright(c) 2024 João Caram <caram@pucminas.br>
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Produtos {

    /**
     * Programa demonstração do uso de uma classe produtos e operações básicas com objetos
     * Estrutura de dados: vetor
     */
    internal class Program {
        
        /// <summary>
        /// Carrega um vetor de produtos a partir da leitura do usuário
        /// </summary>
        /// <param name="vet">O vetor a ser lido</param>
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

        /// <summary>
        /// Soma os preços dos produtos de um vetor
        /// </summary>
        /// <param name="vet">Vetor com os produtos</param>
        /// <returns>Soma dos preços dos produtos do vetor (float)</returns>
        static float somaPrecos(Produto[] vet) {
            float soma = 0;
            for (int i = 0; i < vet.Length; i++) {
                soma += vet[i].precoUnitario;
            }
            return soma;
        }

        /// <summary>
        /// Média dos preços dos produtos de um vetor
        /// </summary>
        /// <param name="vet">O vetor com os produtos</param>
        /// <returns>A média dos preços unitários dos produtos no vetor (float)</returns>
        static float mediaPrecos(Produto[] vet) {
            float soma = somaPrecos(vet);
            return soma / vet.Length;
        }

        
        /// <summary>
        /// Localiza um produto por sua descrição em um vetor, ainda sem o conceito de encapsulamento.
        /// Retorna o produto localizado (objeto) ou null, se não existir. A busca é case-insensitive (transforma em minúsculas para buscar)
        /// </summary>
        /// <param name="desc">Descrição do produto a ser localizado</param>
        /// <param name="vetor">Vetor com os produtos</param>
        /// <returns>Produto localizado (objeto) ou null se ele não existir.</returns>
        static Produto localizarProduto(string desc, Produto[] vetor) {
            string descMinuscula = desc.ToLower();
            foreach (Produto item in vetor){
                if (item.descricao.ToLower().Equals(descMinuscula))
                    return item;
            }
            return null;
        }

        /// <summary>
        /// Encapsula/modulariza a rotina de consultar um produto no vetor.
        /// </summary>
        /// <param name="produtos">Vetor com os produtos</param>
        static void consultarProduto(Produto[] produtos) {
            string descricao;
            Console.Write("Qual é o nome do produto a consultar? ");
            descricao = Console.ReadLine();
            Produto prod = localizarProduto(descricao, produtos);
            if (prod != null) {
                Console.WriteLine("PRODUTO LOCALIZADO:");
                Console.WriteLine(prod.descricaoProduto());
            }
            else
                Console.WriteLine("Produto não encontrado.");
        }
        static void Main(string[] args) {
            const int TAM_VETOR = 3;
            float media;
            string opcao;
            

            Produto[] produtos = new Produto[TAM_VETOR];

            lerVetorProdutos(produtos);
            media = mediaPrecos(produtos);

            Console.WriteLine("A média de preço dos produtos é de R$ " + media.ToString("0.##"));

            Console.Write("Deseja consultar um produto (S/N)? ");
            opcao = Console.ReadLine().ToLower();

            while (opcao.Equals("s")){

                consultarProduto(produtos);
                           
                Console.Write("Enter para continuar.");
                Console.ReadKey();
                Console.Clear();

                Console.Write("Deseja consultar outro produto (S/N)? ");
                opcao = Console.ReadLine().ToLower();
            }
            
        }
    }
}

