using Microsoft.AspNetCore.Mvc;
using Dominio;

namespace WebApp.Controllers;

public class VueloController : Controller
{
    private Sistema _sistema = Sistema.Instancia;
    // GET
    public IActionResult Index()
    {
        return View(_sistema.Vuelos);
    }
}