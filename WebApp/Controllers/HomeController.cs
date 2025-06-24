using System.Net;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [ClienteFilter]
        [AdministradorFilter]
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

                string rol = HttpContext.Session.GetString("rol");

                if (rol == "Premium" || rol == "Ocasional")
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}