using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Binding_Exercise___Northwinds_Database
{
    public partial class frmNorthwind : Form
    {
        public frmNorthwind()
        {
            InitializeComponent();
        }

        

        private void frmNorthwind_Load(object sender, EventArgs e)
        {
            OleDbConnection DBconnection;

            string databaseDirectory = "../../../database/Northwind.accdb";     //Better way of writing this?

            string connectionString = $@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source={databaseDirectory}; Persist Security Info = False;";

            DBconnection = new OleDbConnection(connectionString);

            DBconnection.Open();
        }
    }
}
