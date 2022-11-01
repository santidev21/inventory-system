using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace InventoryApp.Logic
{
    public class Connection
    {
        private string db;
        private static Connection Con = null;

        private Connection()
        {
            this.db = "./data.db";
        }

        public SQLiteConnection createConection()
        {
            SQLiteConnection c = new SQLiteConnection();
            try
            {
                c.ConnectionString = "Data Source=" + this.db;
            }
            catch (Exception ex)
            {
                c = null;
                throw ex;
            }
            return c;
        }

        public static Connection getInstance()
        {
            if (Con == null)
            {
                Con = new Connection();
            }
            return Con;
        }
    }
}
