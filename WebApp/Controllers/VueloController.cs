using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace WebApp.Controllers;

public class VueloController : Controller
{
    private Sistema _sistema = Sistema.Instancia;
    public IActionResult Index()
    {
        List<Cliente> clientes = _sistema.ObtenerListaClientes();
        //Esto es temporal para poder testearlo
        ViewBag.Pasajero = clientes[0].Correo;
        return View(_sistema.Vuelos);
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