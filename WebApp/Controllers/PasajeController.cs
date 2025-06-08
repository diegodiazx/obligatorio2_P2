using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class PasajeController : Controller
{
    private Sistema _sistema = Sistema.Instancia;

    public IActionResult Index()
    {
        List<Cliente> clientes = _sistema.ObtenerListaClientes();
        Usuario usuarioLogueado = clientes[0];
        //Usuario usuarioLogueado = _sistema.ObtenerAdmin();

        List<Pasaje> pasajes = _sistema.Pasajes;

        if(usuarioLogueado is Cliente clienteLogueado)
        {
            _sistema.OrdenarPasajesPorPrecio();
            pasajes = _sistema.ObtenerPasajesCliente(clienteLogueado);
        }
        else
        {
            _sistema.OrdenarPasajesPorFecha();
        }
        return View(pasajes);
    }

    //la compra del pasaje
    [HttpPost]
    public IActionResult Add(string numeroVuelo, string correoPasajero, DateTime fecha, TipoEquipaje tipoEquipaje)
    {
        try
        {
            Cliente cliente = _sistema.ObtenerCliente(correoPasajero);
            Vuelo vuelo = _sistema.ObtenerVuelo(numeroVuelo);
            Pasaje nuevo = new Pasaje(vuelo, fecha, tipoEquipaje, cliente);
            _sistema.AgregarPasaje(nuevo);
            //si se compra exitosamente lo lleva a la lista de sus pasajes
            //por ahora lo llevamos a la lista de todos los pasajes
            return RedirectToAction("Index",
                new
                {
                    mensaje = "Pasaje comprado"
                }
            );
        }
        catch (Exception ex)
        {
            //si sale algo mal lo vuelve a llevar a la lista de vuelos
            return RedirectToAction("Index", "Vuelo",
                new
                {
                    mensaje = ex.Message
                }
            );
        }

    }

}