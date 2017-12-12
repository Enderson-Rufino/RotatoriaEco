using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Activities.Statements;
using System.Data;
using SistemaRotatoria.Models;

namespace SistemaRotatoria.Controllers
{
    public class RotatoriaController : Controller
    {
        // GET: Rotatoria
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AutorizacoesUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SalvarAutorizacoesUsuario(VeiculosData veiculosdata)
        {
            using(var context = new RotatoriaEntities())
            {
                Veiculo veiculo = new Veiculo
                {
                    VeiCor = veiculosdata.VeiCor,
                    VeiEix = veiculosdata.VeiEix,
                    VeiMar = veiculosdata.VeiMar,
                    VeiMod = veiculosdata.VeiMod,
                    VeiPla = veiculosdata.VeiPla
                };

                Autorizacao autorizacao = new Autorizacao
                {
                    VeiPla = veiculosdata.VeiPla,
                    AutdatPro = veiculosdata.AutdatPro,
                    AutDatInc = veiculosdata.AutDatInc
                };

                using (var scope = new TransactionScope())
                {
                    var placaView = autorizacao.VeiPla;
                    var placaBanco = context.Veiculo.FirstOrDefault(e => e.VeiPla == placaView);

                    if (placaBanco != null)
                    {
                        context.Autorizacao.Add(autorizacao);
                        context.SaveChanges();
                        scope.Complete();
                        return RedirectToAction("Index", "Autorizacaos");
                    }
                    else
                    {
                        context.Veiculo.Add(veiculo);
                        context.Autorizacao.Add(autorizacao);
                        context.SaveChanges();
                        scope.Complete();
                        return RedirectToAction("Index", "Autorizacaos");
                    }
                }
            }
        }

        public ActionResult ListaAutorizacaoUsuario()
        {
            List<GetAutorizacoes> ListGetAutorizacoesUsuario = new List<GetAutorizacoes>();
            return View();
        }

        public ActionResult GetAutorizacoesUsuario()
        {
            var db = new RotatoriaEntities();
            List<Autorizacao> ListGetAutorizacoesUsuario = new List<Autorizacao>();
            db.Configuration.ProxyCreationEnabled = false;
            ListGetAutorizacoesUsuario = db.Autorizacao.Select(e => e).ToList();

            return Json(ListGetAutorizacoesUsuario, JsonRequestBehavior.AllowGet);
        }
    }

    public class VeiculosData
    {
        public string VeiPla { get; set; }
        public string VeiMar { get; set; }
        public string VeiMod { get; set; }
        public string VeiCor { get; set; }
        public Nullable<short> VeiEix { get; set; }
        public string VeiSit { get; set; }
        public DateTime? AutdatPro { get; set; }
        public DateTime? AutDatInc { get; set; }
    }

    public class GetAutorizacoes
    {
        public string VeiPla { get; set; }
        public string VeiMar { get; set; }
        public string VeiMod { get; set; }
        public string VeiCor { get; set; }
        public Nullable<short> VeiEix { get; set; }
        public string VeiSit { get; set; }
        public DateTime? AutdatPro { get; set; }
    }
}