using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2025_1.src {

    public abstract class Comida {
        private int _maxIngredientes;
        private string _descricao;
        private double _precoBase;
        private double _valorAdicional;
        protected int _quantidadeIngredientes;

        protected Comida(string desc, int maxAdicionais, double valorBase, double valorAdicional) {
            _descricao = desc;
            if (maxAdicionais < 1)
                maxAdicionais = 1;
            _maxIngredientes = maxAdicionais;
            if (valorBase < 2)
                valorBase = 2;
            _precoBase = valorBase;
            if (valorAdicional < 1)
                valorAdicional = 1;
            _valorAdicional = valorAdicional;

        }
                /// <summary>
        /// Calcula o valor dos adicionais para o preço final da pizza. Atualmente o valor dos adicionais é a multiplicação da quantidade de adicionais por seu valor unitário
        /// </summary>
        /// <returns>Double com o valor a ser cobrado pelos adicionais.</returns>
        protected virtual double ValorAdicionais() {
            return _quantidadeIngredientes * _valorAdicional;
        }
                /// <summary>
        ///Faz a verificação de limites para adicionar ingredientes na pizza.Retorna TRUE/FALSE conforme seja possível ou não adicionar 
        ///esta quantidade de ingredientes.
        /// </summary>
        /// <param name="quantos">Quantidade de ingredientes a adicionar.</param>
        /// <returns>TRUE/FALSE conforme seja possível ou não adicionar esta quantidade de ingredientes.</returns>
        protected bool PodeAlterarIngredientes(int quantos) {
            return (quantos + _quantidadeIngredientes >= 0 &&
                    quantos + _quantidadeIngredientes <= _maxIngredientes);
        }

        public abstract double ValorFinal(); //permite OCP e LSP

        /// <summary>
        /// Tenta adicionar ingredientes na pizza.Caso a adição seja inválida(ultrapassando limites ou com valores negativos), mantém
        /// a quantidade atual de ingredientes.Retorna a quantidade de ingredientes após a execução do método.
        /// </summary>
        /// <param name="quantos">Quantos ingredientes a serem adicionados (>0)</param>
        /// <returns>Quantos ingredientes a pizza tem após a execução</returns>
        public int AdicionarIngredientes(int quantos) {
            if (PodeAlterarIngredientes(quantos)) {
                _quantidadeIngredientes += quantos;
            }
            return _quantidadeIngredientes;
        }

        /// <summary>
        /// Tenta retirar ingredientes na pizza.Caso a adição seja inválida(resultando em  valores negativos), mantém
        /// a quantidade atual de ingredientes.Retorna a quantidade de ingredientes após a execução do método.
        /// </summary>
        /// <param name="quantos">Quantos ingredientes a serem retirados (>0)</param>
        /// <returns>Quantos ingredientes a pizza tem após a execução</returns>
        public int RetirarIngredientes(int quantos) {
            return AdicionarIngredientes(0 - quantos);
        }


        public override string ToString() {
            return $"{_descricao} ({_precoBase:C2}) com {_quantidadeIngredientes} ingredientes ({ValorAdicionais():C2})";
        }
    }
}


