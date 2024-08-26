/** 
 * MIT License
 *
 * Copyright(c) 2022-4 João Caram <caram@pucminas.br>
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

//Regra 1: Não Viaje
//Regra 2: Não interessa como o método resolve o problema

namespace Comercio {

    internal class Program {
        static void Main(string[] args) {
            Produto primeiroProduto = new Produto();
            int quantidade;

            primeiroProduto.registrar("Chá mate com gás", 2.99);

            Console.WriteLine("Produto disponível: "+ primeiroProduto.ToString());
            
            primeiroProduto.reajustar(3.25);
          

            Console.WriteLine("Produto disponível: " + primeiroProduto.ToString());
            Console.Write("Quantos você quer comprar? ");
            quantidade = int.Parse(Console.ReadLine());

            Console.WriteLine(quantidade + " produtos vão custar R$ " 
                                        + primeiroProduto.valorLote(quantidade));
        }
    }
}
