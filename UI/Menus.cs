using Ejercicio_Integrador_POO.Logica;
using Ejercicio_Integrador_POO.Repositorios;
using Microsoft.Data.SqlClient;


namespace Ejercicio_Integrador_POO.UI
{
    public class Menu
    {
        private SocioRepository socioRepo;
        private CuotaRepository cuotaRepo;
        private bool salir = false;

        public Menu(SocioRepository socioRepo, CuotaRepository cuotaRepo)
        {
            this.socioRepo = socioRepo;
            this.cuotaRepo = cuotaRepo;
        }

        public void Mostrar(string connectionString)
        {
            while (!salir)
            {
                Console.WriteLine("\n=== CRUD Socios ===");
                Console.WriteLine("1. Crear nuevo socio");
                Console.WriteLine("2. Listar todos los socios");
                Console.WriteLine("3. Actualizar socio");
                Console.WriteLine("4. Eliminar socio");
                Console.WriteLine("5. Registrar cuota pagada");
                Console.WriteLine("6. Ver cuotas de un socio");
                Console.WriteLine("7. Modificar una cuota");
                Console.WriteLine("8. Eliminar una cuota");
                Console.WriteLine("9. Salir");
                Console.Write("Seleccione opción: ");

                string opcion = Console.ReadLine()?.Trim() ?? "";

                switch (opcion)
                {
                    case "1":
                        CrearSocio();
                        break;

                    case "2":
                        ListarSocios();
                        break;

                    case "3":
                        ActualizarSocio();
                        break;

                    case "4":
                        EliminarSocio();
                        break;

                    case "5":
                        RegistrarCuotaPagada();
                        break;

                    case "6":
                        VerCuotasDeSocio();
                        break;

                    case "7":
                        modificarCuota();
                        break;

                    case "8":
                        eliminarCuota();
                        break;

                    case "9":
                        mostrarResumenBase();
                        finlaizar();
                        break;

                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }
            }

            void CrearSocio()
            {
                var socio = new Socio();
                Console.Write("Nombre: "); socio.Nombre = Console.ReadLine();
                Console.Write("Apellido: "); socio.Apellido = Console.ReadLine();
                Console.Write("Teléfono: "); socio.Telefono = Console.ReadLine();
                Console.Write("Correo: "); socio.Correo = Console.ReadLine();

                socioRepo.Agregar(socio);
                Console.WriteLine(" Socio agregado correctamente a la base de datos.");
            }

            void ListarSocios()
            {
                var socios = socioRepo.ObtenerTodos();
                if (socios.Count == 0)
                {
                    Console.WriteLine("No hay socios registrados.");
                    return;
                }

                Console.WriteLine($"\n Se encontraron {socios.Count} socios:\n");
                foreach (var s in socios)
                {
                    s.MostrarSocio();
                    Console.WriteLine("-----------------");
                }
            }

            void ActualizarSocio()
            {
                Console.Write("Ingrese ID del socio a actualizar: ");
                int id = int.Parse(Console.ReadLine());
                var socios = socioRepo.ObtenerTodos();
                var socio = socios.Find(s => s.ID_Socio == id);
                if (socio == null)
                {
                    Console.WriteLine(" Socio no encontrado.");
                    return;
                }

                Console.Write($"Nuevo nombre ({socio.Nombre}): ");
                string nuevoNombre = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoNombre)) socio.Nombre = nuevoNombre;

                Console.Write($"Nuevo apellido ({socio.Apellido}): ");
                string nuevoApellido = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoApellido)) socio.Apellido = nuevoApellido;

                Console.Write($"Nuevo teléfono ({socio.Telefono}): ");
                string nuevoTel = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoTel)) socio.Telefono = nuevoTel;

                Console.Write($"Nuevo correo ({socio.Correo}): ");
                string nuevoCorreo = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoCorreo)) socio.Correo = nuevoCorreo;

                socioRepo.Actualizar(socio);
                Console.WriteLine(" Socio actualizado correctamente.");
            }

            void EliminarSocio()
            {
                Console.Write("Ingrese ID del socio a eliminar: ");
                int id = int.Parse(Console.ReadLine());
                socioRepo.Eliminar(id);
                Console.WriteLine(" Socio eliminado correctamente.");
            }

            void RegistrarCuotaPagada()
            {
                Console.Write("Ingrese ID del socio: ");
                if (int.TryParse(Console.ReadLine(), out int idSocio))
                {
                    Console.Write("Ingrese mes (1-12): ");
                    int mes = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese monto: ");
                    decimal monto = decimal.Parse(Console.ReadLine());

                    var cuota = new Cuota
                    {
                        IdSocio = idSocio,
                        Mes = mes,
                        Monto = monto,
                        Pagada = true
                    };

                    cuotaRepo.AgregarCuota(cuota);
                    Console.WriteLine("Cuota registrada correctamente.");
                }
                else Console.WriteLine("ID inválido.");
            }

            void VerCuotasDeSocio()
            {
                Console.Write("Ingrese ID del socio: ");
                if (int.TryParse(Console.ReadLine(), out int socioId))
                {
                    var cuotas = cuotaRepo.ObtenerPorSocio(socioId);
                    if (cuotas.Count == 0)
                        Console.WriteLine(" No hay cuotas registradas.");
                    else
                    {
                        Console.WriteLine($"=== Cuotas del socio {socioId} ===");
                        foreach (var c in cuotas)
                            c.MostrarCuota();
                    }
                }
                else Console.WriteLine(" ID inválido.");
            }

            void modificarCuota()
            {
                Console.Write("Ingrese ID de la cuota a modificar: ");
                if (int.TryParse(Console.ReadLine(), out int idCuota))
                {
                    Console.Write("Nuevo mes (1-12): ");
                    int nuevoMes = int.Parse(Console.ReadLine());
                    Console.Write("Nuevo monto: ");
                    decimal nuevoMonto = decimal.Parse(Console.ReadLine());
                    Console.Write("¿Está pagada? (s/n): ");
                    bool pagada = Console.ReadLine().ToLower() == "s";

                    var cuota = new Cuota
                    {
                        IdCuota = idCuota,
                        Mes = nuevoMes,
                        Monto = nuevoMonto,
                        Pagada = pagada
                    };

                    cuotaRepo.ActualizarCuota(cuota);
                    Console.WriteLine(" Cuota modificada correctamente.");
                }
                else Console.WriteLine(" ID inválido.");
            }


            void eliminarCuota()
            {
                Console.Write("Ingrese ID del socio: ");
                if (!int.TryParse(Console.ReadLine(), out int idSocio))
                {
                    Console.WriteLine("ID inválido.");
                    return;
                }

                // Obtengo las cuotas del socio
                var cuotas = cuotaRepo.ObtenerPorSocio(idSocio);

                if (cuotas.Count == 0)
                {
                    Console.WriteLine("Este socio no tiene cuotas registradas.");
                    return;
                }

                Console.WriteLine($"=== Cuotas del socio {idSocio} ===");
                foreach (var c in cuotas)
                {
                    Console.WriteLine($"ID Cuota: {c.IdCuota}, Mes: {c.Mes}, Monto: {c.Monto}, Pagada: {c.Pagada}");
                }

                Console.Write("Ingrese ID de la cuota que desea eliminar: ");
                if (!int.TryParse(Console.ReadLine(), out int idCuota))
                {
                    Console.WriteLine("ID de cuota inválido.");
                    return;
                }

                // Verifico que la cuota exista
                var cuotaAEliminar = cuotas.Find(c => c.IdCuota == idCuota);
                if (cuotaAEliminar == null)
                {
                    Console.WriteLine("No se encontró la cuota con ese ID para este socio.");
                    return;
                }

                // Confirmación opcional
                Console.WriteLine($"Está por eliminar la cuota del mes {cuotaAEliminar.Mes} con monto {cuotaAEliminar.Monto}. ¿Confirmar? (S/N)");
                string confirm = Console.ReadLine()?.Trim().ToUpper() ?? "N";
                if (confirm == "S")
                {
                    cuotaRepo.EliminarCuota(idCuota);
                    Console.WriteLine("Cuota eliminada correctamente.");
                }
                else
                {
                    Console.WriteLine("Operación cancelada.");
                }
            }

            void mostrarResumenBase()
            {
                Console.WriteLine("\n=== Resumen de la Base de Datos ===");

                using (var conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    // Primero obtenemos todos los socios
                    var socios = new List<(int Id, string Nombre, string Apellido)>();
                    string sqlSocios = "SELECT * FROM Socios";
                    using (var comando = new SqlCommand(sqlSocios, conexion))
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            socios.Add((
                                (int)reader["ID_Socio"],
                                reader["Nombre"].ToString(),
                                reader["Apellido"].ToString()
                            ));
                        }
                    }

                    // Luego obtenemos todas las cuotas
                    var cuotas = new List<(int IdSocio, int Mes, decimal Monto, bool Pagada)>();
                    string sqlCuotas = "SELECT * FROM Cuotas";
                    using (var comando = new SqlCommand(sqlCuotas, conexion))
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cuotas.Add((
                                (int)reader["ID_Socio"],
                                (int)reader["Mes"],
                                Convert.ToDecimal(reader["Monto"]),
                                (bool)reader["Pagada"]
                            ));
                        }
                    }

                    // Mostramos agrupado por socio
                    foreach (var socio in socios)
                    {
                        Console.WriteLine($"\nSocio: {socio.Nombre} {socio.Apellido} (ID {socio.Id})");
                        var cuotasSocio = cuotas.FindAll(c => c.IdSocio == socio.Id);
                        if (cuotasSocio.Count == 0)
                        {
                            Console.WriteLine("  No tiene cuotas registradas.");
                        }
                        else
                        {
                            foreach (var cuota in cuotasSocio)
                            {
                                Console.WriteLine($"  - Cuota Mes {cuota.Mes}: ${cuota.Monto}, Pagada: {(cuota.Pagada ? "Sí" : "No")}");
                            }
                        }
                    }

                    conexion.Close();
                }

                Console.WriteLine("\nPresione cualquier tecla para finalizar...");
                Console.ReadKey();
            }

            void finlaizar() => salir = true;
        }
    }
}