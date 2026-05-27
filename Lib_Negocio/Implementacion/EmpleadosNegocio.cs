using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class EmpleadosNegocio : IEmpleadosNegocio
    {
        private Conexion? IConexion = null;

        public EmpleadosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Empleados> Listar()
        {
            return this.IConexion!.Empleados
                .Take(50).ToList();
        }

        public List<EmpleadosAuditoria> ListarAuditoria()
        {
            return this.IConexion!.EmpleadosAuditoria
                .Take(50).ToList();
        }

        public Empleados? Guardar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleado != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Empleados.Add(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.EmpleadosAuditoria.Add(new EmpleadosAuditoria
            {
                IdEmpleado = entidad.IdEmpleado,
                Accion = "Insertar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados? Modificar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleado == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Empleados.Update(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.EmpleadosAuditoria.Add(new EmpleadosAuditoria
            {
                IdEmpleado = entidad.IdEmpleado,
                Accion = "Modificar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Empleados? Borrar(Empleados? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleado == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Empleados.Remove(entidad);
            this.IConexion.SaveChanges();

            this.IConexion!.EmpleadosAuditoria.Add(new EmpleadosAuditoria
            {
                IdEmpleado = entidad.IdEmpleado,
                Accion = "Borrar",
                Fecha = DateTime.Now
            });
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
