using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Entidades y reglas del negocio
namespace Ejercicio_Integrador_POO.Logica
{
    public class Persona
    {
        public string nombre;
        public string apellido;
        public string telefono;
        public string correo;

        public Persona() { }

        public Persona(string nombre, string apellido, string telefono, string correo)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.correo = correo;
            console.log("hola mundo");
        }
    }
}