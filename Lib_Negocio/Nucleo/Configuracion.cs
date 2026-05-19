namespace Lib_Negocio.Nucleo
{
    public class Configuraciones
    {
        public static string obtener(string clave)
        {
            return "server=localhost;database=db_programas;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}