using databaseremovebutton.Helper;
using databaseremovebutton.Model;
using databaseremovebutton.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            set { personelModel = value;
                
            }
        }

        public UpdateWindowViewModel(MainWindowViewModel mainWindowViewModel,PersonelModel personelModel,UpdateWindow updateWindow)
        {
            this.updateWindow = updateWindow;
            this.personelModel = new PersonelModel();
            PersonelModel.Id = personelModel.Id;
            PersonelModel.Name = personelModel.Name;
            PersonelModel.Address = personelModel.Address;
            PersonelModel.Image = personelModel.Image;
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
        private ICommand updateImageCommand;
        public ICommand UpdateImageCommand
        {
            get
            {
                if (updateImageCommand == null)
                    updateImageCommand = new RelayCommand(UpdateImage);
                return updateImageCommand;
            }
        }
        private void UpdateImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                PersonelModel.Image = convert(openFileDialog.FileName);
                OnPropertyChanged("PersonelModel");

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

    }
}
