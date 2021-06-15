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
        public Dictionary<string, string> queryList = new Dictionary<string, string>();

        public AdminWindow()
        {
            InitializeComponent();

            LoadQuery();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            foreach(var p in queryList)
            {
                cmbSelect.Items.Add(p.Key);
            }
        }

        private void LoadQuery() //da cambiare, magari lettura da file
        {
            queryList.Add("Select Player","SELECT * FROM progettodatabase.player;");
            queryList.Add("Select Videogame", "SELECT * FROM progettodatabase.videogame;");
            queryList.Add("Select State", "SELECT * FROM progettodatabase.state;");
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string query = queryList[cmbSelect.SelectedItem.ToString()];

            string connection = "Persist Security Info=False;database=progettodatabase;server=localhost;port=3306;user id=root;Password=password;";
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, conn);
            adapter.Fill(ds);

            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }

    }
}
