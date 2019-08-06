using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using databaseremovebutton.Helper;
using databaseremovebutton.Model;
using databaseremovebutton.View;

namespace databaseremovebutton.ViewModel
{
    public class AddWindowViewModel:INotifyPropertyChanged
    {




        #region Members
        private PersonelModel addPersonModel;
        private MainWindowViewModel mainWindowViewModel;
        private AddWindow addWindow;

        #endregion

        #region Constructor
        public AddWindowViewModel(MainWindowViewModel mainWindowViewModel,AddWindow addWindow)
        {
            AddPersonModel = new PersonelModel();
            this.mainWindowViewModel = mainWindowViewModel;

            this.addWindow = addWindow;
            
        }
        #endregion
  
        #region Properties

        public PersonelModel AddPersonModel
        {
            get { return addPersonModel; }
            set
            {
                addPersonModel = value;
                OnPropertyChanged(nameof(AddPersonModel));
            }
        }
        #endregion

        #region Commands

        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                    addCommand = new RelayCommand(Add);
                return addCommand;
            }

        }

        #endregion

        #region ICommands (member)
        private ICommand addCommand;

        #endregion

        #region Method

        private void Add()
        {

            int sayac = 0;
            while (mainWindowViewModel.PersonList.Where(s => s.Id == sayac).Count() != 0)
                sayac++;

            PersonelModel personelModel= new PersonelModel()
            {
                Name = AddPersonModel.Name,
                Address = AddPersonModel.Address,
                Id = sayac
            };
            mainWindowViewModel.DatabaseHelper.AddDataBase(personelModel);
            if (MessageBox.Show("Başka Eklemek İstermisiniz?", "Question",MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                CloseEvent.Invoke(addWindow, null);
            }

           

        }
        public event EventHandler CloseEvent;

        /// <summary>
        /// update butonuna basıldığında tetiklenen yer
        /// </summary>

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
