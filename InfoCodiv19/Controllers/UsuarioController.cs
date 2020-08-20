using Firebase.Auth;
using InfoCodiv19.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InfoCodiv19.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario

        Usuario u = new Usuario();

        private static string key1 = "AIzaSyA-kMntk_eseruYPRgHd83glT9wG5HF0Ug";

        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Get_Usuarios()
        {
            var lista = await u.Get_Usuarios();
            return Json(lista, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Get(string id_usu)
        {
            var lista = await u.Get_Usuarios();

            var dataos = lista.Where(x => x.id_usuariio == id_usu).FirstOrDefault();
            return Json(dataos, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Save_Usuario(Usuario o)
        {
            bool completado = false;

            object status = "";
           // Task tarea = Task.Run(() =>  o.Create_Usuario(o));

            await Task.Run(() => o.Create_Usuario(o));
            

            return Json(completado, JsonRequestBehavior.AllowGet);

        }


        public async Task<ActionResult> Modiciar_Usuario(Usuario o)
        {
            bool completado = false;

            object status = "";
            // Task tarea = Task.Run(() =>  o.Create_Usuario(o));

            await Task.Run(() => o.Modicar_Usuario(o));

            completado = true;
            return Json(completado, JsonRequestBehavior.AllowGet);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    // return this.Redi
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);

            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var auth = new FirebaseAuthProvider(new FirebaseConfig(key1));
                    var ab = await auth.SignInWithEmailAndPasswordAsync(model.Email, model.Password);
                    var token = ab.FirebaseToken;
                    //    id_user = ab.User.LocalId;
                    //  correo = ab.User.Email;

                    var user = ab.User;
                    var verificado = ab.User.IsEmailVerified;

                    if (token != "")
                    {
                        Session["correo"] = ab.User.Email;
                     //   ViewBag.Verificado = verificado;
                       // ArchivoController.verifcado = verificado.ToString();
                        return this.RedirectToAction("Index", "Archivo");
                     


                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "invalide usrname or password");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return View(model);
        }

        public ActionResult Cerrar()
        {
            string correo = Session["correo"].ToString();
            Session.Clear();
            
            return RedirectToAction("Login");
        }


    }
}