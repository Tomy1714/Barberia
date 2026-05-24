using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class InventarioProductosUnitaria
    {
        private IConexion?  iConexion;
        private InventarioProductos?  entidad;

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

            var lista = this.iConexion.InventarioProductos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en InventarioProductos");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new InventarioProductos()
            {
                IdInventario = 1,
                IdProducto = 1,
                Cantidad = 10
            };

            this.iConexion.InventarioProductos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdInventarioProducto != 0)
                return;
            throw new Exception("No se guardo el registro en InventarioProductos");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Cantidad = 20;

            var entry = this.iConexion!.Entry<InventarioProductos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdInventarioProducto != 0)
                return;
            throw new Exception("No se modifico el registro en InventarioProductos");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.InventarioProductos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
