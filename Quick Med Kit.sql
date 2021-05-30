create database QuickMedkit
use QuickMedkit

create table Utilisateur(
ID_Utilisateur int primary key identity(1,1),
Nom_Utiliateur varchar(50) not null,
Email_Utilisateur nvarchar(50) not null unique,
telephone_Utilisateur int constraint check_tele check (telephone_Utilisateur like '[6-7][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
Pswrd_Utilisateur nvarchar(50) not null,
IsValid bit)

select * from Utilisateur
insert into Utilisateur values('Haytam Jbilou','admin@gmail.com','0655568952','adminadmin',1);

create table Medicament(
Code_Medicament int primary key not null,
Nom varchar(50) not null,Description varchar(700) not null,
Date_Production date,Date_expiration date,
Droit_Usage varchar(700),Prix money,upload_images varchar(100))

insert into Medicament values(00122050,'Mask Covid-19','Protuction Contre Le Virus','12-02-2020','20-12-2025','',25,'~/images/masque-type1.jpg');
insert into Medicament values(00122051,'Algantil','R�serv� � l�adulte et enfant de plus de 15 ans. Appliquer 3 � 4 fois par jour, en couche mince sur la r�gion douloureuse, en massant l�g�rement. Avant la pause d�une bande de maintien ou d�un pansement, attendre que le gel soit compl�tement sec en raison de sa teneur en alcool. Le traitement est limit� � 5 jours en l absence de prescription m�dicale.','12-02-2020','20-12-2025','- Le port de gants par le masseur kin�sith�rapeute en cas d�utilisation intensive est recommand�. Bien se laver les mains apr�s chaque utilisation.',49,'~/images/algantil.jpg');
insert into Medicament values(00122052,'Doliprane','Traitement symptomatique des douleurs d intensit� l�g�re � mod�r�e et des �tats f�briles chez les enfants. Ce m�dicament contient du parac�tamol.','12-05-2020','20-12-2022','',24,'~/images/doliprane-24-sirop-enfant-100-ml.jpg');
insert into Medicament values(00122053,'Surgam','SURGAM contient de l acide tiaprof�nique. Ce m�dicament appartient � une famille appel�e les anti-inflammatoires non st�ro�diens. Ces m�dicaments sont utilis�s notamment pour diminuer l inflammation et calmer la douleur. SURGAM est destin� � l adulte et l enfant � partir de 20 kg (soit environ 6 ans).','10-02-2019','24-12-2022','',55,'~/images/surgam.jpg');
insert into Medicament values(00122054,'Alairgix','Chez l adulte et l enfant � partir de 6 ans, ALAIRGIX ALLERGIE CETIRIZINE est indiqu� : � pour le traitement des sympt�mes nasaux et oculaires de la rhinite allergique saisonni�re ou perannuelle, � pour le traitement des sympt�mes de l urticaire chronique (urticaire chronique idiopathique)','01-05-2018','05-08-2027','',40,'~/images/alairgix.jpg');
insert into Medicament values(00122055,'RHINOFEBRYL','Ce m�dicament contient du parac�tamol, un antihistaminique, la chlorph�namine et de la vitamine C. Il est indiqu� pour le traitement symptomatique de l �coulement nasal lors d un rhume avec maux de t�te et/ou fi�vre chez l adulte et l enfant de plus de 12 ans.','12-05-2020','20-12-2022','',60,'~/images/rhinofebryl.jpg');

create table Livreur(
ID_Livreur int primary key identity(1,1),
Nom_Livreur varchar(50) not null,Prenom_Livreur varchar(50),
CIN nvarchar(8) check(CIN like 'EE[0-9][0-9][0-9][0-9][0-9][0-9]') not null,
Email_Livreur nvarchar(50) not null unique,
telephone_Livreur int constraint check_teleLivreur check (telephone_Livreur like '[6-7][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
age int,
Pswrd_telephone_Livreur nvarchar(50) not null,
IsValid bit,Isenligne bit)

insert into Livreur values('Haytam','Jbilou','EE958641','haytamjbilou@gmail.com',0655568952,23,'timo123',1,1);
select * from Livreur


create table Commande(
ID_Commande int primary key identity(1,1),
Code_Medicament int not null,ID_Utilisateur int not null,
ID_Livreur int,Date_Commande date,Quantity int,Prix decimal,total decimal,
Constraint fk_1 foreign key(Code_Medicament) references Medicament(Code_Medicament),
Constraint fk_2 foreign key(ID_Utilisateur) references Utilisateur(ID_Utilisateur),
Constraint fk_3 foreign key(ID_Livreur) references Livreur(ID_Livreur))

insert into Commande values(00122051,1,5,GETDATE(),1,25.00,25.00);
select * from Commande