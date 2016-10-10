using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace AddressList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //declare connection string, please substitute DataSource with your Server name
            string cnString = "Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

            //declare Connection, command and other related objects
            SqlConnection conReport = new SqlConnection(cnString);
            SqlCommand cmdReport = new SqlCommand();
            SqlDataReader drReport;
            DataSet dsReport = new dsAddressList();

            try
            {
                //open connection
                conReport.Open();

                //prepare connection object to get the data through reader and populate into dataset
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = "Select * FROM dbo.AddressList";

                //read data from command object
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset
                dsReport.Tables[0].Load(drReport);

                //close reader and connection
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer
                reportViewer1.LocalReport.ReportEmbeddedResource = "AddressList.rptAddressList.rdlc";

                // you need to set this to show multi column output in report viewer
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

                // set the zoom mode of report viewer to 100%
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;

                //prepare report data source
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsAddressList_dtAddressList";
                rds.Value = dsReport.Tables[0];
                reportViewer1.LocalReport.DataSources.Add(rds);

                //load report viewer
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                //display generic error message back to user
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //check if connection is still open then attempt to close it
                if (conReport.State == ConnectionState.Open)
                {
                    conReport.Close();
                }
            }
        }
    }
}