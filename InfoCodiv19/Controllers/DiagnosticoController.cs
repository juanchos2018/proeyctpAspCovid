using InfoCodiv19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InfoCodiv19.Controllers
{
    public class DiagnosticoController : Controller
    {
        // GET: Diagnostico

        Diagnostico d = new Diagnostico();
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Get_Diagnostico()
        {
            var lista = await d.Get_Diagnostico();

            return Json(lista, JsonRequestBehavior.AllowGet);

        }


    }
}