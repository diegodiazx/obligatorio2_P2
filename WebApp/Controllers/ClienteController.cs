using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp.Controllers
{
    public class ClienteController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;

        [Authentication]
        [ClienteFilter]
        public IActionResult Index(string mensaje, bool exito)
        {
            List<Cliente> clientes = _sistema.OrdenarClientesPorDocumento();
            ViewBag.Exito = exito;
            ViewBag.Mensaje = mensaje;
            return View(clientes);
        }

        [Authentication]
        [ClienteFilter]
        [HttpPost]
        public IActionResult Index(string correoCliente, int puntos, bool elegible)
        {
            try
            {
                _sistema.ActualizarCliente(correoCliente, puntos, elegible);
                return RedirectToAction("Index", new { mensaje = "Cliente actualizado correctamente", exito = true });

            } catch(Exception e)
            {
                return RedirectToAction("Index", new { mensaje = e.Message, exito = false });
            }
        }

        [ClienteFilter]
        [AdministradorFilter]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Ocasional cliente)
        {
            try
            {
                _sistema.AgregarUsuario(cliente);
                HttpContext.Session.SetString("correo", cliente.Correo);
                HttpContext.Session.SetString("rol", cliente.GetType().Name);

                return RedirectToAction("Index", "Vuelo");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [Authentication]
        [AdministradorFilter]
        public IActionResult Perfil()
        {
            Cliente cliente = _sistema.ObtenerCliente(HttpContext.Session.GetString("correo"));
            return View(cliente);
        }
    }
}
