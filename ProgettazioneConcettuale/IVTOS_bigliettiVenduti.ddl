-- *********************************************
-- * SQL MySQL generation                      
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.1              
-- * Generator date: Dec  4 2018              
-- * Generation date: Sun Jun 20 18:45:55 2021 
-- * LUN file: D:\Documenti\università\2 ANNO\BASI DI DATI\progetto\IVTOS\ProgettazioneConcettuale\IVTOS.lun 
-- * Schema: MODELLO RELAZIONALE/1 
-- ********************************************* 


-- Database Section
-- ________________ 

create database MODELLO RELAZIONALE;
use MODELLO RELAZIONALE;


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
     BigliettiRimanenti int not null,
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


-- Index Section
-- _____________ 

