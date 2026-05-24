using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class TurnosNegocio : ITurnosNegocio
    {
        private Conexion? IConexion = null;

        public TurnosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Turnos> Listar()
        {
            return this.IConexion!.Turnos
                .Include(e => e.Barbero)
                .Include(e => e.Sede)
                .Take(50).ToList();
        }

        public Turnos? Guardar(Turnos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdTurno != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Turnos.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Turnos? Modificar(Turnos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdTurno == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Turnos.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Turnos? Borrar(Turnos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdTurno == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Turnos.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public List<Turnos> PorBarbero(int idBarbero)
        {
            return this.IConexion!.Turnos
                .Where(t => t.IdBarbero == idBarbero)
                .Take(50).ToList();
        }
    }
}
