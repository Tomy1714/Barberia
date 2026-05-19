using Lib_Negocio.Entidades;
using Lib_Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lib_Negocio.Implementacion
{
    public class PuntosFidelidadNegocio : IPuntosFidelidadNegocio
    {
        private Conexion? IConexion = null;

        public PuntosFidelidadNegocio(Conexion iConexion)
        {
            this.IConexion = iConexion;
        }

        public void Configurar(string StringConexion)
        {
            this.IConexion!.Database.GetConnectionString();
        }

        public List<PuntosFidelidad> Listar()
        {
            return this.IConexion!.PuntosFidelidad
                .Include(e => e.Clientes)
                .Take(50).ToList();
        }

        public PuntosFidelidad? Guardar(PuntosFidelidad? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPuntos != 0)
                throw new Exception("lbYaSeGuardo");

            this.IConexion!.PuntosFidelidad.Add(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public PuntosFidelidad? Modificar(PuntosFidelidad? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPuntos == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.PuntosFidelidad.Update(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public PuntosFidelidad? Borrar(PuntosFidelidad? entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.IdPuntos == 0)
                throw new Exception("lbNoSeGuardo");

            this.IConexion!.PuntosFidelidad.Remove(entidad);
            this.IConexion.SaveChanges();
            return entidad;
        }

        public int GanarPuntos(PuntosFidelidad entidad, decimal montoPagado)
        {
            int puntos = (int)(montoPagado / 1000);
            entidad.PuntosAcumulados  += puntos;
            entidad.FechaActualizacion = DateTime.Now;
            this.IConexion!.SaveChanges();
            return puntos;
        }

        public decimal CanjearPuntos(PuntosFidelidad entidad, int puntosACanjear)
        {
            if (puntosACanjear <= entidad.PuntosAcumulados)
            {
                entidad.PuntosAcumulados  -= puntosACanjear;
                entidad.FechaActualizacion = DateTime.Now;
                this.IConexion!.SaveChanges();
                return puntosACanjear * entidad.FactorConversion;
            }
            return 0;
        }
    }
}
