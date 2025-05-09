using Dominio;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;



namespace AplicacionConsola
{
    internal class Program
    {
        private static Sistema s;

        static void Main(string[] args)
        {
            try
            {
                Sistema sistema = Sistema.Instancia;
                Program.s = sistema;
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ListarPasajes()
        {
            DateTime fecha1 = SolicitarDateTime("Ingrese la primera fecha (DD/MM/YYYY): ");
            DateTime fecha2 = SolicitarDateTime("Ingrese la segunda fecha (DD/MM/YYYY): ");

            if (fecha1.CompareTo(fecha2) != -1)
            {
                Console.WriteLine("La fecha 1 debe ser menor a la fecha 2.");
                return;
            }
            
            try
            {
                Console.WriteLine(s.RecorrerPasajes(fecha1, fecha2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }

        }

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
                clienteOc.Validar();

                //En s.AgregarUsuario(clienteOc) se van a volver a validar los datos, lo cual es redundante pero es necesario tener la validacion
                //en el metodo para que se validen los datos al momento de la precarga.
                s.AgregarUsuario(clienteOc);

                Console.WriteLine("Cliente nuevo dado de alta: \n" + clienteOc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        static void ListarVuelos()
        {
            try
            {
                Console.WriteLine("Escriba el codigo del aeropuerto (formato IATA en mayuscula):");
                string codigo = Console.ReadLine();
                Aeropuerto.ValidarCodigo(codigo);
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
                Console.WriteLine(e.Message);
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
        
        static DateTime SolicitarDateTime(string mensaje)
        {
            bool esCorrecto = false;
            DateTime retorno = DateTime.Now;
            while (!esCorrecto)
            {
                try
                {
                    Console.WriteLine(mensaje);
                    retorno = DateTime.Parse(Console.ReadLine());
                    esCorrecto = true;
                }catch(Exception e)
                {
                    Console.WriteLine("Formato de fecha incorrecto. Formato: dd/mm/yyyy");
                }
            }
            return retorno;
        }
    }
}
