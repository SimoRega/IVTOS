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
using System.Windows.Shapes;

namespace IVTOS
{
    /// <summary>
    /// Logica di interazione per IscrizioneSquadraTorneo.xaml
    /// </summary>
    public partial class IscrizioneSquadraTorneo : Window
    {
        public IscrizioneSquadraTorneo(int idSquadra)
        {
            InitializeComponent();
            lblTitolo.Content = "Stai iscrivendo la squadra: " + Queries.GetOneField("SELECT nome FROM squadra WHERE IdSquadra = " + idSquadra);
            dbTornei.ItemsSource = Queries.GetDataSet("SELECT * FROM torneo").Tables[0].DefaultView;
        }
    }
}
