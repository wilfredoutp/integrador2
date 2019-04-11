using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WILF.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult ValidaUsuario(string user, string pass)
        {
            BE.Usuario usuario = null;
            try
            {
                var result = new BL.Usuario.GestionUsuario().Valid(user, pass);
                if (result)
                {
                    usuario = new BL.Usuario.GestionUsuario().GetUser(user);
                    usuario.Menus = new BL.Menu.GestionMenu().GetMenu(usuario.IdPerfil);
                    var persona = new BL.Persona.GestionPersona().GetCliente(usuario.IdPersona);
                    var paciente = new BL.Paciente.GestionPaciente().GetPacientexPersonaId(usuario.IdPersona);
                    if (persona.IdPersona > 0)
                    {
                        usuario.RutaImagen = persona.RutaImagen;
                    }
                    if (paciente != null && paciente.Count > 0)
                    {
                        var dd = paciente.Where(s => s.Estado == 1).FirstOrDefault();
                        if (dd != null)
                        {
                            usuario.ImagenMascota = dd.RutaImagen;
                        }
                    }
                    HttpContext.Session["Usuario"] = usuario;
                }
                else {
                    return Json("No esta registrado en la Veterinaria", JsonRequestBehavior.AllowGet);
                }
                return Json(usuario, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public ActionResult Logon()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Home");
            //return View();
        }
    }
}