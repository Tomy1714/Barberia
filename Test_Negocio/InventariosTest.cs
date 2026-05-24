using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace MSTestBarberia
{
    [TestClass]
    public class InventariosTest
    {
        private InventariosNegocio negocio = null!;

        [TestInitialize]
        public void Inicializar()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new InventariosNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        [TestMethod]
        public void Listar_Retorna_Lista()
        {
            // Act
            var lista = negocio.Listar();

            // Assert
            Assert.IsNotNull(lista);
            Assert.IsInstanceOfType(lista, typeof(List<Inventarios>));
        }

        [TestMethod]
        public void Guardar_Entidad_Valida()
        {
            // Arrange
            var entidad = new Inventarios
            {
                IdSede = 1, StockMinimo = 5, FechaActualizacion = DateTime.Now
            };

            // Act
            var resultado = negocio.Guardar(entidad);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreNotEqual(0, resultado.IdInventario);
        }

        [TestMethod]
        public void Guardar_Entidad_Nula_LanzaExcepcion()
        {
            // Act & Assert
            Assert.ThrowsException<Exception>(() => negocio.Guardar(null));
        }

        [TestMethod]
        public void Modificar_Entidad_Valida()
        {
            // Arrange
            var lista = negocio.Listar();
            if (lista.Count == 0) return;

            var entidad = lista.First();
            StockMinimo = 10;

            // Act
            var resultado = negocio.Modificar(entidad);

            // Assert
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void Modificar_Entidad_Nula_LanzaExcepcion()
        {
            // Act & Assert
            Assert.ThrowsException<Exception>(() => negocio.Modificar(null));
        }

        [TestMethod]
        public void Borrar_Entidad_Nula_LanzaExcepcion()
        {
            // Act & Assert
            Assert.ThrowsException<Exception>(() => negocio.Borrar(null));
        }
    }
}
