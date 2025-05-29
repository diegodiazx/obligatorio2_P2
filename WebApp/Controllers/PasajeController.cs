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
    
}