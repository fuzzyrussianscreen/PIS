namespace PISLawyerView
{
    partial class FormMainLawyer
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
            this.buttonArch = new System.Windows.Forms.Button();
            this.buttonDIagr = new System.Windows.Forms.Button();
            this.buttonClient = new System.Windows.Forms.Button();
            this.buttonContract = new System.Windows.Forms.Button();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonContractList = new System.Windows.Forms.Button();
            this.buttonProgramm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonArch
            // 
            this.buttonArch.Location = new System.Drawing.Point(12, 17);
            this.buttonArch.Name = "buttonArch";
            this.buttonArch.Size = new System.Drawing.Size(198, 35);
            this.buttonArch.TabIndex = 0;
            this.buttonArch.Text = "Архивация";
            this.buttonArch.UseVisualStyleBackColor = true;
            this.buttonArch.Click += new System.EventHandler(this.ButtonArch_Click);
            // 
            // buttonDIagr
            // 
            this.buttonDIagr.Location = new System.Drawing.Point(12, 58);
            this.buttonDIagr.Name = "buttonDIagr";
            this.buttonDIagr.Size = new System.Drawing.Size(198, 35);
            this.buttonDIagr.TabIndex = 1;
            this.buttonDIagr.Text = "Диаграмма";
            this.buttonDIagr.UseVisualStyleBackColor = true;
            this.buttonDIagr.Click += new System.EventHandler(this.ButtonDIagr_Click);
            // 
            // buttonClient
            // 
            this.buttonClient.Location = new System.Drawing.Point(12, 99);
            this.buttonClient.Name = "buttonClient";
            this.buttonClient.Size = new System.Drawing.Size(198, 35);
            this.buttonClient.TabIndex = 2;
            this.buttonClient.Text = "Работа с клиентами";
            this.buttonClient.UseVisualStyleBackColor = true;
            this.buttonClient.Click += new System.EventHandler(this.ButtonClient_Click);
            // 
            // buttonContract
            // 
            this.buttonContract.Location = new System.Drawing.Point(12, 140);
            this.buttonContract.Name = "buttonContract";
            this.buttonContract.Size = new System.Drawing.Size(198, 48);
            this.buttonContract.TabIndex = 3;
            this.buttonContract.Text = "Заключение договора";
            this.buttonContract.UseVisualStyleBackColor = true;
            this.buttonContract.Click += new System.EventHandler(this.ButtonContract_Click);
            // 
            // buttonReport
            // 
            this.buttonReport.Location = new System.Drawing.Point(12, 245);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Size = new System.Drawing.Size(198, 35);
            this.buttonReport.TabIndex = 4;
            this.buttonReport.Text = "Работа с отчётами";
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.ButtonReport_Click);
            // 
            // buttonContractList
            // 
            this.buttonContractList.Location = new System.Drawing.Point(12, 194);
            this.buttonContractList.Name = "buttonContractList";
            this.buttonContractList.Size = new System.Drawing.Size(198, 45);
            this.buttonContractList.TabIndex = 5;
            this.buttonContractList.Text = "Работа с договорами";
            this.buttonContractList.UseVisualStyleBackColor = true;
            this.buttonContractList.Click += new System.EventHandler(this.ButtonContractList_Click);
            // 
            // buttonProgramm
            // 
            this.buttonProgramm.Location = new System.Drawing.Point(12, 286);
            this.buttonProgramm.Name = "buttonProgramm";
            this.buttonProgramm.Size = new System.Drawing.Size(198, 35);
            this.buttonProgramm.TabIndex = 6;
            this.buttonProgramm.Text = "О программе";
            this.buttonProgramm.UseVisualStyleBackColor = true;
            this.buttonProgramm.Click += new System.EventHandler(this.ButtonProgramm_Click);
            // 
            // FormMainLawyer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 340);
            this.Controls.Add(this.buttonProgramm);
            this.Controls.Add(this.buttonContractList);
            this.Controls.Add(this.buttonReport);
            this.Controls.Add(this.buttonContract);
            this.Controls.Add(this.buttonClient);
            this.Controls.Add(this.buttonDIagr);
            this.Controls.Add(this.buttonArch);
            this.Name = "FormMainLawyer";
            this.Text = "FormMainLawyer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonArch;
        private System.Windows.Forms.Button buttonDIagr;
        private System.Windows.Forms.Button buttonClient;
        private System.Windows.Forms.Button buttonContract;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonContractList;
        private System.Windows.Forms.Button buttonProgramm;
    }
}