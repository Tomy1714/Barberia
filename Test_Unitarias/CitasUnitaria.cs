using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class CitasUnitaria
    {
        private IConexion?  iConexion;
        private Citas?  entidad;

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

            var lista = this.iConexion.Citas!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Citas");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Citas()
            {
                IdCliente = 1,
                IdBarbero = 1,
                IdServicio = 1,
                FechaHoraInicio = DateTime.Now.AddDays(1),
                Estado = "Pendiente",
                Observaciones = "Prueba unitaria",
                FechaCreacion = DateTime.Now
            };

            this.iConexion.Citas!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdCita != 0)
                return;
            throw new Exception("No se guardo el registro en Citas");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.Estado = "Confirmada";

            var entry = this.iConexion!.Entry<Citas>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdCita != 0)
                return;
            throw new Exception("No se modifico el registro en Citas");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Citas!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
