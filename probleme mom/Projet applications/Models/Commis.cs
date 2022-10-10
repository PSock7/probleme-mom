using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.SymbolStore;
using Projet_applications;
/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
namespace Projet_applications
{
    public class Commis : Employee
    {
        public String MsgClient()
        {
            // TODO implement here
            return null;
        }


        public String MsgCuisine()
        {
            // TODO implement here
            return null;
        }


        public String MsgLivreur()
        {
            // TODO implement here
            return null;
        }


        public String MsgCommis()
        {
            // TODO implement here
            return null;
        }


        /* public null recupererEtat() {
             // TODO implement here
             return null;
         }*/
        public void DemanderFacture()
        {
            // TODO implement here
        }

        public void Encaisser()
        {
            // TODO implement here
        }

        public void FermerCommande()
        {
            CurrentCommande.Etat = Etat.Ferme;
        }


        public int GererCumul()
        {
            // TODO implement here
            return 0;
        }


        public Facture GenererFacture()
        {
            // TODO implement here
            return null;
        }


        public void CreerCommande(Database database)
        {
            Console.WriteLine("Entrez le numero de telephone du client : ");
            string tel = Console.ReadLine();
            String query = "SELECT * FROM client where telephone=" + Int32.Parse(tel);
            SQLiteCommand idCommand = new SQLiteCommand(query, database.myConnection);
            SQLiteDataReader reader = idCommand.ExecuteReader();
            CurrentCommande = new Commande();
            if (!reader.HasRows)
            {
                Console.WriteLine("Client inexistant");
                CurrentCommande.Client = AjouterClient(database);
            }
            else
            {
                reader.Read();
                CurrentCommande.Client = new Client()
                {
                    Id = Int32.Parse("" + reader.GetValue(0)),
                    Nom = (string) reader.GetValue(1),
                    Prenom = (string) reader.GetValue(2),
                    Telephone = Int32.Parse("" + reader.GetValue(4)),
                    DateFirst = (string) reader.GetValue(5),
                    Adresse = new Adresse()
                    {
                        Ville = (string) reader.GetValue(3),
                        NumRue = Int32.Parse("" + reader.GetValue(6)),
                        Rue = (string) reader.GetValue(7),
                    },
                };
            }

            string insertFacture = "INSERT INTO Facture (prix) VALUES (0)";
            SQLiteCommand commandFacture = new SQLiteCommand(insertFacture, database.myConnection);
            int countRowFacture = commandFacture.ExecuteNonQuery();
            if (countRowFacture > 0)
                CustomConsole.PrintSuccess("Facture n° " + database.myConnection.LastInsertRowId + " ajoutée ");
            else CustomConsole.PrintError("Facture non ajoutée ");

            string[] dateHeure = CurrentCommande.DateHeure.Split(" ");
            string insertCommand =
                "INSERT INTO Commande (heure, date, factureId, clientId, commisId, livreurId, cuisinierId) VALUES ('" +
                dateHeure[0] + "','" + dateHeure[1] + "', " + database.myConnection.LastInsertRowId + ", " +
                CurrentCommande.Client.Id + ", " + Id + ", null, null)";
            SQLiteCommand commandPizza = new SQLiteCommand(insertCommand, database.myConnection);
            int countRowCommande = commandPizza.ExecuteNonQuery();
            CurrentCommande.Id = Int32.Parse("" + database.myConnection.LastInsertRowId);
            if (countRowCommande > 0)
                CustomConsole.PrintSuccess("Commande n° " + database.myConnection.LastInsertRowId + " créée ! ");
            else CustomConsole.PrintError("Commande non ajoutée ");

            bool commandeValide = false;
            while (!commandeValide)
            {
                BeginSet:
                Console.WriteLine("Entrez : ");
                CustomConsole.PrintChoice(1, "Ajouter des pizzas");
                CustomConsole.PrintChoice(2, "Ajouter des Annexes");
                CustomConsole.PrintChoice(3, "Valider la commande en cours");
                CustomConsole.PrintChoice(9, "Pour quitter");

                string stringChoice = Console.ReadLine();
                int choice;
                if (!Int32.TryParse(stringChoice, out choice))
                {
                    CustomConsole.PrintError("Veuillez entrer un numero valide");
                    goto BeginSet;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            AjouterPizzaCommande(database);
                            goto BeginSet;
                        case 2:
                            AjouterAnnexe(database);
                            goto BeginSet;
                        case 3:
                            commandeValide = true;
                            break;
                        case 9:
                            break;
                        default:
                            goto BeginSet;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    goto BeginSet;
                }
            }

            CurrentCommande.Etat = Etat.Preparation;
        }


        public Client AjouterClient(Database databaseObject)
        {
            String nom;
            String prenom;
            String ville;
            double tel;
            String date;
            int numRue;
            String rue;

            Console.Write("Saisissez un nom : ");
            nom = Console.ReadLine();

            Console.Write("Saisissez un prenom : ");
            prenom = Console.ReadLine();

            Console.Write("Saisissez une ville : ");
            ville = Console.ReadLine();

            Console.Write("Saisissez un 06 : ");
            tel = double.Parse(Console.ReadLine());


            Console.Write("Saisissez un numero de rue : ");
            numRue = int.Parse(Console.ReadLine());

            Console.Write("Saisissez une rue : ");
            rue = Console.ReadLine();

            DateTime now = DateTime.Now;
            date = "" + now.Hour + "h" + now.Hour + " " + now.Day + "/" + now.Month + "/" + now.Year;

            String query =
                "insert into Client ('nom','prenom','ville','telephone','dateFirst','numeroRue','rue') values (@nom,@prenom,@ville,@telephone,@dateFirst,@numeroRue,@rue)";

            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);

            myCommand.Parameters.AddWithValue("@nom", nom);
            myCommand.Parameters.AddWithValue("@prenom", prenom);
            myCommand.Parameters.AddWithValue("@ville", ville);
            myCommand.Parameters.AddWithValue("@telephone", tel);
            myCommand.Parameters.AddWithValue("@dateFirst", date);
            myCommand.Parameters.AddWithValue("@numeroRue", numRue);
            myCommand.Parameters.AddWithValue("@rue", rue);
            var result = myCommand.ExecuteNonQuery();
            int idClient = (int) databaseObject.myConnection.LastInsertRowId;
            if (result > 0) CustomConsole.PrintSuccess("Client n° " + idClient + " ajouté");
            else CustomConsole.PrintError("Client non ajouté");
            return new Client()
            {
                Nom = nom, Prenom = prenom, Telephone = (int) tel, DateFirst = date,
                Adresse = new Adresse() {NumRue = numRue, Rue = rue, Ville = ville},
                Id = idClient
            };
        }

        public void AjouterPizzaCommande(Database databaseObject)
        {
            int id = 0;
            float prix = 0;
            int choixNom;
            int choixTaille;
            string nom = "";
            string taille = "";


            Console.WriteLine("Quelle type de pizza desirez-vous ?\r");
            CustomConsole.PrintChoice(1, "Quatre fromages");
            CustomConsole.PrintChoice(2, "Barbecue");
            CustomConsole.PrintChoice(3, "Veggie");
            choixNom = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Quelle taille souhaitez-vous pour votre pizza ?\r");
            CustomConsole.PrintChoice(1, "Petite");
            CustomConsole.PrintChoice(2, "Moyenne");
            CustomConsole.PrintChoice(3, "Grande");
            choixTaille = Convert.ToInt32(Console.ReadLine());


            switch (choixNom)
            {
                case 1:
                    switch (choixTaille)
                    {
                        case 1:
                            nom = "QuatreFromages";
                            taille = "Petite";
                            break;
                        case 2:
                            nom = "QuatreFromages";
                            taille = "Moyenne";
                            break;
                        case 3:
                            nom = "QuatreFromages";
                            taille = "Grande";
                            break;
                    }

                    break;
                case 2:
                    switch (choixTaille)
                    {
                        case 1:
                            nom = "Barbecue";
                            taille = "Petite";
                            break;
                        case 2:
                            nom = "Barbecue";
                            taille = "Moyenne";
                            break;
                        case 3:
                            nom = "Barbecue";
                            taille = "Grande";
                            break;
                    }

                    break;
                case 3:
                    switch (choixTaille)
                    {
                        case 1:
                            nom = "Veggie";
                            taille = "Petite";
                            break;
                        case 2:
                            nom = "Veggie";
                            taille = "Moyenne";
                            break;
                        case 3:
                            nom = "Veggie";
                            taille = "Grande";
                            break;
                    }

                    break;
            }

            string queryId = "select id , prix from Pizza where nom LIKE '" + nom + "' and taille LIKE'" + taille + "'";
            SQLiteCommand idCommand = new SQLiteCommand(queryId, databaseObject.myConnection);
            SQLiteDataReader readerId = idCommand.ExecuteReader();
            while (readerId.Read())
            {
                id = readerId.GetInt32(0);
                prix = readerId.GetInt32(1);
            }

            Type nomPizza = (Type) Enum.Parse(typeof(Type), nom);
            Taille taillePizza = (Taille) Enum.Parse(typeof(Taille), taille);

            Pizza pizza = new Pizza() {Id = id, taille = taillePizza, type = nomPizza};

            CurrentCommande.Pizza.Add(pizza);

            string insertPizza = "Insert into PizzaCommande (pizzaId,commandeId) VALUES (" + id + " , " +
                                 CurrentCommande.Id + ")";
            string insertFacture = "UPDATE facture SET prix = prix + " + prix;
            SQLiteCommand commandPizza = new SQLiteCommand(insertPizza, databaseObject.myConnection);
            SQLiteCommand commandFacture = new SQLiteCommand(insertFacture, databaseObject.myConnection);
            int count = commandPizza.ExecuteNonQuery();
            commandFacture.ExecuteNonQuery();
            CurrentCommande.Facture.Prix += prix;
            if (count > 0) CustomConsole.PrintSuccess("Pizza ajoutée ! ");
            else CustomConsole.PrintError("erreur ");
        }

        public void GetClients(Database databaseObject)
        {
            String query = "select * from Client ORDER BY nom, prenom";

            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);


            SQLiteDataReader requete = myCommand.ExecuteReader();
            if (!requete.HasRows) Console.WriteLine("Aucun client");
            while (requete.Read())
            {
                Client c = new Client()
                {
                    Id = Int32.Parse("" + requete.GetValue(0)),
                    Nom = (string) requete.GetValue(1),
                    Prenom = (string) requete.GetValue(2),
                    Telephone = Int32.Parse("" + requete.GetValue(4)),
                    DateFirst = (string) requete.GetValue(5),
                    Adresse = new Adresse()
                    {
                        Ville = (string) requete.GetValue(3),
                        NumRue = Int32.Parse("" + requete.GetValue(6)),
                        Rue = (string) requete.GetValue(7),
                    },
                };
                Console.WriteLine(c);
            }
        }

        public void GetClientsByCity(Database databaseObject)
        {
            Console.WriteLine("Veuillez entrer une ville : ");
            string ville = Console.ReadLine();
            String query = "select * from Client where UPPER(ville) LIKE UPPER('" + ville + "')";
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            SQLiteDataReader requete = myCommand.ExecuteReader();
            if (!requete.HasRows) CustomConsole.PrintError("Aucun client à " + ville);
            while (requete.Read())
            {
                Client c = new Client()
                {
                    Id = Int32.Parse("" + requete.GetValue(0)),
                    Nom = (string) requete.GetValue(1),
                    Prenom = (string) requete.GetValue(2),
                    Telephone = Int32.Parse("" + requete.GetValue(4)),
                    DateFirst = (string) requete.GetValue(5),
                    Adresse = new Adresse()
                    {
                        Ville = (string) requete.GetValue(3),
                        NumRue = Int32.Parse("" + requete.GetValue(6)),
                        Rue = (string) requete.GetValue(7),
                    },
                };
                Console.WriteLine(c);
            }
        }

        public Adresse trouverAdresse(Database databaseObject)
        {
            Adresse adresse = new Adresse();
            double tel = 0;
            Console.WriteLine("Saisissez le numero de telephone a partir duquel retrouver l'adresse");
            tel = double.Parse(Console.ReadLine());

            String rue = null;
            String query = "select rue, numeroRue, ville from Client where telephone =  " + tel;
            SQLiteCommand myCommand = new SQLiteCommand(query, databaseObject.myConnection);
            SQLiteDataReader rdr = myCommand.ExecuteReader();
            bool rez = rdr.Read();
            Console.WriteLine(rez);
            adresse.Rue = Convert.ToString(rdr["rue"]);
            adresse.NumRue = Convert.ToInt32(rdr["numeroRue"]);
            adresse.Ville = Convert.ToString(rdr["ville"]);
            return adresse;
        }

        public override string ToString()
        {
            return Id + " : " + Nom + " " + Prenom;
        }

        public void GetAmounts(Database database)
        {
            String query =
                "select  c.id, c.nom , prenom , ville, telephone , dateFirst , numeroRue , rue , SUM(prix) from Client c , Commande co  , Facture f WHERE c.id=co.clientId AND f.id=co.factureId GROUP BY c.id";
            SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
            SQLiteDataReader requete = myCommand.ExecuteReader();
            if (!requete.HasRows) CustomConsole.PrintError("Aucun client");
            while (requete.Read())
            {
                Client c = new Client()
                {
                    Id = Int32.Parse("" + requete.GetValue(0)),
                    Nom = (string) requete.GetValue(1),
                    Prenom = (string) requete.GetValue(2),
                    Telephone = Int32.Parse("" + requete.GetValue(4)),
                    DateFirst = (string) requete.GetValue(5),
                    Adresse = new Adresse()
                    {
                        Ville = (string) requete.GetValue(3),
                        NumRue = Int32.Parse("" + requete.GetValue(6)),
                        Rue = (string) requete.GetValue(7),
                    },
                };
                Console.WriteLine(c + "\r\n           Montant des achats cumulés : " + requete.GetValue(8));
            }
        }

        public void AfficherCommande(Database database)
        {
            Begin:
            Console.WriteLine("Veuillez entrer le numero d'une commande : ");
            string stringIdCommande = Console.ReadLine();
            int idCommand;
            if (!Int32.TryParse(stringIdCommande, out idCommand))
            {
                CustomConsole.PrintError("Veuillez entrer un numero valide ");
                goto Begin;
            }

            String query =
                "SELECT c.id , heure , 'date', cl.nom , cl.prenom , cl.id ,  f.id , f.prix FROM Commande c INNER JOIN Client cl on cl.id = c.clientId INNER JOIN Facture F on F.id = c.factureId WHERE c.id=" + idCommand;
            Console.WriteLine(query);
            SQLiteCommand myCommand = new SQLiteCommand(query, database.myConnection);
            SQLiteDataReader requete = myCommand.ExecuteReader();
            if (!requete.HasRows) CustomConsole.PrintError("Aucune commande correspondant à  ce numero " + idCommand);
            requete.Read();
            Commande c = new Commande()
            {
                Id = Int32.Parse("" + requete.GetValue(0)),
                DateHeure = (string) requete.GetValue(1) + " " + (string) requete.GetValue(2),
                Client = new Client()
                {
                    Id = Int32.Parse("" + requete.GetValue(5)),
                    Nom = (string) requete.GetValue(3),
                    Prenom = (string) requete.GetValue(4),
                },
                Facture = new Facture()
                    {Id = Int32.Parse("" + requete.GetValue(6)), Prix = Double.Parse("" + requete.GetValue(7))}
            };

            String getA = "SELECT a.nom FROM AnnexeCommande c , Annexe a  WHERE c.annexeId=a.id AND c.commandeId=" +
                          c.Id;
            SQLiteDataReader requeteA = new SQLiteCommand(getA, database.myConnection).ExecuteReader();
            while (requeteA.Read())
            {
                c.Annexe.Add(new Annexe() {Nom = ""+requeteA.GetValue(0)});
            }

            String getP = "SELECT  a.nom FROM PizzaCommande c , Pizza a  WHERE c.pizzaId=a.id AND c.commandeId=" + c.Id;
            SQLiteDataReader requeteP = new SQLiteCommand(getP, database.myConnection).ExecuteReader();
            while (requeteP.Read())
            {
                c.Pizza.Add(new Pizza() {Nom = (string) requeteP.GetValue(0)});
            }

            Console.WriteLine(c);
        }

        public void AjouterAnnexe(Database database)
        {
            Console.WriteLine("Quelle boisson voulez vous ?\r");
            CustomConsole.PrintChoice(1, "coca");
            CustomConsole.PrintChoice(2, "fanta");
            CustomConsole.PrintChoice(3, "orangina");
            int choice = Convert.ToInt32(Console.ReadLine());
            string nom = "";
            switch (choice)
            {
                case 1:
                    nom = "coca";
                    break;
                case 2:
                    nom = "fanta";
                    break;
                case 3:
                    nom = "orangina";
                    break;
            }

            string queryId = "select * from Annexe where nom LIKE '" + nom + "'";
            SQLiteCommand idCommand = new SQLiteCommand(queryId, database.myConnection);
            SQLiteDataReader readerId = idCommand.ExecuteReader();
            readerId.Read();
            int id = Int32.Parse("" + readerId.GetValue(0));
            CurrentCommande.Annexe.Add(new Annexe()
            {
                Nom = nom,
                Id = id,
            });


            string insertAssoc = "Insert into AnnexeCommande (commandeId,annexeId) VALUES (" + CurrentCommande.Id +
                                 " , " +
                                 id + ")";
            int count = new SQLiteCommand(insertAssoc, database.myConnection).ExecuteNonQuery();
            if (count > 0) CustomConsole.PrintSuccess("Annexe ajoutée ! ");
            else CustomConsole.PrintError("erreur ");
        }
    }
}
