using Microsoft.VisualStudio.TestTools.UnitTesting;
using XulambsFoods_2024_2.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src.Tests {
    [TestClass()]
    public class PizzaTests {
        static Pizza pizza;

        [TestInitialize]
        public void SetUp() {
            pizza = new Pizza();
        }

        [TestMethod()]
        public void CriarPizzaVazia() {
            Assert.IsTrue(pizza.NotaDeCompra().Contains("0 ingredientes"));
        }

        [TestMethod()]
        public void CriarPizzarComIngredientes() {
            pizza = new Pizza(2);
            Assert.IsTrue(pizza.NotaDeCompra().Contains("2 ingredientes"));
        }

        [TestMethod()]
        public void CalculaValorCorretamente() {
            pizza.AdicionarIngredientes(2);
            Assert.AreEqual(39d, pizza.ValorFinal());
        }

        [TestMethod()]
        public void AdicionaIngredientesCorretamente() {
            pizza = new Pizza();
            Assert.AreEqual(3, pizza.AdicionarIngredientes(3));
        }

        [TestMethod()]
        public void NaoPodeAdicionarIngredientesAlemDoMaximo() {
            pizza = new Pizza(6);
            Assert.AreEqual(6, pizza.AdicionarIngredientes(3));
        }

        [TestMethod()]
        public void GeraNotaDeCompraComDetalhes() {
            pizza = new Pizza(6);
            Assert.IsTrue(pizza.NotaDeCompra().Contains("6 ingredientes"));
        }

        [TestMethod()]
        public void CalculaValorCorretamenteComDesconto() {
            pizza.AdicionarIngredientes(8);
            Assert.AreEqual(61,5d, pizza.ValorFinal());
        }
    }
}