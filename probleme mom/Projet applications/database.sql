/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
DROP TABLE IF EXISTS AnnexeCommande;
DROP TABLE IF EXISTS PizzaCommande;
DROP TABLE IF EXISTS Pizza;
DROP TABLE IF EXISTS Annexe;
DROP TABLE IF EXISTS Commande;
DROP TABLE IF EXISTS Facture;
DROP TABLE IF EXISTS Employee;
DROP TABLE IF EXISTS Client;


CREATE TABLE IF NOT EXISTS Client
(
    id        integer PRIMARY KEY AUTOINCREMENT,
    nom       TEXT,
    prenom    TEXT,
    ville     TEXT,
    telephone integer,
    dateFirst TEXT,
    numeroRue TEXT,
    rue       TEXT

);

CREATE TABLE IF NOT EXISTS Employee
(
    id     integer PRIMARY KEY AUTOINCREMENT,
    nom    TEXT,
    prenom TEXT,
    type   TEXT

);

CREATE TABLE IF NOT EXISTS Facture
(
    id   integer PRIMARY KEY AUTOINCREMENT,
    prix NUMERIC

);

CREATE TABLE IF NOT EXISTS Annexe
(
    id     integer PRIMARY KEY AUTOINCREMENT,
    nom    TEXT,
    volume NUMERIC,
    prix   NUMERIC

);
CREATE TABLE IF NOT EXISTS Pizza
(
    id     integer PRIMARY KEY AUTOINCREMENT,
    nom    TEXT,
    taille TEXT,
    prix   NUMERIC,
    type   text
);
CREATE TABLE IF NOT EXISTS Commande
(
    id          integer PRIMARY KEY AUTOINCREMENT,
    heure       TEXT,
    date        TEXT,
    etat        TEXT,
    clientId    integer,
    factureId   integer DEFAULT NULL,
    commisId    integer,
    livreurId   integer,
    cuisinierId integer,
    CONSTRAINT clientFk FOREIGN KEY (clientId) REFERENCES Client (id),
    CONSTRAINT factureFk FOREIGN KEY (factureId) REFERENCES Facture (id),
    CONSTRAINT commisFk FOREIGN KEY (commisId) REFERENCES Employee (id),
    CONSTRAINT livreurFk FOREIGN KEY (livreurId) REFERENCES Employee (id),
    CONSTRAINT cuisinierFk FOREIGN KEY (cuisinierId) REFERENCES Employee (id)

);
CREATE TABLE IF NOT EXISTS AnnexeCommande
(
    id         integer PRIMARY KEY AUTOINCREMENT,
    annexeId   int,
    commandeId int,
    CONSTRAINT annexeFk FOREIGN KEY (annexeId) REFERENCES Annexe (id),
    CONSTRAINT commandeFk FOREIGN KEY (commandeId) REFERENCES Commande (id)


);
CREATE TABLE IF NOT EXISTS PizzaCommande
(
    id         integer PRIMARY KEY AUTOINCREMENT,
    pizzaId    int,
    commandeId int,
    CONSTRAINT pizzaFk FOREIGN KEY (pizzaId) REFERENCES Pizza (id),
    CONSTRAINT commandeFk FOREIGN KEY (commandeId) REFERENCES Commande (id)

);
