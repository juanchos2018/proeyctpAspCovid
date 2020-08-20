using InfoCodiv19.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InfoCodiv19.Controllers
{
    public class ArchivoController : Controller
    {
        // GET: Archivo



        Noticias n = new Noticias();
        public static string verifcado;
        public ActionResult Index()
        {
            ViewBag.Verificado = verifcado;
            return View();
        }


     

        public async Task<ActionResult> Get_Noticias()
        {
            var lista = await n.Get_Noticias();

            return Json(lista, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CreateNoticia()
        {
            Noticias noti = new Noticias();
            object status = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    System.IO.FileStream stream;
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];

                    noti.descripcion = HttpContext.Request.Params["txtdescripcion"];  
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    Directory.CreateDirectory(Server.MapPath("~/uploads/"));

                    Task task = Task.Run(() => noti.Upload(stream, noti, file.FileName));

                   
                    if (task.IsCompleted)
                    {
                        return Json(status, JsonRequestBehavior.AllowGet);
                    }

                }

                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModificarNoticia()
        {
            Noticias noti = new Noticias();
            object status = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    System.IO.FileStream stream;
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];

                    noti.key = HttpContext.Request.Params["key"];
                    noti.descripcion = HttpContext.Request.Params["descripcion"];
                    string path = Path.Combine(Server.MapPath("~/Content/Images/"), file.FileName);
                    file.SaveAs(path);
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    Directory.CreateDirectory(Server.MapPath("~/uploads/"));
                    Task task = Task.Run(() => noti.Update(stream, noti, file.FileName));
                    if (task.IsCompleted)
                    {
                        return Json(status, JsonRequestBehavior.AllowGet);
                    }

                }

                catch (Exception e)
                {
                    return Json("error" + e.Message);
                }
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Eliminar(string key_noticia)
        {
           // await firebaseHelper.DeletePerson(Convert.ToInt32(txtId.Text));
            await n.DeleteNoticia(key_noticia);
          //  await DisplayAlert("Success", "Person Deleted Successfully", "OK");
        //    var allPersons = await firebaseHelper.GetAllPersons();
          //  lstPersons.ItemsSource = allPersons;

            return Json("lista", JsonRequestBehavior.AllowGet);

        }

    }
}