using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class ClientesNegocio : IClientesNegocio
    {
        private Conexion? IConexion = null;

        public ClientesNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Clientes> Listar()
        {
            return this.IConexion!.Clientes
                .Include(e => e.Persona)
                .Take(50).ToList();
        }

        public Clientes? Guardar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCliente != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Clientes.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clientes? Modificar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCliente == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Clientes.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Clientes? Borrar(Clientes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCliente == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Clientes.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Clientes> PorVisitas(int minVisitas)
        {
            return this.IConexion!.Clientes
                .Where(c => c.TotalVisitas >= minVisitas)
                .Take(50).ToList();
        }

        public bool EsClienteFrecuente(Clientes entidad)
        {
            return entidad.TotalVisitas > 10;
        }
    }
}
