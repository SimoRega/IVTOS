using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace IVTOS
{
    /// <summary>
    /// Logica di interazione per AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private Dictionary<string, string> queryList = new Dictionary<string, string>();
        private int steps = 0;
        private string newTorneo="";
        private string dataTorneo="";
        private string capienzaTorneo="";
        private string videogameTorneo="";
        private string arenaTorneo="";

        public AdminWindow()
        {
            InitializeComponent();

            LoadQuery();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            dataGrid.IsReadOnly = true;
            btnElimina.IsEnabled = false;
            btnAvanti.IsEnabled = false;

            foreach (var p in queryList)
            {
                cmbSelect.Items.Add(p.Key);
            }
        }

        private void LoadQuery() //da cambiare, magari lettura da file
        {
            queryList.Add("Visualizza tutti i Player","SELECT * FROM ivtos.player;");
            queryList.Add("Visualizza info principali dei Player", "SELECT Nickname, Nome, Cognome, Genere FROM ivtos.player;");
            queryList.Add("Visualizza tutti i Videogiochi", "SELECT * FROM ivtos.videogioco;");
            queryList.Add("Visualizza tutte le Aziende di Videogiochi", "SELECT * FROM ivtos.videogioco;");
            queryList.Add("Visualizza tutti gli Stati", "SELECT * FROM ivtos.stato;");
            queryList.Add("Visualizza tutte le Arene", "SELECT * FROM ivtos.arena;");
            queryList.Add("Visualizza tutte le Città", "SELECT * FROM ivtos.cittá;");
            queryList.Add("Visualizza tutti i Tornei", "SELECT idTorneo AS Torneo, nomevideogioco AS Videogioco, nomearena AS Arena, sponsor.Nome, DataInizio, nmaxiscrizioni AS Capienza " +
                        "FROM(torneo JOIN Arena ON torneo.IdArena = arena.IdArena)JOIN Sponsor on torneo.Sponsor = sponsor.idsponsor ; ");
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string query = queryList[cmbSelect.SelectedItem.ToString()];

            string connection = "Persist Security Info=False;database=ivtos;server=localhost;port=3306;user id=root;Password=password;";
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, conn);
            adapter.Fill(ds);

            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void btnTorneo_Click(object sender, RoutedEventArgs e)
        {          
           dataTorneo = DateTime.Now.ToString("yyyy-MM-dd");
           dataGrid.ItemsSource = Queries.GetDataSet("SELECT * From ivtos.arena").Tables[0].DefaultView;
           lblStep.Content = "<2° Step: Scegli Arena>";
            steps = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DataRow DR = (DataRow)DRV.Row;
            int idTorneo = (int)DR.ItemArray[0];
            string q = "DELETE FROM torneo WHERE IdTorneo="+idTorneo;
            Queries.ExecuteOnly(q);
            dataGrid.ItemsSource = Queries.GetDataSet(queryList["Visualizza tutti i Tornei"]).Tables[0].DefaultView;
            btnElimina.IsEnabled = false;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnElimina.IsEnabled = true;

            if(steps>=0)
                btnAvanti.IsEnabled = true;
        }

        private void btnAvanti_Click(object sender, RoutedEventArgs e)
        {
            
            DataRowView DRV;
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Prima di andare avanti selezionare una riga dalla tabella");
                return;
            }
            switch (steps)
            {
                case 0:
                    DRV = (DataRowView)dataGrid.SelectedItem;
                    capienzaTorneo =  DRV.Row.ItemArray[2].ToString() ;
                    arenaTorneo = DRV.Row.ItemArray[0].ToString();
                    dataGrid.ItemsSource = Queries.GetDataSet("SELECT * FROM videogioco").Tables[0].DefaultView;
                    steps++;
                    lblStep.Content = "<3° Step: Scegli Gioco>";
                    break;
                case 1:
                    DRV = (DataRowView)dataGrid.SelectedItem;
                    videogameTorneo = DRV.Row.ItemArray[0].ToString();
                    steps++;
                    newTorneo = String.Format("INSERT INTO torneo VALUES (IdTorneo,'{0}',NULL,{1},1,{2},'{3}',NULL);", dataTorneo, capienzaTorneo, arenaTorneo, videogameTorneo);
                    Queries.ExecuteOnly(newTorneo);
                    dataGrid.ItemsSource = Queries.GetDataSet(queryList["Visualizza tutti i Tornei"]).Tables[0].DefaultView;
                    steps = -1;
                    lblStep.Content = "<1° Step: Crea Torneo>";
                    btnAvanti.IsEnabled = false;
                    break;
                default:
                    steps = -1;
                    break;
            }
        }

        private void btnIndietro_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
