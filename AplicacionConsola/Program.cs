using Dominio;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;



namespace AplicacionConsola
{//agregar metodos solicitar int, solicitar double, etc.
 //Esos mismos hacen validaciones, por eso no estan dentro de un try ctach jiji shvnsrhunvk

//En luagar de tener el equals en Cliente y Admin, lo podemos poner en Usuario y ta, ya que es la misma condicion

//Para la documentacion no copiamos y pegamos la parte de aplicacion de consola. Solo el codigo de lo otro
//Pedirle a chat GPT una TABLA de los datos precargados
//Si le hicimos cambios a lo que chat GPT nos dio, decir cuales cambios hicimos


    internal class Program
    {
        private static Sistema s = Sistema.Instancia;

        static void Main(string[] args)
        {
            try //Por que no muestra el mensaje correcto? Solo muestra uno generico
            {
                Sistema sistema = s;
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Bienvenidos a nuestra Aerolinea!");

            int seleccion = -1;
            while (seleccion != 0)
            {
                seleccion = SolicitarInt(
                    "Escoja la accion deseada: \n" +
                    "0-Salir.\n" +
                    "1-Listar Clientes.\n" +
                    "2-Listar los vuelos de un aeropuerto.\n" +
                    "3-Dar de alta a un cliente ocasional.\n" +
                    "4-Listar pasajes entre dos fechas.\n",
                    true, 4, 0);

                switch (seleccion)
                {
                    case 4: ListarPasajes();
                        break;
                    case 3: AltaClienteOc();
                        break;
                    case 2: ListarVuelos();
                        break;
                    case 1: ListarClientes();
                        break;
                    case 0: 
                        break;
                }
            }
        }

        /*
         Pedir la info en el switch????
           case 1: foreach(Usuario u) .....
                    Listar Usuarios()
         */

        static void ListarPasajes()
        {
            try
            {
                Console.WriteLine("Ingrese la primera fecha (DD/MM/YYYY): ");
                DateTime fecha1 = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Ingrese la segunda fecha (DD/MM/YYYY): ");
                DateTime fecha2 = DateTime.Parse(Console.ReadLine());

                if (fecha1.CompareTo(fecha2) != -1)
                {
                    Console.WriteLine("La fecha 1 debe ser menor a la fecha 2.");
                    return;
                }

                Console.WriteLine(s.RecorrerPasajes(fecha1, fecha2));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); //para evitar el default message aca por eso deberiamos hacer un
                                              //"solicitar date time" que lance la excepcion
            }

        }

        static void AltaClienteOc()
        {
            try
            {
                Console.WriteLine("Escriba el correo del cliente: ");
                string correo = Console.ReadLine();

                Console.WriteLine("Escriba una contraseña (debe tener como minimo 8 caracteres, una mayuscula, una minuscula, y un numero): ");
                string contra = Console.ReadLine();

                Console.WriteLine("Escriba el documento del cliente (no puede contener puntos ni guiones): ");
                string doc = Console.ReadLine();

                Console.WriteLine("Escriba el nombre completo del cliente: ");
                string nom = Console.ReadLine();

                Console.WriteLine("Escriba la nacionalidad del cliente: ");
                string nac = Console.ReadLine();

                Cliente clienteOc = new Ocasional(correo, contra, doc, nom, nac);

                s.AgregarUsuario(clienteOc);

                Console.WriteLine("Cliente nuevo dado de alta: \n" + clienteOc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /*
        static void AltaClienteOc()
        {
                Console.WriteLine("Escriba el correo del cliente: ");
                string correo = Console.ReadLine();

                Console.WriteLine("Escriba una contraseña (debe tener como minimo 8 caracteres, una mayuscula, una minuscula, y un numero): ");
                string contra = Console.ReadLine();

                Console.WriteLine("Escriba el documento del cliente (no puede contener puntos ni guiones): ");
                string doc = Console.ReadLine();

                Console.WriteLine("Escriba el nombre completo del cliente: ");
                string nom = Console.ReadLine();

                Console.WriteLine("Escriba la nacionalidad del cliente: ");
                string nac = Console.ReadLine();

            try
            { 
                Cliente clienteOc = new Ocasional(correo, contra, doc, nom, nac);

                s.AgregarUsuario(clienteOc); //aca adentro tiene el existeUsuario

                Console.WriteLine("Cliente nuevo dado de alta: \n" + clienteOc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
         */

        static void ListarVuelos()
        {
            try
            {
                Console.WriteLine("Escriba el codigo del aeropuerto (formato IATA en mayuscula):");
                string codigo = Console.ReadLine();
                Aeropuerto aeropuerto = s.ObtenerAeropuerto(codigo);
                Console.WriteLine(s.MostrarVuelosAero(aeropuerto));
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void ListarClientes()
        {
            try
            {
                Console.WriteLine(s.MostrarClientes());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        static int SolicitarInt(string mensaje, bool conRango, int maximo, int minimo)
        {
            int retorno = -1;
            bool seleccionCorrecta = false;
            while (!seleccionCorrecta)
            {
                try
                {
                    Console.WriteLine(mensaje);
                    string seleccionString = Console.ReadLine();
                    retorno = int.Parse(seleccionString);
                    if (conRango && (retorno < minimo || retorno > maximo))
                    {
                        Console.WriteLine("Numero incorrecto.");
                    }
                    else
                    {
                        seleccionCorrecta = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Solo se aceptan numeros.");
                }
            }
            return retorno;
        }




    }
}
