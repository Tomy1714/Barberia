using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Nucleo;

namespace MSTestBarberia
{
    [TestClass]
    public class ServiciosTest
    {
        private ServiciosNegocio negocio = null!;

        [TestInitialize]
        public void Inicializar()
        {
            Conexion conexion = new Conexion();
            conexion.StringConexion = Configuraciones.obtener("StringConexion");
            negocio = new ServiciosNegocio(conexion);
            negocio.Configurar(conexion.StringConexion!);
        }

        [TestMethod]
        public void Listar_Retorna_Lista()
        {
            // Act
            var lista = negocio.Listar();

            // Assert
            Assert.IsNotNull(lista);
            Assert.IsInstanceOfType(lista, typeof(List<Servicios>));
        }

        [TestMethod]
        public void Guardar_Entidad_Valida()
        {
            // Arrange
            var entidad = new Servicios
            {
                Nombre = "Corte Clasico", Descripcion = "Corte con tijera", PrecioBase = 25000, DuracionMinutos = 40, Activo = true
            };

            // Act
            var resultado = negocio.Guardar(entidad);

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreNotEqual(0, resultado.IdServicio);
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
            PrecioBase = 30000;

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
