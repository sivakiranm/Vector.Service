
namespace PRFileDownload
{
    partial class ReGenerateInvoice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnREgenerateINvoices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnREgenerateINvoices
            // 
            this.btnREgenerateINvoices.Location = new System.Drawing.Point(104, 47);
            this.btnREgenerateINvoices.Name = "btnREgenerateINvoices";
            this.btnREgenerateINvoices.Size = new System.Drawing.Size(112, 34);
            this.btnREgenerateINvoices.TabIndex = 0;
            this.btnREgenerateINvoices.Text = "RegenerateInvoices";
            this.btnREgenerateINvoices.UseVisualStyleBackColor = true;
            this.btnREgenerateINvoices.Click += new System.EventHandler(this.btnREgenerateINvoices_Click);
            // 
            // ReGenerateInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnREgenerateINvoices);
            this.Name = "ReGenerateInvoice";
            this.Text = "ReGenerateInvoice";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnREgenerateINvoices;
    }
}