using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace CustomerEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Customer Email List by Country Report";

            LocalReport rpvCustomerEmail = new LocalReport();

            //declare connection string
            string cnString = "Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

            //declare Connection, command and other related objects
            SqlConnection conReport = new SqlConnection(cnString);
            SqlCommand cmdReport = new SqlCommand();
            SqlDataReader drReport;
            DataSet dsReport = new dsCustomerEmail();

            try
            {
                //open connection
                conReport.Open();

                //prepare connection object to get the data through reader and populate into dataset
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = "Select * FROM tblCustomerEmail Order By CountryRegionName";

                //read data from command object
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset
                dsReport.Tables[0].Load(drReport);

                //close reader and connection
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer
                rpvCustomerEmail.ReportEmbeddedResource = "CustomerEmail.rptCustomerEmail.rdlc";

                //prepare report data source
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsCustomerEmail_dtCustomerList";
                rds.Value = dsReport.Tables[0];
                rpvCustomerEmail.DataSources.Add(rds);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = rpvCustomerEmail.Render(
                    "Excel", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                using (FileStream fs = new FileStream(@"output.xls", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Initial Error Message: " + ex.Message);

                string FinalErrorMessage = string.Empty;
                Exception innerError = ex.InnerException;
                while (!((innerError == null)))
                {
                    FinalErrorMessage += innerError.Message;
                    innerError = innerError.InnerException;
                }

                Console.WriteLine("Final Error Message:" + FinalErrorMessage);
                Console.WriteLine("Press any key to exit");

                //Wait for user action of press nay key
                Console.ReadKey();
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
