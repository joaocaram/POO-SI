using Microsoft.VisualStudio.TestTools.UnitTesting;
using XulambsFoods_2024_2.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src.Tests {
    [TestClass()]
    public class SanduicheTests {
        static Sanduiche sanduiche;

        [TestInitialize]
        public void SetUp() {
            sanduiche = new Sanduiche();
        }

        [TestMethod()]
        public void criarSanduicheVazio() {
            Assert.AreEqual(15d, sanduiche.ValorFinal());
        }

        [TestMethod()]
        public void criarSanduicheComIngredientesValidos() {
            sanduiche = new Sanduiche(5);
            Assert.AreEqual(30d, sanduiche.ValorFinal());
        }

        [TestMethod()]
        public void naoPodeCriarSanduicheComIngredientesInvalidos() {
            sanduiche = new Sanduiche(8);
            Assert.AreEqual(15d, sanduiche.ValorFinal());
        }

        [TestMethod()]
        public void criarSanduicheCombo() {
            sanduiche = new Sanduiche(true);
            Assert.AreEqual(20d, sanduiche.ValorFinal());
        }


        [TestMethod()]
        public void calculaValorFinal() {
            sanduiche.AdicionarIngredientes(1);
            Assert.AreEqual(18d, sanduiche.ValorFinal());
        }

        [TestMethod()]
        public void calculaValorFinalComCombo() {
            sanduiche = new Sanduiche(true);
            sanduiche.AdicionarIngredientes(1);
            Assert.AreEqual(23d, sanduiche.ValorFinal());
        }

        [TestMethod()]
        public void NotaDeCompraTest() {
            sanduiche = new Sanduiche(true);
            sanduiche.AdicionarIngredientes(1);
            Assert.IsTrue(sanduiche.NotaDeCompra().Contains("Combo"));
            Assert.IsTrue(sanduiche.NotaDeCompra().Contains("1 ingredientes"));
        }
    }
}