using System;
using System.Data.SQLite;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
namespace Projet_applications
{
    public class Cuisinier : Employee
    {
        public async void PreparerCommande()
        {
            CustomConsole.PrintInfo("Preparation de la commande n°"+ CurrentCommande.Id+" en cours");
            await Task.Delay(10000);
            CustomConsole.PrintInfo("Commande n°"+ CurrentCommande.Id+" prete");
            CurrentCommande.Etat = Etat.Livraison;
            
        }
    }
}