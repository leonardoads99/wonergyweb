using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using vonergyDom.ViewModel;

namespace Vonergy.Controllers
{
    public class HomeController : BaseController
    {
        private readonly HttpClient client;
        private const string enderecoFuncionario = "http://vonergyapi.azurewebsites.net/api/Funcionario/";
        private const string enderecoConsumo = "http://vonergyapi.azurewebsites.net/api/Consumo/";
        private static Funcionario funcionarioRegistrado;

        public HomeController()
        {
            HttpContext httpContext;
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            funcionarioRegistrado = new Funcionario();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Acessar(Funcionario funcionario)
        {
            try
            {
                var serializedLogin = JsonConvert.SerializeObject(funcionario);
                client.BaseAddress = new Uri(enderecoFuncionario + "Acessar");
                var content = new StringContent(serializedLogin, Encoding.UTF8, "application/json");
                var funcionarioenviado = client.PostAsync(client.BaseAddress, content).Result;

                var funcionarioCarregado = funcionarioenviado.Content.ReadAsAsync<Funcionario>();

                if (string.IsNullOrEmpty(funcionario.Email) || string.IsNullOrEmpty(funcionario.Senha))
                {
                    ViewBag.message = "Campos Cpf e Senha são  Obrigatorios";
                    return View("Index");
                }
                if (funcionarioCarregado.IsCompleted && funcionarioCarregado.Result.Senha.Equals(funcionario.Cpf))
                {
                    ViewBag.message = "Senha Igual";
                    return View("Index");
                }
                funcionarioRegistrado = funcionario;

                DashBoard();
                AlimentarGrafico();
                return RedirectToAction("DashBoard");
            }
            catch (HttpException httpException)
            {
                return View("Error", new HandleErrorInfo(httpException, "Acessar", "funcionario"));
            }
        }

        public static Funcionario VerificaSeOUsuarioEstaLogado()
        {
            var httpRequest = new HttpRequest(string.Empty, enderecoFuncionario, string.Empty);
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponce);
            httpContext = new HttpContext(httpRequest, httpResponce);

            var funcionarioAutenticado = httpContext.Request.Cookies["UserCookieAuthentication"];
            if (funcionarioRegistrado == null)
            {
                return null;
            }
            return funcionarioRegistrado;
        }

        [HttpGet]
        public ActionResult DashBoard()
        {
            string valor = string.Empty;
            HttpResponseMessage response = client.GetAsync(enderecoConsumo + "ConsumoReal").Result;

            if (response.IsSuccessStatusCode)
            {
                var ConsumoReal = response.Content.ReadAsAsync<String>();
                ConsumoReal.Wait();
                valor = ConsumoReal.Result;
            }

            decimal valorReais = decimal.Multiply(Convert.ToDecimal(valor), Convert.ToDecimal(0.57));
            string specifier = "#,#.00;(#,#.00)";
            ViewBag.valorReais = valorReais.ToString(specifier);
            ViewBag.consumo = Convert.ToDecimal(valor).ToString(specifier);
            return View();
        }


        [HttpGet]
        public JsonResult AlimentarGraficoMes()
        {
            HttpResponseMessage response = client.GetAsync(enderecoConsumo + "ConsumoMesal ").Result;

            var ConsumoDiario = response.Content.ReadAsAsync<IList<ConsumoRegistrado>>();
            ConsumoDiario.Wait();

            IList<String> listaCarregada = new List<String>();
            foreach (ConsumoRegistrado drow in ConsumoDiario.Result)
            {
                listaCarregada.Add(drow.Potencia.ToString());
            }
            return Json(listaCarregada, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult AlimentarGraficoAnual()
        {
            HttpResponseMessage response = client.GetAsync(enderecoConsumo + "ConsumoAnual ").Result;

            var ConsumoDiario = response.Content.ReadAsAsync<IList<ConsumoRegistrado>>();
            ConsumoDiario.Wait();

            IList<String> listaCarregada = new List<String>();
            foreach (ConsumoRegistrado drow in ConsumoDiario.Result)
            {
                listaCarregada.Add(drow.Potencia.ToString());
            }
            return Json(listaCarregada, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AlimentarGraficoDiario()
        {
            HttpResponseMessage response = client.GetAsync(enderecoConsumo + "ConsumoDiario ").Result;

            var ConsumoDiario = response.Content.ReadAsAsync<IList<ConsumoRegistrado>>();
            ConsumoDiario.Wait();

            IList<String> listaCarregada = new List<String>();
            foreach (ConsumoRegistrado drow in ConsumoDiario.Result)
            {
                listaCarregada.Add(drow.Potencia.ToString());
            }
            return Json(listaCarregada, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AlimentarGraficoHora()
        {
            HttpResponseMessage response = client.GetAsync(enderecoConsumo + "ConsumoHora ").Result;

            var ConsumoDiario = response.Content.ReadAsAsync<IList<ConsumoRegistrado>>();
            ConsumoDiario.Wait();

            IList<String> listaCarregada = new List<String>();
            foreach (ConsumoRegistrado drow in ConsumoDiario.Result)
            {
                listaCarregada.Add(drow.Potencia.ToString());
            }
            return Json(listaCarregada, JsonRequestBehavior.AllowGet);
        }


        //        Select
        //distinct(DateName(month, DateAdd(month, DATEPART(MONTH, orders_date) , -1 ) ))
        //as month_name
        //from mobile_sales
        //where DATEPART(YEAR, orders_date)='" + year + "' order by month_number


        //        select
        //month(orders_date) as month_number ,
        //   sum(orders_quantity) as total_quantity
        //from mobile_sales
        //where YEAR(orders_date)='" + year + "' and mobile_id = '" + mobileId_one + "'
        //group by   month(orders_date)   order by  month_number

        [HttpGet]
        public JsonResult AlimentarGrafico()
        {
            //List<string> listaConsumoDiario = new List<string>();

            HttpResponseMessage response = client.GetAsync(enderecoConsumo + "ConsumoMesalDicionario").Result;

            if (response.IsSuccessStatusCode)
            {
                var ConsumoDiario = response.Content.ReadAsAsync<Dictionary<int, ConsumoRegistrado>>();
                ConsumoDiario.Wait();

                return Json(ConsumoDiario, JsonRequestBehavior.AllowGet);

            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}