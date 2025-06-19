using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Filters
{
    public class ClienteFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string rol = context.HttpContext.Session.GetString("rol");

            if (rol == "Premium" || rol == "Ocasional")
            {
                context.Result = new RedirectToActionResult("Index", "Vuelo", new { mensaje = "Los clientes no pueden acceder" });
            }
            base.OnActionExecuted(context);
        }
    }
}
