using Microsoft.Data.SqlClient;

namespace Ejercicio_Integrador_POO.BD
{
    public static class Conexion
    {
        private static string connectionString =
            "Server=TU_SERVIDOR;Database=ClubDB;Trusted_Connection=True;";
        public static SqlConnection ObtenerConexion()
        {
            var conexion = new SqlConnection(connectionString);
            conexion.Open();
            return conexion;
        }
    }
}