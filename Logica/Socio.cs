using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
// Entidades y reglas del negocio
namespace Ejercicio_Integrador_POO.Logica
{
    public class Socio
    {
        public int ID_Socio { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public void MostrarSocio()
        {
            Console.WriteLine($"ID: {ID_Socio}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Apellido: {Apellido}");
            Console.WriteLine($"Teléfono: {Telefono}");
            Console.WriteLine($"Correo: {Correo}");
        }
    }
}