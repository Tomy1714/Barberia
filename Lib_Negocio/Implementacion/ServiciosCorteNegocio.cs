using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class ServiciosCorteNegocio : IServiciosCorteNegocio
    {
        private Conexion? IConexion = null;

        public ServiciosCorteNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<ServiciosCorte> Listar()
        {
            return this.IConexion!.ServiciosCorte
                .Include(e => e.Servicio)
                .Take(50).ToList();
        }

        public ServiciosCorte? Guardar(ServiciosCorte? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicioCorte != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.ServiciosCorte.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ServiciosCorte? Modificar(ServiciosCorte? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicioCorte == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ServiciosCorte.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ServiciosCorte? Borrar(ServiciosCorte? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicioCorte == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ServiciosCorte.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularPrecioFinal(ServiciosCorte entidad, decimal descuento)
        {
            return entidad.Servicio!.PrecioBase + entidad.RecargoComplejidad - descuento;
        }
    }
}
