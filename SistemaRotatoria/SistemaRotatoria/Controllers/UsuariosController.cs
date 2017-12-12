using SistemaRotatoria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaRotatoria.Controllers
{
    public class UsuariosController : Controller
    {
        public RotatoriaEntities db = new RotatoriaEntities();
        //private User user = new User();

        private static int cod;
        private static string mail;

        public ActionResult GetUsuario()
        {
            var db = new RotatoriaEntities();
            List<Usuario> ListGetUsuario = new List<Usuario>();
            db.Configuration.ProxyCreationEnabled = false;
            ListGetUsuario = db.Usuario.Select(e => e).ToList();
            //foreach (var item in ListGetAutorizacoes)
            //{
            //    item.Veiculo = db.Veiculo.Where(e => e.VeiPla == item.VeiPla).Select(e => e).FirstOrDefault();
            //}

            return Json(ListGetUsuario, JsonRequestBehavior.AllowGet);
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                List<GetUsuario> ListGetUsuarios = new List<GetUsuario>();
                return View();
            }
        }

        [HttpGet]
        public ActionResult ListaUsuarios()
        {
            var users = db.Usuario.OrderBy(u => u.UsuNom).ToList();
            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }


        //GET: Usuarios/Cadastro
        public ActionResult Cadastro()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Cadastro(Usuario usuario)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (usuario.UsuSen != null) //Criptografa senhas
                {
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(usuario.UsuSen);
                    byte[] hash = md5.ComputeHash(inputBytes);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("X2"));
                    }
                    //return sb.ToString();
                    usuario.UsuSen = sb.ToString(); //altera o valor da senha que vai pro banco
                }

                if (ModelState.IsValid)
                {
                    var context = new RotatoriaEntities();
                    context.Usuario.Add(usuario);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string email = frm["email"];
            string senha = frm["senha"];
            mail = frm["email"];
            if (senha != null) //Criptografa senha que veio da view de login
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                //return sb.ToString();
                senha = sb.ToString(); //altera o valor da senha para comparar com a do banco
            }


            var usuario = db.Usuario.FirstOrDefault(x => x.UsuMai == email && x.UsuSen == senha);

            if (usuario != null)
            {
                Session["UserId"] = mail;
                var perfil = usuario.UsuPer;
                var admin = string.Compare(perfil, "A");
                if (admin == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(usuario);
                }
            }
            else
            {
                Session["UserId"] = null;
                ModelState.AddModelError("", "Usuário ou senha incorretos");
                return View(usuario);
            }

        }

        //Get: Usuarios/Editar
        [HttpGet]
        public ActionResult Editar(int id)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index");
            }

            cod = id;

            var usuario = db.Usuario.FirstOrDefault(u => u.UsuMai == mail);
            if (usuario != null)
            {
                var admin = string.Compare(usuario.UsuPer, "A");
                if (admin == 0)
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("index");
            }

            return View();

        }

        public JsonResult CarregaUsuario()
        {
            var id = cod;
            try
            {
                Usuario usuario = new Usuario();
                usuario = db.Usuario.SingleOrDefault(u => u.UsuCod == id);
                return Json(new
                {
                    UsuCod = usuario.UsuCod,
                    UsuNom = usuario.UsuNom,
                    UsuEmp = usuario.UsuEmp,
                    UsuMai = usuario.UsuMai,
                    UsuSen = usuario.UsuSen,
                    UsuPer = usuario.UsuPer,
                    UsuSit = usuario.UsuSit
                }
                 , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Editar(string Nome, string Senha, string Email, string Empresa, string Perfil, string Situacao)
        {
            if (Senha != null) //Criptografa senhas
            {
                var comparasenha = db.Usuario.Where(u => u.UsuSen == Senha);
                if (comparasenha == null)
                {
                    System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);
                    byte[] hash = md5.ComputeHash(inputBytes);
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("X2"));
                    }
                    //return sb.ToString();
                    Senha = sb.ToString(); //altera o valor da senha que vai pro banco
                }

            }
            try
            {
                Usuario usuario = new Usuario
                {
                    UsuCod = cod,
                    UsuNom = Nome,
                    UsuSen = Senha,
                    UsuMai = Email,
                    UsuEmp = Empresa,
                    UsuPer = Perfil,
                    UsuSit = Situacao
                };

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {

            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["UserId"] = null;
            return RedirectToAction("Login");
        }
    }

    public class GetUsuario
    {
        public int UsuCod { get; set; }
        public string UsuNom { get; set; }
        public string UsuMai { get; set; }
        public string UsuEmp { get; set; }
        public string UsuPer { get; set; }
        public string UsuSit { get; set; }
        public string UsuSen { get; set; }
    }
}
