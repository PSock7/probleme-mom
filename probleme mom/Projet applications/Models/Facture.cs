using System;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
namespace Projet_applications
{
    public class Facture
    {
        public int Id { get; set; }
        public double Prix { get; set; }

        public override string ToString()
        {
            return "Facture n°" + Id + " : " + Prix + " € ";
        }
    }
}