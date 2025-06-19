using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Filters
{
    public class AdministradorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string rol = context.HttpContext.Session.GetString("rol");

            if (rol == "Administrador")
            {
                context.Result = new RedirectToActionResult("Index", "Cliente", new { mensaje = "Los administradores no pueden acceder" });
            }
            base.OnActionExecuted(context);
        }
    }
}
