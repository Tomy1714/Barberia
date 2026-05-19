using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class FacturasNegocio : IFacturasNegocio
    {
        private Conexion? IConexion = null;

        public FacturasNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Facturas> Listar()
        {
            return this.IConexion!.Facturas
                .Include(e => e.Pagos)
                .Include(e => e.Clientes)
                .Take(50).ToList();
        }

        public Facturas? Guardar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdFactura != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Facturas.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Facturas? Modificar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdFactura == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Facturas.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Facturas? Borrar(Facturas? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdFactura == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Facturas.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Facturas> PorCliente(int idCliente)
        {
            return this.IConexion!.Facturas
                .Where(f => f.IdCliente == idCliente)
                .Take(50).ToList();
        }
    }
}
