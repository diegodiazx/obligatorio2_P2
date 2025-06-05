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
    public IActionResult Add(Vuelo vuelo, Cliente pasajero, DateTime fecha, TipoEquipaje tipoEquipaje)
    {
        try
        {
            Pasaje nuevo = new Pasaje(vuelo, fecha, tipoEquipaje, pasajero);
            return RedirectToAction("Index",
                new
                {
                    mensaje = "Pasaje comprado"
                }
            );
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index",
                new
                {
                    mensaje = ex.Message
                }
            );
        }

    }

}