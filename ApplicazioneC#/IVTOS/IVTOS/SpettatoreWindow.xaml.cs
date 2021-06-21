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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IVTOS
{
    /// <summary>
    /// Logica di interazione per SpettatoreWindow.xaml
    /// </summary>
    public partial class SpettatoreWindow : Window
    {
        private string cf;
        public SpettatoreWindow(string codice_fiscale)
        {
            cf = codice_fiscale;
            InitializeComponent();
            btn_partite_torneo.IsEnabled = false;
            btnCompraBiglietto.IsEnabled = false;
            dataGrid.IsReadOnly = true;
        }

        private void MostraTornei_Click(object sender, RoutedEventArgs e)
        {
            

            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaTorneiAttiviConIdArena()).Tables[0].DefaultView;
            btn_partite_torneo.IsEnabled = true;
            btnCompraBiglietto.IsEnabled = false;
            
        }

        private void btn_partite_torneo_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Selezionare un torneo");
                return;
            }
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DRV = (DataRowView)dataGrid.SelectedItem;
            var torneo = DRV.Row.ItemArray[0].ToString();
            var arena = DRV.Row.ItemArray[2].ToString();
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaPartiteTorneo(torneo, arena)).Tables[0].DefaultView;
            btnCompraBiglietto.IsEnabled = true;
            btn_partite_torneo.IsEnabled = false;

        }

        private void btnCompraBiglietto_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                System.Windows.MessageBox.Show("Selezionare una partita");
                return;
            }

            
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DRV = (DataRowView)dataGrid.SelectedItem;
            
            var squadra1 = DRV.Row.ItemArray[0].ToString();
            var nomesquadra1 = DRV.Row.ItemArray[1].ToString();
            var squadra2 = DRV.Row.ItemArray[2].ToString();
            var nomesquadra2 = DRV.Row.ItemArray[3].ToString();
            var giorno = DRV.Row.ItemArray[4].ToString();
            string[] data= giorno.Split(' ');
            string[] cacca = data[0].Split('/');
            string bella = cacca[2] + "-" + cacca[1] + "-" + cacca[0];
            var nomeArena = Queries.GetOneField(QueryList.NomeArenaInCuiSiSvolgePartita(squadra1, squadra2, bella));
            var nomeStato = Queries.GetOneField(QueryList.StatoArenaInCuiSiSvolgePartita(squadra1, squadra2, bella));
            var nomeCitta = Queries.GetOneField(QueryList.CittaArenaInCuiSiSvolgePartita(squadra1, squadra2, bella));
            var arena = Queries.GetOneField(QueryList.IdArenaInCuiSiSvolgePartita(squadra1, squadra2, bella));
            var prezzo = Queries.GetOneField(QueryList.PrezzoBiglietto(arena, squadra1, squadra2, bella));
            string message = "Sei sicuro di voler comprare il biglietto per la partita del giorno " + bella + " fra la squadra " + nomesquadra1 + " e " + nomesquadra2
                + " nell'arena " + nomeArena + ", che si trova nello Stato di " + nomeStato +
                ", nella città di " + nomeCitta + " al prezzo di " + prezzo;
                
            string caption = "Compra Biglietto";

            if (System.Windows.Forms.MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Queries.ExecuteOnly(QueryList.CompraBiglietto(squadra1, squadra2, bella, arena, cf));
                    int br = Convert.ToInt32(Queries.GetOneField(QueryList.VisualizzaBigliettiRimanentiPerUnaPartita(squadra1, squadra2, arena, bella)));
                    br--;
                    Queries.ExecuteOnly(QueryList.UpdateBiglietto(br, arena, squadra1, squadra2, bella));
                    System.Windows.Forms.MessageBox.Show("Hai comprato il biglietto", "Conferma Acquisto", (MessageBoxButtons)MessageBoxButton.OK, MessageBoxIcon.Information);

                }catch(Exception ex)
                {
                    if(ex.Message.Contains("Duplicate entry"))
                    {
                        System.Windows.Forms.MessageBox.Show("Hai già comprato questo biglietto", "Errore Acquisto Biglietto", 
                            (MessageBoxButtons)MessageBoxButton.OK , (MessageBoxIcon)MessageBoxImage.Stop);

                    }
                }

            }
            

        }

        private void btn_visualizzaBiglietti_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaBigliettiAcquistati(cf)).Tables[0].DefaultView;
            btnCompraBiglietto.IsEnabled = false;
        }
    }
}
