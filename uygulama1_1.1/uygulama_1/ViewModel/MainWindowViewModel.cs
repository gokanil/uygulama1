using databaseremovebutton.Helper;
using databaseremovebutton.Model;
using databaseremovebutton.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace databaseremovebutton.ViewModel
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
       

        
        #region Members
        /// <summary>
        /// personel listesini tutar
        /// </summary>
        private ObservableCollection<PersonelModel> personList;
        private PersonelModel selectedPersonModel;
        private PersonelModel updatePersonModel;
        private PersonelModel addPersonModel;
        private MainWindow mainWindow;
        private AddWindow addWindow;
        private UpdateWindow updateWindow;
        private DatabaseHelper _databaseHelper;

        #endregion
        #region Properties


        public PersonelModel SelectedPersonModel
        {
            get { return selectedPersonModel; }
            set
            {
                if (value != null)
                {
                    selectedPersonModel = value;
                    UpdatePersonModel.Id = SelectedPersonModel.Id;
                    UpdatePersonModel.Name = SelectedPersonModel.Name;
                    UpdatePersonModel.Address = SelectedPersonModel.Address;
                    OnPropertyChanged(nameof(SelectedPersonModel));
                }
                
            }
        }
        public DatabaseHelper DatabaseHelper
        {
            get
            {
                // return _databaseHelper;
                //return _databaseHelper ?? _databaseHelper = new DatabaseHelper(this);
                if (_databaseHelper == null)
                    _databaseHelper = new DatabaseHelper();
                return _databaseHelper;
            }
        }

        public PersonelModel UpdatePersonModel
        {
            get { return updatePersonModel; }
            set
            {
                updatePersonModel = value;
                OnPropertyChanged(nameof(UpdatePersonModel));
            }
        }

        /*
            member
            properties
            constructor
            Icommand
            Commands
            events
            methods
            */

        public PersonelModel AddPersonModel
        {
            get { return addPersonModel; }
            set
            {
                addPersonModel = value;
                OnPropertyChanged(nameof(AddPersonModel));
            }
        }

        /// <summary>
        /// Personel listesini tutar
        /// </summary>
        public ObservableCollection<PersonelModel> PersonList
        {
            get { return personList; }
            set
            {
                personList = value;
            }
        }

        #endregion
        #region Constructor
        public MainWindowViewModel(MainWindow mainWindow)
        {

            this.mainWindow = mainWindow;
            UpdatePersonModel = new PersonelModel();
            AddPersonModel = new PersonelModel();
            //PersonList = new ObservableCollection<PersonelModel>();
            PersonList = DatabaseHelper.ListPersonelModel;
            // PersonList.Add(new Model.PersonelModel() { Id = 0, Name = "asd", Address = "dsf" });
            

        }

        #endregion

        #region Commands
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                    updateCommand = new RelayCommand(Update);
                return updateCommand;
            }

        }

        public ICommand AddWindowCommand
        {
            get
            {
                if (addWindowCommand == null)
                    addWindowCommand = new RelayCommand(AddWindow);
                return addWindowCommand;
            }

        }
        public ICommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                    removeCommand = new RelayCommand(Remove);
                return removeCommand;
            }
        }
        #endregion

        #region ICommands (member)
        private ICommand updateCommand;
        private ICommand removeCommand;
        private ICommand addWindowCommand;

        #endregion

        #region Method
        private void AddWindow()
        {
            addWindow = new AddWindow(this);
            addWindow.addWindowViewModel.CloseEvent += Window_CloseEvent;
            // addWindow.DataContext = this;
          //  addWindow.Owner = this ;
            
            addWindow.Show();
        }

        private void Window_CloseEvent(object sender, EventArgs e)
        {
            // addWindow.Close();
            Window window = sender as Window;
            window.Close();
            
        }

        /// <summary>
        /// update butonuna basıldığında tetiklenen yer
        /// </summary>
        private void Update()
        {
            if (SelectedPersonModel != null)
            {
                updateWindow = new UpdateWindow(this, SelectedPersonModel);
                updateWindow.updateWindowViewModel.CloseEvent += Window_CloseEvent;
                updateWindow.Show();
            }
        }
        private void Remove()
        {
            DatabaseHelper.DeleteDataBase(SelectedPersonModel.Id);
        }
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        #endregion
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Relaycommand
        private ICommand deleteCommand;
        private MainWindowViewModel mainWindowViewModel;

        public  ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                    deleteCommand = new RelayCommand<PersonelModel>(Delete);
                return deleteCommand;

            }
        }
        private void Delete(PersonelModel personelModel)
        {
            DatabaseHelper.DeleteDataBase(personelModel.Id);
        }
        #endregion

    }
}
