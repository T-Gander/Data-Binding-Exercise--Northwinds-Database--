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
        OleDbConnection DBconnection;               //Objects needed to interact with my database
        OleDbCommand customerCommand;
        OleDbDataAdapter customerAdapter;
        DataTable CustomersTable;
        BindingManagerBase customerManager;         //Binding manager base is a collection of all binded objects with the same source.

        public frmNorthwind()
        {
            InitializeComponent();
        }

        private void frmNorthwind_Load(object sender, EventArgs e)
        {
            string databaseDirectory = "../../../database/Northwind.accdb";     //Better way of writing this?           //Used to establish the location of database

            //Connection string needed to connect to database (unsecure), x64 debug platform mode must be set in order to run the application.
            string connectionString = $@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source={databaseDirectory}; Persist Security Info = False;"; 

            DBconnection = new OleDbConnection(connectionString);   //Creates the database connection.
            DBconnection.Open();                                    //Opens the connection

            customerCommand = new OleDbCommand("Select * From Customers", DBconnection);    //How to add an SQL command using the db connection.
            customerAdapter = new OleDbDataAdapter(customerCommand);                        //Used to convert the database into C# readable language.
            CustomersTable = new DataTable();                                               //Creates a datatable required to hold the SQL query/command.
            customerAdapter.Fill(CustomersTable);                                           //Fill method is then used to add the query to the datatable.
            customerManager = BindingContext[CustomersTable];                               //Customer manager is used to easily navigate all records at once connected in a datatable. BindingContext is an array of bindings connected to a data source that can be selected using a dataset as the index (datatable in this case).

            txtCustomerID.DataBindings.Add(new Binding("Text", CustomersTable, "CustomerID"));      //Used to add a databinding to a control. Using the controls output type, data source and then column string.
            txtCompanyName.DataBindings.Add(new Binding("Text", CustomersTable, "CompanyName"));
            txtContactTitle.DataBindings.Add(new Binding("Text", CustomersTable, "ContactTitle"));
            txtContactName.DataBindings.Add(new Binding("Text", CustomersTable, "ContactName"));

            DBconnection.Close();           //Closes and disposes of all connection type objects as the data table is filled with all relevant info.
            DBconnection.Dispose();
            customerAdapter.Dispose();
        }
        

        //Navigation buttons using position, (basically an index) of the BindingManagerBase

        private void btnFirstRecord_Click(object sender, EventArgs e)
        {
            customerManager.Position = 0;
        }

        private void btnPreviousRecord_Click(object sender, EventArgs e)
        {
            customerManager.Position--;
        }

        private void btnNextRecord_Click(object sender, EventArgs e)
        {
            customerManager.Position++;
        }

        private void btnLastRecord_Click(object sender, EventArgs e)
        {
            customerManager.Position = CustomersTable.Rows.Count - 1;
        }
    }
}
