using Microsoft.Data.SqlClient;
using Ejercicio_Integrador_POO.BD;
using Ejercicio_Integrador_POO.Logica;

namespace Ejercicio_Integrador_POO.Repositorios
{
    public class SocioRepository
    {
        private readonly string connectionString;
        public SocioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void Agregar(Socio socio)
        {
            using var conexion = Conexion.ObtenerConexion();
            string sql = "INSERT INTO Socios (Nombre, Apellido, Telefono, Correo) VALUES (@n, @a, @t, @c)";
            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@n", socio.Nombre);
            cmd.Parameters.AddWithValue("@a", socio.Apellido);
            cmd.Parameters.AddWithValue("@t", socio.Telefono);
            cmd.Parameters.AddWithValue("@c", socio.Correo);
            cmd.ExecuteNonQuery();
        }

        public List<Socio> ObtenerTodos()
        {
            var socios = new List<Socio>();
            using var conexion = Conexion.ObtenerConexion();
            string sql = "SELECT * FROM Socios";
            using var cmd = new SqlCommand(sql, conexion);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                socios.Add(new Socio
                {
                    ID_Socio = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellido = reader.GetString(2),
                    Telefono = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    Correo = reader.IsDBNull(4) ? "" : reader.GetString(4)
                });
            }
            return socios;
        }

        public void Eliminar(int id)
        {
            using var conexion = Conexion.ObtenerConexion();
            string sql = "DELETE FROM Socios WHERE ID_Socio = @id";
            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Socio socio)
        {
            using var conexion = Conexion.ObtenerConexion();
            string sql = @"UPDATE Socios SET Nombre=@n, Apellido=@a, Telefono=@t, Correo=@c 
                           WHERE ID_Socio=@id";
            using var cmd = new SqlCommand(sql, conexion);
            cmd.Parameters.AddWithValue("@n", socio.Nombre);
            cmd.Parameters.AddWithValue("@a", socio.Apellido);
            cmd.Parameters.AddWithValue("@t", socio.Telefono);
            cmd.Parameters.AddWithValue("@c", socio.Correo);
            cmd.Parameters.AddWithValue("@id", socio.ID_Socio);
            cmd.ExecuteNonQuery();
        }
    }
}
