using Ejercicio_Integrador_POO.Repositorios;
using Ejercicio_Integrador_POO.UI;


//Esto reemplazaria al main ya que no trabajamos mas en memoria sino con la base de datos.
//Primero se crea la base de datos con el script SQL que esta en la carpeta BD.


//asignamos la conexion a la base de datos a una variable
string connectionString = "Server=TU_SERVIDOR;Database=ClubDB;Trusted_Connection=True;";
//una vez creada la base de datos, se usa esta otra variable con la coneccion a la base como parametro.
var socioRepo = new SocioRepository(connectionString);
var cuotaRepo = new CuotaRepository(connectionString);

Menu menu = new Menu(socioRepo, cuotaRepo);
menu.Mostrar(connectionString);