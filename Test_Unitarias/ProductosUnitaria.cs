using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class ProductosUnitaria
    {
        private IConexion?  iConexion;
        private Productos?  entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Productos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Productos");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Productos()
            {
                Nombre = "UT-Producto-" + DateTime.Now.Ticks.ToString(),
                Marca = "Taft",
                Categoria = "Fijacion",
                PrecioCompra = 8000,
                PrecioVenta = 15000,
                StockActual = 20,
                Activo = true
            };

            this.iConexion.Productos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdProducto != 0)
                return;
            throw new Exception("No se guardo el registro en Productos");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.StockActual = 10;

            var entry = this.iConexion!.Entry<Productos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdProducto != 0)
                return;
            throw new Exception("No se modifico el registro en Productos");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Productos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
