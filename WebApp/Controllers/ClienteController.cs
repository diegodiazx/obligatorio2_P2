using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClienteController : Controller
    {
        Sistema _sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
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
