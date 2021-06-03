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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String testo = txtUserName.Text.ToUpper();
            if (testo == "PLAYER")
            {
                PlayerWindow pw = new PlayerWindow();
                pw.Show();
                this.Close();
            }else if(testo == "ADMIN")
            {
                AdminWindow aw = new AdminWindow();
                aw.Show();
                this.Close();
            }else if(testo == "SPETTATORE")
            {
                SpettatoreWindow sw = new SpettatoreWindow();
                sw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Utente non corretto");
            }
        }

        private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = "";
        }
    }
}
