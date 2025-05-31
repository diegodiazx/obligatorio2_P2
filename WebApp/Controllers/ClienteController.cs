using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClienteController : Controller
    {
        Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            List<Cliente> _clientes = _sistema.ObtenerListaClientes();
            _sistema.OrdenarClientesPorDocumento(_clientes);
            return View(_clientes);
        }

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
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }
}
