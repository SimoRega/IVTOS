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
    /// Logica di interazione per SpettatoreWindow.xaml
    /// </summary>
    public partial class SpettatoreWindow : Window
    {
        public SpettatoreWindow()
        {
            InitializeComponent();
            btn_partite_torneo.IsEnabled = false;
            dataGrid.IsReadOnly = true;
        }

        private void MostraTornei_Click(object sender, RoutedEventArgs e)
        {
            

            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaTorneiAttivi()).Tables[0].DefaultView;
            btn_partite_torneo.IsEnabled = true;
            
        }

        private void btn_partite_torneo_Click(object sender, RoutedEventArgs e)
        {
            if(dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Selezionare un torneo");
                return;
            }
            DataRowView DRV = (DataRowView)dataGrid.SelectedItem;
            DRV = (DataRowView)dataGrid.SelectedItem;
            var torneo = DRV.Row.ItemArray[0].ToString();
            dataGrid.ItemsSource = Queries.GetDataSet(QueryList.VisualizzaPartiteTorneo(torneo)).Tables[0].DefaultView;

        }
    }
}
