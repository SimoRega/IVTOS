-- *********************************************
-- * SQL MySQL generation                      
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.1              
-- * Generator date: Dec  4 2018              
-- * Generation date: Thu Jun 17 11:30:21 2021 
-- * LUN file: D:\Documenti\università\2 ANNO\BASI DI DATI\progetto\IVTOS\ProgettazioneConcettuale\IVTOS.lun 
-- * Schema: MODELLO RELAZIONALE/1 
-- ********************************************* 


-- Database Section
-- ________________ 

drop database if exists IVTOS;
create database IVTOS;
use IVTOS;

-- Tables Section
-- _____________ 

create table ACQUISTO_BIGLIETTO (
     IdArena int not null,
     IdSquadra1 int not null,
     IdSquadra2 int not null,
     DataOra date not null,
     CF_Spettatore char(16) not null,
     constraint IDBIGLIETTO primary key (CF_Spettatore, IdArena, IdSquadra1, IdSquadra2, DataOra));

create table ADESIONE_COACH_SQUADRA (
     IdSquadra int not null auto_increment,
     CF_Coach char(16) not null,
     DataInizio date not null,
     DataFine date,
     constraint IDADESIONE_COACH_SQUADRA primary key (IdSquadra, CF_Coach, DataInizio));

create table ADESIONE_PLAYER_SQUADRA (
     IdSquadra int not null,
     CF_Player char(16) not null,
     DataInizio date not null,
     DataFine date,
     constraint IDADESIONE_PLAYER_SQUADRA primary key (IdSquadra, CF_Player, DataInizio));

create table ARBITRO (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     PartiteArbitrate int not null,
     constraint IDPERSONA primary key (CF));

create table ARENA (
     IdArena int not null auto_increment,
     NomeArena varchar(255) not null,
     Capienza int not null,
     NomeStato varchar(255) not null,
     NomeCitta varchar(255) not null,
     constraint IDARENA primary key (IdArena));

create table AZIENDA_VIDEOGIOCO (
     Nome varchar(255) not null,
     PartitaIVA char(11) not null,
     constraint IDAZIENDA_VIDEOGIOCO primary key (PartitaIVA));

create table BIGLIETTO (
     IdArena int not null,
     IdSquadra1 int not null,
     IdSquadra2 int not null,
     DataOra date not null,
     Costo float(4) not null,
     constraint IDBIGLIETTO_TEMPLATE primary key (IdArena, IdSquadra1, IdSquadra2, DataOra));

create table CITTÁ (
     NomeStato varchar(255) not null,
     Nome varchar(255) not null,
     Superficie int not null,
     NumAbitanti int not null,
     constraint IDCITTÁ primary key (NomeStato, Nome));

create table COACH (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     constraint IDPERSONA primary key (CF));

create table CONTINENTE (
     Nome varchar(255) not null,
     constraint IDCONTINENTE_ID primary key (Nome));

create table GIOCA (
     NomeVideogioco varchar(255) not null,
     CF_Player char(16) not null,
     PartiteVinte int not null,
     PartiteGiocate int not null,
     constraint IDGIOCA primary key (CF_Player, NomeVideogioco));

create table Iscrizione (
     IdTorneo int not null,
     IdSquadra int not null,
     constraint IDIscrizione primary key (IdSquadra, IdTorneo));

create table PARTITA (
     IdSquadra2 int not null,
     IdSquadra1 int not null,
     DataOra date not null,
     CF_Arbitro char(16) not null,
     CF_Speaker char(16) not null,
     IdSquadraVincitrice int,
     IdTorneo int not null,
     constraint IDPARTITA primary key (IdSquadra1, IdSquadra2, DataOra));

create table PLAYER (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     Nickname varchar(255) not null,
     Nome_Stato varchar(255) not null,
     constraint IDPERSONA primary key (CF));

create table Riguarda (
     IdSquadra int not null,
     NomeVideogioco varchar(255) not null,
     constraint IDRiguarda primary key (NomeVideogioco, IdSquadra));

create table SPEAKER (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     Nickname varchar(255) not null,
     PartiteCommentate int not null,
     constraint IDPERSONA primary key (CF));

create table SPETTATORE (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     constraint IDPERSONA primary key (CF));

create table SPONSOR (
     IdSponsor int not null auto_increment,
     Nome varchar(255) not null,
     constraint IDSPONSOR primary key (IdSponsor));

create table SQUADRA (
     Nome varchar(255) not null,
     IdSquadra int not null auto_increment,
     data_creazione date not null,
     constraint IDSQUADRA primary key (IdSquadra));

create table STATO (
     Nome varchar(255) not null,
     Superficie int not null,
     NumAbitanti int not null,
     NomeContinente varchar(255) not null,
     constraint IDSTATO_ID primary key (Nome));

create table TIPOLOGIA_GIOCO (
     IdTipologia int not null auto_increment,
     Tipo varchar(255) not null,
     constraint IDTIPOLOGIA_GIOCO primary key (IdTipologia));

create table TORNEO (
     IdTorneo int not null auto_increment,
     DataInizio date not null,
     DataFine date,
     NmaxIscrizioni int not null,
     IdSponsor int,
     NomeVideogioco varchar(255) not null,
     IdArena int not null,
     IdSquadraVincitrice int,
     constraint IDTORNEO primary key (IdTorneo));

create table VIDEOGIOCO (
     Nome varchar(255) not null,
     DataCreazione date not null,
     TipologiaGioco int not null,
     PartitaIVAAzienda char(11) not null,
     constraint IDVIDEOGIOCO primary key (Nome));


-- Constraints Section
-- ___________________ 

alter table ACQUISTO_BIGLIETTO add constraint FKAcquistare
     foreign key (CF_Spettatore)
     references SPETTATORE (CF);

alter table ACQUISTO_BIGLIETTO add constraint FKCopia_di
     foreign key (IdArena, IdSquadra1, IdSquadra2, DataOra)
     references BIGLIETTO (IdArena, IdSquadra1, IdSquadra2, DataOra);

alter table ADESIONE_COACH_SQUADRA add constraint FKPartecipazioneCoach
     foreign key (CF_Coach)
     references COACH (CF);

alter table ADESIONE_COACH_SQUADRA add constraint FKAppartenenzaCoach
     foreign key (IdSquadra)
     references SQUADRA (IdSquadra);

alter table ADESIONE_PLAYER_SQUADRA add constraint FKPartecipazione
     foreign key (CF_Player)
     references PLAYER (CF);

alter table ADESIONE_PLAYER_SQUADRA add constraint FKAppartenenza
     foreign key (IdSquadra)
     references SQUADRA (IdSquadra);

alter table ARENA add constraint FKSituata
     foreign key (NomeStato, NomeCitta)
     references CITTÁ (NomeStato, Nome);

alter table BIGLIETTO add constraint FKRiservare
     foreign key (IdSquadra1, IdSquadra2, DataOra)
     references PARTITA (IdSquadra1, IdSquadra2, DataOra);

alter table BIGLIETTO add constraint FKRiferimento
     foreign key (IdArena)
     references ARENA (IdArena);

alter table CITTÁ add constraint FKContenuta
     foreign key (NomeStato)
     references STATO (Nome);

-- Not implemented
-- alter table CONTINENTE add constraint IDCONTINENTE_CHK
--     check(exists(select * from STATO
--                  where STATO.NomeContinente = Nome)); 

alter table GIOCA add constraint FKGIO_PLA
     foreign key (CF_Player)
     references PLAYER (CF);

alter table GIOCA add constraint FKGIO_VID
     foreign key (NomeVideogioco)
     references VIDEOGIOCO (Nome);

alter table Iscrizione add constraint FKIsc_SQU
     foreign key (IdSquadra)
     references SQUADRA (IdSquadra);

alter table Iscrizione add constraint FKIsc_TOR
     foreign key (IdTorneo)
     references TORNEO (IdTorneo);

alter table PARTITA add constraint FKControlla
     foreign key (CF_Arbitro)
     references ARBITRO (CF);

alter table PARTITA add constraint FKCommentare
     foreign key (CF_Speaker)
     references SPEAKER (CF);

alter table PARTITA add constraint FKSquadra1
     foreign key (IdSquadra1)
     references SQUADRA (IdSquadra);

alter table PARTITA add constraint FKSquadra2
     foreign key (IdSquadra2)
     references SQUADRA (IdSquadra);

alter table PARTITA add constraint FKVittoria_Partita
     foreign key (IdSquadraVincitrice)
     references SQUADRA (IdSquadra);

alter table PARTITA add constraint FKPrevedere
     foreign key (IdTorneo)
     references TORNEO (IdTorneo);

alter table PLAYER add constraint FKRisiede_Stato
     foreign key (Nome_Stato)
     references STATO (Nome);

alter table Riguarda add constraint FKRig_VID
     foreign key (NomeVideogioco)
     references VIDEOGIOCO (Nome);

alter table Riguarda add constraint FKRig_SQU
     foreign key (IdSquadra)
     references SQUADRA (IdSquadra);

-- Not implemented
-- alter table STATO add constraint IDSTATO_CHK
--     check(exists(select * from CITTÁ
--                  where CITTÁ.NomeStato = Nome)); 

alter table STATO add constraint FKAppartenere
     foreign key (NomeContinente)
     references CONTINENTE (Nome);

alter table TORNEO add constraint FKFinanziamento
     foreign key (IdSponsor)
     references SPONSOR (IdSponsor);

alter table TORNEO add constraint FKCompare
     foreign key (NomeVideogioco)
     references VIDEOGIOCO (Nome);

alter table TORNEO add constraint FKSvolgimento
     foreign key (IdArena)
     references ARENA (IdArena);

alter table TORNEO add constraint FKVittoria_Torneo
     foreign key (IdSquadraVincitrice)
     references SQUADRA (IdSquadra);

alter table VIDEOGIOCO add constraint FKTipo_di
     foreign key (TipologiaGioco)
     references TIPOLOGIA_GIOCO (IdTipologia);

alter table VIDEOGIOCO add constraint FKCreare
     foreign key (PartitaIVAAzienda)
     references AZIENDA_VIDEOGIOCO (PartitaIVA);


-- INSERT Section
-- _____________ 
INSERT INTO continente VALUES ('Europa');
INSERT INTO continente VALUES ('America');
INSERT INTO continente VALUES ('Asia');
INSERT INTO continente VALUES ('Africa');
INSERT INTO continente VALUES ('Oceania');

INSERT INTO Stato VALUES ('Francia',675417,68303234,'Europa');
INSERT INTO Stato VALUES ('Italia',302068,59257566,'Europa');
INSERT INTO Stato VALUES ('Spagna',504645,47431256,'Europa');
INSERT INTO Stato VALUES ('Giappone',377975,126176770,'Asia');
INSERT INTO Stato VALUES ('USA',9834000,332500290,'America');

INSERT INTO Player VALUES ('RGESMN00D01H294G','Simone','Rega','M','sus@gmail.com','2000-04-01','SexyDraksta','Italia');
INSERT INTO Player VALUES ('FBBLND00D01H294G','Linda','Fabbri','F','sus@gmail.com','2001-04-01','Adnilla','Italia');
INSERT INTO Player VALUES ('XGESMN00D01H294G','Federico','Raffoni','M','sus@gmail.com','2000-04-01','WildTurtle','Italia');
INSERT INTO Player VALUES ('MGLGLC00H06XXXXX','Gianluca','Migliarini','M','sus@gmail.com','2000-04-01','lil_Flanny','Italia');
INSERT INTO Player VALUES ('PLNMNV00H06XXXXX','Polina','Maneva','F','sus@gmail.com','2000-04-01','Polina','Spagna');

INSERT INTO tipologia_gioco VALUES (IdTipologia,'SPARATUTTO');
INSERT INTO tipologia_gioco VALUES (IdTipologia,'CORSE AUTOMOBILISTICHE');
INSERT INTO tipologia_gioco VALUES (IdTipologia,'GIOCO DI RUOLO');

INSERT INTO sponsor VALUES (IdSponsor,'Fiat');
INSERT INTO sponsor VALUES (IdSponsor,'Nike');

INSERT INTO tipologia_gioco VALUES (IdTipologia,'GIOCO DI CARTE');
INSERT INTO tipologia_gioco VALUES (IdTipologia,'AVVENTURA');
INSERT INTO tipologia_gioco VALUES (IdTipologia,'SPORT');

INSERT INTO azienda_videogioco VALUES ('Blizzard',86334519757);
INSERT INTO azienda_videogioco VALUES ('Rockstar',86399519757);
INSERT INTO azienda_videogioco VALUES ('Ubisoft',12334519757);
INSERT INTO azienda_videogioco VALUES ('EpicGames',86334519123);
INSERT INTO azienda_videogioco VALUES ('EA_Sports',11114519123);

INSERT INTO videogioco VALUES ('Overwatch','2016-01-01',1,86334519757);
INSERT INTO videogioco VALUES ('Overwatch2','2016-01-01',1,86334519757);
INSERT INTO videogioco VALUES ('HearthStone','2016-01-01',4,86334519757);
INSERT INTO videogioco VALUES ('StarCraft','2016-01-01',3,86334519757);
INSERT INTO videogioco VALUES ('Rainbow Six Siege','2015-01-01',1,12334519757);
INSERT INTO videogioco VALUES ('Fortnite','2018-01-01',1,86334519123);
INSERT INTO videogioco VALUES ('Rocket League','2018-01-01',2,86334519123);
INSERT INTO videogioco VALUES ('Gran Theft Auto V','2014-01-01',2,86399519757);
INSERT INTO videogioco VALUES ('Red Dead Redemption','2014-01-01',5,86399519757);
INSERT INTO videogioco VALUES ('FIFA 21','2020-01-01',6,11114519123);

INSERT INTO squadra values ("ceppi", IdSquadra, "2015-05-03");
INSERT INTO squadra values ("montiFan", IdSquadra, "2010-12-05");
INSERT INTO squadra values ("olimpo", IdSquadra, "2020-10-01");

INSERT INTO adesione_player_squadra VALUES(1, "RGESMN00D01H294G", "2015-02-02",null);
INSERT INTO adesione_player_squadra VALUES(1, "FBBLND00D01H294G","2015-02-02",null);
INSERT INTO adesione_player_squadra VALUES(1, "XGESMN00D01H294G","2015-02-02",null);

INSERT INTO adesione_player_squadra VALUES(2, "PLNMNV00H06XXXXX","2008-02-02",null);
INSERT INTO adesione_player_squadra VALUES(2, "MGLGLC00H06XXXXX","2013-02-02",null);

INSERT INTO riguarda VALUES (1, "Overwatch");
INSERT INTO riguarda VALUES (1, "Rocket League");
INSERT INTO riguarda VALUES (2, "HearthStone");

INSERT INTO cittá VALUES ('Francia','Parigi',675417,68303234);
INSERT INTO cittá VALUES ('Francia','Cannes',675417,68303234);
INSERT INTO cittá VALUES ('Francia','Marsiglia',675417,68303234);
INSERT INTO cittá VALUES ('Italia','Roma',302068,59257566);
INSERT INTO cittá VALUES ('Italia','Milano',302068,59257566);
INSERT INTO cittá VALUES ('Italia','Torino',302068,59257566);
INSERT INTO cittá VALUES ('Spagna','Madrid',504645,47431256);
INSERT INTO cittá VALUES ('Spagna','Barcellona',504645,47431256);
INSERT INTO cittá VALUES ('Spagna','Siviglia',504645,47431256);
INSERT INTO cittá VALUES ('Giappone','Tokyo',377975,126176770);
INSERT INTO cittá VALUES ('Giappone','Kyoto',377975,126176770);
INSERT INTO cittá VALUES ('Giappone','Osaka',377975,126176770);
INSERT INTO cittá VALUES ('USA','Washington D.C.',377975,332500290);
INSERT INTO cittá VALUES ('USA','New York',377975,332500290);
INSERT INTO cittá VALUES ('USA','Detroit',377975,332500290);

INSERT INTO arena VALUES (IdArena,"Olimpico di Roma",200000,'Italia','Roma');
INSERT INTO arena VALUES (IdArena,"Atletico di Madrid",200000,'Spagna','Madrid');
INSERT INTO arena VALUES (IdArena,"Sakamoto Stadium",200000,'Giappone','Tokyo');
INSERT INTO arena VALUES (IdArena,"Savouir Fair",200000,'Francia','Parigi');
INSERT INTO arena VALUES (IdArena,"Golden Dream Staidum",200000,'USA','New York');

