-- *********************************************
-- * Standard SQL generation                   
-- *--------------------------------------------
-- * DB-MAIN version: 11.0.1              
-- * Generator date: Dec  4 2018              
-- * Generation date: Fri May 21 12:28:42 2021 
-- * LUN file: C:\Users\Simon\Desktop\ProgettoDatabase\IVTOS\ProgettazioneConcettuale\IVTOS.lun 
-- * Schema: sql/SQL 
-- ********************************************* 


-- Database Section
-- ________________ 

create database sql;


-- DBSpace Section
-- _______________


-- Tables Section
-- _____________ 

create table ADESIONE_COACH_SQUADRA (
     IdSquadra numeric(6) not null,
     CF char(16) not null,
     DataInizio date not null,
     DataFine date,
     constraint ID_ADESIONE_COACH_SQUADRA_ID primary key (IdSquadra, CF, DataInizio));

create table ADESIONE_PLAYER_SQUADRA (
     IdSquadra numeric(6) not null,
     CF char(16) not null,
     DataInizio date not null,
     DataFine date,
     constraint ID_ADESIONE_PLAYER_SQUADRA_ID primary key (IdSquadra, CF, DataInizio));

create table ARBITRO (
     PartiteArbitrate numeric(4) not null,
     genere char(1) not null,
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     constraint ID_ARBITRO_ID primary key (CF));

create table ARENA (
     IdArena numeric(6) not null,
     NomeArena varchar(255) not null,
     Capienza numeric(6) not null,
     NomeStato varchar(255) not null,
     NomeCitta varchar(255) not null,
     constraint ID_ARENA_ID primary key (IdArena));

create table AZIENDA_VIDEOGIOCO (
     Nome varchar(255) not null,
     PartitaIVA char(11) not null,
     constraint ID_AZIENDA_VIDEOGIOCO_ID primary key (PartitaIVA));

create table BIGLIETTO (
     CF char(16) not null,
     IdSquadra1 numeric(6) not null,
     IdSquadra2 numeric(6) not null,
     DataOra date not null,
     IdBiglietto numeric(2) not null,
     constraint ID_BIGLIETTO_ID primary key (CF, IdSquadra1, IdSquadra2, DataOra, IdBiglietto));

create table BIGLIETTO_TEMPLATE (
     IdSquadra1 numeric(6) not null,
     IdSquadra2 numeric(6) not null,
     DataOra date not null,
     IdBiglietto numeric(2) not null,
     Costo float(4) not null,
     constraint ID_BIGLIETTO_TEMPLATE_ID primary key (IdSquadra1, IdSquadra2, DataOra, IdBiglietto));

create table CITTA (
     NomeStato varchar(255) not null,
     Nome varchar(255) not null,
     Superficie numeric(6) not null,
     NumAbitanti numeric(6) not null,
     constraint ID_CITTA_ID primary key (NomeStato, Nome));

create table COACH (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     constraint ID_COACH_ID primary key (CF));

create table CONTINENTE (
     Nome varchar(255) not null,
     constraint ID_CONTINENTE_ID primary key (Nome));

create table GIOCA (
     CF char(16) not null,
     Nome varchar(255) not null,
     PartiteVinte numeric(6) not null,
     PartiteGiocate numeric(6) not null,
     constraint ID_GIOCA_ID primary key (Nome, CF));

create table Iscrizione (
     IdSquadra numeric(6) not null,
     IdTorneo numeric(6) not null,
     constraint ID_Iscrizione_ID primary key (IdTorneo, IdSquadra));

create table PARTITA (
     IdSquadra1 numeric(6) not null,
     IdSquadra2 numeric(6) not null,
     DataOra date not null,
     IdTorneo numeric(6) not null,
     IdArbitro char(16) not null,
     IdSpeaker char(16) not null,
     constraint ID_PARTITA_ID primary key (IdSquadra1, IdSquadra2, DataOra));

create table PLAYER (
     CF char(16) not null,
     Nickname varchar(255) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere varchar(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     residenzaStato varchar(255) not null,
     constraint ID_PLAYER_ID primary key (CF));

create table SPEAKER (
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     genere char(1) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     Nickname varchar(255) not null,
     PartiteCommentate numeric(4) not null,
     constraint ID_SPEAKER_ID primary key (CF));

create table SPETTATORE (
     genere char(1) not null,
     CF char(16) not null,
     nome varchar(255) not null,
     cognome varchar(255) not null,
     mail varchar(255) not null,
     data_di_nascita date not null,
     constraint ID_SPETTATORE_ID primary key (CF));

create table SPONSOR (
     IdSponsor numeric(4) not null,
     Nome varchar(255) not null,
     constraint ID_SPONSOR_ID primary key (IdSponsor));

create table SQUADRA (
     Nome varchar(255) not null,
     IdSquadra numeric(6) not null,
     data_creazione date not null,
     NomeVideogioco varchar(255) not null,
     constraint ID_SQUADRA_ID primary key (IdSquadra));

create table STATO (
     Nome varchar(255) not null,
     Superficie numeric(6) not null,
     NumAbitanti numeric(6) not null,
     NomeContinente varchar(255) not null,
     constraint ID_STATO_ID primary key (Nome));

create table TORNEO (
     IdTorneo numeric(6) not null,
     DataInizio date not null,
     DataFine date,
     NmaxIscrizioni numeric(4) not null,
     IdSponsor numeric(4),
     IdArena numeric(6) not null,
     NomeVideogioco varchar(255) not null,
     constraint ID_TORNEO_ID primary key (IdTorneo));

create table VIDEOGIOCO (
     Nome varchar(255) not null,
     DataCreazione date not null,
     TipologiaGioco varchar(255) not null,
     NumGiocatoriSquadra numeric(2) not null,
     IdAzienda char(11) not null,
     constraint ID_VIDEOGIOCO_ID primary key (Nome));


-- Constraints Section
-- ___________________ 

alter table ADESIONE_COACH_SQUADRA add constraint REF_ADESI_COACH_FK
     foreign key (CF)
     references COACH;

alter table ADESIONE_COACH_SQUADRA add constraint REF_ADESI_SQUAD_1
     foreign key (IdSquadra)
     references SQUADRA;

alter table ADESIONE_PLAYER_SQUADRA add constraint REF_ADESI_PLAYE_FK
     foreign key (CF)
     references PLAYER;

alter table ADESIONE_PLAYER_SQUADRA add constraint REF_ADESI_SQUAD
     foreign key (IdSquadra)
     references SQUADRA;

alter table ARENA add constraint REF_ARENA_CITTA_FK
     foreign key (NomeStato, NomeCitta)
     references CITTA;

alter table BIGLIETTO add constraint REF_BIGLI_BIGLI_FK
     foreign key (IdSquadra1, IdSquadra2, DataOra, IdBiglietto)
     references BIGLIETTO_TEMPLATE;

alter table BIGLIETTO add constraint REF_BIGLI_SPETT
     foreign key (CF)
     references SPETTATORE;

alter table BIGLIETTO_TEMPLATE add constraint REF_BIGLI_PARTI
     foreign key (IdSquadra1, IdSquadra2, DataOra)
     references PARTITA;

alter table CITTA add constraint EQU_CITTA_STATO
     foreign key (NomeStato)
     references STATO;

alter table CONTINENTE add constraint ID_CONTINENTE_CHK
     check(exists(select * from STATO
                  where STATO.NomeContinente = Nome)); 

alter table GIOCA add constraint REF_GIOCA_VIDEO
     foreign key (Nome)
     references VIDEOGIOCO;

alter table GIOCA add constraint REF_GIOCA_PLAYE_FK
     foreign key (CF)
     references PLAYER;

alter table Iscrizione add constraint REF_Iscri_TORNE
     foreign key (IdTorneo)
     references TORNEO;

alter table Iscrizione add constraint REF_Iscri_SQUAD_FK
     foreign key (IdSquadra)
     references SQUADRA;

alter table PARTITA add constraint REF_PARTI_SQUAD_1_FK
     foreign key (IdSquadra2)
     references SQUADRA;

alter table PARTITA add constraint REF_PARTI_SQUAD
     foreign key (IdSquadra1)
     references SQUADRA;

alter table PARTITA add constraint REF_PARTI_TORNE_FK
     foreign key (IdTorneo)
     references TORNEO;

alter table PARTITA add constraint REF_PARTI_ARBIT_FK
     foreign key (IdArbitro)
     references ARBITRO;

alter table PARTITA add constraint REF_PARTI_SPEAK_FK
     foreign key (IdSpeaker)
     references SPEAKER;

alter table PLAYER add constraint REF_PLAYE_STATO_FK
     foreign key (residenzaStato)
     references STATO;

alter table SQUADRA add constraint REF_SQUAD_VIDEO_FK
     foreign key (NomeVideogioco)
     references VIDEOGIOCO;

alter table STATO add constraint ID_STATO_CHK
     check(exists(select * from CITTA
                  where CITTA.NomeStato = Nome)); 

alter table STATO add constraint EQU_STATO_CONTI_FK
     foreign key (NomeContinente)
     references CONTINENTE;

alter table TORNEO add constraint REF_TORNE_SPONS_FK
     foreign key (IdSponsor)
     references SPONSOR;

alter table TORNEO add constraint REF_TORNE_ARENA_FK
     foreign key (IdArena)
     references ARENA;

alter table TORNEO add constraint REF_TORNE_VIDEO_FK
     foreign key (NomeVideogioco)
     references VIDEOGIOCO;

alter table VIDEOGIOCO add constraint REF_VIDEO_AZIEN_FK
     foreign key (IdAzienda)
     references AZIENDA_VIDEOGIOCO;


-- Index Section
-- _____________ 

create unique index ID_ADESIONE_COACH_SQUADRA_IND
     on ADESIONE_COACH_SQUADRA (IdSquadra, CF, DataInizio);

create index REF_ADESI_COACH_IND
     on ADESIONE_COACH_SQUADRA (CF);

create unique index ID_ADESIONE_PLAYER_SQUADRA_IND
     on ADESIONE_PLAYER_SQUADRA (IdSquadra, CF, DataInizio);

create index REF_ADESI_PLAYE_IND
     on ADESIONE_PLAYER_SQUADRA (CF);

create unique index ID_ARBITRO_IND
     on ARBITRO (CF);

create unique index ID_ARENA_IND
     on ARENA (IdArena);

create index REF_ARENA_CITTA_IND
     on ARENA (NomeStato, NomeCitta);

create unique index ID_AZIENDA_VIDEOGIOCO_IND
     on AZIENDA_VIDEOGIOCO (PartitaIVA);

create unique index ID_BIGLIETTO_IND
     on BIGLIETTO (CF, IdSquadra1, IdSquadra2, DataOra, IdBiglietto);

create index REF_BIGLI_BIGLI_IND
     on BIGLIETTO (IdSquadra1, IdSquadra2, DataOra, IdBiglietto);

create unique index ID_BIGLIETTO_TEMPLATE_IND
     on BIGLIETTO_TEMPLATE (IdSquadra1, IdSquadra2, DataOra, IdBiglietto);

create unique index ID_CITTA_IND
     on CITTA (NomeStato, Nome);

create unique index ID_COACH_IND
     on COACH (CF);

create unique index ID_CONTINENTE_IND
     on CONTINENTE (Nome);

create unique index ID_GIOCA_IND
     on GIOCA (Nome, CF);

create index REF_GIOCA_PLAYE_IND
     on GIOCA (CF);

create unique index ID_Iscrizione_IND
     on Iscrizione (IdTorneo, IdSquadra);

create index REF_Iscri_SQUAD_IND
     on Iscrizione (IdSquadra);

create unique index ID_PARTITA_IND
     on PARTITA (IdSquadra1, IdSquadra2, DataOra);

create index REF_PARTI_SQUAD_1_IND
     on PARTITA (IdSquadra2);

create index REF_PARTI_TORNE_IND
     on PARTITA (IdTorneo);

create index REF_PARTI_ARBIT_IND
     on PARTITA (IdArbitro);

create index REF_PARTI_SPEAK_IND
     on PARTITA (IdSpeaker);

create unique index ID_PLAYER_IND
     on PLAYER (CF);

create index REF_PLAYE_STATO_IND
     on PLAYER (residenzaStato);

create unique index ID_SPEAKER_IND
     on SPEAKER (CF);

create unique index ID_SPETTATORE_IND
     on SPETTATORE (CF);

create unique index ID_SPONSOR_IND
     on SPONSOR (IdSponsor);

create unique index ID_SQUADRA_IND
     on SQUADRA (IdSquadra);

create index REF_SQUAD_VIDEO_IND
     on SQUADRA (NomeVideogioco);

create unique index ID_STATO_IND
     on STATO (Nome);

create index EQU_STATO_CONTI_IND
     on STATO (NomeContinente);

create unique index ID_TORNEO_IND
     on TORNEO (IdTorneo);

create index REF_TORNE_SPONS_IND
     on TORNEO (IdSponsor);

create index REF_TORNE_ARENA_IND
     on TORNEO (IdArena);

create index REF_TORNE_VIDEO_IND
     on TORNEO (NomeVideogioco);

create unique index ID_VIDEOGIOCO_IND
     on VIDEOGIOCO (Nome);

create index REF_VIDEO_AZIEN_IND
     on VIDEOGIOCO (IdAzienda);

