using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databaseremovebutton.Model;
using databaseremovebutton.ViewModel;
namespace databaseremovebutton.ViewModel
{
     public class SqlConnect
    {

        SQLiteConnection SqlConnection;
        SQLiteCommand SqlCommand;
        SQLiteDataReader SqlReader;
        //public MainWindowViewModel mainWindowViewModel;
        public SqlConnect(MainWindowViewModel mainWindowViewModel)
        {
           // this.mainWindowViewModel = mainWindowViewModel;
            Connect(mainWindowViewModel);
        }
        //databasehelper (get person) databaseprovider bağlantı





        private void Connect(MainWindowViewModel mainWindowViewModel)
        {
            
            SqlCommand.CommandText = "select * from personels";
            SqlCommand.ExecuteNonQuery();
            SqlReader = SqlCommand.ExecuteReader();
            mainWindowViewModel.PersonList.Clear();
            int sayac = 0;
            while (SqlReader.Read())
            {
                PersonelModel personelModel = new PersonelModel();
                
                personelModel.Id = Convert.ToInt32( SqlReader["id"]);
                personelModel.Name = SqlReader["name"].ToString();
                personelModel.Address = SqlReader["address"].ToString();
                
                /*
                personelModel.Id = (int)SqlReader[0];
                personelModel.Name = SqlReader[1].ToString();
                personelModel.Address = SqlReader[2].ToString();
                */

                mainWindowViewModel.PersonList.Add(personelModel);
            }
            //sqlText = SqlCommand.ExecuteScalar().ToString();
            SqlConnection.Close();
            // mainWindowViewModel.PersonList = SqlReader.read;

        }
        public void Reflesh()
        {

        }
    }
}
