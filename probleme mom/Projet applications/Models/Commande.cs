using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Xml.Linq;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
namespace Projet_applications
{
    public class Commande
    {
        public int Id { get; set; }
        public string DateHeure { get; set; }
        public List<Pizza> Pizza { get; set; }
        public List<Annexe> Annexe { get; set; }
        public Client Client { get; set; }
        public Facture Facture { get; set; }
        public Commis Commis { get; set; }
        public Livreur Livreur { get; set; }
        public Cuisinier Cuisinier { get; set; }

        private Etat _etat;

        public Etat Etat
        {
            get { return _etat; }
            set
            {
                _etat = value;
                Database db = Database.getInstance();
                db.Open();
                if (value == Etat.Preparation)
                {
                    string query = "SELECT * FROM Employee WHERE type = 'cuisinier'";
                    SQLiteCommand commande = new SQLiteCommand(query, db.myConnection);
                    commande.CommandTimeout = 60;
                    SQLiteDataReader r = commande.ExecuteReader();
                    if (!r.HasRows) return;
                    r.Read();
                    Cuisinier = new Cuisinier()
                    {
                        Id = Int32.Parse("" + r.GetValue(0)),
                        Nom = (string) r.GetValue(1),
                        Prenom = (string) r.GetValue(2)
                    };
                    Cuisinier.CurrentCommande = this;
                    string updateCommande = "UPDATE Commande SET cuisinierId = " + Cuisinier.Id +
                                            " , etat = 'Preparation' WHERE id="+Id;
                    int coutnRow = new SQLiteCommand(updateCommande, db.myConnection).ExecuteNonQuery();
                    Cuisinier.PreparerCommande();
                }

                if (value == Etat.Pret)
                {
                    string query = "SELECT * FROM Employee WHERE type = 'livreur'";
                    SQLiteDataReader r = new SQLiteCommand(query, db.myConnection).ExecuteReader();
                    if (!r.HasRows) return;

                    r.Read();
                    Livreur = new Livreur()
                    {
                        Id = Int32.Parse("" + r.GetValue(0)),
                        Nom = (string) r.GetValue(1),
                        Prenom = (string) r.GetValue(2)
                    };
                    
                    Livreur.CurrentCommande = this;
                    string updateCommande =
                        "UPDATE Commande SET livreurId = " + Livreur.Id + " , etat = 'Pret' WHERE id="+Id;
                    int coutnRow = new SQLiteCommand(updateCommande, db.myConnection).ExecuteNonQuery();
                    Console.WriteLine("yesy");
                    Livreur.PrendreCommande();
                }

                if (value == Etat.Livraison)
                {
                    string query = "SELECT * FROM Employee WHERE type = 'livreur'";
                    SQLiteDataReader r = new SQLiteCommand(query, db.myConnection).ExecuteReader();
                    if (!r.HasRows) return;

                    r.Read();
                    Livreur = new Livreur()
                    {
                        Id = Int32.Parse("" + r.GetValue(0)),
                        Nom = (string) r.GetValue(1),
                        Prenom = (string) r.GetValue(2)
                    };
                    Livreur.CurrentCommande = this;
                    string updateCommande =
                        "UPDATE Commande SET livreurId = " + Livreur.Id + " , etat = 'Livraison' WHERE id="+Id;
                    int coutnRow = new SQLiteCommand(updateCommande, db.myConnection).ExecuteNonQuery();
                    Livreur.EffectuerLivraison();
                }

                if (value == Etat.Ferme)
                {
                    string updateCommande = "UPDATE Commande SET etat = 'Ferme' WHERE id="+Id;
                    int coutnRow = new SQLiteCommand(updateCommande, db.myConnection).ExecuteNonQuery();
                    CustomConsole.PrintInfo("Commande n°"+Id + " fermée");
                }
            }
        }
        public void GenerateFacture()
        {
            // TODO implement here
        }

        public Commande()
        {
            DateTime now = DateTime.Now;
            DateHeure = "" + now.Hour + "h" + now.Hour + " " + now.Day + "/" + now.Month + "/" + now.Year;
            Pizza = new List<Pizza>();
            Annexe = new List<Annexe>();
            Facture = new Facture();
        }

        public override string ToString()
        {
            string res = "";
            if (Id != 0) res += "Commande n° : " + Id + "\r\n";
            if (DateHeure != "") res += "           Date et heure : " + DateHeure + "\r\n";
            if (!(Client is null)) res += "           " + Client + "\r\n";
            if (!(Commis is null)) res += "           Commis : " + Commis + "\r\n";
            if (!(Livreur is null)) res += "           Livreur : " + Livreur + "\r\n";
            if (!(Cuisinier is null)) res += "           Cuisinier : " + Cuisinier + "\r\n";
            if (!(Facture is null)) res += "           " + Facture + "\r\n";
            res += "           Pizzas : \r\n";
            foreach (var pizza in Pizza)
            {
                res += "                    - " + pizza + "\r\n";
            }

            res += "           Annexes : \r\n";
            foreach (var annexe in Annexe)
            {
                res += "                    - " + annexe + "\r\n";
            }

            return res;
        }


    }
}