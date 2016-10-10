using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;

namespace Complaint
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                //setup timer interval from settings file
                timer1.Interval = Convert.ToInt32(SettingsComplaint.Default.TimerDelay);

                //genearte report first time before calling according to time interval
                generateReport();

                timer1.Enabled = true;
                timer1.Start();

                using (StreamWriter sw = new StreamWriter(@"c:\ServiceStart.txt"))
                {
                    sw.WriteLine("service start");
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = new StreamWriter(@"c:\ServiceError.txt"))
                {
                    sw.WriteLine(ex.Message);
                }
            }
        }

        protected override void OnStop()
        {
            //notify via email if service is stoped
            //SendMail("SMTPserver", "Complaint", SettingsComplaint.Default.EmailTo, "Service Stoped!", "Contact IT support","");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            generateReport();
        }

        public void generateReport()
        {
            LocalReport rpvComplaint = new LocalReport();

            //declare connection string
            string cnString = "Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

            //declare Connection, command and other related objects
            SqlConnection conReport = new SqlConnection(cnString);
            SqlCommand cmdReport = new SqlCommand();
            SqlDataReader drReport;
            DataSet dsReport = new dsComplaint();

            try
            {
                //open connection
                conReport.Open();

                //prepare connection object to get the data through reader and populate into dataset
                cmdReport.CommandType = CommandType.Text;
                cmdReport.Connection = conReport;
                cmdReport.CommandText = "Select * FROM Complaint ORDER BY ComplaintLevel, ComplaintSource, ComplaintID";

                //read data from command object
                drReport = cmdReport.ExecuteReader();

                //load data directly from reader to dataset
                dsReport.Tables[0].Load(drReport);

                //close reader and connection
                drReport.Close();
                conReport.Close();

                //provide local report information to viewer
                rpvComplaint.ReportEmbeddedResource = "Complaint.rptComplaint.rdlc";

                //prepare report data source
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsComplaint_dtComplaintList";
                rds.Value = dsReport.Tables[0];
                rpvComplaint.DataSources.Add(rds);

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                byte[] bytes = rpvComplaint.Render(
                    "PDF", null, out mimeType, out encoding, out filenameExtension,
                    out streamids, out warnings);

                using (FileStream fs = new FileStream(@"c:\output.pdf", FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                //send newly create pdf file as email attachment
                //SendMail("SMTPserver", "Complaint", SettingsComplaint.Default.EmailTo, "Service Stoped!", "Contact IT support", "output.pdf");
            }
            catch (Exception ex)
            {
                string strInitialError = "Initial Error Message: " + ex.Message;

                string FinalErrorMessage = string.Empty;
                Exception innerError = ex.InnerException;
                while (!((innerError == null)))
                {
                    FinalErrorMessage += innerError.Message;
                    innerError = innerError.InnerException;
                }

                //write the error message to text file
                using (StreamWriter sw = new StreamWriter(@"c:\ServiceError.txt"))
                {
                    sw.WriteLine(strInitialError);
                    sw.WriteLine("-------------------");
                    sw.WriteLine(FinalErrorMessage);
                }
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

        public void SendMail(string mailServerName, string mailFrom, string MailTo, string subject, string body, string fileName)
        {
            try
            {
                //MailMessage represents the e-mail being sent
                using (MailMessage message = new MailMessage(mailFrom,
                       MailTo, subject, body))
                {
                    message.IsBodyHtml = true;
                    message.Attachments.Add(new Attachment(fileName));
                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Host = mailServerName;
                    mailClient.UseDefaultCredentials = true;
                    mailClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                    //Send delivers the message to the mail server
                    mailClient.Send(message);
                }
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                   ("Smtp error sending mail: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
