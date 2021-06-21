using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace IVTOS
{
    /// <summary>
    /// Logica di interazione per PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        public PlayerWindow(String CF)
        {
            myCF = CF;
            InitializeComponent();
            LoadWelcome();
            //LoadCmbBox();
            dataGrid.IsReadOnly = true;
            disableAllButton();
        }

        private void LoadWelcome()
        {
            lbl_Welcome.Content = "Accesso eseguito come: " + Queries.GetOneField("SELECT nickname FROM ivtos.player where CF = '" + myCF + "' ;");
        }

        private string myCF;
        public Dictionary<string, string> queryList = new Dictionary<string, string>();




        private void btn_esegui_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMostraMieSquadre_Click(object sender, RoutedEventArgs e)
        {
            disableAllButton();
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.MostraMieSquadre(myCF)).Tables[0].DefaultView;
            btnAbbandonaSq.IsEnabled = true;
            btnAbbandonaSq.Visibility = Visibility.Visible;

            btnIscriviSqTorneo.IsEnabled = true;
            btnIscriviSqTorneo.Visibility = Visibility.Visible;

            btnMostraMembri.IsEnabled = true;
            btnMostraMembri.Visibility = Visibility.Visible;

            btnMostraProssimaPartita.IsEnabled = true;
            btnMostraProssimaPartita.Visibility = Visibility.Visible;
        }
        private void btnMostraSquadre_Click(object sender, RoutedEventArgs e)
        {
            disableAllButton();
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.MostraSqNonComplete(myCF)).Tables[0].DefaultView;
            btnEntraSq.IsEnabled = true;
            btnEntraSq.Visibility = Visibility.Visible;
        }

        private void disableAllButton()
        {
            btnAbbandonaSq.IsEnabled = false;
            btnAbbandonaSq.Visibility = Visibility.Hidden;

            btnEntraSq.IsEnabled = false;
            btnEntraSq.Visibility = Visibility.Hidden;

            btnIscriviSqTorneo.IsEnabled = false;
            btnIscriviSqTorneo.Visibility = Visibility.Hidden;

            btnMostraMembri.IsEnabled = false;
            btnMostraMembri.Visibility = Visibility.Hidden;

            btnMostraProssimaPartita.IsEnabled = false;
            btnMostraProssimaPartita.Visibility = Visibility.Hidden;

            HideCreaSqElement();
        }


        private void btnAbbandonaSq_Click(object sender, RoutedEventArgs e)
        {
            int idSquadra;
            string nomeSquadra;

            try
            {

                DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
                DataRow DR = (DataRow)DRV.Row;
                idSquadra = (int)DR.ItemArray[1];
                nomeSquadra = DR.ItemArray[0].ToString();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra da abbandonare");
                return;
            }
            string message = "Sei sicuro di voler lasciare la squadra: " + nomeSquadra;
            string caption = "Abbandona squadra";

            if (System.Windows.Forms.MessageBox.Show(message, caption, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                Queries.ExecuteOnly(QueryList.LasciaSquadra(myCF, idSquadra));
                System.Windows.Forms.MessageBox.Show("Sei uscito dalla squadra '" + nomeSquadra + "'");
                dataGrid.ItemsSource = Queries.GetDataSet(QueryList.MostraMieSquadre(myCF)).Tables[0].DefaultView;

            }
        }


        private void btnEntraSq_Click(object sender, RoutedEventArgs e)
        {
            int idSquadra;
            string nomeSquadra;

            try
            {
                DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
                DataRow DR = (DataRow)DRV.Row;
                nomeSquadra = DR.ItemArray[0].ToString();
                idSquadra = int.Parse(Queries.GetOneField("SELECT IdSquadra FROM squadra WHERE nome = '" + nomeSquadra + "';"));
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra");
                return;
            }

            try
            {
                Queries.ExecuteOnly(QueryList.EntraSquadra(myCF, idSquadra));
                System.Windows.Forms.MessageBox.Show("Sei entrato nella Squadra " + nomeSquadra);
                dataGrid.ItemsSource = Queries.GetDataSet(QueryList.MostraSqNonComplete(myCF)).Tables[0].DefaultView;

            }
            catch (Exception err)
            {
                if (err.Message.Contains("Duplicate entry"))
                {
                    System.Windows.Forms.MessageBox.Show("Devi aspettare un giorno per rientrare in questa squadra");
                }
            }


        }

        private void btnIscriviSqTorneo_Click(object sender, RoutedEventArgs e)
        {
            int idSquadra;
            string nomeSquadra;
            try
            {

                DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
                DataRow DR = (DataRow)DRV.Row;
                idSquadra = (int)DR.ItemArray[1];
                nomeSquadra = DR.ItemArray[0].ToString();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra da iscrivere ad un torneo");
                return;
            }

            IscrizioneSquadraTorneo iscrizione = new IscrizioneSquadraTorneo(idSquadra, this);
            iscrizione.Show();
            this.Visibility = Visibility.Hidden;

        }

        private void btnCreaSquadra_Click(object sender, RoutedEventArgs e)
        {
            disableAllButton();
            showCreaSqElement();
        }
        private void HideCreaSqElement()
        {
            sq_name.Visibility = Visibility.Hidden;
            fifa.Visibility = Visibility.Hidden;
            apex.Visibility = Visibility.Hidden;
            fortnite.Visibility = Visibility.Hidden;
            gta.Visibility = Visibility.Hidden;
            hearthstone.Visibility = Visibility.Hidden;
            overwatch.Visibility = Visibility.Hidden;
            overwatch2.Visibility = Visibility.Hidden;
            rainbow.Visibility = Visibility.Hidden;
            rocket.Visibility = Visibility.Hidden;
            starcraft.Visibility = Visibility.Hidden;
            btc_crea.Visibility = Visibility.Hidden;

            sq_name.Text = "Inserisci il nome della squadra";
        }
        private void showCreaSqElement()
        {
            sq_name.Visibility = Visibility.Visible;
            fifa.Visibility = Visibility.Visible;
            apex.Visibility = Visibility.Visible;
            fortnite.Visibility = Visibility.Visible;
            gta.Visibility = Visibility.Visible;
            hearthstone.Visibility = Visibility.Visible;
            overwatch.Visibility = Visibility.Visible;
            overwatch2.Visibility = Visibility.Visible;
            rainbow.Visibility = Visibility.Visible;
            rocket.Visibility = Visibility.Visible;
            starcraft.Visibility = Visibility.Visible;
            btc_crea.Visibility = Visibility.Visible;
        }

        private void btnMostraMembri_Click(object sender, RoutedEventArgs e)
        {
            int idSquadra;
            string nomeSquadra;
            try
            {

                DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
                DataRow DR = (DataRow)DRV.Row;
                idSquadra = (int)DR.ItemArray[1];
                nomeSquadra = DR.ItemArray[0].ToString();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra");
                return;
            }

            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.MostraMembriSquadra(idSquadra)).Tables[0].DefaultView;

        }

        private void btnMostraProssimaPartita_Click(object sender, RoutedEventArgs e)
        {
            int idSquadra;
            string nomeSquadra;
            try
            {

                DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
                DataRow DR = (DataRow)DRV.Row;
                idSquadra = (int)DR.ItemArray[1];
                nomeSquadra = DR.ItemArray[0].ToString();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra");
                return;
            }
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.MostraProssimePartita(idSquadra)).Tables[0].DefaultView;

        }

        private void btc_crea_Click(object sender, RoutedEventArgs e)
        {

            int idSquadra;
            string res = sq_name.Text.ToString();


            if (!(bool)fifa.IsChecked && !(bool)apex.IsChecked && !(bool)fortnite.IsChecked && !(bool)gta.IsChecked && !(bool)hearthstone.IsChecked && !(bool)overwatch.IsChecked && !(bool)overwatch2.IsChecked && !(bool)rainbow.IsChecked && !(bool)rocket.IsChecked && !(bool)starcraft.IsChecked)
            {

                System.Windows.Forms.MessageBox.Show("Devi selezionare almeno un videogioco");
                HideCreaSqElement();
                return;
            }

            Queries.ExecuteOnly(QueryList.CreaSquadra(res));
            idSquadra = int.Parse(Queries.GetOneField("SELECT IdSquadra FROM squadra WHERE nome = '" + res + "';"));
            Queries.ExecuteOnly(QueryList.EntraSquadra(myCF, idSquadra));
            try
            {
                if ((bool)fifa.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, fifa.Content.ToString()));
                }
                if ((bool)apex.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, apex.Content.ToString()));
                }
                if ((bool)fortnite.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, fortnite.Content.ToString()));
                }
                if ((bool)gta.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, gta.Content.ToString()));
                }
                if ((bool)hearthstone.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, hearthstone.Content.ToString()));
                }
                if ((bool)overwatch.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, overwatch.Content.ToString()));
                }
                if ((bool)overwatch2.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, overwatch2.Content.ToString()));
                }
                if ((bool)rainbow.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, rainbow.Content.ToString()));
                }
                if ((bool)rocket.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, rocket.Content.ToString()));
                }
                if ((bool)starcraft.IsChecked)
                {
                    Queries.ExecuteOnly(QueryList.InsertSquadraRiguarda(idSquadra, starcraft.Content.ToString()));
                }

                System.Windows.Forms.MessageBox.Show("Congratulazioni, hai creato la squadra :" + res) ;

            }
            catch
            {

            }
            HideCreaSqElement();
        }

        private void sq_name_GotFocus(object sender, RoutedEventArgs e)
        {
            sq_name.Clear();
        }

    }
}
