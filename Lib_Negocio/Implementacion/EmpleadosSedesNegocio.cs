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

        public List<EmpleadosSedes> Listar()
        {
            return this.IConexion!.EmpleadosSedes
                .Include(e => e.Empleados)
                .Include(e => e.Sedes)
                .Take(50).ToList();
        }

        public EmpleadosSedes? Guardar(EmpleadosSedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleadoSede != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.EmpleadosSedes.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public EmpleadosSedes? Modificar(EmpleadosSedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleadoSede == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.EmpleadosSedes.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public EmpleadosSedes? Borrar(EmpleadosSedes? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdEmpleadoSede == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.EmpleadosSedes.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }
    }
}
