using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Forms;

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


        private void LoadCmbBox()
        {
            foreach (var p in queryList)
            {
                cmb_Queries.Items.Add(p.Key);
            }
        }


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
                Queries.ExecuteOnly(QueryList.LasciaSquadra(myCF,idSquadra));
                System.Windows.Forms.MessageBox.Show("Sei uscito dalla squadra '" + nomeSquadra + "'");
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
                idSquadra =int.Parse( Queries.GetOneField("SELECT IdSquadra FROM squadra WHERE nome = '" + nomeSquadra + "';"));
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra da abbandonare");
                return;
            }

            try
            {
                Queries.ExecuteOnly(QueryList.EntraSquadra(myCF,idSquadra));
            }
            catch (Exception err)
            {
                if (err.Message.Contains("Duplicate entry"))
                {
                    System.Windows.Forms.MessageBox.Show("Devi aspettare un giorno per rientrare in una squadra");
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
            catch
            {
                System.Windows.Forms.MessageBox.Show("Seleziona una squadra da abbandonare");
                return;
            }
        }
    }
}
