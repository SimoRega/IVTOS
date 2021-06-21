using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace IVTOS
{
    class Queries
    {
        private static string connection;

        public static string Connection { get => connection; set => connection = value; }

        static DataSet Execute(string query)
        {
            connection = "Persist Security Info=False;database=ivtos;server=localhost;port=3306;user id=root;Password=password;";
            DataSet ds = new DataSet();
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = new MySqlCommand(query, conn);
            adapter.Fill(ds);
            conn.Close();
            return ds;
        }

        static public string GetOneField(string query)
        {
            DataSet ds = Execute(query);
            return ds.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        static public List<string> GetListOfField(string query)
        {
            List<string> list = new List<string>();
            DataSet ds = Execute(query);

            foreach(var x in ds.Tables[0].Rows)
            {
                list.Add(x.ToString());
            }
            return list;
        }

        static public DataSet GetDataSet(string query)
        {
            return Execute(query);
        }

        static public void ExecuteOnly(string query)
        {
            Execute(query);
        }

        static public void ExecuteFile(string query, string connExecute)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connExecute);
                MySqlScript s = new MySqlScript(conn, query);
                s.Execute();
                conn.Close();
                MessageBox.Show("Operazione andata a buon fine!", "Installazione Riuscita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Stringa di connessione al DB errata, riprovare", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
