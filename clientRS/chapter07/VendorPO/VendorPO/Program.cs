using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Net;

namespace VendorPO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Daily Vendor PO Summary Report";

            LocalReport rpvVendorPO = new LocalReport();

            //declare connection string
            string cnString = "Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

            //declare Connection, command and other related objects
            SqlConnection conReport = new SqlConnection(cnString);
            SqlCommand cmdReport = new SqlCommand();
            SqlDataReader drReport;
            DataSet dsReport = new dsVendorPO();

            try
            {
                //open connection
                conReport.Open();

                //prepare connection object to get the data through reader and populate into dataset
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = "SELECT * FROM tblVendorPO ORDER BY PurchaseOrderID";

                //read data from command object
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset
                dsReport.Tables[0].Load(drReport);

                //close reader and connection
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer
                rpvVendorPO.ReportEmbeddedResource = "VendorPO.rptVendorPO.rdlc";

                //setup report parameter to pass vendor name
                ReportParameter[] Param = new ReportParameter[1];
                Param[0] = new ReportParameter("parVendorName", "ABCD1234 Inc.");

                rpvVendorPO.SetParameters(Param);

                //prepare report data source
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsVendorPO_dtPOList";
                rds.Value = dsReport.Tables[0];
                rpvVendorPO.DataSources.Add(rds);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = rpvVendorPO.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                using (FileStream fs = new FileStream("output.pdf", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                //upload fle to FTP site
                UploadToFTP("output.pdf");
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

        static void UploadToFTP(string FileToUpload)
        {
            FileInfo FileInformation = new FileInfo(FileToUpload);
            string FTPUri = "ftp://localhost/" + FileInformation.Name;
            FtpWebRequest FTPRequest;

            //Create FtpWebRequest object and passing Creditails
            FTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://localhost/" + FileInformation.Name));
            //FTPRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            FTPRequest.Credentials = new NetworkCredential();

            //Close request after command is executed
            FTPRequest.KeepAlive = false;

            //Specify the command to be executed and mode
            FTPRequest.Method = WebRequestMethods.Ftp.UploadFile;
            FTPRequest.UseBinary = true;

            //File size notification
            FTPRequest.ContentLength = FileInformation.Length;

            int buffLength = 2048; //2kb 
            byte[] buff = new byte[buffLength];
            int contentLen;

            //Open the file stream
            FileStream FTPFs = FileInformation.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream StreamRequest = FTPRequest.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = FTPFs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the 
                    // FTP Upload Stream
                    StreamRequest.Write(buff, 0, contentLen);
                    contentLen = FTPFs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                StreamRequest.Close();
                FTPFs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to contine...");
                Console.Read();
            }
        }
    }
}
