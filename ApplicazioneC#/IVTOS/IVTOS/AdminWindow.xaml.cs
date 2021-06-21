using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace IVTOS
{
    /// <summary>
    /// Logica di interazione per AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Dictionary<string, string> queryList = new Dictionary<string, string>();
        private Dictionary<string, string> stats = new Dictionary<string, string>();
        private int steps = 0;
        private string newTorneo = "";
        private string dataTorneo = "";
        private string capienzaTorneo = "";
        private string videogameTorneo = "";
        private string arenaTorneo = "";
        private string sponsorTorneo = "";
        private string lastQuery = "";

        public AdminWindow()
        {
            InitializeComponent();

            LoadQuery();
            LoadComboBox();
            //CreaBiglietti();
        }

        private void CreaBiglietti()
        {
            Random rnd = new Random();
            DataSet setPartite = Queries.GetDataSet(QueryList.VisualizzaChiavePartite());
            Queries.ExecuteOnly("DELETE FROM acquisto_biglietto;");
            Queries.ExecuteOnly("DELETE FROM biglietto;");
            foreach (DataRow elem in setPartite.Tables[0].Rows)
            {
                string giornoGrezzo = elem.ItemArray[2].ToString();
                string[] giornoSplittato = giornoGrezzo.Split(' ');
                string[] dataFull = giornoSplittato[0].Split('/');
                string data = dataFull[2] + "-" + dataFull[1] + "-" + dataFull[0];
                string idArena = Queries.GetOneField(QueryList.IdArenaInCuiSiSvolgePartita(elem.ItemArray[0].ToString(), elem.ItemArray[1].ToString(), data));
                string capienza = Queries.GetOneField(QueryList.CapienzaInCuiSiSvolgePartita(elem.ItemArray[0].ToString(), elem.ItemArray[1].ToString(), data));
                Queries.ExecuteOnly(QueryList.InsertBiglietto(idArena, elem.ItemArray[0].ToString(), elem.ItemArray[1].ToString(), data, rnd.Next(30, 200).ToString(), capienza));

                File.AppendAllText("./biglietti.txt", QueryList.InsertBiglietto(idArena, elem.ItemArray[0].ToString(), elem.ItemArray[1].ToString(), data, rnd.Next(30, 200).ToString(), capienza) + "\n");
            }
        }

        private void LoadComboBox()
        {
            dataGrid.IsReadOnly = true;
            btnAvanti.IsEnabled = false;
            btnSelezioneTorneo.IsEnabled = false;
            btnSelezioneSquadra.IsEnabled = false;
            btnTerminaTorneo.IsEnabled = false;
            btn.IsEnabled = false;
            sliderTorneo.IsEnabled = false;

            foreach (var p in queryList)
            {
                cmbSelect.Items.Add(p.Key);
            }
            foreach (var p in stats)
            {
                cmbStatistiche.Items.Add(p.Key);
            }
        }

        private void LoadQuery() //da cambiare, magari lettura da file
        {
            queryList.Add("Visualizza tutti i Player", QueryList.VisualizzaPlayer());
            queryList.Add("Visualizza tutti i Videogiochi", QueryList.VisualizzaVideogiochi());
            queryList.Add("Visualizza tutte le Squadre", QueryList.VisualizzaSquadre());
            queryList.Add("Visualizza tutti i Coach", QueryList.VisualizzaCoach());
            queryList.Add("Visualizza tutti gli Arbitri", QueryList.VisualizzaArbitri());
            queryList.Add("Visualizza tutti gli Speaker", QueryList.VisualizzaSpeaker());
            queryList.Add("Visualizza tutte le Aziende di Videogiochi", QueryList.VisualizzaAziendeGiochi());
            queryList.Add("Visualizza tutti gli Stati", QueryList.VisualizzaStati());
            queryList.Add("Visualizza tutte le Arene", QueryList.VisualizzaArena());
            queryList.Add("Visualizza tutte le Città", QueryList.VisualizzaCitta());
            queryList.Add("Visualizza tutti i Tornei", QueryList.VisualizzaTornei());
            queryList.Add("Visualizza tutti i Tornei non conclusi", QueryList.VisualizzaTorneiAttivi());
            queryList.Add("Visualizza tutti Biglietti", QueryList.VisualizzaBiglietti());
            queryList.Add("Visualizza tutte le Partite", QueryList.VisualizzaPartite());

            stats.Add("I 20 Tornei con più Squadre Iscritte", QueryList.VisualizzaTorneiConPiuSquadre());
            stats.Add("I 20 Tornei con meno Squadre Iscritte", QueryList.VisualizzaTorneiConMenoSquadre());
            stats.Add("I 20 Tornei con più Biglietti Venduti", QueryList.VisualizzaTorneiBiglietti());
            stats.Add("I 3 Videogiochi più giocati ai Tornei", QueryList.VisualizzaVideogiochiTornei());
            stats.Add("La Squadra che ha partecipato a più Tornei", QueryList.VisualizzaSquadraTornei());
            stats.Add("Il Player che ha partecipato a più Tornei", QueryList.VisualizzaPlayerTornei());
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string query = queryList[cmbSelect.SelectedItem.ToString()];
            lastQuery = queryList[cmbSelect.SelectedItem.ToString()];
            dataGrid.ItemsSource = Queries.GetDataSet(query).Tables[0].DefaultView;
        }

        private void btnTorneo_Click(object sender, RoutedEventArgs e)
        {
            dataTorneo = DateTime.Now.ToString("yyyy-MM-dd");
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaArena()).Tables[0].DefaultView;
            lastQuery = QueryList.VisualizzaArena();
            lblStep.Content = "<2° Step: Scegli Arena>";
            MessageBox.Show("Selezionare un Arena e premere il tasto Avanti", "Creazione Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
            steps = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DataRow DR = (DataRow)DRV.Row;
            int idTorneo = (int)DR.ItemArray[0];
            Queries.ExecuteOnly(QueryList.TerminaTorneo(idTorneo.ToString()));
            dataGrid.ItemsSource = Queries.GetDataSet(queryList["Visualizza tutti i Tornei"]).Tables[0].DefaultView;
            lastQuery = queryList["Visualizza tutti i Tornei"];
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lastQuery == QueryList.VisualizzaTornei() || lastQuery == QueryList.VisualizzaTorneiAttivi())
            {
                btnTerminaTorneo.IsEnabled = true;
                btnAvanti.IsEnabled = false;
            }
            else
            {
                btnTerminaTorneo.IsEnabled = false;
                btnAvanti.IsEnabled = false;
            }

            if ((steps == 0 && lastQuery == QueryList.VisualizzaArena()) ||
                (steps == 1 && lastQuery == QueryList.VisualizzaVideogiochi()) ||
                    (steps == 2 && lastQuery == QueryList.VisualizzaSponsor()))
                btnAvanti.IsEnabled = true;
        }

        private void btnAvanti_Click(object sender, RoutedEventArgs e)
        {

            DataRowView DRV;
            if (dataGrid.SelectedItem == null && steps != 2 &&  steps != 3)
            {
                MessageBox.Show("Prima di andare avanti selezionare una riga dalla tabella", "Creazione Torneo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            switch (steps)
            {
                case 0:
                    DRV = (DataRowView)dataGrid.SelectedItem;
                    capienzaTorneo = DRV.Row.ItemArray[2].ToString();
                    arenaTorneo = DRV.Row.ItemArray[0].ToString();
                    dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaVideogiochi()).Tables[0].DefaultView;
                    lastQuery = QueryList.VisualizzaVideogiochi();
                    steps++;
                    lblStep.Content = "<3° Step: Scegli Gioco>";
                    MessageBox.Show("Selezionare un Videogioco e premere il tasto Avanti", "Creazione Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 1:
                    DRV = (DataRowView)dataGrid.SelectedItem;
                    videogameTorneo = DRV.Row.ItemArray[0].ToString();
                    steps++;
                    dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaSponsor()).Tables[0].DefaultView;
                    lastQuery = QueryList.VisualizzaSponsor();
                    lblStep.Content = "<4° Step: Scegli Sponsor>";
                    MessageBox.Show("Selezionare uno Sponsor e premere il tasto Avanti \n(Non selezionare nulla per non inserire lo sponsor)", "Creazione Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnAvanti.IsEnabled = true;
                    break;
                case 2:
                    lblStep.Content = "<5° Step: N°Iscrizioni>";
                    MessageBox.Show("Indicare con lo Slider il numero di Squadre che si desidera far partecipare al torneo", "Creazione Torneo", MessageBoxButton.OK, MessageBoxImage.Information);
                    sliderTorneo.IsEnabled = true;
                    steps++;
                    break;
                case 3:
                    capienzaTorneo = sliderTorneo.Value.ToString();
                    sliderTorneo.IsEnabled = false;
                    try
                    {

                        DRV = (DataRowView)dataGrid.SelectedItem;
                        sponsorTorneo = DRV.Row.ItemArray[0].ToString();
                        newTorneo = String.Format("INSERT INTO torneo VALUES (IdTorneo,'{0}',NULL,{1},{2},'{3}',{4},NULL);", dataTorneo, capienzaTorneo, sponsorTorneo, videogameTorneo, arenaTorneo);
                    }
                    catch
                    {
                        newTorneo = String.Format("INSERT INTO torneo VALUES (IdTorneo,'{0}',NULL,{1},NULL,'{2}',{3},NULL);", dataTorneo, capienzaTorneo, videogameTorneo, arenaTorneo);

                    }
                    Queries.ExecuteOnly(newTorneo);
                    dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaTornei()).Tables[0].DefaultView;
                    lastQuery = QueryList.VisualizzaTornei();
                    
                    steps = -1;
                    lblStep.Content = "<1° Step: Crea Torneo>";
                    btnAvanti.IsEnabled = false;
                    break;
                default:
                    steps = -1;
                    break;
            }
        }

        private void btnIscrizioniTorneo_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaTorneiAttivi()).Tables[0].DefaultView;
            lastQuery = QueryList.VisualizzaTorneiAttivi();
            btnIscrizioniTorneo.IsEnabled = false;
            btnSelezioneTorneo.IsEnabled = true;
        }

        private void btnSelezioneTorneo_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Prima selezionare una riga dalla tabella", "SelezioneTorneo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            btnIscrizioniTorneo.IsEnabled = true;
            btnSelezioneTorneo.IsEnabled = false;
            DataRowView DRV;
            DRV = (DataRowView)dataGrid.SelectedItem;
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaIscrizioniTorneo(DRV.Row.ItemArray[0].ToString())).Tables[0].DefaultView;
            lastQuery = QueryList.VisualizzaIscrizioniTorneo("LastQuery");
        }

        private void btnSelezioneSquadra_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Prima selezionare una riga dalla tabella", "SelezioneSquadra", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            btnIscrizioniSquadra.IsEnabled = true;
            btnSelezioneSquadra.IsEnabled = false;
            DataRowView DRV;
            DRV = (DataRowView)dataGrid.SelectedItem;
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaIscrizioniSquadra(DRV.Row.ItemArray[1].ToString())).Tables[0].DefaultView;
            lastQuery = QueryList.VisualizzaIscrizioniSquadra("LastQuery");
        }

        private void btnIscrizioniSquadra_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaSquadre()).Tables[0].DefaultView;
            lastQuery = QueryList.VisualizzaSquadre();
            btnIscrizioniSquadra.IsEnabled = false;
            btnSelezioneSquadra.IsEnabled = true;
        }

        private void btnTerminaTorneo_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DataRow DR = (DataRow)DRV.Row;
            string idTorneo = DR.ItemArray[0].ToString();
            Queries.ExecuteOnly(QueryList.TerminaTorneo(idTorneo));
            dataGrid.ItemsSource = Queries.GetDataSet(queryList["Visualizza tutti i Tornei"]).Tables[0].DefaultView;
            lastQuery = queryList["Visualizza tutti i Tornei"];
            btnTerminaTorneo.IsEnabled = false;
        }

        private void btnEseguiStatistiche_Click(object sender, RoutedEventArgs e)
        {
            string query = stats[cmbStatistiche.SelectedItem.ToString()];
            lastQuery = stats[cmbStatistiche.SelectedItem.ToString()];
            dataGrid.ItemsSource = Queries.GetDataSet(query).Tables[0].DefaultView;
        }

        private void cmbSelect_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbSelect.SelectedItem != null)
                btn.IsEnabled = true;
            else
                btn.IsEnabled = false;
        }

        private void sliderTorneo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblValue.Content = sliderTorneo.Value;
        }
    }
}
