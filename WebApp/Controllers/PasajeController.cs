using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [Authentication]
    public class PasajeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        public IActionResult Index(string mensaje)
        {
            Usuario usuarioLogueado = _sistema.ObtenerUsuario(HttpContext.Session.GetString("correo"));
            List<Pasaje> pasajes = _sistema.OrdenarPasajes(usuarioLogueado);
            ViewBag.mensaje = mensaje;
            return View(pasajes);
        }

        [AdministradorFilter]
        [HttpPost]
        public IActionResult Add(string numeroVuelo, DateTime fecha, TipoEquipaje tipoEquipaje,
            string origen)
        {
            try
            {
                Cliente cliente = _sistema.ObtenerCliente(HttpContext.Session.GetString("correo"));
                Vuelo vuelo = _sistema.ObtenerVuelo(numeroVuelo);
                Pasaje nuevo = new Pasaje(vuelo, fecha, tipoEquipaje, cliente);
                _sistema.AgregarPasaje(nuevo);
                return RedirectToAction("Index", new { mensaje = "Pasaje comprado con éxito" });
            }
            catch (Exception ex)
            {
                if (origen == "Detalles")
                {
                    return RedirectToAction("Detalles", "Vuelo", new { id = numeroVuelo, mensaje = ex.Message });
                }
                return RedirectToAction("Index", "Vuelo", new { mensaje = ex.Message });
            }

        }

    }
}