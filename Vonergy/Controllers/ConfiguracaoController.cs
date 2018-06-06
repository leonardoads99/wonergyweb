using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vonergy.Controllers
{
    public class ConfiguracaoController : Controller
    {
        // GET: Configuracao
        public ActionResult Index()
        {
            return View("ConfiguracaoSistema");
        }
    }
}