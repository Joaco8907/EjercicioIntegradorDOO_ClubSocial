using System;

namespace Ejercicio_Integrador_POO.Logica
{
    public class Cuota
    {
        public int IdCuota { get; set; }
        public int IdSocio { get; set; }
        public int Mes { get; set; }
        public decimal Monto { get; set; }
        public bool Pagada { get; set; }

        public void MostrarCuota()
        {
            Console.WriteLine($"ID Cuota: {IdCuota} | Mes: {Mes} | Monto: {Monto:C} | Pagada: {(Pagada ? "Sí" : "No")}");
        }
    }
}