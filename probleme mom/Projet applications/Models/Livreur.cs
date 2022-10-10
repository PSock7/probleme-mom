using System;
using System.Threading;
using System.Threading.Tasks;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/

namespace Projet_applications
{
    public class Livreur : Employee
    {
        public void PrendreCommande()
        {
            CustomConsole.PrintInfo("Récupération de la commande n°" + CurrentCommande.Id);
            CurrentCommande.Etat = Etat.Pret;
        }

        public async void EffectuerLivraison()
        {
            CustomConsole.PrintInfo("Livraison en cours");
            await Task.Delay(10000);
            CustomConsole.PrintInfo("Livraison de la commande n°"+CurrentCommande.Id + " effectuée");
            CurrentCommande.Etat = Etat.Ferme;
            
        }
    }
}