using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string logueado = context.HttpContext.Session.GetString("correo");

            if (string.IsNullOrEmpty(logueado))
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "Debe estar logueado" });
            }
            base.OnActionExecuted(context);
        }
    }
}
