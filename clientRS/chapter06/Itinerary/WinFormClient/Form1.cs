using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinFormClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                WinGenItinerary.Service GenerateItinerarySrv = new WinGenItinerary.Service();

                // call to WebMethod and store the result
                byte[] GenItineraryResult = GenerateItinerarySrv.GenerateItinerary();

                // read through bytes arry and generate pdf file
                using (FileStream fs = new FileStream(@"c:\Itinerary.pdf", FileMode.Create))
                {
                    fs.Write(GenItineraryResult, 0, GenItineraryResult.Length);
                }

                MessageBox.Show(@"Itinerary saved as c:\Itinerary.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}