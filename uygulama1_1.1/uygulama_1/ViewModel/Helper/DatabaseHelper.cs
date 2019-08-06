using databaseremovebutton.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using databaseremovebutton.ViewModel;
using databaseremovebutton.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace databaseremovebutton.Helper
{
    public class DatabaseHelper:INotifyPropertyChanged
    {
        SQLiteConnection SqlConnection;
        SQLiteCommand SqlCommand;
        SQLiteDataReader SqlReader;

        private ObservableCollection<PersonelModel> listpersonelModels;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged == null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public ObservableCollection<PersonelModel> ListPersonelModel
        {
            get { return listpersonelModels; }
            set
            {
                listpersonelModels = value;
            }
        }
        public DatabaseHelper()
        {
            listpersonelModels = new ObservableCollection<PersonelModel>();
            RefleshDataBase();
        }
        public void RefleshDataBase()
        {
            SqlConnection = new SQLiteConnection("Data Source=db.db;");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
            SqlCommand.CommandText = "select * from personels";
            SqlCommand.ExecuteNonQuery();
            SqlReader = SqlCommand.ExecuteReader();
            ListPersonelModel.Clear();
            while (SqlReader.Read())
            {

                // ListPersonelModel.Add(new PersonelModel() { Id = Convert.ToInt32(SqlReader["id"]), Name = SqlReader["name"].ToString(), Address = SqlReader["address"].ToString(), Image = (string)SqlReader["image"]});  
                ListPersonelModel.Add(new PersonelModel() { Id = Convert.ToInt32(SqlReader["id"]), Name = SqlReader["name"].ToString(), Address = SqlReader["address"].ToString(), Image = SqlReader["image"].ToString()});
            }
            SqlReader.Close();
            SqlConnection.Close();
        }
        public void AddDataBase(PersonelModel personModel)
        {
            /*
            // SqlExecute("INSERT INTO personels VALUES ('" + personModel.Id + "','" + personModel.Name + "','" + personModel.Address + "');");
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.CommandText = String.Format("INSERT INTO personels VALUES ('" + personModel.Id + "','" + personModel.Name + "','" + personModel.Address + "' ,@0);");
            SQLiteParameter param = new SQLiteParameter("@0", System.Data.DbType.Binary);
            param.Value = personModel.Image;
            cmd.Parameters.Add(param);
            SqlExecute(cmd);
            */

            SqlExecute( "INSERT INTO personels VALUES ('" + personModel.Id + "','" + personModel.Name + "','" + personModel.Address + "' ,'" + personModel.Image+"');");

        }
        public void DeleteDataBase(int i)
        {
            SqlExecute("delete from personels where id= '" + i + "'");
        }
        public void UpdateDataBase(PersonelModel personelModel)
        {

            SqlExecute( "update personels set name='" + personelModel.Name + "',address='" + personelModel.Address + "',image='" +  personelModel.Image + "' where id='" + personelModel.Id + "';");

        }
        private void SqlExecute(string query)
        {
            SqlConnection = new SQLiteConnection("Data Source=db.db;");
            SqlConnection.Open();
            SqlCommand = SqlConnection.CreateCommand();
            SqlCommand.CommandText = query;
            SqlCommand.ExecuteNonQuery();
            RefleshDataBase();
            SqlReader.Close();
            SqlConnection.Close();
           
        }
    }
}
