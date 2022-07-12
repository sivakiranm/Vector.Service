using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vector.Common.BusinessLayer;
using Vector.Common.DataLayer;
using Vector.Garage.BusinessLayer;
using Vector.Garage.Entities;
using Vector.Master.BusinessLayer;

namespace PRFileDownload
{
    public partial class ReGenerateInvoice : Form
    {
        string connectionString = "Server=tcp:vectorplatformdevserver.database.windows.net,1433;Initial Catalog=vectorplatform-QA-9162021;Persist Security Info=False;User ID=dbadmin;Password=vectorplatform@2;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        string requestURL = "http://40.90.236.168/VectorAPI/api/Invoice/GenerateInvoice";

        string token = "37C2553C-EEEE-4A31-9354-D6FC1EE29969";
        string appVersion = "vectorVersion=22121985";

        public ReGenerateInvoice()
        {
            InitializeComponent();
        }


        private void btnREgenerateINvoices_Click(object sender, EventArgs e)
        {
            using (var objMasterDataBL = new MasterDataBL(new VectorDataConnection(connectionString)))
            {

                int total = 0;
                int CurrentCount = 0;
                DataSet DSData = objMasterDataBL.GetDownloadInfo("GetInvocesForAutoRegenerate", "");



                //string requestURL = "http://13.67.216.228/VectorDemoAPI/api/Invoice/GenerateInvoice";
                string requestData = "";
                string requestType = "POST";
                string statusCode = "";
                string contentType = "application/json";

                total = DSData.Tables[0].Rows.Count;

                foreach (DataRow dr in DSData.Tables[0].Rows)
                {
                    CurrentCount = CurrentCount + 1; 
                    //requestData = Convert.ToString(new StringContent(JsonConvert.SerializeObject(objInvoiceInfo)));
                    //var obj = WebRequestManager.SubmitRequest(requestURL, requestData, requestType, ref statusCode, contenttype: contentType,
                    //                                header: "rstoken:" + token + "|appversion:" + "vectorVersion=" + appVersion, isEncodingRequire: true);





                    try
                    {
                        DataSet dsInvo = objMasterDataBL.RunCalculationsServiceForInvoice(Convert.ToInt64(dr["InvoiceId"]));
                        InvoiceInfo objInvoiceInfo = new InvoiceInfo();
                        objInvoiceInfo.Action = "Summary";

                        objInvoiceInfo.InvoiceId = Convert.ToInt64(dr["InvoiceId"]);

                        bool resultSummary = GenerateSummary(objInvoiceInfo);
                        bool resultVector = GenerateVector(objInvoiceInfo);
                        bool resultVendor = GenerateVendor(objInvoiceInfo);


                        //Console.WriteLine("Response " + res.Result.Content.ReadAsStringAsync().Result + Environment.NewLine);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error " + ex.Message + " Error " +
                        ex.ToString());
                    }



                }

            }

        }

        private bool GenerateSummary(InvoiceInfo objInvoiceInfo)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("rstoken", token);
            client.DefaultRequestHeaders.Add("appversion", appVersion);

            objInvoiceInfo.Action = "Summary";
            var content = new StringContent(JsonConvert.SerializeObject(objInvoiceInfo), Encoding.UTF8, "application/json");

            var res = client.PostAsync(requestURL, content);

            res.Result.EnsureSuccessStatusCode();
            return true;
        }

        private bool GenerateVector(InvoiceInfo objInvoiceInfo)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("rstoken", token);
            client.DefaultRequestHeaders.Add("appversion", appVersion);

            objInvoiceInfo.Action = "Savings";
            var content = new StringContent(JsonConvert.SerializeObject(objInvoiceInfo), Encoding.UTF8, "application/json");

            var res = client.PostAsync(requestURL, content);

            res.Result.EnsureSuccessStatusCode();
            return true;
        }

        private bool GenerateVendor(InvoiceInfo objInvoiceInfo)
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("rstoken", token);
            client.DefaultRequestHeaders.Add("appversion", appVersion);

            objInvoiceInfo.Action = "Vendor";
            var content = new StringContent(JsonConvert.SerializeObject(objInvoiceInfo), Encoding.UTF8, "application/json");

            var res = client.PostAsync(requestURL, content);

            res.Result.EnsureSuccessStatusCode();
            return true;
        }
    }
}
