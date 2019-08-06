using databaseremovebutton.Model;
using databaseremovebutton.ViewModel;
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

namespace databaseremovebutton.View
{
    /// <summary>
    /// Interaction logic for UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindowViewModel updateWindowViewModel;
        public UpdateWindow(MainWindowViewModel mainWindowViewModel, PersonelModel personelModel)
        {
            updateWindowViewModel = new UpdateWindowViewModel(mainWindowViewModel, personelModel, this);
            InitializeComponent();

            this.DataContext = updateWindowViewModel;
        }

    }
}
