using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WILF.WEB.Controllers
{
    public class GeneralController : Controller
    {
        // GET: General
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(BE.Especie especie)
        {
            try
            {
                new BL.Paciente.GestionEspecie().Save(especie);

                return Json(true, JsonRequestBehavior.AllowGet);
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
                var result = new BL.Paciente.GestionEspecie().GetEspecie();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetEspecieId(Int32 idespecie)
        {
            try
            {
                var result = new BL.Paciente.GestionEspecie().GetEspecie(idespecie);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetEspecieRaza(Int32 idespecie)
        {
            try
            {
                var result = new BL.Paciente.GestionRaza().GetRazaEspecieId(idespecie);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public ActionResult SaveRaza(BE.Raza raza)
        {
            try
            {
                new BL.Paciente.GestionRaza().Save(raza);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public ActionResult GetRazaId(Int32 idraza)
        {
            try
            {
                var result = new BL.Paciente.GestionRaza().GetRaza(idraza);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}