using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class ServiciosTratamientoNegocio : IServiciosTratamientoNegocio
    {
        private Conexion? IConexion = null;

        public ServiciosTratamientoNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<ServiciosTratamiento> Listar()
        {
            return this.IConexion!.ServiciosTratamiento
                .Include(e => e.Servicio)
                .Take(50).ToList();
        }

        public ServiciosTratamiento? Guardar(ServiciosTratamiento? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicioTratamiento != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.ServiciosTratamiento.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ServiciosTratamiento? Modificar(ServiciosTratamiento? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicioTratamiento == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ServiciosTratamiento.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public ServiciosTratamiento? Borrar(ServiciosTratamiento? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdServicioTratamiento == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.ServiciosTratamiento.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularPrecioFinal(ServiciosTratamiento entidad, decimal descuento)
        {
            return entidad.Servicio!.PrecioBase + (entidad.CostoProducto * entidad.SesionesRequeridas) - descuento;
        }
    }
}
