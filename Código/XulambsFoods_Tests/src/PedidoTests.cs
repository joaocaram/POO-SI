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
            pedido = new Pedido(0);
        }

        [TestMethod()]
        public void AdicionaPizzaCorretamente() {
            Assert.AreEqual(1, pedido.Adicionar(pizzaCom2Ingredientes));
        }

        [TestMethod()]
        public void naoAdicionaPizzaEmPedidoFechado() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.FecharPedido();
            Assert.ThrowsException<InvalidOperationException>(
                () => pedido.Adicionar(pizzaSemIngredientes));
        }

        [TestMethod()]
        public void calculaPrecoCorretamente() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.Adicionar(pizzaSemIngredientes);
            Assert.AreEqual(74.8d, pedido.PrecoAPagar(), 0.01);
        }

        [TestMethod()]
        public void relatorioContemDetalhes() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.Adicionar(pizzaSemIngredientes);
            string relatorio = pedido.Relatorio();
            Assert.IsTrue(relatorio.Contains("29,00"));
            Assert.IsTrue(relatorio.Contains("39,00"));
            Assert.IsTrue(relatorio.Contains("74,80"));
        }

        [TestMethod()]
        public void rejeitaComidaNula() {
            Assert.ThrowsException<ArgumentNullException>(
                () => pedido.Adicionar(null));
        }

        [TestMethod()]
        public void pedidoVazioContinuaAbertoAposTentativaDeFechamento() {
            pedido.FecharPedido();

            Assert.AreEqual(1, pedido.Adicionar(pizzaSemIngredientes));
        }

        [TestMethod()]
        public void criaPedidoParaEntregaQuandoDistanciaEhPositiva() {
            Pedido pedidoEntrega = new Pedido(5);

            pedidoEntrega.Adicionar(pizzaSemIngredientes);

            Assert.IsTrue(pedidoEntrega.Relatorio().Contains("ENTREGA"));
            Assert.AreEqual(34d, pedidoEntrega.PrecoAPagar(), 0.01);
        }

        [TestMethod()]
        public void criaPedidoLocalQuandoDistanciaEhZero() {
            Assert.IsTrue(pedido.Relatorio().Contains("LOCAL"));
        }

        [TestMethod()]
        public void toStringRetornaRelatorioDoPedido() {
            Assert.AreEqual(pedido.Relatorio(), pedido.ToString());
        }

        [TestMethod()]
        public void pedidoEhIgualAoProprioIdentificador() {
            Assert.IsTrue(pedido.Equals(pedido.GetHashCode()));
        }

        [TestMethod()]
        public void pedidosDiferentesNaoSaoIguais() {
            Pedido outroPedido = new Pedido(0);

            Assert.IsFalse(pedido.Equals(outroPedido));
        }

        [TestMethod()]
        public void comparaPedidoMaisBaratoComPedidoMaisCaro() {
            Pedido pedidoMaisCaro = new Pedido(0);
            pedidoMaisCaro.Adicionar(pizzaCom2Ingredientes);

            pedido.Adicionar(pizzaSemIngredientes);

            Assert.AreEqual(-1, pedido.CompareTo(pedidoMaisCaro));
            Assert.AreEqual(1, pedidoMaisCaro.CompareTo(pedido));
        }

        [TestMethod()]
        public void comparaPedidosComMesmoPreco() {
            Pedido outroPedido = new Pedido(0);

            Assert.AreEqual(0, pedido.CompareTo(outroPedido));
        }

        [TestMethod()]
        public void pedidoFechadoNaoPodeSerAlterado() {
            pedido.Adicionar(pizzaSemIngredientes);
            pedido.FecharPedido();

            Assert.ThrowsException<InvalidOperationException>(
                () => pedido.Adicionar(pizzaCom2Ingredientes));
        }
    }
}