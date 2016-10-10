using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace Transfer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // data set at the class level to access by all methods
        DataSet dsReport = new dsTransfer();

        private void Form1_Load(object sender, EventArgs e)
        {
            // connection string
            string cnString = @"Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

            SqlConnection conReport = new SqlConnection(cnString);
            SqlCommand cmdReport = new SqlCommand();
            SqlDataReader drReport;

            try
            {
                // open connection
                conReport.Open();

                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;

                // get query string from string builder
                cmdReport.CommandText = "SELECT * FROM TransferHeader ORDER BY TransferID; SELECT * FROM TransferDetail ORDER BY TransferID";

                // execute query and load result to dataset
                drReport = cmdReport.ExecuteReader();
                //dsReport.Tables[1].Load(drReport);

                dsReport.Load(drReport, LoadOption.OverwriteChanges, dsReport.Tables[1], dsReport.Tables[0]);

                // close connection
                drReport.Close();
                conReport.Close();

                // prepare report for view
                reportViewer1.LocalReport.ReportEmbeddedResource = "Transfer.rptTransferHeader.rdlc";

                // Add a handler for SubreportProcessing
                reportViewer1.LocalReport.SubreportProcessing +=
                      new SubreportProcessingEventHandler(SubreportProcessingEventHandler);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsTransfer_dtTransferHeader";

                rds.Value = dsReport.Tables[1];
                reportViewer1.LocalReport.DataSources.Add(rds);

                // preivew the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conReport.State == ConnectionState.Open)
                {
                    conReport.Close();
                }
            }
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsTransfer_dtTransferDetail", dsReport.Tables[0]));
        }
    }
}