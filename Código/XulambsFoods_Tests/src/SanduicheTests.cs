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
        public void inicializar() {
            sanduiche = new Sanduiche();
        }

        
        [TestMethod()]
        public void CalcularValorFinal() {
            sanduiche.AdicionarIngredientes(3);
            Assert.AreEqual(24d, sanduiche.ValorFinal());
        }

        [TestMethod()]
        public void ValorFinalComCombo() {
            sanduiche = new Sanduiche(true);
            sanduiche.AdicionarIngredientes(3);
            Assert.AreEqual(29d, sanduiche.ValorFinal());
        }


        [TestMethod()]
        public void NotaDeCompraTest() {
            string nota = sanduiche.NotaDeCompra();
            Assert.IsTrue(nota.Contains("0 ingredientes") && 
                          (nota.Contains("15,00")));
        }
    }
}