using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using vonergyDom.ViewModel;


namespace Vonergy.Controllers
{
    public class FuncionarioController : Controller
    {

        private readonly HttpClient client;
        private const string endereco = "https://vonergyapi.azurewebsites.net/api/Funcionario/";

        public FuncionarioController()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(endereco);
        }

        public ActionResult Index()
        {
            return View("CadastroFuncionario");
        }

        [HttpPost]
        public ActionResult CadastroFuncionario(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                funcionario.Senha = funcionario.Cpf.Replace(".", "").Replace("-", "");

                var serializedfuncionario = JsonConvert.SerializeObject(funcionario);
                client.BaseAddress = new Uri(endereco + "Cadastrar");
                var content = new StringContent(serializedfuncionario, Encoding.UTF8, "application/json");
                var result = client.PostAsync(client.BaseAddress, content).Result;

                ViewBag.Mensagem = "Funcionáro Cadastrado com sucesso";
            }
            return View();
        }

        public ActionResult ListarFuncionario()
        {
            IList<Funcionario> funcionarios = null;
            try
            {
                HttpResponseMessage response = client.GetAsync("Listar").Result;
                if (response.IsSuccessStatusCode)
                {
                    var funcionarioCarregado = response.Content.ReadAsAsync<IList<Funcionario>>();
                    funcionarioCarregado.Wait();
                    funcionarios = funcionarioCarregado.Result;
                }
            }
            catch (HttpException httpException)
            {
                return View("Error", new HandleErrorInfo(httpException, "Funcionario", "CadastroFuncionario"));
            }
            return View(funcionarios);
        }

        [HttpGet]
        public ActionResult Delete(long? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "Delete?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var FuncionarioCarregado = response.Content.ReadAsAsync<Funcionario>();
                FuncionarioCarregado.Wait();
            }
            ViewBag.Message = "Funcionario Excluido";
            return RedirectToAction("ListarFuncionario");

        }

        [HttpGet]
        public ActionResult Detalhes(long? id)
        {
            Funcionario Funcionario = new Funcionario();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var FuncionarioCarregado = response.Content.ReadAsAsync<Funcionario>();
                FuncionarioCarregado.Wait();
                Funcionario = FuncionarioCarregado.Result;
            }
            return PartialView("_DetalhesFuncionarioPartial", Funcionario);
        }

        [HttpPut]
        public ActionResult AlterarSenhaFuncionario(Funcionario funcionario)
        {
            var serializedfuncionario = JsonConvert.SerializeObject(funcionario);
            client.BaseAddress = new Uri(endereco + "AlterarSenha");
            var content = new StringContent(serializedfuncionario, Encoding.UTF8, "application/json");
            var result = client.PutAsync(client.BaseAddress, content).Result;

            if (result.IsSuccessStatusCode)
                ViewBag.message = "Senha Alterada Com Sucesso";
            return View("Index");
        }

        [HttpGet]
        public ActionResult Editar(long? id)
        {
            Funcionario Funcionario = new Funcionario();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(endereco + "ListarId?id=" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var FuncionarioCarregado = response.Content.ReadAsAsync<Funcionario>();
                FuncionarioCarregado.Wait();
                Funcionario = FuncionarioCarregado.Result;
            }
            return PartialView("_FuncionarioEditarPartial", Funcionario);
        }

        [HttpPost]
        public ActionResult AtualizarFuncionario(Funcionario funcionario)
        {

            //var serializedfuncionario = JsonConvert.SerializeObject(funcionario);
            //client.BaseAddress = new Uri(endereco + "Cadastrar");
            //var content = new StringContent(serializedfuncionario, Encoding.UTF8, "application/json");
            //var result = client.PostAsync(client.BaseAddress, content).Result;
            if (funcionario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var serializedfuncionario = JsonConvert.SerializeObject(funcionario);
            client.BaseAddress = new Uri(endereco + "Alterar");

            var content = new StringContent(serializedfuncionario, Encoding.UTF8, "application/json");
            var result = client.PutAsync(client.BaseAddress, content).Result;
            var funcionarioCarregado = result.Content.ReadAsAsync<Funcionario>();

            ViewBag.Mensagem = "Funcionáro Alterado com sucesso";
            return RedirectToAction("ListarFuncionario");
        }
    }
}
