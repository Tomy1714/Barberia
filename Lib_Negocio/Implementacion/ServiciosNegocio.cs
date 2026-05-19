using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class ServiciosNegocio : IServiciosNegocio
    {
        private Conexion? IConexion = null;

        public ServiciosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Servicios> Listar()
        {
            return this.IConexion!.Servicios
                .Take(50).ToList();
        }

        public Servicios? Guardar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicio != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Servicios.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Servicios? Modificar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicio == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Servicios.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Servicios? Borrar(Servicios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicio == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Servicios.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
