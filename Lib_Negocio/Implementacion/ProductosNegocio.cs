using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class ProductosNegocio : IProductosNegocio
    {
        private Conexion? IConexion = null;

        public ProductosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Productos> Listar()
        {
            return this.IConexion!.Productos
                .Take(50).ToList();
        }

        public Productos? Guardar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdProducto != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Productos.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Productos? Modificar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdProducto == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Productos.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Productos? Borrar(Productos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdProducto == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Productos.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Productos> PorCategoria(string categoria)
        {
            return this.IConexion!.Productos
                .Where(p => p.Categoria == categoria)
                .Take(50).ToList();
        }
    }
}
