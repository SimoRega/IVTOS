using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IVTOS
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void chooseUser()
        {
            String testo = txtUserName.Text.ToUpper();
            switch (testo)
            {
                case "PLAYER":
                    PlayerWindow pw = new PlayerWindow();
                    pw.Show();
                    this.Close();
                    break;
                case "ADMIN":
                    AdminWindow aw = new AdminWindow();
                    aw.Show();
                    this.Close();
                    break;
                case "SPETTATORE":
                    SpettatoreWindow sw = new SpettatoreWindow();
                    sw.Show();
                    this.Close();
                    break;
                default:
                    MessageBox.Show("Utente non corretto");
                    break;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            chooseUser();
        }

        private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "";
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                chooseUser();
        }
    }
}
