using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class CombosNegocio : ICombosNegocio
    {
        private Conexion? IConexion = null;

        public CombosNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<Combos> Listar()
        {
            return this.IConexion!.Combos
                .Include(e => e.Servicios)
                .Take(50).ToList();
        }

        public Combos? Guardar(Combos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCombo != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.Combos.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Combos? Modificar(Combos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCombo == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Combos.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public Combos? Borrar(Combos? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdCombo == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.Combos.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public decimal CalcularPrecioFinal(Combos entidad, decimal descuento)
        {
            return entidad.Servicio!.PrecioBase * (1 - entidad.DescuentoCombo / 100) - descuento;
        }
    }
}
