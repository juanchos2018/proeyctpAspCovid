using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InfoCodiv19.Models
{
    public class CasoReportado
    {

        public string tipoCaso { get; set; }
        public string apellidoPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public string sexo { get; set; }
        public int edad { get; set; }
        public int celular { get; set; }
        public string distrito { get; set; }
        public string direccion { get; set; }       
        public string otrasObservaciones { get; set; }
        public string key_caso { get; set; }
        public string estado { get; set; }

        public string token { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }


        public async Task<List<CasoReportado>> Get_ReporteCaso()
        {

            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            return (await firebase
              .Child("ReporteCaso")
              .OnceAsync<CasoReportado>()).Select(item => new CasoReportado
              {

                    tipoCaso=item.Object.tipoCaso,
                    apellidoPaciente=item.Object.apellidoPaciente,
                    nombrePaciente=item.Object.nombrePaciente,
                    sexo=item.Object.sexo,
                    edad=item.Object.edad,
                    celular=item.Object.celular,
                    distrito=item.Object.distrito,
                    direccion=item.Object.direccion,
                    otrasObservaciones=item.Object.otrasObservaciones,
                    key_caso=item.Object.key_caso,
                    estado=item.Object.estado,
                    latitud=item.Object.latitud,
                    longitud=item.Object.longitud,
                    token=item.Object.token                  


              })
            .ToList();
        }


        public async Task Modificar(string key, string estado)
        {
            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            var item = (await firebase
              .Child("ReporteCaso")
              .OnceAsync<CasoReportado>()).Where(a => a.Object.key_caso == key).FirstOrDefault();

                  string  tipoCaso = item.Object.tipoCaso;
                  string  apellidoPaciente = item.Object.apellidoPaciente;
                  string  nombrePaciente = item.Object.nombrePaciente;
                  string  sexo = item.Object.sexo;
                  int  edad = item.Object.edad;
                  int  celular = item.Object.celular;
                  string  distrito = item.Object.distrito;
                  string  direccion = item.Object.direccion;
                  string  otrasObservaciones = item.Object.otrasObservaciones;
                  string  key_caso = item.Object.key_caso;

                    string token_do = item.Object.token;
                    string laitud = item.Object.latitud;
                    string longitud_1 = item.Object.longitud;
            //   string  estado = item.Object.estado;


            await firebase
              .Child("ReporteCaso")
              .Child(item.Key)
              .PutAsync(new CasoReportado() { tipoCaso = tipoCaso, apellidoPaciente = apellidoPaciente, nombrePaciente = nombrePaciente, sexo = sexo, edad = edad, celular = celular, distrito = distrito, direccion = direccion, otrasObservaciones = otrasObservaciones, key_caso = key_caso,estado=estado, token = token_do, latitud = laitud, longitud = longitud_1 });
        }


    }
}