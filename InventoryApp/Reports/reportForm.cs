using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;
using InventoryApp.Logic;
using Microsoft.Reporting.WinForms;

namespace InventoryApp.Reports
{
    public partial class reportForm : Form
    {
        public reportForm()
        {
            InitializeComponent();
        }
        #region "Methods"
        private void itemsList()
        {
            SQLiteConnection SqlCon = new SQLiteConnection();
            try
            {
                SqlCon = Connection.getInstance().createConection();
                string SqlCommand = "select id,name,quantity,brand,category from itemsTb";
                SQLiteDataAdapter da = new SQLiteDataAdapter(SqlCommand, SqlCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ReportDataSource source = new ReportDataSource("DataSet1", ds.Tables[0]);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(source);
                reportViewer1.LocalReport.ReportEmbeddedResource = "InventoryApp.Reports.itemsReport.rdlc";
                reportViewer1.LocalReport.Refresh();
                reportViewer1.Refresh();
                reportViewer1.RefreshReport();
            } catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }
        #endregion
        private void reportForm_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
            this.itemsList();
        }
    }
}
