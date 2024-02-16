using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


/*
 * Classe Hora simples para demonstração de conceitos básicos de POO. 
 * Primeira fase: atributos, métodos, parâmetros e documentação. 
 * Há muito a evoluir. 
 */
namespace Hora {
    internal class Hora {
        #region Atributos
        /// <summary>
        /// Representa o componente Hora (entre 0 e 23)
        /// </summary>
        public byte hora;
        /// <summary>
        /// Representa o componente Minuto (entre 0 e 59)
        /// </summary>
        public byte minuto;
        /// <summary>
        /// Representa o componente Segundo (entre 0 e 59)
        /// </summary>
        public byte segundo;
        #endregion

        #region Métodos de negócio
        /// <summary>
        /// Faz a validação da hora atribuída. Em caso de hora, minuto ou segundo inválidos, sem reverter o horário em caso de não-validação.
        /// </summary>
        /// <returns>TRUE ou FALSE conforme a hora atual é válida ou não </returns>
        public bool validar() {
            bool resposta = false;
            if ((hora >= 0 && hora <= 23) && (minuto >= 0 && minuto <= 59)
                     && (segundo >= 0 && segundo <= 59)) {
                resposta = true;
            }
            return resposta;       
        }

        /// <summary>
        /// Ajusta o horário com os parâmetros passados pelo usuário. Chama o método de validação e, em caso de não-validação, reverte o horário para 00:00:00 (meia noite)
        /// </summary>
        /// <param name="hora">A hora para ser ajustada (0 a 23)</param>
        /// <param name="min">O minuto para ser ajustado (0 a 59)</param>
        /// <param name="seg">O segundo para ser ajustado (0 a 59)</param>
        public void ajustar(byte hora, byte min, byte seg) {
            this.hora = hora;
            this.minuto = min;
            this.segundo = seg;
            if (!validar()) {
                this.hora = this.minuto = this.segundo = 0;
            }
        }
       
        /// <summary>
        /// Retorna uma string com a hora formatada como HH:MM:SS.
        /// </summary>
        /// <returns>String no formato HH:MM:SS</returns>
        public string horaFormatada() {
            String horaF = hora.ToString("00");
            String minF = minuto.ToString("00");
            String segF = segundo.ToString("00");

            return horaF + ":" + minF + ":" + segF;

        }
        #endregion
    }
}
