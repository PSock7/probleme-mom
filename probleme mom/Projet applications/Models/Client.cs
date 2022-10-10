using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projet_applications
{
    public class Client
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public int Telephone { get; set; }
        public string DateFirst { get; set; }
        public Adresse Adresse { get; set; }

        public override string ToString()
        {
            string res = "";
            if (Id != 0) res += "CLient n° " + Id + " : \r\n";
            if (!(Nom is null)) res += "           Nom : " + Nom + "\r\n";
            if (!(Prenom is null)) res += "           Prenom : " + Prenom + "\r\n";
            if (Telephone != 0) res += "           Telephone : " + Telephone + "\r\n";
            if (!(DateFirst is null)) res += "           DateFirst : " + DateFirst + "\r\n";
            if (!(Adresse is null))
                res += "           Adresse : " + Adresse.NumRue + " rue " + Adresse.Rue + " " + Adresse.Ville;
            return res;
        }
    }
}