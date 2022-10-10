using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SQLite;
using System.Linq.Expressions;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/

namespace Projet_applications
{
    public class App
    {
        public Commis CurrentCommis { get; set; }

        public Commis CurrentCommande { get; set; }
        public bool Running { get; set; }
        public Database Database { get; set; }

        public bool hasSeeding { get; set; }

        public void Start()
        {
            Database.Open();
            if (!hasSeeding)
            {
                Database.Seed();
                hasSeeding = true;
            }

            if (CurrentCommis == null)
            {
                Begin:
                WelcomeMessage();
                string stringCommisId = Console.ReadLine();
                int commisId;
                if (!Int32.TryParse(stringCommisId, out commisId))
                {
                    CustomConsole.PrintError("Veuillez entrer un id valide");
                    goto Begin;
                }

                SQLiteDataReader reader =
                    Database.Select("SELECT * FROM Employee WHERE type='commis' AND  id=" + commisId.ToString());
                if (!reader.HasRows)
                {
                    CustomConsole.PrintError("Aucun commis ne correspond à ce numero");
                    goto Begin;
                }

                reader.Read();

                CurrentCommis = new Commis()
                {
                    Id = Int32.Parse("" + reader.GetValue(0)),
                    Nom = (string) reader.GetValue(1),
                    Prenom = (string) reader.GetValue(2)
                };
            }

            Menu:
            MenuMessage();
            try
            {
                string stringChoice = Console.ReadLine();
                int choice;
                if (!Int32.TryParse(stringChoice, out choice))
                {
                    CustomConsole.PrintError("Veuillez entrer un numero valide");
                    goto Menu;
                }

                switch (choice)
                {
                    case 1:
                        ClientManagement();
                        break;
                    case 2:
                        CommandeManagement();
                        break;
                    case 3:
                        StatistiquesManagement();
                        break;
                    case 9:
                        Running = false;
                        break;
                    default:
                        CustomConsole.PrintError("Veuillez choisir un nombre valide");
                        goto Menu;
                }
            }
            catch (Exception e)
            {
                CustomConsole.PrintError(e.Message);
                goto Menu;
            }
        }


        public void WelcomeMessage()
        {
            Console.WriteLine("Bienvenue ! ");
            Console.WriteLine("Veuillez entrer votre identifiant de commis : ");
        }

        public void MenuMessage()
        {
            Console.WriteLine("Bonjour " + CurrentCommis.Nom + " " + CurrentCommis.Prenom);
            Console.WriteLine("Entrez : ");
            CustomConsole.PrintChoice(1, "pour gerer les clients");
            CustomConsole.PrintChoice(2, "pour gerer les commandes");
            CustomConsole.PrintChoice(3, "pour afficher les statistiques");
            CustomConsole.PrintChoice(9, "pour quitter");
        }

        public void ClientManagement()
        {
            Begin:
            Console.WriteLine("Entrez : ");
            CustomConsole.PrintChoice(1, "pour afficher les clients par ordre alphabetiques");
            CustomConsole.PrintChoice(2, "pour afficher les clients d'une ville");
            CustomConsole.PrintChoice(3, "pour afficher montants des achats cumulés");
            CustomConsole.PrintChoice(9, "pour quitter");
            try
            {
                string stringChoice = Console.ReadLine();
                int choice;
                if (!Int32.TryParse(stringChoice, out choice))
                {
                    CustomConsole.PrintError("Veuillez entrer un numero valide");
                    goto Begin;
                }

                switch (choice)
                {
                    case 1:
                        CurrentCommis.GetClients(Database);
                        goto Begin;
                    case 2:
                        CurrentCommis.GetClientsByCity(Database);
                        goto Begin;
                    case 3:
                        CurrentCommis.GetAmounts(Database);
                        goto Begin;
                    case 9:
                        break;
                    default:
                        goto Begin;
                }
            }
            catch (Exception e)
            {
                CustomConsole.PrintError(e.Message);
                goto Begin;
            }
        }

        public void CommandeManagement()
        {
            Begin:
            Console.WriteLine("Entrez : ");
            CustomConsole.PrintChoice(1, "creer une nouvelle commande");
            CustomConsole.PrintChoice(2, "afficher une commande");
            CustomConsole.PrintChoice(9, "pour quitter");
            try
            {
                string stringChoice = Console.ReadLine();
                int choice;
                if (!Int32.TryParse(stringChoice, out choice))
                {
                    CustomConsole.PrintError("Veuillez entrer un numero valide");
                    goto Begin;
                }

                switch (choice)
                {
                    case 1:
                        CurrentCommis.CreerCommande(Database);
                        goto Begin;
                    case 2:
                        CurrentCommis.AfficherCommande(Database);
                        goto Begin;
                    case 9:
                        break;
                    default:
                        goto Begin;
                }
            }
            catch (Exception e)
            {
                CustomConsole.PrintError(e.Message);
                CustomConsole.PrintError(e.StackTrace);
                goto Begin;
            }
        }


        public void StatistiquesManagement()
        {
            Begin:
            int choixStats;

            Console.WriteLine("Entrez : ");
            CustomConsole.PrintChoice(1, "Afficher par commis, le nombre de commandes gerees");
            CustomConsole.PrintChoice(2, "Afficher par livreur le nombre de livraisons effectuees ");
            CustomConsole.PrintChoice(3, "Afficher les commandes selon une periode de temps ");
            CustomConsole.PrintChoice(4, "Afficher la moyenne des prix des commandes");
            CustomConsole.PrintChoice(5, "Afficher la moyenne des comptes clients");
            CustomConsole.PrintChoice(9, "Quitter ");
            choixStats = Convert.ToInt32(Console.ReadLine());

            switch (choixStats)
            {
                case 1:
                    int commisId;
                    int nbCommandes = 0;
                    Console.WriteLine("Quel est l'id du commis dont vous voulez connaitre les commandes?");
                    commisId = Convert.ToInt32(Console.ReadLine());
                    string query1 = "SELECT id FROM Commande WHERE commisId =" + commisId;
                    SQLiteCommand myCommand1 = new SQLiteCommand(query1, Database.myConnection);
                    SQLiteDataReader requete1 = myCommand1.ExecuteReader();
                    if (!requete1.HasRows) Console.WriteLine("Aucun commis avec cet id\n\n\n");
                    for (int i = 1; requete1.Read(); i++)
                    {
                        nbCommandes = i;
                    }

                    Console.WriteLine("Ce commis est lié à " + nbCommandes + " commandes\n\n\n");
                    goto Begin;
                case 2:
                    int livreurId;
                    int nbLivraisons = 0;
                    Console.WriteLine("Quel est l'id du livreur dont vous voulez connaitre les livraisons?");
                    livreurId = Convert.ToInt32(Console.ReadLine());
                    string query2 = "SELECT id FROM Commande WHERE livreurId =" + livreurId;
                    SQLiteCommand myCommand2 = new SQLiteCommand(query2, Database.myConnection);
                    SQLiteDataReader requete2 = myCommand2.ExecuteReader();
                    if (!requete2.HasRows) Console.WriteLine("Aucun commis avec cet id\n\n\n");
                    for (int i = 1; requete2.Read(); i++)
                    {
                        nbLivraisons = i;
                    }

                    Console.WriteLine("Ce livreur est lié à " + nbLivraisons + " commandes\n\n\n");
                    goto Begin;
                case 3:
                    string query3 =
                        "SELECT * FROM Commande WHERE date = strftime('%d-%m-%Y %H:%M:%S', '2012-09-13 12:44:22')";
                    SQLiteCommand myCommand3 = new SQLiteCommand(query3, Database.myConnection);
                    SQLiteDataReader requete3 = myCommand3.ExecuteReader();
                    CustomConsole.PrintError("Fonctionnalité non implémentée");
                    goto Begin;
                case 4:
                    float avgCommandes = 0;
                    string query4 = "SELECT AVG(prix) FROM Facture";
                    SQLiteCommand myCommand4 = new SQLiteCommand(query4, Database.myConnection);
                    SQLiteDataReader requete4 = myCommand4.ExecuteReader();
                    while (requete4.Read())
                    {
                        avgCommandes = requete4.GetFloat(0);
                    }

                    Console.WriteLine("La moyenne des prix des commandes est : " + avgCommandes + " euros\n\n\n");
                    goto Begin;
                case 5:
                    CustomConsole.PrintError("Fonctionnalité non implémentée");
                    goto Begin;
                case 9:
                    break;
                default:
                    goto Begin;
            }
        }
    }
}