using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using vonergyDom.ViewModel;

namespace Vonergy.Controllers
{
    public class EmpresaController : Controller
    {
        private const string endereco = "https://vonergyapi.azurewebsites.net/api/Empresa/";
        private HttpClient client;

        public EmpresaController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            return View("CadastrarEmpresa");
        }

        [HttpPost]
        public ActionResult CadastrarEmpresa(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    var serializedempresa = JsonConvert.SerializeObject(empresa);
                    client.BaseAddress = new Uri(endereco + "Cadastrar");
                    var content = new StringContent(serializedempresa, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(client.BaseAddress, content).Result;
                }
                ViewBag.Mensagem = "Empresa Cadastrada com sucesso";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ListarEmpresa()
        {
            IList<Empresa> Empresas = null;

            client.BaseAddress = new Uri(endereco + "Listar");
            HttpResponseMessage response = client.GetAsync("Listar").Result;

            if (response.IsSuccessStatusCode)
            {
                var EmpresaCarregada = response.Content.ReadAsAsync<IList<Empresa>>();
                EmpresaCarregada.Wait();

                Empresas = EmpresaCarregada.Result;
            }
            return View(Empresas);
        }

        [HttpGet]
        public ActionResult Detalhes(long? id)
        {
            Empresa Empresa = new Empresa();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var EmpresaCarregada = response.Content.ReadAsAsync<Empresa>();
                EmpresaCarregada.Wait();
                Empresa = EmpresaCarregada.Result;
            }
            var teste = PartialView("_DetalhesPartial", Empresa);
            return teste;
        }

        [HttpGet]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //client.BaseAddress = new Uri(endereco);

            HttpResponseMessage response = client.GetAsync(endereco + "Delete?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var EmpresaCarregada = response.Content.ReadAsAsync<Empresa>();
                EmpresaCarregada.Wait();

            }
            ViewBag.Message = "Empresa Excluida";
            return RedirectToAction("ListarEmpresa");
        }

        [HttpGet]
        public ActionResult Editar(long? id)
        {
            Empresa Empresa = new Empresa();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //client.BaseAddress = new Uri(endereco);

            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var EmpresaCarregado = response.Content.ReadAsAsync<Empresa>();
                EmpresaCarregado.Wait();
                Empresa = EmpresaCarregado.Result;

            }
            return PartialView("_EmpresaEditarPartial", Empresa);
        }

        [HttpGet]
        public ActionResult AtualizarEmpresa(Empresa empresa)
        {

            var serializedEmpresa = JsonConvert.SerializeObject(empresa);
            client.BaseAddress = new Uri(endereco + "AtualizarEmpresa");
            var content = new StringContent(serializedEmpresa, Encoding.UTF8, "application/json");
            var result = client.PostAsync(client.BaseAddress, content).Result;

            ViewBag.Mensagem = "Empresa Alterada com sucesso";
            return RedirectToAction("ListarFuncionario");

        }

    }
}
