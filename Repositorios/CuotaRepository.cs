using System;
using Ejercicio_Integrador_POO.Logica;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Ejercicio_Integrador_POO.Repositorios
{
    public class CuotaRepository
    {
        private readonly string connectionString;

        public CuotaRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // 🔹 1. Agregar nueva cuota
        public void AgregarCuota(Cuota cuota)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Cuotas (ID_Socio, Mes, Monto, Pagada) VALUES (@idSocio, @mes, @monto, @pagada)";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idSocio", cuota.IdSocio);
                    cmd.Parameters.AddWithValue("@mes", cuota.Mes);
                    cmd.Parameters.AddWithValue("@monto", cuota.Monto);
                    cmd.Parameters.AddWithValue("@pagada", cuota.Pagada);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 🔹 2. Listar cuotas por socio
        public List<Cuota> ObtenerPorSocio(int idSocio)
        {
            var cuotas = new List<Cuota>();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Cuotas WHERE ID_Socio = @idSocio";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idSocio", idSocio);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cuotas.Add(new Cuota
                            {
                                IdCuota = (int)reader["ID_Cuota"],
                                IdSocio = (int)reader["ID_Socio"],
                                Mes = (int)reader["Mes"],
                                Monto = (decimal)reader["Monto"],
                                Pagada = (bool)reader["Pagada"]
                            });
                        }
                    }
                }
            }
            return cuotas;
        }

        // 🔹 3. Modificar cuota existente
        public void ActualizarCuota(Cuota cuota)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Cuotas SET Mes = @mes, Monto = @monto, Pagada = @pagada WHERE ID_Cuota = @id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mes", cuota.Mes);
                    cmd.Parameters.AddWithValue("@monto", cuota.Monto);
                    cmd.Parameters.AddWithValue("@pagada", cuota.Pagada);
                    cmd.Parameters.AddWithValue("@id", cuota.IdCuota);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 🔹 4. Eliminar cuota
        public void EliminarCuota(int idCuota)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Cuotas WHERE ID_Cuota = @id";
                console.log("Haciendo cambios de prueba como si alguien ubiera cambiado el codigo")
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idCuota);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
