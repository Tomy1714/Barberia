using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class CalificacionesUnitaria
    {
        private IConexion?  iConexion;
        private Calificaciones?  entidad;

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

            var lista = this.iConexion.Calificaciones!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Calificaciones");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Calificaciones()
            {
                IdCita = 1,
                IdBarbero = 1,
                Puntaje = 5,
                Comentario = "Excelente servicio",
                FechaCalificacion = DateTime.Now
            };

            this.iConexion.Calificaciones!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdCalificacion != 0)
                return;
            throw new Exception("No se guardo el registro en Calificaciones");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Puntaje = 4;

            var entry = this.iConexion!.Entry<Calificaciones>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdCalificacion != 0)
                return;
            throw new Exception("No se modifico el registro en Calificaciones");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Calificaciones!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
