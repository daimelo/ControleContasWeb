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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
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

                if (Request.Cookies["contas"]["grupo_usuario"] != null)
                {
                    ViewBag.GrupoUsuario = Request.Cookies["contas"]["grupo_usuario"];
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

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Controle de Contas Web";

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Request.Cookies["contas"] != null)
            {
                if (Request.Cookies["contas"]["id_usuario"] != null)
                {
                    ViewBag.IdUsuario = Request.Cookies["contas"]["id_usuario"];
                }

                int IdUsuario = Int32.Parse(Request.Cookies["contas"]["id_usuario"]);
            }
            else
            {
                return RedirectToAction("Index");
            }     

            ViewBag.Tipo = new SelectList(ContasTipoRepository.GetTipos(), "Id", "Tipo");

            return View();
        }

        [HttpGet]
        public ActionResult CreateTipo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateUsuarios()
        {
            ViewBag.Grupo = new SelectList(UsuariosGrupoRepository.GetGrupos(), "Id", "Grupo");

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Tipo = new SelectList(ContasTipoRepository.GetTipos(), "Id", "Tipo");
            var contas = ContasRepository.GetOne(id, Int32.Parse(Request.Cookies["contas"]["id_usuario"]));

            return View(contas);
        }

        [HttpGet]
        public ActionResult EditTipo(int id)
        {
            var tipos = ContasTipoRepository.GetOneTipo(id);

            return View(tipos);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            ContasRepository rep = new ContasRepository();
            rep.Delete(id);

            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult DeleteTipo(int id)
        {
            ContasTipoRepository rep = new ContasTipoRepository();
            rep.Delete(id);

            return RedirectToAction("Tipos");
        }

        [HttpGet]
        public ActionResult Pesquisar()
        {
            return View("Home");
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
                myCookie["grupo_usuario"] = usuario.Grupo.Grupo;

                myCookie.Expires = DateTime.Now.AddDays(1d);
                HttpContext.Response.Cookies.Add(myCookie);

                return RedirectToAction("Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Create(Contas conta)
        {
            ContasRepository rep = new ContasRepository();
            conta.IdUsuario = Int32.Parse(Request.Cookies["contas"]["id_usuario"]);
            rep.Create(conta);

            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult CreateTipo(ContasTipo pTipo)
        {
            ContasTipoRepository rep = new ContasTipoRepository();
            rep.CreateTipo(pTipo);

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult CreateUsuarios(Usuarios pUsuario)
        {
            UsuariosRepository rep = new UsuariosRepository();
            rep.Create(pUsuario);

            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult Edit(Contas conta)
        {
            ContasRepository rep = new ContasRepository();
            rep.Update(conta);

            return RedirectToAction("Home");
        }

        [HttpPost]
        public ActionResult EditTipo(ContasTipo tipo)
        {
            ContasTipoRepository rep = new ContasTipoRepository();
            rep.Update(tipo);

            return RedirectToAction("Tipos");
        }

        [HttpPost]
        public ActionResult Pesquisar(int pIdUsuario, string dataInicio, string dataFim)
        {
            DateTime dataI = Convert.ToDateTime(dataInicio);
            DateTime dataF = Convert.ToDateTime(dataFim);
            pIdUsuario = Int32.Parse(Request.Cookies["contas"]["id_usuario"]);

            var contas = ContasRepository.GetBySearch(pIdUsuario, dataI, dataF);

            return View(contas);
        }

    }
}