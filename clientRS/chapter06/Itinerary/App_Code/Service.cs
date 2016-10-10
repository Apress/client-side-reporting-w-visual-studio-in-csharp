using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public byte[] GenerateItinerary()
    {
        LocalReport rpvItinerary = new LocalReport();

        //declare connection string
        string cnString = "Data Source=(local);Initial Catalog=RealWorld;Integrated Security=SSPI;";

        //declare Connection, command and other related objects
        SqlConnection conReport = new SqlConnection(cnString);
        SqlCommand cmdReport = new SqlCommand();
        SqlDataReader drReport;
        DataSet dsReport = new dsItinerary();

        try
        {
            //open connection
            conReport.Open();

            //prepare connection object to get the data through reader and populate into dataset
            cmdReport.CommandType = CommandType.Text;
            cmdReport.Connection = conReport;
            cmdReport.CommandText = "Select TOP 1 * FROM Dbo.Itinerary";

            //read data from command object
            drReport = cmdReport.ExecuteReader();

            //load data directly from reader to dataset
            dsReport.Tables[0].Load(drReport);

            //close reader and connection
            drReport.Close();
            conReport.Close();

            //provide local report information to viewer
            rpvItinerary.ReportPath = "rptItinerary.rdlc";

            //prepare report data source
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "dsItinerary_dtItinerary";
            rds.Value = dsReport.Tables[0];
            rpvItinerary.DataSources.Add(rds);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            byte[] bytes = rpvItinerary.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            return bytes;
        }
        catch
        {
            return null;
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
