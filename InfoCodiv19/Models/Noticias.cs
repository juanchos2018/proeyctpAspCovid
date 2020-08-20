using Firebase.Auth;
using Firebase.Database.Query;
using Firebase.Storage;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace InfoCodiv19.Models
{
    public class Noticias
    {

        public string descripcion { get; set; }
        public string ruta { get; set; }
       
        public string key { get; set; }


        public string fecha { get; set; }
        //gs://proyecto1-a1300.appspot.com

        private Conexion conexion;
        private string Bucket = "proyecto1-a1300.appspot.com";
        private IFirebaseClient client;



        public async Task<List<Noticias>> Get_Noticias()
        {

            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            return (await firebase
              .Child("Noticias")
              .OnceAsync<Noticias>()).Select(item => new Noticias
              {

                  descripcion = item.Object.descripcion,
                  ruta = item.Object.ruta,
                  fecha = item.Object.fecha,
                  key=item.Object.key
                  

              })
            .ToList();
        }




        public async Task<bool> Upload(FileStream stream, Noticias obj,string filenanme)
        {

            conexion = new Conexion();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
            var a = await auth.SignInWithEmailAndPasswordAsync(conexion.AthEmail(), conexion.AthPassword());

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                .Child("Noticias")
                .Child(filenanme)
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task; // wwwfireabsdfksndfimg .com
                obj.ruta = link;
                Task tarea2 = Task.Run(() => Crear_Noticia(obj));
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            return true;
        }

        public async Task<bool> Crear_Noticia(Noticias o)
        {
            try
            {

                var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

                var key_noti = Firebase.Database.FirebaseKeyGenerator.Next();

                //o.estado_producto = "NoPublico";
                await firebase
                  .Child("Noticias").Child(key_noti)
                  .PutAsync(new Noticias() { key= key_noti, descripcion = o.descripcion, ruta = o.ruta,fecha=DateTime.Now.ToShortDateString() });


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }


        public async Task<bool> Update(FileStream stream, Noticias obj, string filenanme)
        {

            conexion = new Conexion();
            var auth = new FirebaseAuthProvider(new FirebaseConfig(conexion.Firekey()));
            var a = await auth.SignInWithEmailAndPasswordAsync(conexion.AthEmail(), conexion.AthPassword());

            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {   
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                })
                .Child("Noticias")
                .Child(filenanme)
                .PutAsync(stream, cancellation.Token);

            string k = obj.key;
            string des = obj.descripcion;


            try
            {
                string link = await task; // wwwfireabsdfksndfimg .com
               // obj.ruta = link;
                Task tarea2 = Task.Run(() => Modificar2(k, link,des));
            }

            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
            return true;
        }

        //  
        public async Task<bool> Modificar_Noticia(Noticias o)
        {
            try
            {

                var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

                var key_noti = Firebase.Database.FirebaseKeyGenerator.Next();

                //o.estado_producto = "NoPublico";
                // aqui es para modificar



                await firebase
                  .Child("Noticias").Child(key_noti)
                  .PutAsync(new Noticias() { key = key_noti, descripcion = o.descripcion, ruta = o.ruta, fecha = DateTime.Now.ToShortDateString() });


            }
            catch (Exception ex)
            {
                // ModelState.AddModelError(string.Empty, ex.Message);
            }
            return true;
        }


        //---------------------------------
        public async Task Modificar2(string key, string url,string descripcion)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            var item = (await firebase
              .Child("Noticias")
              .OnceAsync<Noticias>()).Where(a => a.Object.key == key).FirstOrDefault();

          //  string descripcion = item.Object.descripcion;
            string key1 = item.Object.key;
          //  string ruta = item.Object.ruta;            

            await firebase
              .Child("Noticias")
              .Child(item.Key)
              .PutAsync(new Noticias() { descripcion = descripcion, ruta = url, key = key1,fecha=DateTime.Now.ToString()  });
        }

        public async Task DeleteNoticia(string key)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            var toDeleteNoti = (await firebase
              .Child("Noticias")
              .OnceAsync<Noticias>()).Where(a => a.Object.key == key).FirstOrDefault();
            await firebase.Child("Noticias").Child(toDeleteNoti.Key).DeleteAsync();

        }
    }
}