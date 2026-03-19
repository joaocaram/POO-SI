
namespace XulambsFoods {
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

    /// <summary>
    /// Classe Pizza para a Xulambs Pizza. Uma pizza tem um preço base e pode ter até 8 ingredientes adicionais. Cada ingrediente tem custo fixo.
    /// A pizza deve emitir uma nota de compra com os seus detalhes.
    /// </summary>

    public class Pizza {

        //Regra 0 -- não entre em pânico
        //Regra 1 -- não viaje
        //Regra 2 -- não interessa
        //Regra 3 -- isto não é um cachimbo

        #region constantes
        private const double PrecoBase = 29d;
        private const double PrecoIngrediente = 5d;
        private const int MaxIngredientes = 8;
        #endregion

        #region atributos
        private int _quantIngredientes;
        private string? _descricao;
        #endregion

        #region construtores
        private void Inicializador(int quantos) {

            AdicionarIngredientes(quantos);
            _descricao = $"Pizza com {_quantIngredientes} adicionais";
        }

        public Pizza() {
            Inicializador(0);
        }

        public Pizza(int quantos) {
            Inicializador(quantos);
        }
        #endregion

        #region métodos privados
        private double ValorAdicionais() {
            return PrecoIngrediente * _quantIngredientes;
        }

        private void ModificarDescricao() {
            _descricao = $"Pizza com {_quantIngredientes} adicionais";
        }

        private bool IngredientesSaoValidos(int quantos) {
            int novosIngredientes = _quantIngredientes + quantos;
            return (novosIngredientes <= MaxIngredientes && quantos > 0);
        }
        #endregion

        #region métodos públicos
        public double CalcularValorFinal() {
            return PrecoBase + ValorAdicionais();
        }

        public int AdicionarIngredientes(int quantos) {
            if (IngredientesSaoValidos(quantos)) {
                _quantIngredientes = _quantIngredientes + quantos;
                ModificarDescricao();
            }
            return _quantIngredientes;
        }

        public string GerarCupom() {
            return $"--- CUPOM XULAMBS PIZZA ---\n" +
            $"{_descricao}\n" +
            $"\tPizza Simples:  {PrecoBase:C2}\n" +
            $"\tAdicionais:  {ValorAdicionais():C2}\n" +
            $"---------------------------\nTOTAL:  {CalcularValorFinal():C2}\n---------------------------";
        }
        #endregion

    }
}