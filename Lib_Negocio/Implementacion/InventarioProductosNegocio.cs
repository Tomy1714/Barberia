using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class InventarioProductosNegocio : IInventarioProductosNegocio
    {
        private Conexion? IConexion = null;

        public InventarioProductosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<InventarioProductos> Listar()
        {
            return this.IConexion!.InventarioProductos
                .Include(e => e.Inventario)
                .Include(e => e.Producto)
                .Take(50).ToList();
        }

        public InventarioProductos? Guardar(InventarioProductos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdInventarioProducto != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.InventarioProductos.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public InventarioProductos? Modificar(InventarioProductos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdInventarioProducto == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.InventarioProductos.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public InventarioProductos? Borrar(InventarioProductos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdInventarioProducto == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.InventarioProductos.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
