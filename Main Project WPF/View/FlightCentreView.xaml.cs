using Main_Project.Facade;
using Main_Project_WPF.ViewModel;
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

namespace Main_Project_WPF.View
{
    /// <summary>
    /// Interaction logic for FlightCentreView.xaml
    /// </summary>
    public partial class FlightCentreView : Window
    {
        WpfFacade wpfFacade = new WpfFacade();

        public FlightCentreView()
        {
            InitializeComponent();
            DataContext = new FlightCentreViewModel();
        }
    }
}
