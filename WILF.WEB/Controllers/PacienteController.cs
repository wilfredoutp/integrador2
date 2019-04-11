using System;
using System.Net;
using System.Web.Mvc;

namespace WILF.WEB.Controllers
{
    public class PacienteController : Controller
    {
        // GET: Paciente
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Buscar(string dni, string nombre)
        {
            try
            {
                var result = new BL.Persona.GestionPersona().GetCliente(dni, nombre);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public ActionResult SaveHisto(BE.Historial historial)
        {
            try
            {
                new BL.Paciente.GestionHistorial().Save(historial);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetHistoPaciId(Int32 idpaciente)
        {
            try
            {
                var result = new BL.Paciente.GestionHistorial().GetHistorialPaciente(idpaciente);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetHistoId(Int32 idhistorial)
        {
            try
            {
                var result = new BL.Paciente.GestionHistorial().GetHistorialId(idhistorial);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}