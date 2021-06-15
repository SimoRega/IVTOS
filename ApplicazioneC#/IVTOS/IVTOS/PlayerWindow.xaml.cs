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
            LoadQuery();
            //LoadCmbBox();
            dataGrid.IsReadOnly = true;
            btnAbbandonaSq.IsEnabled = false;
            btnAbbandonaSq.Visibility = Visibility.Hidden;
        }

        private void LoadWelcome()
        {
            lbl_Welcome.Content ="Accesso eseguito come: "+ Queries.GetOneField("SELECT nickname FROM ivtos.player where CF = '"+myCF+"' ;");
        }

        private string myCF;
        public Dictionary<string, string> queryList = new Dictionary<string, string>();
        private int sqToExit;

        private void LoadCmbBox()
        {
            foreach (var p in queryList)
            {
                cmb_Queries.Items.Add(p.Key);
            }
        }

        private void LoadQuery() //da cambiare, magari lettura da file
        {
            queryList.Add("Show my Teams", "select squadra.Nome, squadra.IdSquadra from squadra  join adesione_player_squadra on adesione_player_squadra.IdSquadra = squadra.IdSquadra where CF_Player = '" + myCF +"'; ");
            queryList.Add("Select Videogame", "SELECT * FROM progettodatabase.videogame;");
            queryList.Add("Select State", "SELECT * FROM progettodatabase.state;");

            queryList.Add("Exit from team", "UPDATE adesione_player_squadra SET DataFine = now() WHERE CF_Player = '" + myCF +"' AND IdSquadra = " + sqToExit + "; ");
        }

        private void btn_esegui_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnMostraMieSquadre_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Queries.GetDataSet(queryList["Show my Teams"]).Tables[0].DefaultView;

        }

        private void dataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            btnAbbandonaSq.IsEnabled = true;
            btnAbbandonaSq.Visibility = Visibility.Visible;

        }

        private void btnAbbandonaSq_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DataRow DR = (DataRow)DRV.Row;
            int idSquadra =(int)DR.ItemArray[1];
            string nomeSquadra = DR.ItemArray[0].ToString();
            string message = "Sei sicuro di voler lasciare la squadra: " + nomeSquadra;
            string caption = "Abbandona squadra";

            if (System.Windows.Forms.MessageBox.Show(message, caption, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                sqToExit = idSquadra;
                Queries.ExecuteOnly(queryList["Exit from team"]);
            }
        }
    }
}
