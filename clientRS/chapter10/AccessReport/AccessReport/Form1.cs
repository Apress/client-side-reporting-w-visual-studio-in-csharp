using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Reporting.WinForms;

namespace AccessReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // connection string
            string cnString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Apress\chapter10\nwind.mdb;User Id=admin;Password=;";

            // string builder for query text

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT  Products.ProductName, Categories.CategoryName, Products.QuantityPerUnit, Products.UnitsInStock ");
            sb.Append("FROM Categories INNER JOIN Products ON Categories.CategoryID = Products.CategoryID ");
            sb.Append("ORDER BY Products.ProductName");

            OleDbConnection conReport = new OleDbConnection(cnString);
            OleDbCommand cmdReport = new OleDbCommand();
            OleDbDataReader drReport;

            DataSet dsReport = new dsAccess();

            try
            {
                // open connection
                conReport.Open();

                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                
                // get query string from string builder
                cmdReport.CommandText = sb.ToString();

                // execute query and load result to dataset
                drReport = cmdReport.ExecuteReader();
                dsReport.Tables[0].Load(drReport);

                // close connection
                drReport.Close();
                conReport.Close();
                
                // prepare report for view
                reportViewer1.LocalReport.ReportEmbeddedResource = "AccessReport.rptAccess.rdlc";
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsAccess_dtAccess";
                rds.Value = dsReport.Tables[0];
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
    }
}