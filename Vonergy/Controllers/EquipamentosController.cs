using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using vonergyDom.ViewModel;

namespace Vonergy.Controllers
{
    public class EquipamentosController : Controller
    {

        private const string endereco = "https://vonergyapi.azurewebsites.net/api/Equipamento/";

        //private const string endereco = "http://localhost:60529/api/Equipamento/";

        private HttpClient client;

        public EquipamentosController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("CadastroEquipamento");
        }

        [HttpGet]
        public ActionResult Detalhes(long? id)
        {
            Equipamentos equipamentos = new Equipamentos();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var FuncionarioCarregado = response.Content.ReadAsAsync<Equipamentos>();
                FuncionarioCarregado.Wait();
                equipamentos = FuncionarioCarregado.Result;
            }
            return PartialView("_DetalhesEquipamentoPartial", equipamentos);
        }

        [HttpPost]
        public ActionResult CadastroEquipamento(Equipamentos equipamentos)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var serializeEquipamento = JsonConvert.SerializeObject(equipamentos);
                    client.BaseAddress = new Uri(endereco + "Cadastrar");
                    var content = new StringContent(serializeEquipamento, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(client.BaseAddress, content).Result;
                }
                ViewBag.Mensagem = "Equipamento Cadastrado com sucesso";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Editar(long? id)
        {
            Equipamentos Equipamento = new Equipamentos();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var EquipamentoCarregado = response.Content.ReadAsAsync<Equipamentos>();
                EquipamentoCarregado.Wait();
                Equipamento = EquipamentoCarregado.Result;
            }
            return PartialView("_EquipamentoEditarPartial", Equipamento);

        }


        [HttpGet]
        public ActionResult ListarEquipamento()
        {
            IList<Equipamentos> equipamentos = null;

            client.BaseAddress = new Uri(endereco + "Listar");
            HttpResponseMessage response = client.GetAsync("Listar").Result;

            if (response.IsSuccessStatusCode)
            {
                var EquipamentoCarregado = response.Content.ReadAsAsync<IList<Equipamentos>>();
                EquipamentoCarregado.Wait();

                equipamentos = EquipamentoCarregado.Result;
            }
            return View("ListarEquipamento",equipamentos);
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
                var EquipamentoCarregado = response.Content.ReadAsAsync<Equipamentos>();
                EquipamentoCarregado.Wait();
            }
            ViewBag.Message = "Equipamento Excluida";
            return RedirectToAction("ListarEquipamento");
        }

    }
}
