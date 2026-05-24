using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace Test_Unitarias
{
    [TestClass]
    public class HorariosUnitaria
    {
        private IConexion?  iConexion;
        private Horarios?  entidad;

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

            var lista = this.iConexion.Horarios!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Horarios");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Horarios()
            {
                IdEmpleado = 1,
                HoraEntrada = 8,
                HoraSalida = 17,
                Activo = true
            };

            this.iConexion.Horarios!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdHorario != 0)
                return;
            throw new Exception("No se guardo el registro en Horarios");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.HoraEntrada = 9;

            var entry = this.iConexion!.Entry<Horarios>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdHorario != 0)
                return;
            throw new Exception("No se modifico el registro en Horarios");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Horarios!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
