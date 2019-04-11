using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace WILF.WEB.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(BE.Persona persona)
        {
            try
            {
                var result = new BL.Persona.GestionPersona().Save(persona);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult uploadcliente()
        {
            var request = HttpContext.Request;
            try
            {
                string newruta = string.Empty;
                if (request.Files.Count > 0)
                {
                    //var idorgpolitica = Convert.ToInt32(request.Form["idorganizacion"]);
                    //var idrevicion = Convert.ToInt32(request.Form["idrevicion"]);
                    string filePath = string.Empty;
                    string ruta = System.Web.HttpContext.Current.Server.MapPath("~/images/cliente");
                    newruta = "/images/cliente";
                    if (!System.IO.Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }
                    foreach (string file in request.Files)
                    {
                        var postedFile = request.Files[file];
                        filePath = System.IO.Path.Combine(ruta, postedFile.FileName);
                        newruta = System.IO.Path.Combine(newruta, postedFile.FileName);
                        postedFile.SaveAs(filePath);

                    }
                    if (!string.IsNullOrEmpty(newruta))
                    {
                        newruta = newruta.Replace(@"\", "/");
                    }
                }
                return Json(newruta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetCliente()
        {
            try
            {
                var result = new BL.Persona.GestionPersona().GetCliente();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetClienteId(Int32 idpersona)
        {
            try
            {
                var result = new BL.Persona.GestionPersona().GetCliente(idpersona);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        #region Paciente
        [HttpPost]
        public ActionResult SavePaciente(BE.Paciente persona)
        {
            try
            {
                new BL.Paciente.GestionPaciente().Save(persona);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetPacientePersonaId(Int32 idpersona)
        {
            try
            {
                var result = new BL.Paciente.GestionPaciente().GetPacientexPersonaId(idpersona);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetPacienteId(Int32 idpaciente)
        {
            try
            {
                var result = new BL.Paciente.GestionPaciente().GetPaciente(idpaciente);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetEspecie()
        {
            try
            {
                var lista = new BL.Paciente.GestionEspecie().GetEspecie();
                var result = lista.Where(s => s.Estado == 1).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetRaza(Int32 idespacie)
        {
            try
            {
                var lista = new BL.Paciente.GestionRaza().GetRazaEspecieId(idespacie);
                var result = lista.Where(s => s.Estado == 1).ToList();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public ActionResult uploadpaciente()
        {
            var request = HttpContext.Request;
            try
            {
                string newruta = string.Empty;
                if (request.Files.Count > 0)
                {
                    string filePath = string.Empty;
                    string ruta = System.Web.HttpContext.Current.Server.MapPath("~/images/paciente");
                    newruta = "/images/paciente";
                    if (!System.IO.Directory.Exists(ruta))
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }
                    foreach (string file in request.Files)
                    {
                        var postedFile = request.Files[file];
                        filePath = System.IO.Path.Combine(ruta, postedFile.FileName);
                        newruta = System.IO.Path.Combine(newruta, postedFile.FileName);
                        postedFile.SaveAs(filePath);

                    }
                    if (!string.IsNullOrEmpty(newruta))
                    {
                        newruta = newruta.Replace(@"\", "/");
                    }
                }
                return Json(newruta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        #endregion

        public ActionResult Cliente()
        {
            return View();
        }
    }
}