using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using vonergyDom.ViewModel;

namespace Vonergy.Controllers
{
    public class DispositivoIotController : Controller
    {
        private const string endereco = "https://vonergyapi.azurewebsites.net/api/Iot/";

        //private const string endereco = "http://localhost:60529/api/Iot/";

        private HttpClient client;

        public DispositivoIotController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("CadastroIot");
        }

        [HttpPost]
        public ActionResult CadastroIot(DispositivoIOT dispositivoIOT)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var serializeEquipamento = JsonConvert.SerializeObject(dispositivoIOT);
                    client.BaseAddress = new Uri(endereco + "Cadastrar");
                    var content = new StringContent(serializeEquipamento, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(client.BaseAddress, content).Result;
                }
                ViewBag.Mensagem = "Dispostivo Cadastrado com sucesso";
            }
            return View();

        }

        [HttpGet]
        public ActionResult Detalhes(long? id)
        {
            DispositivoIOT dispositivoIOT = new DispositivoIOT();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var IotCarregado = response.Content.ReadAsAsync<DispositivoIOT>();
                IotCarregado.Wait();
                dispositivoIOT = IotCarregado.Result;
            }
            return PartialView("_DetalhesDispositivoIotPartial", dispositivoIOT);

        }

        public ActionResult Editar(long? id)
        {
            DispositivoIOT dispositivoIOT = new DispositivoIOT();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var IotCarregado = response.Content.ReadAsAsync<DispositivoIOT>();
                IotCarregado.Wait();
                dispositivoIOT = IotCarregado.Result;
            }
            return PartialView("_IotEditarPartial", dispositivoIOT);
        }

        [HttpGet]
        public ActionResult ListarDispositivo()
        {
            IList<DispositivoIOT> DispositivoIOT = null;

            client.BaseAddress = new Uri(endereco + "Listar");
            HttpResponseMessage response = client.GetAsync("Listar").Result;

            if (response.IsSuccessStatusCode)
            {
                var DispositivoIotCarregado = response.Content.ReadAsAsync<IList<DispositivoIOT>>();
                DispositivoIotCarregado.Wait();

                DispositivoIOT = DispositivoIotCarregado.Result;
            }
            return View("ListarIot", DispositivoIOT);
        }

 
    [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = client.GetAsync(endereco + "Delete?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var IotCarregado = response.Content.ReadAsAsync<Equipamentos>();
                IotCarregado.Wait();
            }
            ViewBag.Message = "Equipamento Excluida";
            return RedirectToAction("ListarDispositivo");
        }

    }
}