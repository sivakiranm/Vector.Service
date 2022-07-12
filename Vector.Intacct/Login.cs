using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Vector.Common.BusinessLayer;
using Vector.Intacct.APIAccess;
using Vector.Intacct.BusinessLogic;
using Vector.Intacct.IntacctUI;

namespace Vector.Intacct
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.AcceptButton = this.btnSubmit;
            this.txtUserName.AutoSize = false;
            this.txtPassword.AutoSize = false;
            this.txtUserName.Size = new System.Drawing.Size(231, 27);
            this.txtPassword.Size = new System.Drawing.Size(231, 27);
            this.AcceptButton.NotifyDefault(false);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateLogin();
            }
            catch (Exception ex)
            {
                ShowAlert("Error", ex.Message);
            }
            finally
            {
                LoadProgress(false);
            }
        }

        private void ValidateLogin()
        {
            bool result = ValidateUser();
            if (result)
            {
                // after we've done all the processing, 
                this.Invoke(new MethodInvoker(delegate
                {
                    // load the control with the appropriate data
                    if (result)
                    {
                        frmIntacct objfrmIntacct = new frmIntacct();
                        objfrmIntacct.Show();
                        this.Hide();
                    }
                }));
            }
            else
            {
                ShowAlert("Validation", "Please enter valid Username & Password..!");
            }
            return;
        }

        private bool ValidateUser()
        {
            using (IntacctBOLayer objIntacctBOLayer = new IntacctBOLayer())
            {
                LoadProgress(true);
                var vectorResponseObj = (VectorResponse<object>)objIntacctBOLayer.ValidateUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                if(vectorResponseObj == null)
                {
                    ShowAlert("Warning", "Kindly Check UserName & Password");
                }
                else if (vectorResponseObj.Error != null)
                {
                    ShowAlert("Warning", vectorResponseObj.Error.ErrorDescription);
                }
                else
                {
                    SecurityContext.Instance.UserDetails = JsonConvert.DeserializeObject<DataTable>(vectorResponseObj.ResponseData.ToString());
                    SecurityContext.Instance.LogInUserId = txtUserName.Text.Trim();
                    SecurityContext.Instance.UserId = Convert.ToInt32(SecurityContext.Instance.UserDetails.Rows[0]["userId"].ToString());
                    SecurityContext.Instance.VectorToken = SecurityContext.Instance.UserDetails.Rows[0]["vectorToken"].ToString();
                    SecurityContext.Instance.LogInPassword = txtPassword.Text.Trim();
                    return true;
                }
            }
            return false;
        }

        private void LoadProgress(bool isLoading)
        {
            if (isLoading)
                Cursor = Cursors.WaitCursor;
            else
                Cursor = Cursors.Default;
        }

        private void ShowAlert(string title, string mesg, string type = "Alert")
        {
            frmAlert objfrmAlert = new frmAlert(title, mesg, type);
            if (objfrmAlert.ShowDialog(this) == DialogResult.OK)
                LoadProgress(false);

            objfrmAlert.Dispose();
        }
    }
}
