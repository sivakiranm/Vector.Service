﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vector.Intacct.IntacctUI
{
    public partial class frmAlert : Form
    {
        public frmAlert(string title, string mesg, string type = "Alert")
        {
            InitializeComponent();
            txtMesg.Text = mesg;
            lblTitle.Text = title;
            btnCancel.Visible = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK; 
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel; 
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
