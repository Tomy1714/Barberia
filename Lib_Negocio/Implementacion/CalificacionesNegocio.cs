using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class CalificacionesNegocio : ICalificacionesNegocio
    {
        private Conexion? IConexion = null;

        public CalificacionesNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Calificaciones> Listar()
        {
            return this.IConexion!.Calificaciones
                .Include(e => e.Cita)
                .Include(e => e.Barbero)
                .Take(50).ToList();
        }

        public Calificaciones? Guardar(Calificaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCalificacion != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Calificaciones.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Calificaciones? Modificar(Calificaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCalificacion == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Calificaciones.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Calificaciones? Borrar(Calificaciones? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCalificacion == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Calificaciones.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public bool EsValida(Calificaciones entidad)
        {
            return entidad.Puntaje >= 1 && entidad.Puntaje <= 5;
        }

        public List<Calificaciones> PorBarbero(int idBarbero)
        {
            return this.IConexion!.Calificaciones
                .Where(c => c.IdBarbero == idBarbero)
                .Take(50).ToList();
        }
    }
}
