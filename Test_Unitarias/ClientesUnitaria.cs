using Lib_Negocio.Entidades;
using Lib_Negocio.Implementacion;
using Lib_Negocio.Interfaces;
using Lib_Negocio.Nucleo;
using Microsoft.EntityFrameworkCore;


namespace Test_Unitarias
{
    [TestClass]
    public class ClientesUnitaria
    {
        private IConexion?  iConexion;
        private Clientes?  entidad;

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

            var lista = this.iConexion.Clientes!.ToList();
            if (lista.Count > 0)
                return;
            throw new Exception("No se encontraron registros en Clientes");
        }

        private void Guardar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad = new Clientes()
            {
                IdPersona = 1,
                TotalVisitas = 0,
                Activo = true
            };

            this.iConexion.Clientes!.Add(this.entidad!);
            this.iConexion.SaveChanges();

            if (this.entidad.IdCliente != 0)
                return;
            throw new Exception("No se guardo el registro en Clientes");
        }

        private void Modificar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.entidad!.TotalVisitas = 5;

            var entry = this.iConexion!.Entry<Clientes>(this.entidad!);
            entry.State = EntityState.Modified;
            this.iConexion!.SaveChanges();

            if (this.entidad.IdCliente != 0)
                return;
            throw new Exception("No se modifico el registro en Clientes");
        }

        private void Borrar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Clientes!.Remove(this.entidad!);
            this.iConexion.SaveChanges();
        }
    }
}
