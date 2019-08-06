using databaseremovebutton.Helper;
using databaseremovebutton.Model;
using databaseremovebutton.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace databaseremovebutton.ViewModel
{
    public class UpdateWindowViewModel:INotifyPropertyChanged
    {
        private MainWindowViewModel mainWindowViewModel;
        private PersonelModel personelModel;
        UpdateWindow updateWindow;
        public PersonelModel PersonelModel
        {
            get { return personelModel; }
            set { personelModel = value; }
        }

        public UpdateWindowViewModel(MainWindowViewModel mainWindowViewModel,PersonelModel personelModel,UpdateWindow updateWindow)
        {
            this.updateWindow = updateWindow;
            this.personelModel = new PersonelModel();
            PersonelModel.Id = personelModel.Id;
            PersonelModel.Name = personelModel.Name;
            PersonelModel.Address = personelModel.Address;
            this.mainWindowViewModel = mainWindowViewModel;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,new PropertyChangedEventArgs(property));
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                    updateCommand = new RelayCommand(Update);
                return updateCommand;
            }
        }
        public event EventHandler CloseEvent;
        private void Update()
        {
            mainWindowViewModel.DatabaseHelper.UpdateDataBase(PersonelModel);
            CloseEvent.Invoke(updateWindow,null);
        }
    }
}
