using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using databaseremovebutton.Helper;
using databaseremovebutton.Model;
using databaseremovebutton.View;
using Microsoft.Win32;

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
        private string image;

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
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
        public ICommand AddImageCommand
        {
            get
            {
                if (addImageCommand == null)
                    addImageCommand = new RelayCommand(AddImage);
                return addImageCommand;
            }

        }

        #endregion

        #region ICommands (member)
        private ICommand addCommand;
        private ICommand addImageCommand;

        #endregion

        #region Method

        private void Add()
        {

            int sayac = 0;
            while (mainWindowViewModel.PersonList.Where(s => s.Id == sayac).Count() != 0)
                sayac++;
            //convert();
            PersonelModel personelModel = new PersonelModel()
            {
                Name = AddPersonModel.Name,
                Address = AddPersonModel.Address,
                Id = sayac,
                Image = convert(Image)
                
            };
            mainWindowViewModel.DatabaseHelper.AddDataBase(personelModel);
            if (MessageBox.Show("Başka Eklemek İstermisiniz?", "Question",MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                CloseEvent.Invoke(addWindow, null);
            }

           
            
        }
        //düzenle------------------------------------
        private void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // convert(openFileDialog.FileName);
                Image = openFileDialog.FileName;
            }
        }
        private string convert(string imgg)
        {
            Image img = new Image();
            BitmapImage bimg = new BitmapImage();
            bimg.BeginInit();
            bimg.UriSource = new Uri(imgg);
            bimg.EndInit();

            byte[] data;
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bimg));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            string bs64 = Convert.ToBase64String(data);
            return bs64;
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
