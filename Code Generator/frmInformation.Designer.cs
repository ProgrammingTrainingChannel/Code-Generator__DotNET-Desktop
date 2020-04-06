namespace Code_Generator
{
    partial class frmInformation
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtWebApiProjectName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDataAccessProjectName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBusinessEntityProjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBusinessLogicProjectName = new System.Windows.Forms.TextBox();
            this.btnGenerateLogic = new System.Windows.Forms.Button();
            this.btnGenerateEntity = new System.Windows.Forms.Button();
            this.btnGenerateModel = new System.Windows.Forms.Button();
            this.btnGenerateWebApiController = new System.Windows.Forms.Button();
            this.lblwebapi = new System.Windows.Forms.Label();
            this.lbldataaccess = new System.Windows.Forms.Label();
            this.lblbusinesslogic = new System.Windows.Forms.Label();
            this.lblbusinessentity = new System.Windows.Forms.Label();
            this.lblconnectionstring = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolderLocation = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnGenerateMvcController = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTemplateFolderName = new System.Windows.Forms.TextBox();
            this.lblInterface = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblService = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblComponent = new System.Windows.Forms.Label();
            this.txtComponentFolderName = new System.Windows.Forms.TextBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.txtInterfaceFolderName = new System.Windows.Forms.TextBox();
            this.btnGenerateComponent = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnGenerateTemplate = new System.Windows.Forms.Button();
            this.txtServiceFolderName = new System.Windows.Forms.TextBox();
            this.btnGenerateService = new System.Windows.Forms.Button();
            this.btnGenerateInterface = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(21, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "WebApi Project Name:";
            // 
            // txtWebApiProjectName
            // 
            this.txtWebApiProjectName.Location = new System.Drawing.Point(213, 129);
            this.txtWebApiProjectName.Name = "txtWebApiProjectName";
            this.txtWebApiProjectName.Size = new System.Drawing.Size(223, 20);
            this.txtWebApiProjectName.TabIndex = 10;
            this.txtWebApiProjectName.Text = "WebApi";
            this.txtWebApiProjectName.TextChanged += new System.EventHandler(this.txtWebApiProjectName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(11, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 17);
            this.label4.TabIndex = 19;
            this.label4.Text = "Connection String:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(140, 29);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(619, 34);
            this.txtConnectionString.TabIndex = 18;
            this.txtConnectionString.Text = "Data Source=DESKTOP-QIS1VR4\\SQLSERVER2014;Initial Catalog=SMSDatabase;Persist Sec" +
    "urity Info=True;User ID=sa;Password=p@55w0rd";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(18, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 17);
            this.label2.TabIndex = 23;
            this.label2.Text = "Data Access Layer Name";
            // 
            // txtDataAccessProjectName
            // 
            this.txtDataAccessProjectName.Location = new System.Drawing.Point(213, 94);
            this.txtDataAccessProjectName.Name = "txtDataAccessProjectName";
            this.txtDataAccessProjectName.Size = new System.Drawing.Size(223, 20);
            this.txtDataAccessProjectName.TabIndex = 24;
            this.txtDataAccessProjectName.Text = "DataAccessLogic";
            this.txtDataAccessProjectName.TextChanged += new System.EventHandler(this.txtDataAccessProjectName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 17);
            this.label1.TabIndex = 23;
            this.label1.Text = "Business Entity Layer Name";
            // 
            // txtBusinessEntityProjectName
            // 
            this.txtBusinessEntityProjectName.Location = new System.Drawing.Point(213, 23);
            this.txtBusinessEntityProjectName.Name = "txtBusinessEntityProjectName";
            this.txtBusinessEntityProjectName.Size = new System.Drawing.Size(223, 20);
            this.txtBusinessEntityProjectName.TabIndex = 24;
            this.txtBusinessEntityProjectName.Text = "BusinessEntity";
            this.txtBusinessEntityProjectName.TextChanged += new System.EventHandler(this.txtBusinessEntityProjectName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(18, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "Business Logic Layer Name";
            // 
            // txtBusinessLogicProjectName
            // 
            this.txtBusinessLogicProjectName.Location = new System.Drawing.Point(213, 58);
            this.txtBusinessLogicProjectName.Name = "txtBusinessLogicProjectName";
            this.txtBusinessLogicProjectName.Size = new System.Drawing.Size(223, 20);
            this.txtBusinessLogicProjectName.TabIndex = 24;
            this.txtBusinessLogicProjectName.Text = "BusinessLogic";
            this.txtBusinessLogicProjectName.TextChanged += new System.EventHandler(this.txtBusinessLogicProjectName_TextChanged);
            // 
            // btnGenerateLogic
            // 
            this.btnGenerateLogic.Location = new System.Drawing.Point(156, 191);
            this.btnGenerateLogic.Name = "btnGenerateLogic";
            this.btnGenerateLogic.Size = new System.Drawing.Size(126, 42);
            this.btnGenerateLogic.TabIndex = 27;
            this.btnGenerateLogic.Text = "Generate Logic";
            this.btnGenerateLogic.UseVisualStyleBackColor = true;
            this.btnGenerateLogic.Click += new System.EventHandler(this.btnGenerateLogic_Click);
            // 
            // btnGenerateEntity
            // 
            this.btnGenerateEntity.Location = new System.Drawing.Point(24, 190);
            this.btnGenerateEntity.Name = "btnGenerateEntity";
            this.btnGenerateEntity.Size = new System.Drawing.Size(126, 43);
            this.btnGenerateEntity.TabIndex = 26;
            this.btnGenerateEntity.Text = "Generate Entity";
            this.btnGenerateEntity.UseVisualStyleBackColor = true;
            this.btnGenerateEntity.Click += new System.EventHandler(this.btnGenerateEntity_Click);
            // 
            // btnGenerateModel
            // 
            this.btnGenerateModel.Location = new System.Drawing.Point(287, 191);
            this.btnGenerateModel.Name = "btnGenerateModel";
            this.btnGenerateModel.Size = new System.Drawing.Size(126, 42);
            this.btnGenerateModel.TabIndex = 29;
            this.btnGenerateModel.Text = "Generate Model";
            this.btnGenerateModel.UseVisualStyleBackColor = true;
            this.btnGenerateModel.Click += new System.EventHandler(this.btnGenerateModel_Click);
            // 
            // btnGenerateWebApiController
            // 
            this.btnGenerateWebApiController.Location = new System.Drawing.Point(418, 191);
            this.btnGenerateWebApiController.Name = "btnGenerateWebApiController";
            this.btnGenerateWebApiController.Size = new System.Drawing.Size(126, 43);
            this.btnGenerateWebApiController.TabIndex = 28;
            this.btnGenerateWebApiController.Text = "Generate WebAPI Controller";
            this.btnGenerateWebApiController.UseVisualStyleBackColor = true;
            this.btnGenerateWebApiController.Click += new System.EventHandler(this.btnGenerateController_Click);
            // 
            // lblwebapi
            // 
            this.lblwebapi.AutoSize = true;
            this.lblwebapi.ForeColor = System.Drawing.Color.Red;
            this.lblwebapi.Location = new System.Drawing.Point(442, 136);
            this.lblwebapi.Name = "lblwebapi";
            this.lblwebapi.Size = new System.Drawing.Size(81, 13);
            this.lblwebapi.TabIndex = 30;
            this.lblwebapi.Text = "[Error Message]";
            // 
            // lbldataaccess
            // 
            this.lbldataaccess.AutoSize = true;
            this.lbldataaccess.ForeColor = System.Drawing.Color.Red;
            this.lbldataaccess.Location = new System.Drawing.Point(443, 97);
            this.lbldataaccess.Name = "lbldataaccess";
            this.lbldataaccess.Size = new System.Drawing.Size(81, 13);
            this.lbldataaccess.TabIndex = 31;
            this.lbldataaccess.Text = "[Error Message]";
            // 
            // lblbusinesslogic
            // 
            this.lblbusinesslogic.AutoSize = true;
            this.lblbusinesslogic.ForeColor = System.Drawing.Color.Red;
            this.lblbusinesslogic.Location = new System.Drawing.Point(442, 61);
            this.lblbusinesslogic.Name = "lblbusinesslogic";
            this.lblbusinesslogic.Size = new System.Drawing.Size(81, 13);
            this.lblbusinesslogic.TabIndex = 31;
            this.lblbusinesslogic.Text = "[Error Message]";
            // 
            // lblbusinessentity
            // 
            this.lblbusinessentity.AutoSize = true;
            this.lblbusinessentity.ForeColor = System.Drawing.Color.Red;
            this.lblbusinessentity.Location = new System.Drawing.Point(442, 26);
            this.lblbusinessentity.Name = "lblbusinessentity";
            this.lblbusinessentity.Size = new System.Drawing.Size(81, 13);
            this.lblbusinessentity.TabIndex = 31;
            this.lblbusinessentity.Text = "[Error Message]";
            // 
            // lblconnectionstring
            // 
            this.lblconnectionstring.AutoSize = true;
            this.lblconnectionstring.ForeColor = System.Drawing.Color.Red;
            this.lblconnectionstring.Location = new System.Drawing.Point(388, 9);
            this.lblconnectionstring.Name = "lblconnectionstring";
            this.lblconnectionstring.Size = new System.Drawing.Size(81, 13);
            this.lblconnectionstring.TabIndex = 32;
            this.lblconnectionstring.Text = "[Error Message]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(25, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "Folder Location:";
            // 
            // txtFolderLocation
            // 
            this.txtFolderLocation.Location = new System.Drawing.Point(140, 69);
            this.txtFolderLocation.Name = "txtFolderLocation";
            this.txtFolderLocation.Size = new System.Drawing.Size(619, 20);
            this.txtFolderLocation.TabIndex = 24;
            this.txtFolderLocation.Text = "C:\\\\Users\\\\Programming\\\\Desktop\\\\GeneratorLocation";
            this.txtFolderLocation.TextChanged += new System.EventHandler(this.txtBusinessEntityProjectName_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(6, 99);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(759, 276);
            this.tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtWebApiProjectName);
            this.tabPage1.Controls.Add(this.lblbusinessentity);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.lblbusinesslogic);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.lbldataaccess);
            this.tabPage1.Controls.Add(this.txtDataAccessProjectName);
            this.tabPage1.Controls.Add(this.lblwebapi);
            this.tabPage1.Controls.Add(this.txtBusinessEntityProjectName);
            this.tabPage1.Controls.Add(this.btnGenerateModel);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnGenerateMvcController);
            this.tabPage1.Controls.Add(this.btnGenerateWebApiController);
            this.tabPage1.Controls.Add(this.txtBusinessLogicProjectName);
            this.tabPage1.Controls.Add(this.btnGenerateLogic);
            this.tabPage1.Controls.Add(this.btnGenerateEntity);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(751, 250);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Generate WebApi Features";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnGenerateMvcController
            // 
            this.btnGenerateMvcController.Location = new System.Drawing.Point(550, 190);
            this.btnGenerateMvcController.Name = "btnGenerateMvcController";
            this.btnGenerateMvcController.Size = new System.Drawing.Size(126, 43);
            this.btnGenerateMvcController.TabIndex = 28;
            this.btnGenerateMvcController.Text = "Generate MVC Controller";
            this.btnGenerateMvcController.UseVisualStyleBackColor = true;
            this.btnGenerateMvcController.Click += new System.EventHandler(this.btnGenerateMvcController_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtTemplateFolderName);
            this.tabPage2.Controls.Add(this.lblInterface);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.lblService);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.lblComponent);
            this.tabPage2.Controls.Add(this.txtComponentFolderName);
            this.tabPage2.Controls.Add(this.lblTemplate);
            this.tabPage2.Controls.Add(this.txtInterfaceFolderName);
            this.tabPage2.Controls.Add(this.btnGenerateComponent);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.btnGenerateTemplate);
            this.tabPage2.Controls.Add(this.txtServiceFolderName);
            this.tabPage2.Controls.Add(this.btnGenerateService);
            this.tabPage2.Controls.Add(this.btnGenerateInterface);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(751, 250);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Generate Angular Features";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(40, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 17);
            this.label7.TabIndex = 34;
            this.label7.Text = "Interface Folder Name";
            // 
            // txtTemplateFolderName
            // 
            this.txtTemplateFolderName.Location = new System.Drawing.Point(218, 128);
            this.txtTemplateFolderName.Name = "txtTemplateFolderName";
            this.txtTemplateFolderName.Size = new System.Drawing.Size(223, 20);
            this.txtTemplateFolderName.TabIndex = 32;
            this.txtTemplateFolderName.Text = "Template";
            // 
            // lblInterface
            // 
            this.lblInterface.AutoSize = true;
            this.lblInterface.ForeColor = System.Drawing.Color.Red;
            this.lblInterface.Location = new System.Drawing.Point(447, 25);
            this.lblInterface.Name = "lblInterface";
            this.lblInterface.Size = new System.Drawing.Size(81, 13);
            this.lblInterface.TabIndex = 45;
            this.lblInterface.Text = "[Error Message]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(36, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 17);
            this.label9.TabIndex = 33;
            this.label9.Text = "Template Folder Name";
            // 
            // lblService
            // 
            this.lblService.AutoSize = true;
            this.lblService.ForeColor = System.Drawing.Color.Red;
            this.lblService.Location = new System.Drawing.Point(447, 60);
            this.lblService.Name = "lblService";
            this.lblService.Size = new System.Drawing.Size(81, 13);
            this.lblService.TabIndex = 46;
            this.lblService.Text = "[Error Message]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(23, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(165, 17);
            this.label11.TabIndex = 35;
            this.label11.Text = "Component Folder Name";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.ForeColor = System.Drawing.Color.Red;
            this.lblComponent.Location = new System.Drawing.Point(448, 96);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(81, 13);
            this.lblComponent.TabIndex = 47;
            this.lblComponent.Text = "[Error Message]";
            // 
            // txtComponentFolderName
            // 
            this.txtComponentFolderName.Location = new System.Drawing.Point(218, 93);
            this.txtComponentFolderName.Name = "txtComponentFolderName";
            this.txtComponentFolderName.Size = new System.Drawing.Size(223, 20);
            this.txtComponentFolderName.TabIndex = 37;
            this.txtComponentFolderName.Text = "Component";
            // 
            // lblTemplate
            // 
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.ForeColor = System.Drawing.Color.Red;
            this.lblTemplate.Location = new System.Drawing.Point(447, 135);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(81, 13);
            this.lblTemplate.TabIndex = 44;
            this.lblTemplate.Text = "[Error Message]";
            // 
            // txtInterfaceFolderName
            // 
            this.txtInterfaceFolderName.Location = new System.Drawing.Point(218, 22);
            this.txtInterfaceFolderName.Name = "txtInterfaceFolderName";
            this.txtInterfaceFolderName.Size = new System.Drawing.Size(223, 20);
            this.txtInterfaceFolderName.TabIndex = 38;
            this.txtInterfaceFolderName.Text = "Interface";
            // 
            // btnGenerateComponent
            // 
            this.btnGenerateComponent.Location = new System.Drawing.Point(292, 190);
            this.btnGenerateComponent.Name = "btnGenerateComponent";
            this.btnGenerateComponent.Size = new System.Drawing.Size(126, 42);
            this.btnGenerateComponent.TabIndex = 43;
            this.btnGenerateComponent.Text = "Generate Component";
            this.btnGenerateComponent.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(48, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(140, 17);
            this.label14.TabIndex = 36;
            this.label14.Text = "Service Folder Name";
            // 
            // btnGenerateTemplate
            // 
            this.btnGenerateTemplate.Location = new System.Drawing.Point(423, 190);
            this.btnGenerateTemplate.Name = "btnGenerateTemplate";
            this.btnGenerateTemplate.Size = new System.Drawing.Size(126, 43);
            this.btnGenerateTemplate.TabIndex = 42;
            this.btnGenerateTemplate.Text = "Generate Template";
            this.btnGenerateTemplate.UseVisualStyleBackColor = true;
            // 
            // txtServiceFolderName
            // 
            this.txtServiceFolderName.Location = new System.Drawing.Point(218, 57);
            this.txtServiceFolderName.Name = "txtServiceFolderName";
            this.txtServiceFolderName.Size = new System.Drawing.Size(223, 20);
            this.txtServiceFolderName.TabIndex = 39;
            this.txtServiceFolderName.Text = "Service";
            // 
            // btnGenerateService
            // 
            this.btnGenerateService.Location = new System.Drawing.Point(161, 190);
            this.btnGenerateService.Name = "btnGenerateService";
            this.btnGenerateService.Size = new System.Drawing.Size(126, 42);
            this.btnGenerateService.TabIndex = 41;
            this.btnGenerateService.Text = "Generate Service";
            this.btnGenerateService.UseVisualStyleBackColor = true;
            this.btnGenerateService.Click += new System.EventHandler(this.btnGenerateService_Click);
            // 
            // btnGenerateInterface
            // 
            this.btnGenerateInterface.Location = new System.Drawing.Point(29, 189);
            this.btnGenerateInterface.Name = "btnGenerateInterface";
            this.btnGenerateInterface.Size = new System.Drawing.Size(126, 43);
            this.btnGenerateInterface.TabIndex = 40;
            this.btnGenerateInterface.Text = "Generate Interface";
            this.btnGenerateInterface.UseVisualStyleBackColor = true;
            this.btnGenerateInterface.Click += new System.EventHandler(this.btnGenerateInterface_Click);
            // 
            // frmInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 379);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblconnectionstring);
            this.Controls.Add(this.txtFolderLocation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtConnectionString);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInformation";
            this.Text = "Information";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWebApiProjectName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDataAccessProjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBusinessEntityProjectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBusinessLogicProjectName;
        private System.Windows.Forms.Button btnGenerateLogic;
        private System.Windows.Forms.Button btnGenerateEntity;
        private System.Windows.Forms.Button btnGenerateModel;
        private System.Windows.Forms.Button btnGenerateWebApiController;
        private System.Windows.Forms.Label lblwebapi;
        private System.Windows.Forms.Label lbldataaccess;
        private System.Windows.Forms.Label lblbusinesslogic;
        private System.Windows.Forms.Label lblbusinessentity;
        private System.Windows.Forms.Label lblconnectionstring;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFolderLocation;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTemplateFolderName;
        private System.Windows.Forms.Label lblInterface;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.TextBox txtComponentFolderName;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.TextBox txtInterfaceFolderName;
        private System.Windows.Forms.Button btnGenerateComponent;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnGenerateTemplate;
        private System.Windows.Forms.TextBox txtServiceFolderName;
        private System.Windows.Forms.Button btnGenerateService;
        private System.Windows.Forms.Button btnGenerateInterface;
        private System.Windows.Forms.Button btnGenerateMvcController;
    }
}