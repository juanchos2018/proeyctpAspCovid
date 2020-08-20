using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InfoCodiv19.Models
{
    public class Donacion
    {
        public string tipoDonacion { get; set; }

        public string noombre_producto { get; set; }  
        public int cantidad { get; set; }
        public string celular { get; set; }
        public string nombreDonante { get; set; }        //public string calidadAlimentos { get; set; }
    
        public string direccion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string token { get; set; }
        public string estado_recepcion { get; set; }
        public string key_donacion { get; set; }  


      //  public string fechav { get; set; }

        public async Task<List<Donacion>> Get_Donaciones()
        {
          
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            return (await firebase
              .Child("Donaciones")
              .OnceAsync<Donacion>()).Select(item => new Donacion
              {

              tipoDonacion=item.Object.tipoDonacion,
       //       calidadAlimentos=item.Object.calidadAlimentos,
              cantidad=item.Object.cantidad,
              direccion=item.Object.direccion,
              nombreDonante=item.Object.nombreDonante,
              token=item.Object.token,
              latitud=item.Object.latitud,
              longitud=item.Object.longitud,
              estado_recepcion=item.Object.estado_recepcion,
              key_donacion=item.Object.key_donacion,
              celular=item.Object.celular

              })
            .ToList();            
        }

        public async Task Modificar(string key, string estado)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            var toUpdatePerson = (await firebase
              .Child("Donaciones")
              .OnceAsync<Donacion>()).Where(a => a.Object.key_donacion == key).FirstOrDefault();
            string tipo = toUpdatePerson.Object.tipoDonacion;
          //  string calidad = toUpdatePerson.Object.calidadAlimentos;
            int cantidad = toUpdatePerson.Object.cantidad;
            string direccion = toUpdatePerson.Object.direccion;
            string nombre = toUpdatePerson.Object.nombreDonante;
            string token_do= toUpdatePerson.Object.token;
            string laitud = toUpdatePerson.Object.latitud;
            string longitud_1 = toUpdatePerson.Object.longitud;
            string key_donacion1 = toUpdatePerson.Key;
            string nombre_producto = toUpdatePerson.Object.noombre_producto;
            string celular = toUpdatePerson.Object.celular;

            await firebase
              .Child("Donaciones")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Donacion() { tipoDonacion = tipo, noombre_producto=nombre_producto, cantidad = cantidad, direccion = direccion, nombreDonante = nombre, celular=celular, token=token_do, latitud=laitud,longitud= longitud_1,key_donacion= key_donacion1, estado_recepcion = estado });
        }


    }
}