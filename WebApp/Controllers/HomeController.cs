using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private Sistema _sistema = Sistema.Instancia;
    public IActionResult Index(string mensaje)
    {
        ViewBag.mensaje = mensaje;
        return View();
    }
    
    [HttpPost]
    public IActionResult Index(string correo, string contra)
    {
        try
        {
            Usuario logueado = _sistema.Login(correo, contra);
            HttpContext.Session.SetString("correo", correo);
            HttpContext.Session.SetString("rol", logueado.GetType().Name);
            
            //if (HttpContext.Session.GetString("rol") == "Cliente")
            if (_sistema.ObtenerCliente(correo) != null)
            {
                return RedirectToAction("Index", "Vuelo");
            }
            return RedirectToAction("Index", "Cliente");
        }
        catch (Exception e)
        {
            return RedirectToAction("Index", new { mensaje = e.Message });
        }
    }
}