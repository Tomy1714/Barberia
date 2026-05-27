using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class HorariosNegocio : IHorariosNegocio
    {
        private Conexion? IConexion = null;

        public HorariosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Horarios> Listar()
        {
            return this.IConexion!.Horarios
                .Include(e => e.Empleado)
                .Take(50).ToList();
        }

        public List<HorariosAuditoria> ListarAuditoria()
        {
            return this.IConexion!.HorariosAuditoria
                .Take(50).ToList();
        }

        public Horarios? Guardar(Horarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorario != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Horarios.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.HorariosAuditoria.Add(new HorariosAuditoria
            {
                IdHorario = entidad.IdHorario,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Horarios? Modificar(Horarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorario == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Horarios.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.HorariosAuditoria.Add(new HorariosAuditoria
            {
                IdHorario = entidad.IdHorario,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Horarios? Borrar(Horarios? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdHorario == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Horarios.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.HorariosAuditoria.Add(new HorariosAuditoria
            {
                IdHorario = entidad.IdHorario,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public bool EstaDisponible(Barberos barbero, int hora)
        {
            return hora >= 8 && hora < 20;
        }
    }
}
