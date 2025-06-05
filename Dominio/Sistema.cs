using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dominio.Comparadores;

namespace Dominio
{
    public class Sistema
    {
        private static Sistema _instancia;
        private List<Usuario> _usuarios;
        private List<Vuelo> _vuelos;
        private List<Aeropuerto> _aeropuertos;
        private List<Avion> _aviones;
        private List<Ruta> _rutas;
        private List<Pasaje> _pasajes;

        public List<Vuelo> Vuelos { get { return _vuelos; } }
        public List<Pasaje> Pasajes { get { return _pasajes; } }
        public static Sistema Instancia
        {
            get
            {
                if(Sistema._instancia == null)
                {
                    Sistema._instancia = new Sistema();
                }
                return Sistema._instancia;
            }
        }

        private Sistema()
        {
            this._usuarios = new List<Usuario>();
            this._vuelos = new List<Vuelo>();
            this._aeropuertos = new List<Aeropuerto>();
            this._rutas = new List<Ruta>();
            this._aviones = new List<Avion>();
            this._pasajes = new List<Pasaje>();
            PrecargarDatos();
        }

        public void PrecargarDatos()
        {  
            PrecargaUsuarios();
            PrecargaAvion();
            PrecargaAeropuerto();
            PrecargaRutas();
            PrecargaVuelos();
            PrecargaPasajes();
        }

        //Filtramos los clientes de la lista de usuarios
        public List<Cliente> ObtenerListaClientes()
        {
            List<Cliente> _clientes = new List<Cliente>();
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario is Cliente cliente)
                {
                    _clientes.Add(cliente);
                }
            }
            return _clientes;
        }

        public string MostrarClientes()
        {
            string clientes = "";
            if(this._usuarios.Count == 0)
            {
                throw new Exception("No hay usuarios ingresados en el sistema.");
            }
            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario is Cliente)
                {
                    clientes += usuario.ToString() + "\n";
                }
            }
            if (string.IsNullOrEmpty(clientes))
            {
                throw new Exception("No hay clientes ingresados en el sistema.");
            }
            return clientes;
        }

        public Aeropuerto ObtenerAeropuerto(string codigo)
        {
            Aeropuerto aeropuertoRetorno = null;
            if (this._aeropuertos.Count == 0)
            {
                throw new Exception("No hay aeropuertos ingresados en el sistema.");
            }
            foreach (Aeropuerto aeropuerto in this._aeropuertos)
            {
                if(aeropuerto.Codigo == codigo)
                {
                    aeropuertoRetorno = aeropuerto;
                }
            }
            if(aeropuertoRetorno == null)
            {
                throw new Exception("No existe ningun aeropuerto con ese codigo.");
            }
            return aeropuertoRetorno;
        }

        //Un metodo para obtener el Vuelo segun el id(numero)
        public Vuelo ObtenerVuelo(string numero)
        {
            if (this._vuelos.Count == 0)
            {
                throw new Exception("No hay vuelos ingresados en el sistema.");
            }
            foreach (Vuelo vuelo in this._vuelos)
            {
                if (vuelo.Numero == numero)
                {
                    return vuelo;
                }
            }
            throw new Exception("No existe ningun vuelo con ese numero.");
        }

        public string MostrarVuelosAero(Aeropuerto aeropuerto)
        {
            string vuelos = "";
            if (this._vuelos.Count == 0)
            {
                throw new Exception("No hay vuelos ingresados en el sistema.");
            }
            foreach (Vuelo vuelo in this._vuelos)
            {
                //Le pedimos a Vuelo que se fije si tiene a ese Aeropuerto en su ruta
                if (vuelo.AeropuertoEnRuta(aeropuerto))
                {
                    vuelos += vuelo.ToString() + "\n";
                }
            }
            if(string.IsNullOrEmpty(vuelos))
            {
                throw new Exception("Ningun vuelo incluye a ese aeropuerto en su ruta.");
            }
            return vuelos;
        }
        
        public void ExisteUsuario(Usuario otro) 
        {
            foreach (Usuario usuario in this._usuarios)
            {
                if (usuario.Equals(otro)) 
                {
                    throw new Exception("Ya existe un usuario con ese correo.");
                }
            }
        }

        public void ExisteAvion(Avion otro)
        {
            foreach (Avion avion in this._aviones)
            {
                if (avion.Equals(otro))
                {
                    throw new Exception("Ya existe ese modelo de avion.");
                }
            }
        }
        
        public void ExisteVuelo(Vuelo otro)
        {
            foreach (Vuelo vuelo in this._vuelos)
            {
                if (vuelo.Equals(otro))
                {
                    throw new Exception("Ya existe un vuelo con ese codigo.");
                }
            }
        }
        
        public void ExisteAeropuerto(Aeropuerto otro)
        {
            foreach (Aeropuerto aeropuerto in this._aeropuertos)
            {
                if (aeropuerto.Equals(otro))
                {
                    throw new Exception("Ya existe un aeropuerto con ese codigo.");
                }
            }
        }
        
        public void ExisteRuta(Ruta otro)
        {
            foreach (Ruta ruta in this._rutas)
            {
                if (ruta.Equals(otro))
                {
                    throw new Exception("Ya existe una ruta con ese recorrido (Aeropuertos de salida y llegada).");
                }
            }
        }

        public string RecorrerPasajes(DateTime fecha1, DateTime fecha2)
        {
            string pasajes = "";
            if (this._pasajes.Count == 0)
            {
                throw new Exception("No hay pasajes ingresados en el sistema.");
            }
            foreach (Pasaje pasaje in this._pasajes) 
            {
                if (pasaje.Fecha >= fecha1 && pasaje.Fecha <= fecha2)
                {
                    pasajes += pasaje.ToString() + "\n";
                }
            }
            if (string.IsNullOrEmpty(pasajes))
            {
                throw new Exception("No hay ningun pasaje entre esas fechas");
            }
            return pasajes;
        }

        /*
        public void AgregarUsuario(Usuario usuario)
        {
            ExisteUsuario(usuario);
            if (usuario is Cliente cliente)
            {
                cliente.Validar();
            } else if (usuario is Administrador administrador)
            {
                administrador.Validar();
            }
            this._usuarios.Add(usuario);
        }
        */

        //Ahora que Validar() es polimorfico, no es necesario chequear que tipo de Usuario es para saber que Validar() usar
        public void AgregarUsuario(Usuario usuario)
        {
            ExisteUsuario(usuario);
            usuario.Validar();
            this._usuarios.Add(usuario);
        }

        public void AgregarAvion(Avion avion)
        {
            ExisteAvion(avion);
            avion.Validar();
            this._aviones.Add(avion);
        }

        public void AgregarVuelo(Vuelo vuelo)
        {
            ExisteVuelo(vuelo);
            vuelo.Validar();
            this._vuelos.Add(vuelo);
        }

        public void AgregarAeropuerto(Aeropuerto aeropuerto)
        {
            ExisteAeropuerto(aeropuerto);
            aeropuerto.Validar();
            this._aeropuertos.Add(aeropuerto);
        }

        public void AgregarRuta(Ruta ruta)
        {
            ExisteRuta(ruta);
            ruta.Validar();
            this._rutas.Add(ruta);
        }

        public void AgregarPasaje(Pasaje pasaje)
        {
            pasaje.Validar();
            this._pasajes.Add(pasaje);
        }

        public List<Pasaje> OrdenarPasajesPorFecha()
        {
            List<Pasaje> pasajesOrdenados = new List<Pasaje>(_pasajes);
            pasajesOrdenados.Sort();
            return pasajesOrdenados;
        }
        public List<Pasaje> OrdenarPasajesPorPrecio()
        { 
            List<Pasaje> pasajesOrdenados = new List<Pasaje>(_pasajes);
            pasajesOrdenados.Sort(new CompararPasajePorPrecio());
            return pasajesOrdenados;
        }

        public List<Cliente> OrdenarClientesPorDocumento(List<Cliente> clientes)
        {
            clientes.Sort();
            return clientes;
        }

        private void PrecargaUsuarios()
        {
            Cliente premium1 = new Premium("juan@mail.com", "Obliga1234", "48012345", "Juan Pérez", "Uruguayo");
            Cliente premium2 = new Premium("ana@mail.com", "ObligaAbcd1", "50345678", "Ana López", "Argentina");
            Cliente premium3 = new Premium("roberto@mail.com", "ObligaPass1", "41234567", "Roberto Díaz", "Chileno");
            Cliente premium4 = new Premium("marce@mail.com", "ObligaQwerty1", "43567890", "Marcela Soto", "Uruguaya");
            Cliente premium5 = new Premium("german@mail.com", "Obliga4321", "49876543", "Germán Ríos", "Brasileño");


            AgregarUsuario(premium1);
            AgregarUsuario(premium2);
            AgregarUsuario(premium3);
            AgregarUsuario(premium4);
            AgregarUsuario(premium5);

            Cliente ocasional1 = new Ocasional("carla@mail.com", "Obliga1111", "52345678", "Carla Silva", "Uruguaya");
            Cliente ocasional2 = new Ocasional("dario@mail.com", "ObligaAbcd1", "47890123", "Darío Fumero", "Paraguayo");
            Cliente ocasional3 = new Ocasional("laura@mail.com", "Obliga9999", "48901234", "Laura Vega", "Uruguaya");
            Cliente ocasional4 = new Ocasional("mario@mail.com", "ObligaAaaa1", "44567890", "Mario Suárez", "Argentino");
            Cliente ocasional5 = new Ocasional("juliana@mail.com", "ObligaZzzz1", "41239876", "Juliana Gómez", "Uruguaya");

            AgregarUsuario(ocasional1);
            AgregarUsuario(ocasional2);
            AgregarUsuario(ocasional3);
            AgregarUsuario(ocasional4);
            AgregarUsuario(ocasional5);

            Administrador admin1 = new Administrador("admin1@mail.com", "Obliga1234", "capo1");
            Administrador admin2 = new Administrador("admin2@mail.com", "ObligaAdmin1", "capitana");

            AgregarUsuario(admin1);
            AgregarUsuario(admin2);

        }

        private void PrecargaAvion()
        {
            Avion avion1 = new Avion("Boeing", "737", 180, 5600, 12.5);
            Avion avion2 = new Avion("Airbus", "A320", 160, 6100, 11.8);
            Avion avion3 = new Avion("Embraer", "E190", 100, 4400, 9.6);
            Avion avion4 = new Avion("Boeing", "787 Dreamliner", 250, 13600, 17.2);

            AgregarAvion(avion1);
            AgregarAvion(avion2);
            AgregarAvion(avion3);
            AgregarAvion(avion4);
        }

        private void PrecargaAeropuerto()
        {
            Aeropuerto aeropuerto1 = new Aeropuerto("MVD", "Montevideo", 3000, 150);
            Aeropuerto aeropuerto2 = new Aeropuerto("EZE", "Buenos Aires", 2800, 140);
            Aeropuerto aeropuerto3 = new Aeropuerto("GRU", "São Paulo", 3200, 160);
            Aeropuerto aeropuerto4 = new Aeropuerto("JFK", "New York", 5000, 300);
            Aeropuerto aeropuerto5 = new Aeropuerto("CDG", "Paris", 4800, 280);
            Aeropuerto aeropuerto6 = new Aeropuerto("MAD", "Madrid", 4600, 260);
            Aeropuerto aeropuerto7 = new Aeropuerto("LHR", "London", 4900, 290);
            Aeropuerto aeropuerto8 = new Aeropuerto("FRA", "Frankfurt", 4700, 270);
            Aeropuerto aeropuerto9 = new Aeropuerto("BCN", "Barcelona", 4200, 240);
            Aeropuerto aeropuerto10 = new Aeropuerto("SCL", "Santiago", 3100, 155);
            Aeropuerto aeropuerto11 = new Aeropuerto("BOG", "Bogotá", 3000, 150);
            Aeropuerto aeropuerto12 = new Aeropuerto("LAX", "Los Angeles", 5200, 310);
            Aeropuerto aeropuerto13 = new Aeropuerto("MEX", "Ciudad de México", 3300, 165);
            Aeropuerto aeropuerto14 = new Aeropuerto("LIM", "Lima", 2900, 145);
            Aeropuerto aeropuerto15 = new Aeropuerto("PTY", "Panamá", 3100, 150);
            Aeropuerto aeropuerto16 = new Aeropuerto("AMS", "Amsterdam", 4600, 265);
            Aeropuerto aeropuerto17 = new Aeropuerto("ROM", "Roma", 4500, 250);
            Aeropuerto aeropuerto18 = new Aeropuerto("IST", "Estambul", 4700, 275);
            Aeropuerto aeropuerto19 = new Aeropuerto("SYD", "Sídney", 5500, 320);
            Aeropuerto aeropuerto20 = new Aeropuerto("JNB", "Johannesburgo", 4400, 230);

            AgregarAeropuerto(aeropuerto1);
            AgregarAeropuerto(aeropuerto2);
            AgregarAeropuerto(aeropuerto3);
            AgregarAeropuerto(aeropuerto4);
            AgregarAeropuerto(aeropuerto5);
            AgregarAeropuerto(aeropuerto6);
            AgregarAeropuerto(aeropuerto7);
            AgregarAeropuerto(aeropuerto8);
            AgregarAeropuerto(aeropuerto9);
            AgregarAeropuerto(aeropuerto10);
            AgregarAeropuerto(aeropuerto11);
            AgregarAeropuerto(aeropuerto12);
            AgregarAeropuerto(aeropuerto13);
            AgregarAeropuerto(aeropuerto14);
            AgregarAeropuerto(aeropuerto15);
            AgregarAeropuerto(aeropuerto16);
            AgregarAeropuerto(aeropuerto17);
            AgregarAeropuerto(aeropuerto18);
            AgregarAeropuerto(aeropuerto19);
            AgregarAeropuerto(aeropuerto20);
        }

        private void PrecargaRutas()
        {
            Ruta ruta1 = new Ruta(_aeropuertos[0], _aeropuertos[1], 1000);
            Ruta ruta2 = new Ruta(_aeropuertos[2], _aeropuertos[3], 8200);
            Ruta ruta3 = new Ruta(_aeropuertos[4], _aeropuertos[5], 1050);
            Ruta ruta4 = new Ruta(_aeropuertos[6], _aeropuertos[7], 900);
            Ruta ruta5 = new Ruta(_aeropuertos[8], _aeropuertos[9], 2400);
            Ruta ruta6 = new Ruta(_aeropuertos[10], _aeropuertos[11], 4500);
            Ruta ruta7 = new Ruta(_aeropuertos[12], _aeropuertos[13], 1700);
            Ruta ruta8 = new Ruta(_aeropuertos[14], _aeropuertos[15], 7800);
            Ruta ruta9 = new Ruta(_aeropuertos[16], _aeropuertos[17], 1400);
            Ruta ruta10 = new Ruta(_aeropuertos[18], _aeropuertos[19], 11000);
            Ruta ruta11 = new Ruta(_aeropuertos[1], _aeropuertos[0], 1000);
            Ruta ruta12 = new Ruta(_aeropuertos[3], _aeropuertos[2], 8200);
            Ruta ruta13 = new Ruta(_aeropuertos[5], _aeropuertos[4], 1050);
            Ruta ruta14 = new Ruta(_aeropuertos[7], _aeropuertos[6], 900);
            Ruta ruta15 = new Ruta(_aeropuertos[9], _aeropuertos[8], 2400);
            Ruta ruta16 = new Ruta(_aeropuertos[11], _aeropuertos[10], 4500);
            Ruta ruta17 = new Ruta(_aeropuertos[13], _aeropuertos[12], 1700);
            Ruta ruta18 = new Ruta(_aeropuertos[15], _aeropuertos[14], 7800);
            Ruta ruta19 = new Ruta(_aeropuertos[17], _aeropuertos[16], 1400);
            Ruta ruta20 = new Ruta(_aeropuertos[19], _aeropuertos[18], 11000);
            Ruta ruta21 = new Ruta(_aeropuertos[0], _aeropuertos[10], 6300);
            Ruta ruta22 = new Ruta(_aeropuertos[1], _aeropuertos[11], 7200);
            Ruta ruta23 = new Ruta(_aeropuertos[2], _aeropuertos[12], 6700);
            Ruta ruta24 = new Ruta(_aeropuertos[3], _aeropuertos[13], 8400);
            Ruta ruta25 = new Ruta(_aeropuertos[4], _aeropuertos[14], 8900);
            Ruta ruta26 = new Ruta(_aeropuertos[5], _aeropuertos[15], 7500);
            Ruta ruta27 = new Ruta(_aeropuertos[6], _aeropuertos[16], 9300);
            Ruta ruta28 = new Ruta(_aeropuertos[7], _aeropuertos[17], 9700);
            Ruta ruta29 = new Ruta(_aeropuertos[8], _aeropuertos[18], 10200);
            Ruta ruta30 = new Ruta(_aeropuertos[9], _aeropuertos[19], 11000);

            AgregarRuta(ruta1);
            AgregarRuta(ruta2);
            AgregarRuta(ruta3);
            AgregarRuta(ruta4);
            AgregarRuta(ruta5);
            AgregarRuta(ruta6);
            AgregarRuta(ruta7);
            AgregarRuta(ruta8);
            AgregarRuta(ruta9);
            AgregarRuta(ruta10);
            AgregarRuta(ruta11);
            AgregarRuta(ruta12);
            AgregarRuta(ruta13);
            AgregarRuta(ruta14);
            AgregarRuta(ruta15);
            AgregarRuta(ruta16);
            AgregarRuta(ruta17);
            AgregarRuta(ruta18);
            AgregarRuta(ruta19);
            AgregarRuta(ruta20);
            AgregarRuta(ruta21);
            AgregarRuta(ruta22);
            AgregarRuta(ruta23);
            AgregarRuta(ruta24);
            AgregarRuta(ruta25);
            AgregarRuta(ruta26);
            AgregarRuta(ruta27);
            AgregarRuta(ruta28);
            AgregarRuta(ruta29);
            AgregarRuta(ruta30);

        }

        private void PrecargaVuelos()
        {
            Vuelo vuelo1 = new Vuelo("AA101", _rutas[0], _aviones[3], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.MIERCOLES, DiasSemana.VIERNES });
            Vuelo vuelo2 = new Vuelo("AA102", _rutas[1], _aviones[3], new List<DiasSemana> { DiasSemana.MARTES, DiasSemana.JUEVES });
            Vuelo vuelo3 = new Vuelo("AA103", _rutas[2], _aviones[3], new List<DiasSemana> { DiasSemana.DOMINGO });
            Vuelo vuelo4 = new Vuelo("AA104", _rutas[3], _aviones[3], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.SABADO });
            Vuelo vuelo5 = new Vuelo("AA105", _rutas[4], _aviones[3], new List<DiasSemana> { DiasSemana.MIERCOLES, DiasSemana.VIERNES });

            Vuelo vuelo6 = new Vuelo("AA106", _rutas[5], _aviones[3], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.JUEVES });
            Vuelo vuelo7 = new Vuelo("AA107", _rutas[6], _aviones[3], new List<DiasSemana> { DiasSemana.MARTES, DiasSemana.VIERNES });
            Vuelo vuelo8 = new Vuelo("AA108", _rutas[7], _aviones[3], new List<DiasSemana> { DiasSemana.DOMINGO });
            Vuelo vuelo9 = new Vuelo("AA109", _rutas[8], _aviones[3], new List<DiasSemana> { DiasSemana.SABADO, DiasSemana.MARTES });
            Vuelo vuelo10 = new Vuelo("AA110", _rutas[9], _aviones[3], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.MIERCOLES });

            Vuelo vuelo11 = new Vuelo("BB201", _rutas[10], _aviones[2], new List<DiasSemana> { DiasSemana.MARTES, DiasSemana.JUEVES });
            Vuelo vuelo12 = new Vuelo("BB202", _rutas[11], _aviones[3], new List<DiasSemana> { DiasSemana.DOMINGO });
            Vuelo vuelo13 = new Vuelo("BB203", _rutas[12], _aviones[2], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.SABADO });
            Vuelo vuelo14 = new Vuelo("BB204", _rutas[13], _aviones[2], new List<DiasSemana> { DiasSemana.MIERCOLES, DiasSemana.VIERNES });
            Vuelo vuelo15 = new Vuelo("BB205", _rutas[14], _aviones[2], new List<DiasSemana> { DiasSemana.JUEVES });

            Vuelo vuelo16 = new Vuelo("CC301", _rutas[15], _aviones[1], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.MARTES });
            Vuelo vuelo17 = new Vuelo("CC302", _rutas[16], _aviones[1], new List<DiasSemana> { DiasSemana.JUEVES, DiasSemana.SABADO });
            Vuelo vuelo18 = new Vuelo("CC303", _rutas[17], _aviones[3], new List<DiasSemana> { DiasSemana.VIERNES });
            Vuelo vuelo19 = new Vuelo("CC304", _rutas[18], _aviones[1], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.DOMINGO });
            Vuelo vuelo20 = new Vuelo("CC305", _rutas[19], _aviones[3], new List<DiasSemana> { DiasSemana.MARTES, DiasSemana.MIERCOLES });

            Vuelo vuelo21 = new Vuelo("DD401", _rutas[20], _aviones[3], new List<DiasSemana> { DiasSemana.JUEVES, DiasSemana.VIERNES });
            Vuelo vuelo22 = new Vuelo("DD402", _rutas[21], _aviones[3], new List<DiasSemana> { DiasSemana.MARTES });
            Vuelo vuelo23 = new Vuelo("DD403", _rutas[22], _aviones[3], new List<DiasSemana> { DiasSemana.SABADO, DiasSemana.DOMINGO });
            Vuelo vuelo24 = new Vuelo("DD404", _rutas[23], _aviones[3], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.JUEVES });
            Vuelo vuelo25 = new Vuelo("DD405", _rutas[24], _aviones[3], new List<DiasSemana> { DiasSemana.MARTES, DiasSemana.VIERNES });

            Vuelo vuelo26 = new Vuelo("DD406", _rutas[25], _aviones[3], new List<DiasSemana> { DiasSemana.MIERCOLES });
            Vuelo vuelo27 = new Vuelo("DD407", _rutas[26], _aviones[3], new List<DiasSemana> { DiasSemana.SABADO });
            Vuelo vuelo28 = new Vuelo("DD408", _rutas[27], _aviones[3], new List<DiasSemana> { DiasSemana.LUNES, DiasSemana.VIERNES });
            Vuelo vuelo29 = new Vuelo("DD409", _rutas[28], _aviones[3], new List<DiasSemana> { DiasSemana.DOMINGO, DiasSemana.JUEVES });
            Vuelo vuelo30 = new Vuelo("DD410", _rutas[29], _aviones[3], new List<DiasSemana> { DiasSemana.MIERCOLES, DiasSemana.SABADO });


            AgregarVuelo(vuelo1);
            AgregarVuelo(vuelo2);
            AgregarVuelo(vuelo3);
            AgregarVuelo(vuelo4);
            AgregarVuelo(vuelo5);
            AgregarVuelo(vuelo6);
            AgregarVuelo(vuelo7);
            AgregarVuelo(vuelo8);
            AgregarVuelo(vuelo9);
            AgregarVuelo(vuelo10);
            AgregarVuelo(vuelo11);
            AgregarVuelo(vuelo12);
            AgregarVuelo(vuelo13);
            AgregarVuelo(vuelo14);
            AgregarVuelo(vuelo15);
            AgregarVuelo(vuelo16);
            AgregarVuelo(vuelo17);
            AgregarVuelo(vuelo18);
            AgregarVuelo(vuelo19);
            AgregarVuelo(vuelo20);
            AgregarVuelo(vuelo21);
            AgregarVuelo(vuelo22);
            AgregarVuelo(vuelo23);
            AgregarVuelo(vuelo24);
            AgregarVuelo(vuelo25);
            AgregarVuelo(vuelo26);
            AgregarVuelo(vuelo27);
            AgregarVuelo(vuelo28);
            AgregarVuelo(vuelo29);
            AgregarVuelo(vuelo30);

        }
        private void PrecargaPasajes() 
        {
            //Trabajamos solo con los clientes
            List<Cliente> _clientes = ObtenerListaClientes();

            Pasaje pasaje1 = new Pasaje(_vuelos[0], new DateTime(2025, 5, 5), TipoEquipaje.CABINA, _clientes[0]); 
            Pasaje pasaje2 = new Pasaje(_vuelos[1], new DateTime(2025, 5, 6), TipoEquipaje.BODEGA, _clientes[5]); 
            Pasaje pasaje3 = new Pasaje(_vuelos[2], new DateTime(2025, 5, 4), TipoEquipaje.LIGHT, _clientes[1]); 
            Pasaje pasaje4 = new Pasaje(_vuelos[3], new DateTime(2025, 5, 5), TipoEquipaje.CABINA, _clientes[6]); 
            Pasaje pasaje5 = new Pasaje(_vuelos[4], new DateTime(2025, 5, 7), TipoEquipaje.BODEGA, _clientes[2]); 
            Pasaje pasaje6 = new Pasaje(_vuelos[5], new DateTime(2025, 5, 5), TipoEquipaje.LIGHT, _clientes[7]);
            Pasaje pasaje7 = new Pasaje(_vuelos[6], new DateTime(2025, 5, 6), TipoEquipaje.CABINA, _clientes[3]);
            Pasaje pasaje8 = new Pasaje(_vuelos[7], new DateTime(2025, 5, 4), TipoEquipaje.LIGHT, _clientes[8]); 
            Pasaje pasaje9 = new Pasaje(_vuelos[8], new DateTime(2025, 5, 10), TipoEquipaje.BODEGA, _clientes[4]); 
            Pasaje pasaje10 = new Pasaje(_vuelos[9], new DateTime(2025, 5, 5), TipoEquipaje.CABINA, _clientes[9]); 

            Pasaje pasaje11 = new Pasaje(_vuelos[10], new DateTime(2025, 5, 6), TipoEquipaje.LIGHT, _clientes[0]); 
            Pasaje pasaje12 = new Pasaje(_vuelos[11], new DateTime(2025, 5, 4), TipoEquipaje.BODEGA, _clientes[5]); 
            Pasaje pasaje13 = new Pasaje(_vuelos[12], new DateTime(2025, 5, 10), TipoEquipaje.CABINA, _clientes[1]); 
            Pasaje pasaje14 = new Pasaje(_vuelos[13], new DateTime(2025, 5, 14), TipoEquipaje.LIGHT, _clientes[6]); 
            Pasaje pasaje15 = new Pasaje(_vuelos[14], new DateTime(2025, 5, 15), TipoEquipaje.BODEGA, _clientes[2]);
            Pasaje pasaje16 = new Pasaje(_vuelos[15], new DateTime(2025, 5, 12), TipoEquipaje.CABINA, _clientes[7]); 
            Pasaje pasaje17 = new Pasaje(_vuelos[16], new DateTime(2025, 5, 17), TipoEquipaje.LIGHT, _clientes[3]); 
            Pasaje pasaje18 = new Pasaje(_vuelos[17], new DateTime(2025, 5, 16), TipoEquipaje.BODEGA, _clientes[8]); 
            Pasaje pasaje19 = new Pasaje(_vuelos[18], new DateTime(2025, 5, 18), TipoEquipaje.CABINA, _clientes[4]); 
            Pasaje pasaje20 = new Pasaje(_vuelos[19], new DateTime(2025, 5, 6), TipoEquipaje.LIGHT, _clientes[9]);

            Pasaje pasaje21 = new Pasaje(_vuelos[20], new DateTime(2025, 5, 22), TipoEquipaje.BODEGA, _clientes[0]);
            Pasaje pasaje22 = new Pasaje(_vuelos[21], new DateTime(2025, 5, 20), TipoEquipaje.LIGHT, _clientes[5]); 
            Pasaje pasaje23 = new Pasaje(_vuelos[22], new DateTime(2025, 5, 24), TipoEquipaje.CABINA, _clientes[1]); 
            Pasaje pasaje24 = new Pasaje(_vuelos[23], new DateTime(2025, 5, 26), TipoEquipaje.LIGHT, _clientes[6]); 
            Pasaje pasaje25 = new Pasaje(_vuelos[24], new DateTime(2025, 5, 23), TipoEquipaje.BODEGA, _clientes[2]);

            AgregarPasaje(pasaje1);
            AgregarPasaje(pasaje2);
            AgregarPasaje(pasaje3);
            AgregarPasaje(pasaje4);
            AgregarPasaje(pasaje5);
            AgregarPasaje(pasaje6);
            AgregarPasaje(pasaje7);
            AgregarPasaje(pasaje8);
            AgregarPasaje(pasaje9);
            AgregarPasaje(pasaje10);
            AgregarPasaje(pasaje11);
            AgregarPasaje(pasaje12);
            AgregarPasaje(pasaje13);
            AgregarPasaje(pasaje14);
            AgregarPasaje(pasaje15);
            AgregarPasaje(pasaje16);
            AgregarPasaje(pasaje17);
            AgregarPasaje(pasaje18);
            AgregarPasaje(pasaje19);
            AgregarPasaje(pasaje20);
            AgregarPasaje(pasaje21);
            AgregarPasaje(pasaje22);
            AgregarPasaje(pasaje23);
            AgregarPasaje(pasaje24);
            AgregarPasaje(pasaje25);
        }

    }
}
