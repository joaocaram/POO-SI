namespace XulambsFoods_2024_2.src {
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
    
    internal class XulambsPizza {
        static void Cabecalho() {
            Console.Clear();
            Console.WriteLine("XULAMBS PIZZA\n=============");
        }

        static int ExibirMenu() {
            Cabecalho();
            Console.WriteLine("1 - Comprar pizza");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static void ComprarPizza() {
            Cabecalho();
            Console.WriteLine("Comprando uma nova pizza:");
            Pizza novaPizza = new Pizza();
            EscolherIngredientes(novaPizza);
            MostrarNota(novaPizza);
        }

        static void EscolherIngredientes(Pizza pizza) {
            Console.Write("Quantos adicionais você deseja? (máx. 8): ");
            int adicionais = int.Parse(Console.ReadLine());
            pizza.AdicionarIngredientes(adicionais);
        }

        static void MostrarNota(Pizza pizza) {
            Console.WriteLine("Você acabou de comprar: ");
            Console.WriteLine(pizza.NotaDeCompra());

        }

        static void Main(string[] args) {
            int opcao = -1;
            do {
                opcao = ExibirMenu();
                switch (opcao) {
                    case 1:
                        ComprarPizza();
                        break;
                }
                Console.ReadKey();
            } while (opcao != 0);
        }
    }
}