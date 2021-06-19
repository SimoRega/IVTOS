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
    /// Logica di interazione per IscrizioneSquadraTorneo.xaml
    /// </summary>
    public partial class IscrizioneSquadraTorneo : Window
    {
        public IscrizioneSquadraTorneo(int idSquadra, PlayerWindow playerWindow)
        {
            InitializeComponent();
            lblTitolo.Content = "Stai iscrivendo la squadra: " + Queries.GetOneField("SELECT nome FROM squadra WHERE IdSquadra = " + idSquadra);
            dbTornei.ItemsSource = Queries.GetDataSet("SELECT IdTorneo, DataInizio,NomeVideogioco, NomeArena, NomeCitta , NomeStato FROM torneo join arena on torneo.idArena = arena.idArena").Tables[0].DefaultView;
            dbTornei.IsReadOnly = true;
            btnIscrivi.Visibility = Visibility.Hidden;
            this.idSquadra = idSquadra;
            lastWindow = playerWindow;
        }
        private PlayerWindow lastWindow;
        private int idSquadra;

        private void dbTornei_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnIscrivi.Visibility = Visibility.Visible;
        }

        private void btnIscrivi_Click(object sender, RoutedEventArgs e)
        {
            int idTorneo;
            try
            {
                DataRowView DRV = (DataRowView)dbTornei.SelectedItem;
                DataRow DR = (DataRow)DRV.Row;
                idTorneo = (int)DR.ItemArray[0];
                //nomeSquadra = DR.ItemArray[0].ToString();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona un torneo");
                return;
            }
            try
            {
                Queries.ExecuteOnly(QueryList.IscriviSqATorneo(idSquadra, idTorneo));
                MessageBox.Show("Squadra iscritta con successo");
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Duplicate entry"))
                {
                    MessageBox.Show("La tua squadra è già iscritta a questo torneo");
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            lastWindow.Visibility = Visibility.Visible;
        }
    }
}
