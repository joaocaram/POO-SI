using Microsoft.VisualStudio.TestTools.UnitTesting;
using XulambsFoods_2024_2.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src.Tests {
    [TestClass()]
    public class PedidoTests {
        static Pedido pedido;
        static Pizza pizzaCom2Ingredientes;
        static Pizza pizzaSemIngredientes;

        [TestInitialize]
        public void SetUp() {
            pizzaCom2Ingredientes = new Pizza(2);
            pizzaSemIngredientes = new Pizza();
            pedido = new Pedido();
        }

        [TestMethod()]
        public void AdicionaPizzaCorretamente() {
            Assert.AreEqual(1, pedido.Adicionar(pizzaCom2Ingredientes));
        }

        [TestMethod()]
        public void naoAdicionaPizzaEmPedidoFechado() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.FecharPedido();
            Assert.AreEqual(1, pedido.Adicionar(pizzaSemIngredientes));
        }

        [TestMethod()]
        public void calculaPrecoCorretamente() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.Adicionar(pizzaSemIngredientes);
            Assert.AreEqual(68d, pedido.PrecoAPagar(), 0.01);
        }

        [TestMethod()]
        public void relatorioContemDetalhes() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.Adicionar(pizzaSemIngredientes);
            string relatorio = pedido.Relatorio();
            Assert.IsTrue(relatorio.Contains("29,00"));
            Assert.IsTrue(relatorio.Contains("39,00"));
            Assert.IsTrue(relatorio.Contains("68,00"));
        }
    }
}