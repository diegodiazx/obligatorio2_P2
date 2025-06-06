using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class PasajeController : Controller
{
    private Sistema _sistema = Sistema.Instancia;
    public IActionResult IndexAdmin()
    {
        List<Pasaje> pasajesOrdenadosPorFecha = _sistema.OrdenarPasajesPorFecha();
        return View(pasajesOrdenadosPorFecha);
    }
    public IActionResult IndexCliente()
    {
        List<Pasaje> pasajesOrdenadosPorPrecio = _sistema.OrdenarPasajesPorPrecio();
        return View(pasajesOrdenadosPorPrecio);
    }
    //creo que esta mal lo de tener 2 index separados, después vamos a ver lo de reconocer qué usuario está loggeado.

    //la compra del pasaje
    [HttpPost]
    public IActionResult Add(string numeroVuelo, string correoPasajero, DateTime fecha, TipoEquipaje tipoEquipaje)
    {
        try
        {
            Cliente cliente = _sistema.ObtenerCliente(correoPasajero);
            Vuelo vuelo = _sistema.ObtenerVuelo(numeroVuelo);
            Pasaje nuevo = new Pasaje(vuelo, fecha, tipoEquipaje, cliente);
            _sistema.AgregarPasaje(nuevo);
            //si se compra exitosamente lo lleva a la lista de sus pasajes
            //por ahora lo llevamos a la lista de todos los pasajes
            return RedirectToAction("IndexAdmin",
                new
                {
                    mensaje = "Pasaje comprado"
                }
            );
        }
        catch (Exception ex)
        {
            //si sale algo mal lo vuelve a llevar a la lista de vuelos
            return RedirectToAction("Index", "Vuelo",
                new
                {
                    mensaje = ex.Message
                }
            );
        }

    }

}