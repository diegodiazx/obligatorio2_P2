using Microsoft.AspNetCore.Mvc;
using Dominio;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [Authentication]
    [AdministradorFilter]
    public class VueloController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Aeropuertos = _sistema.Aeropuertos;
            return View(_sistema.Vuelos);
        }

        [HttpPost]
        public IActionResult Index(string aeroSalida, string aeroLlegada)
        {
            try
            {
                ViewBag.Pasajero = HttpContext.Session.GetString("correo");
                ViewBag.Aeropuertos = _sistema.Aeropuertos;
                List<Vuelo> vuelosFiltrados = _sistema.ObtenerVuelosPorRuta(aeroSalida, aeroLlegada);
                return View(vuelosFiltrados);

            }
            catch (Exception e)
            {
                return RedirectToAction("Index", new { mensaje = e.Message });
            }
        }


        public IActionResult Detalles(string id, string mensaje)
        {
            ViewBag.mensaje = mensaje;
            ViewBag.Pasajero = HttpContext.Session.GetString("correo");
            Vuelo vuelo = _sistema.ObtenerVuelo(id);
            return View(vuelo);
        }

    }
}