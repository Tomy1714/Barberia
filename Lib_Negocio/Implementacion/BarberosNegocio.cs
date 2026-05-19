using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class BarberosNegocio : IBarberosNegocio
    {
        private Conexion? IConexion = null;

        public BarberosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Barberos> Listar()
        {
            return this.IConexion!.Barberos
                .Include(e => e.Empleados)
                .Take(50).ToList();
        }

        public Barberos? Guardar(Barberos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdBarbero != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Barberos.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Barberos? Modificar(Barberos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdBarbero == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Barberos.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Barberos? Borrar(Barberos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdBarbero == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Barberos.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularSalario(Barberos entidad)
        {
            decimal comision = entidad.ServiciosMes * entidad.ValorPromServicio * (entidad.PorcentajeComision / 100);
            return entidad.Empleado!.SalarioBase + comision;
        }

        public bool EstaDisponible(Barberos entidad, int hora)
        {
            return hora >= 8 && hora < 20;
        }
    }
}
