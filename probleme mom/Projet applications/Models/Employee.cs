/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
namespace Projet_applications
{
    public abstract class Employee
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Commande CurrentCommande { get; set; }
    }
}