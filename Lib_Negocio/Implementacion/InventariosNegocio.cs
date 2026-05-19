using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class InventariosNegocio : IInventariosNegocio
    {
        private Conexion? IConexion = null;

        public InventariosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Inventarios> Listar()
        {
            return this.IConexion!.Inventarios
                .Include(e => e.Sedes)
                .Take(50).ToList();
        }

        public Inventarios? Guardar(Inventarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdInventario != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Inventarios.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Inventarios? Modificar(Inventarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdInventario == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Inventarios.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Inventarios? Borrar(Inventarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdInventario == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Inventarios.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public int ContarProductosCriticos(Inventarios entidad)
        {
            return this.IConexion!.InventarioProductos
                .Count(ip => ip.IdInventario == entidad.IdInventario
                          && ip.Cantidad <= entidad.StockMinimo);
        }
    }
}
