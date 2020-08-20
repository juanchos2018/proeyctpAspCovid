using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace InfoCodiv19.Models
{
    public class Diagnostico
    {
        public string temperaturaCorporal { get; set; }

        public string congestionNasal { get; set; }
        public string indigestionEstomacal { get; set; }
        public string tieneTos { get; set; }
        public string fatiga { get; set; }
        public string faltaAlimento { get; set; }
        public string perdidaGusto { get; set; }



        public async Task<List<Diagnostico>> Get_Diagnostico()
        {

            var firebase = new Firebase.Database.FirebaseClient("https://proyecto1-a1300.firebaseio.com/");

            return (await firebase
              .Child("Diagnostico")
              .OnceAsync<Diagnostico>()).Select(item => new Diagnostico
              {
                  
                  temperaturaCorporal=item.Object.temperaturaCorporal,
              congestionNasal=item.Object.congestionNasal,
              indigestionEstomacal=item.Object.indigestionEstomacal,
              tieneTos=item.Object.tieneTos,
              fatiga=item.Object.fatiga,
              faltaAlimento=item.Object.faltaAlimento,
              perdidaGusto=item.Object.perdidaGusto



               

              })
            .ToList();
        }

    }
}