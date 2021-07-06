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

using Oracle.DataAccess.Client;
using System.Data;

namespace ProgramHotel
{
    /// <summary>
    /// Interaction logic for MasterCustomer.xaml
    /// </summary>
    public partial class MasterCustomer : Window
    {
        private DataSet ds;

        public MasterCustomer()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            OracleDataAdapter adapter = new OracleDataAdapter("select * from customer", App.Connection);
            ds = new DataSet();
            adapter.Fill(ds, "customer");

            dataGrid.ItemsSource = ds.Tables["customer"].DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand("insert into customer values(:kode,:nama,:alamat,:kota,:telepon)", App.Connection);
            cmd.Parameters.Add(":kode", kodeCustTxt.Text);
            cmd.Parameters.Add(":nama", namaTxt.Text);
            cmd.Parameters.Add(":alamat", alamatTxt.Text);
            cmd.Parameters.Add(":kota", kotaTxt.Text);
            cmd.Parameters.Add(":telepon", teleponTxt.Text);

            App.Connection.Open();

            cmd.ExecuteNonQuery();

            App.Connection.Close();

            LoadData();
        }

        private void GetKodeButton_Click(object sender, RoutedEventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = App.Connection;
            cmd.CommandText = "autogen_cust";
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter retVal = new OracleParameter();
            retVal.Direction = ParameterDirection.ReturnValue;
            retVal.OracleDbType = OracleDbType.Varchar2;
            retVal.Size = 10;
            cmd.Parameters.Add(retVal);

            //OracleParameter param2 = new OracleParameter();
            //param2.Direction = ParameterDirection.Input;
            //param2.OracleDbType = OracleDbType.Int32;
            //param2.ParameterName = "param2";
            //param2.Value = ....;
            //cmd.Parameters.Add(param2);

            App.Connection.Open();

            cmd.ExecuteNonQuery();

            App.Connection.Close();

            kodeCustTxt.Text = retVal.Value.ToString();
        }
    }
}
