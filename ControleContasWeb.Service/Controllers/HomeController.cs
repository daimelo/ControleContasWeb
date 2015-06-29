using ControleContasWeb.Data;
using ControleContasWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleContasWeb.Service.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuarios usuario)
        {
            UsuariosRepository rep = new UsuariosRepository();

            if (rep.Valida(usuario))
            {
                usuario = rep.GetByEmail(usuario.Email);

                HttpCookie myCookie = new HttpCookie("contas");
                myCookie["id_usuario"] = usuario.Id.ToString();
                myCookie["nome_usuario"] = usuario.Nome;
                myCookie.Expires = DateTime.Now.AddDays(1d);
                HttpContext.Response.Cookies.Add(myCookie);

                return RedirectToAction("Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Home()
        {
            if(Request.Cookies["contas"] != null)
            {
                if(Request.Cookies["contas"]["id_usuario"] != null)
                {
                    ViewBag.IdUsuario = Request.Cookies["contas"]["id_usuario"];
                }

                if (Request.Cookies["contas"]["nome_usuario"] != null)
                {
                    ViewBag.IdUsuario = Request.Cookies["contas"]["nome_usuario"];
                }

                int IdUsuario = Int32.Parse(Request.Cookies["contas"]["id_usuario"]);
                var contas = ContasRepository.GetAll(IdUsuario);
                return View(contas);
            }
            else
            {
                return RedirectToAction("Index");
            }

            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Tipo = new SelectList(ContasTipoRepository.GetTipos(), "Id", "Tipo");
            return View();
        }

        public ActionResult CreateTipo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Tipo = new SelectList(ContasTipoRepository.GetTipos(), "IdTipo", "NomeTipo");
            var contas = ContasRepository.GetAll(id);
            return View(contas);
        }

        [HttpGet]
        public ActionResult EditTipo(int id)
        {
            var tipos = ContasTipoRepository.GetOneTipo(id);
            return View(tipos);
        }

        public ActionResult Delete(int id)
        {
            ContasRepository rep = new ContasRepository();
            rep.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTipo(int id)
        {
            ContasTipoRepository rep = new ContasTipoRepository();
            rep.Delete(id);

            return RedirectToAction("Tipos");
        }

        [HttpPost]
        public ActionResult Create(Contas conta)
        {
            ContasRepository rep = new ContasRepository();
            rep.Create(conta);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateTipo(ContasTipo pTipo)
        {
            ContasTipoRepository rep = new ContasTipoRepository();
            rep.CreateTipo(pTipo);

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Edit(Contas conta)
        {
            ContasRepository rep = new ContasRepository();
            rep.Update(conta);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTipo(ContasTipo tipo)
        {
            ContasTipoRepository rep = new ContasTipoRepository();
            rep.Update(tipo);

            return RedirectToAction("Tipos");
        }

        [HttpPost]
        public ActionResult Pesquisar(string dataInicio, string dataFim, int pIdUsuario)
        {
            DateTime dataI = Convert.ToDateTime(dataInicio);
            DateTime dataF = Convert.ToDateTime(dataFim);

            var contas = ContasRepository.GetBySearch(dataI, dataF, pIdUsuario);
            return View(contas);
        }

    }
}