using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class AdministradoresNegocio : IAdministradoresNegocio
    {
        private Conexion? IConexion = null;

        public AdministradoresNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Administradores> Listar()
        {
            return this.IConexion!.Administradores
                .Include(e => e.Empleado)
                .Take(50).ToList();
        }

        public Administradores? Guardar(Administradores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdAdministrador != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Administradores.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Administradores? Modificar(Administradores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdAdministrador == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Administradores.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Administradores? Borrar(Administradores? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdAdministrador == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Administradores.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularSalario(Administradores entidad)
        {
            decimal participacion = entidad.UtilidadesUltimoMes * (entidad.PorcentajeUtilidades / 100);
            return entidad.Empleado!.SalarioBase + participacion;
        }
    }
}
