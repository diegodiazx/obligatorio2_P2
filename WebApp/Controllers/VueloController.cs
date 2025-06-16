using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace WebApp.Controllers;

public class VueloController : Controller
{
    private Sistema _sistema = Sistema.Instancia;
    public IActionResult Index(string mensaje)
    {
        List<Cliente> clientes = _sistema.ObtenerListaClientes();
        //Esto es temporal para poder testearlo
        ViewBag.Pasajero = clientes[0].Correo;

        ViewBag.Mensaje = mensaje;
        ViewBag.Aeropuertos = _sistema.Aeropuertos;
        return View(_sistema.Vuelos);
    }

    [HttpPost]
    public IActionResult Index(string aeroSalida, string aeroLlegada)
    {
        try
        {
            List<Cliente> clientes = _sistema.ObtenerListaClientes();
            //Esto es temporal para poder testearlo
            ViewBag.Pasajero = clientes[0].Correo;

            ViewBag.Aeropuertos = _sistema.Aeropuertos;
            List<Vuelo> vuelosFiltrados = _sistema.ObtenerVuelosPorRuta(aeroSalida, aeroLlegada);
            return View(vuelosFiltrados);

        }
        catch (Exception e)
        {
            return RedirectToAction("Index", new { mensaje = e.Message });
        }

    }

    //Los detalles del vuelo
    
    //agregue lo de mensaje al iactionresult de detalles, para que lo pueda mostrar ahi, no solo en el index %
    public IActionResult Detalles(string id, string mensaje)
    {
        ViewBag.mensaje = mensaje;
        List<Cliente> clientes = _sistema.ObtenerListaClientes();
        ViewBag.Pasajero = clientes[0].Correo;
        Vuelo vuelo = _sistema.ObtenerVuelo(id);
        return View(vuelo);
    }

}