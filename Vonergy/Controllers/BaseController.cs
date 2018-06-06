using System.Web.Mvc;
using Vonergy.filtros;

namespace Vonergy.Controllers
{

    [AutorizacaoDeAcesso]
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}