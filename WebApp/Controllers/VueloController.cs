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
        List<Cliente> clientes = _sistema.ObtenerListaClientes();
        //Esto es temporal para poder testearlo
        ViewBag.Pasajero = clientes[0].Correo;

        ViewBag.Aeropuertos = _sistema.Aeropuertos;
        List<Vuelo> vuelosFiltrados = _sistema.ObtenerVuelosPorRuta(aeroSalida, aeroLlegada);
        return View(vuelosFiltrados);
    }

    //Los detalles del vuelo
    public IActionResult Detalles(string id)
    {
        List<Cliente> clientes = _sistema.ObtenerListaClientes();
        ViewBag.Pasajero = clientes[0].Correo;
        Vuelo vuelo = _sistema.ObtenerVuelo(id);
        return View(vuelo);
    }

}