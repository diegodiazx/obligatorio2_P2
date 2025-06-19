using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class PasajeController : Controller
{
    private Sistema _sistema = Sistema.Instancia;

    public IActionResult Index(string mensaje)
    {
        List<Cliente> clientes = _sistema.ObtenerListaClientes(); 
        Usuario usuarioLogueado = clientes[7];
        //Usuario usuarioLogueado = _sistema.ObtenerAdmin();
        List<Pasaje> pasajes = _sistema.OrdenarPasajes(usuarioLogueado);
        ViewBag.mensaje = mensaje;
        return View(pasajes);
        //agregue lo del mensaje en el iactionresult del index para recibir como parametro el mensaje de exito %
    }

    //la compra del pasaje
    [HttpPost]
    public IActionResult Add(string numeroVuelo, string correoPasajero, DateTime fecha, TipoEquipaje tipoEquipaje,
        string origen)
    {
        try
        {
            Cliente cliente = _sistema.ObtenerCliente(correoPasajero);
            Vuelo vuelo = _sistema.ObtenerVuelo(numeroVuelo);
            Pasaje nuevo = new Pasaje(vuelo, fecha, tipoEquipaje, cliente);
            _sistema.AgregarPasaje(nuevo);
            //si se compra exitosamente lo lleva a la lista de sus pasajes
            //por ahora lo llevamos a la lista de todos los pasajes
            
            return RedirectToAction("Index", new { mensaje = "Pasaje comprado con éxito" });
            //esto antes no se mostraba, ahora si %
            //solo se validaba que el equipaje estuviera completo en el index, por el campo required. te dejaba %
            //comprar un pasaje y el tipo quedaba en 0. ahora lo agregue como validacion %
        }
        catch (Exception ex)
        {
            //si sale algo mal lo vuelve a llevar a la lista de vuelos
            /*
            acá agrego lo de origen = detalles para que te devuelva a detalles si estaba comprando desde ahí
            */
            if (origen == "Detalles")
            {
                return RedirectToAction("Detalles", "Vuelo", new { id = numeroVuelo, mensaje = ex.Message });
            }
            return RedirectToAction("Index", "Vuelo", new { mensaje = ex.Message } );
        }

    }

}