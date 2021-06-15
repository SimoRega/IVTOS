using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;


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
            LoadCmbBox();
            LoadQuery();
        }

        private void LoadWelcome()
        {
            lbl_Welcome.Content ="Accesso eseguito come: "+ Queries.GetOneField("SELECT nickname FROM ivtos.player where CF = '"+myCF+"' ;");
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

        private void LoadQuery() //da cambiare, magari lettura da file
        {
            queryList.Add("Show my Teams", "SELECT * FROM progettodatabase.player;");
            queryList.Add("Select Videogame", "SELECT * FROM progettodatabase.videogame;");
            queryList.Add("Select State", "SELECT * FROM progettodatabase.state;");
        }

        private void btn_esegui_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
