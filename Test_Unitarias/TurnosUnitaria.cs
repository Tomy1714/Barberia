using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class TurnosUnitaria
    {
        private IConexion?  iConexion;
        private Turnos?  entidad;

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Consultar();
            Modificar();
            Borrar();
        }

        private void Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Turnos!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Turnos");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Turnos()
            {
                IdBarbero = 1,
                IdSede = 1,
                FechaTurno = DateTime.Now,
                HoraInicio = 8,
                HoraFin = 17,
                Estado = "Programado"
            };

            this.iConexion.Turnos!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdTurno != 0)
                return;
            throw new Exception("No se guardo el registro en Turnos");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Estado = "Completado";

            var entry = this.iConexion!.Entry<Turnos>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdTurno != 0)
                return;
            throw new Exception("No se modifico el registro en Turnos");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Turnos!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
