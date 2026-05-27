using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class EmpleadosSedesNegocio : IEmpleadosSedesNegocio
    {
        private Conexion? IConexion = null;

        public EmpleadosSedesNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<EmpleadoSede> Listar()
        {
            return this.IConexion!.EmpleadoSede
                .Include(e => e.Empleado)
                .Include(e => e.Sede)
                .Take(50).ToList();
        }

        public List<EmpleadosSedesAuditoria> ListarAuditoria()
        {
            return this.IConexion!.EmpleadosSedesAuditoria
                .Take(50).ToList();
        }

        public EmpleadoSede? Guardar(EmpleadoSede? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleadoSede != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.EmpleadoSede.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.EmpleadosSedesAuditoria.Add(new EmpleadosSedesAuditoria
            {
                IdEmpleadoSede = entidad.IdEmpleadoSede,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public EmpleadoSede? Modificar(EmpleadoSede? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleadoSede == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.EmpleadoSede.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.EmpleadosSedesAuditoria.Add(new EmpleadosSedesAuditoria
            {
                IdEmpleadoSede = entidad.IdEmpleadoSede,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public EmpleadoSede? Borrar(EmpleadoSede? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleadoSede == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.EmpleadoSede.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.EmpleadosSedesAuditoria.Add(new EmpleadosSedesAuditoria
            {
                IdEmpleadoSede = entidad.IdEmpleadoSede,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
