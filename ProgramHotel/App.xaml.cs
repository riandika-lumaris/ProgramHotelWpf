using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Oracle.DataAccess.Client;

namespace ProgramHotel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static OracleConnection Connection =
            new OracleConnection("Data source=localhost:1521/orcl;User id=hotel;Password=hotel");
    }
}
