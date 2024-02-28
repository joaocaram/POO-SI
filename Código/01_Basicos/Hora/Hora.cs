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
        private byte _hora;

        public byte hora{
            get => _hora;
            set{
                if (value >= 0 && value <= 23)
                    _hora = value;
                else
                    _hora = 0;
            }

        }

        /// <summary>
        /// Representa o componente Minuto (entre 0 e 59)
        /// </summary>
        private byte _minuto;
        /// <summary>
        /// Representa o componente Segundo (entre 0 e 59)
        /// </summary>
        private byte _segundo;
        #endregion

        #region Métodos de negócio
        /// <summary>
        /// Faz a validação da hora atribuída. Em caso de hora, _minuto ou _segundo inválidos, sem reverter o horário em caso de não-validação.
        /// </summary>
        /// <returns>TRUE ou FALSE conforme a hora atual é válida ou não </returns>
        public bool validar() {
            bool resposta = false;
            if ((_hora >= 0 && _hora <= 23) && (_minuto >= 0 && _minuto <= 59)
                     && (_segundo >= 0 && _segundo <= 59)) {
                resposta = true;
            }
            return resposta;       
        }

        /// <summary>
        /// Ajusta o horário com os parâmetros passados pelo usuário. Chama o método de validação e, em caso de não-validação, reverte o horário para 00:00:00 (meia noite)
        /// </summary>
        /// <param name="hora">A hora para ser ajustada (0 a 23)</param>
        /// <param name="minuto">O _minuto para ser ajustado (0 a 59)</param>
        /// <param name="segundo">O _segundo para ser ajustado (0 a 59)</param>
        public void ajustar(byte hora, byte minuto, byte segundo) {
            this._hora = hora;
            this._minuto = minuto;
            this._segundo = segundo;
            if (!validar()) {
                this._hora = this._minuto = this._segundo = 0;
            }
        }
       
        /// <summary>
        /// Retorna uma string com a hora formatada como HH:MM:SS.
        /// </summary>
        /// <returns>String no formato HH:MM:SS</returns>
        public string horaFormatada() {
            String horaF = _hora.ToString("00");
            String minF = _minuto.ToString("00");
            String segF = _segundo.ToString("00");

            return horaF + ":" + minF + ":" + segF;

        }
       
        /// <summary>
        /// Verifica se esta hora está na frente (em horário de relógio) de outra hora recebida
        /// por parâmetro. Retorna verdadeiro se a hora estiver estritamente na frente. 
        /// </summary>
        /// <param name="outra">A outra hora para ser comparada com esta.</param>
        /// <returns>TRUE se esta hora está mais à frente, FALSE caso contrário.</returns>
        public bool estahNaFrente(Hora outra) {
            bool resposta = false;
            
            int estaHora = _segundo + _minuto * 60 + _hora * 3600;
            int outraHora = outra._segundo + outra._minuto * 60 + outra._hora * 3600;

            if (estaHora > outraHora)
                resposta = true;

            //String estaHoraString = hora.ToString("00") + _minuto.ToString("00") + _segundo.ToString("00");
            //String outraHoraString = outra.hora.ToString("00") + outra._minuto.ToString("00") + outra._segundo.ToString("00");

            //if(estaHoraString.CompareTo(outraHoraString)>0)
            //    resposta = true;

            //if (hora > outra.hora)
            //    resposta = true;
            //else if (hora == outra.hora && _minuto > outra._minuto)
            //    resposta = true;
            //else if (hora == outra.hora && _minuto == outra._minuto && _segundo > outra._segundo)
            //    resposta = true;

            return resposta;
        }
        
        /// <summary>
        /// Incrementa uma quantidade inteira em algum dos componentes da hora (hora, _minuto
        /// ou _segundo). Ajusta automaticamente o horário resultante para respeitar as regras de 24h, 
        /// 60min e 60seg. 
        /// </summary>
        /// <param name="quant"></param>
        /// <param name="posicao"></param>
        public void incrementar(byte quant, char posicao) {
            string pos = posicao.ToString().ToLower();
            
            switch (pos) {
                case "h": _hora += quant;    
                    break;
                case "m": _minuto += quant;
                    break;
                case "s": _segundo += quant;
                    break;
            }
            ajustarIncremento();
        }

        /// <summary>
        /// Método privado que ajusta a hora após o incremento, verificando os limites de segundos e _minuto (59) e de hora (23).
        /// Ajusta os atributos diretamente usando operações de divisão e resto, de modo que não é necessário retorno de valores.
        /// </summary>
        private void ajustarIncremento() {
            int vaiUm, resto;
            
            vaiUm = _segundo / 60;
            resto = _segundo % 60;
            _segundo = (byte)resto;
            _minuto += (byte)vaiUm;

            vaiUm = _minuto / 60;
            resto = _minuto % 60;
            _minuto = (byte)resto;
            _hora += (byte)vaiUm;

            resto = hora % 24;
            _hora = (byte)resto;
        }

        #endregion
    }
}
