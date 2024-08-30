using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private int horas;
        /// <summary>
        /// Representa o componente Minuto (entre 0 e 59)
        /// </summary>
        private int minutos;
        /// <summary>
        /// Representa o componente Segundo (entre 0 e 59)
        /// </summary>
        private int segundos;
        #endregion

        #region Construtores
        public Hora(){
            horas = minutos = segundos = 0;
        }

        public Hora(int horas, int minutos, int segundos){
            Ajustar(horas, minutos, segundos);
        }   
        #endregion

        #region Métodos de negócio
        /// <summary>
        /// Ajusta o horário com os parâmetros passados pelo usuário. Chama o método de validação e, em caso de não-validação, reverte o horário para 00:00:00 (meia noite)
        /// </summary>
        /// <param name="hora">A horas para ser ajustada (0 a 23)</param>
        /// <param name="min">O minutos para ser ajustado (0 a 59)</param>
        /// <param name="seg">O segundos para ser ajustado (0 a 59)</param>
        public void Ajustar(int hora, int min, int seg) {
            this.horas = hora;
            this.minutos = min;
            this.segundos = seg;
            if (!Validar()) {
                this.horas = this.minutos = this.segundos = 0;
            }
        }


        /// <summary>
        /// Faz a validação da horas atribuída. Em caso de horas, minutos ou segundos inválidos, sem reverter o horário em caso de não-validação.
        /// </summary>
        /// <returns>TRUE ou FALSE conforme a horas atual é válida ou não </returns>
        private bool Validar() {
            //TODO: revisar o código para melhorá-lo
            bool resposta = false;
            if ((horas >= 0 && horas <= 23) && 
                (minutos >= 0 && minutos <= 59) && 
                (segundos >= 0 && segundos <= 59)) {
                    resposta = true;
            }
            return resposta;
        }

        /// <summary>
        /// Incrementa uma quantidade de segundos a uma hora existente, retornando a nova hora. Não modifica a hora já existente.
        /// Quantidades menores ou iguais a 0 são ignoradas e é retornada uma hora idêntica à atual.
        /// </summary>
        /// <param name="quantidadeSegundos">Quantidade de segundos a incrementar (>0)</param>
        /// <returns>Uma nova hora com os segundos incrementados, podendo ser igual à hora atual em caso de valores não positivos.</returns>
        public Hora Incrementar(int quantidadeSegundos) {
            //TODO: revisar o código para melhorá-lo
            int minutosParaSegundos = 60;
            int horasParaMinutos = 60;
            int horasParaSegundos = minutosParaSegundos * horasParaMinutos;
            Hora novaHora = new Hora();
            novaHora.Ajustar(horas, minutos, segundos);

            if (quantidadeSegundos > 0) {
                int totalSegundos = (horas * horasParaSegundos + minutos * minutosParaSegundos + segundos) + quantidadeSegundos;
                int novosSegundos = totalSegundos % 60;

                int novosMinutos = (totalSegundos / minutosParaSegundos) % horasParaMinutos;
                int novasHoras = (totalSegundos / horasParaSegundos) % 24;

                novaHora.Ajustar(novasHoras, novosMinutos, novosSegundos);
            }
            return novaHora;
        }

        public bool EstahNaFrenteDe(Hora outra) {
            //TODO: revisar o código para melhorá-lo
            int minutosParaSegundos = 60;
            int horasParaSegundos = minutosParaSegundos * 60;

            int esta;
            esta = (horas * horasParaSegundos + minutos * minutosParaSegundos + segundos);
            int aquela;
            aquela = outra.horas * horasParaSegundos + outra.minutos * minutosParaSegundos + outra.segundos;
            return (esta > aquela);
        }

        /// <summary>
        /// Retorna uma string com a horas formatada como HH:MM:SS.
        /// </summary>
        /// <returns>String no formato HH:MM:SS</returns>
        public string HoraFormatada() {
              return $"{horas:D2}:{minutos:D2}:{segundos:D2}";
        }
        #endregion
    }
}
