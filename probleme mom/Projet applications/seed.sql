/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
DELETE
FROM Client;
DELETE
FROM Employee;
DELETE
FROM Annexe;
DELETE
FROM Pizza;
DELETE
FROM Commande;
DELETE
FROM AnnexeCommande;
DELETE
FROM PizzaCommande;
DELETE
FROM Facture;

UPDATE `sqlite_sequence`
SET `seq` = 0;

INSERT INTO Client (nom, prenom, ville, telephone, dateFirst, numeroRue, rue)
VALUES ("Griigs", "Rober", "Paris", 0647895210, "08/10/2022", "11", "Patheon"),
       ("Tran", "Othmane", "Nice", 0615402136, "05/10/2022", "60", "Independance");
       

INSERT INTO Employee (nom, prenom, type)
VALUES ("Sock", "Pape", "commis"),
       ("Tran", "Victor", "livreur"),
       ("Oubouselham", "Othmane", "cuisinier"),
       ("Cokoua", "Caleb", "livreur");

INSERT INTO Annexe (nom, prix, volume)
VALUES ("Coca cola", 1.5, 1000),
       ("Pepsi", 0.9, 800),
       ("Schweppes", 1.8, 1500);

INSERT INTO Pizza (nom, prix, taille, type)
VALUES ("QuatreFromages", 10, "Petite", "QuatreFromages"),
       ("Barbecue", 9, "Petite", "Barbecue"),
       ("Veggie", 8, "Petite", "Veggie"),
       ("QuatreFromages", 12, "Moyenne", "QuatreFromages"),
       ("Barbecue", 11, "Moyenne", "Barbecue"),
       ("Veggie", 10, "Moyenne", "Veggie"),
       ("QuatreFromages", 15, "Grande", "QuatreFromages"),
       ("Barbecue", 14, "Grande", "Barbecue"),
       ("Veggie", 13, "Grande", "Veggie");

INSERT INTO Facture (prix)
VALUES (13.1),
       (10.6),
       (8);


INSERT INTO Commande (heure, date, factureId, clientId, commisId, livreurId, cuisinierId,etat)
VALUES ("11h00", "01/02/2020", 1, 1, 1, 2, 3,"FERME"),
       ("12h00", "07/12/2020", 2, 2, 1, 2, 3,"FERME"),
       ("13h00", "09/09/2021", 3, 1, 1, 2, 3,"FERME");


INSERT INTO AnnexeCommande (annexeId, commandeId)
VALUES (1, 1),
       (2, 2),
       (2, 1);

INSERT INTO PizzaCommande (pizzaId, commandeId)
VALUES (1, 1),
       (2, 2),
       (3, 3);