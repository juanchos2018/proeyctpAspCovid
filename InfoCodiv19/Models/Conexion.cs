using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoCodiv19.Models
{
    public class Conexion
    {

        public IFirebaseConfig conec()
        {
            IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "1ER3Tk2rzUArg37zevJazwkQs4phvNrYVRiR643B",
                BasePath = "https://proyecto1-a1300.firebaseio.com/"

            };
            return config;
        }

        public String Firekey()
        {
            //1ER3Tk2rzUArg37zevJazwkQs4phvNrYVRiR643B
            string key = "AIzaSyA-kMntk_eseruYPRgHd83glT9wG5HF0Ug";

            return key;
        }

        public string AthEmail()
        {
            string AthEmail = "fiosalamancac@gmail.com";
            return AthEmail;
        }
        public string AthPassword()
        {
            string AuthPassword = "7654321";
            return AuthPassword;
        }
    }
}