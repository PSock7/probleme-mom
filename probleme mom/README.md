#author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock
# Probleme Mom 
Une pizzeria desire informatiser son système de commande – production – livraison afin d’ameliorer son 
rendement. Voici comment elle aimerait que le processus se deroule une fois informatiser. 
Lorsqu’un client telephone au restaurant, le commis qui prend l’appel commence toujours par demander au 
client s’il en est a sa première commande dans cette entreprise. Si le client a deja commande, le commis lui 
demande son numero de telephone afin de rechercher les coordonnees du client et de confirmer son 
adresse. Lorsque le client en est a sa première commande, le commis doit alors saisir les coordonnees 
complètes du client (nom, prenom, adresse, numero de telephone, et la date de sa première commande). Il 
saisit ensuite la commande. 
Une commande comporte obligatoirement une (ou plusieurs) pizza(s), et eventuellement des produits 
annexes de type boissons (coca, jus d’orange etc.) selon un volume a preciser. Les pizzas sont disponibles 
en trois tailles differentes (petite, moyenne, grande) et en plusieurs types (par exemple sauce 
tomate/fromage, vegetariennes, toutes garnies, etc.). Le prix d’une pizza depend de sa taille et de son type. 
La commande comporte les informations suivantes : no de commande, heure, date, nom du client, nom du 
commis, items commandes. La ou les pizza(s) commandee(s) est (sont) ensuite preparee(s) par les 
cuisiniers pour ensuite etre transmise(s) a la livraison. 
Une fois la commande est passee, un message de confirmation est envoye a :
– client. Message de prise en charge de sa commande ;
– cuisine. Message de confirmant le choix de pizza ;
– livreur. Message de command avec les cordonnees du client avec un delais de 5 min 
– commis. Message de confirmation de l'ouverture de commande.
Le livreur prend alors la ou les pizza(s) et la facture afferente, y ajoute les produits annexes et part effectuer 
la livraison. Lorsque l’adresse indiquee est correcte et trouvee, il remet la commande au client et recoit le 
paiement et il envoie un message de confirmation a la pizzeria. Il retourne alors a la pizzeria avec le double 
de la facture et l’argent. 
A son retour, le commis demande la facture au livreur, saisit le numero de commande, le numero du livreur, 
encaisse l’argent et ferme la commande en envoyant deux messages a 
– livreur. Message de fermeture de la commande et le montant de son payement.
– Commis. Message de confirmation de la fermeture de la commande. 
A tout moment, le commis peut connaitre l’etat d’une commande (en preparation, en livraison ou fermee). 
Les commandes qui n’ont pas pu etre livrees sont marquees comme des pertes par le commis et sont 
fermees. Chaque commande honoree permet d’augmenter le cumul des commandes realisees par le client. 
1. Veuillez donner le schema UML correspondant a votre analyse conceptuelle de ce cas. 
2. En proposer une application C# qui simule le fonctionnement de la pizzeria avec la communication. 
Livrable 
Le projet se fait en equipe seul ou a 3 (equipe TPs). Elle doit rendre compte du cahier des charges cidessus. Tout information non ecrite peut etre interpretee comme vous le souhaitez. 
Vous joindrez un rapport (maximum 3 pages) a votre solution C# dans lequel vous noterez vos ajouts si 
besoin. Vous y expliquerez le diagramme UML de votre solution et les structures C# qui en decoulent. 
La solution C# se presente sous forme d’une interface graphique (WPF). Vous etes libres de representer les 
informations comme vous le souhaitez. 
#Module Client / Effectif 
L’outil doit pouvoir permettre d’entrer, supprimer ou modifier un nouveau Client et un nouveau Commis et 
Livreur d’une part ou bien de lire des fichiers Clients, Commis ou Livreurs 
Il faut a tout moment pouvoir afficher l’ensemble des Clients et/ou des effectifs selon plusieurs critères : 
(successivement ou simultanement) Clients : 
• Par ordre alphabetique 
• Par ville 
• Montant des achats cumule, ce qui permettra de connaitre les meilleurs clients 
#Module Commandes 
Pour faciliter la simulation, il vous est demande de pouvoir charger des fichiers de commandes. 
Il faut, par ailleurs, pouvoir creer une nouvelle commande ou la modifier et simuler ses differentes etapes : 
• Commande en preparation, en livraison ou fermee. 
• Commande encaissee ou a perte. Il faut pouvoir : 
• Calculer le prix d’une commande et l’afficher moyennant son numero 
• Afficher une commande moyennant son numero 
#Module Statistiques 
Il faut egalement faire des bilans generaux 
• Afficher par commis, le nombre de commandes gerees 
• Afficher par livreur le nombre de livraisons effectuees 
• Afficher les commandes selon une periode de temps 
• Afficher la moyenne des prix des commandes 
• Afficher la moyenne des comptes clients 
#Module Communication 
Il doit etre representer par 4 parties :
• client,
• cuisine,
• livreur,
• commis.
Module Autre qui sera le resultat de votre creativite sur la gestion de cette pizzeria 
Dans tous les cas, il faut utiliser tous les concepts :
• POO avec heritage bien-sur, 
• Classe abstraite, 
• Interface (utilisation d’interface C# et creation d’interface), 
• Delegation (utilisation de delegation C#) 
• 1 collection generique differente
• utilisation de Message Broker OU async/await au choix