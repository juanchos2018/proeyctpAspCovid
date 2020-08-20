using Firebase.Auth;
using Firebase.Database.Query;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InfoCodiv19.Models
{
    public class Usuario
    {
        public string id_usuariio { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
     
        public string fecha { get; set; }
        public string verificado { get; set; }



        private Conexion conexion;
        private IFirebaseClient client;

        public async Task<List<Usuario>> Get_Usuarios()
        {
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            return (await firebase
              .Child("Usuarios")
              .OnceAsync<Usuario>()).Select(item => new Usuario
              {

                  id_usuariio = item.Object.id_usuariio,
                  nombre = item.Object.nombre,
                  correo = item.Object.correo,
                  fecha = item.Object.fecha,
                  apellido = item.Object.apellido
             

              })
            .ToList();

            
        }

        public async Task<bool> Create_Usuario(Usuario o)
        {
            try
            {
                conexion = new Conexion();
                var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(o.correo, o.clave, o.nombre, true);

                var id = a.User.LocalId;  //para tener el id del usuario que esta registrado we :V
                client = new FireSharp.FirebaseClient(conexion.conec());
                var data = o;
                data.id_usuariio = id;                
                data.verificado = "false";
                data.fecha = DateTime.Now.ToShortDateString();
                SetResponse setResponse = client.Set("Usuarios/" + data.id_usuariio, data);


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }


        public async Task ModificarUsuario(Usuario u)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            var item = (await firebase
              .Child("Usuarios")
              .OnceAsync<Usuario>()).Where(a => a.Object.id_usuariio == u.id_usuariio).FirstOrDefault();

            //  string descripcion = item.Object.descripcion;
            string id_usu = item.Object.id_usuariio;
            string correo = item.Object.correo;
             //  string ruta = item.Object.ruta;   
            await firebase
              .Child("Usuarios")
              .Child(item.Key)
              .PutAsync(new Usuario() { nombre = u.nombre,apellido=u.apellido, id_usuariio = id_usu, fecha = DateTime.Now.ToString() });
        }

        public async Task<bool> Modicar_Usuario(Usuario o)
        {
            try
            {
                conexion = new Conexion();
                var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
                var a = await auth.CreateUserWithEmailAndPasswordAsync(o.correo, o.clave, o.nombre, true);

                var id = a.User.LocalId;  //para tener el id del usuario que esta registrado we :V
                client = new FireSharp.FirebaseClient(conexion.conec());
                var data = o;
                data.id_usuariio = id;
                data.verificado = "false";
                data.fecha = DateTime.Now.ToShortDateString();
                SetResponse setResponse = client.Set("Usuarios/" + data.id_usuariio, data);


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }
    }
}