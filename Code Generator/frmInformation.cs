using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code_Generator
{
    public partial class frmInformation : Form
    {
        
        //string ConnectionString;
        //string WebapiName;
        public frmInformation()
        {
            InitializeComponent();
        }

        private void btnGenerateEntity_Click(object sender, EventArgs e)
        {
            if (IsValidEntity())
            {
                GenerateBusinessEntity generateEntity = new Code_Generator.GenerateBusinessEntity();
                generateEntity.GenerateEntityForAllTables(txtConnectionString.Text, txtFolderLocation.Text, txtBusinessEntityProjectName.Text, txtDataAccessProjectName.Text);
            }

        }

        public bool IsValidEntity()
        {
            if (txtBusinessEntityProjectName.Text == "")
            {
                lblbusinessentity.Text = "business entity name must fill";
                return false;
            }
            else if (txtConnectionString.Text == "")
            {
                lblconnectionstring.Text = "connection string must fill";
                return false;
            }
            else
            {
                lblwebapi.Text = "";
                lbldataaccess.Text = "";
                lblbusinessentity.Text = "";
                lblbusinesslogic.Text = "";
                lblconnectionstring.Text = "";

                return true;
            }
        }

        public bool IsValidLogic()
        {
            if (txtBusinessLogicProjectName.Text == "")
            {
                lblbusinesslogic.Text = "business Logic name must fill";
                return false;
            }
            else if (txtConnectionString.Text == "")
            {
                lblconnectionstring.Text = "connection string must fill";
                return false;
            }
            else
            {
                lblwebapi.Text = "";
                lbldataaccess.Text = "";
                lblbusinessentity.Text = "";
                lblbusinesslogic.Text = "";
                lblconnectionstring.Text = "";

                return true;
            }
        }

        public bool IsValid()
        {

            if (txtWebApiProjectName.Text == "")
            {
                lblwebapi.Text = "project name must fill";
                return false;
            }
            else if (txtDataAccessProjectName.Text == "")
            {
                lbldataaccess.Text = "data access name must fill";
                return false;
            }
            else if (txtBusinessEntityProjectName.Text == "")
            {
                lblbusinessentity.Text = "business entity name must fill";
                return false;
            }
            else if (txtBusinessLogicProjectName.Text == "")
            {
                lblbusinesslogic.Text = "business logic name must fill";
                return false;
            }

            else if (txtConnectionString.Text == "")
            {
                lblconnectionstring.Text = "connection string must fill";
                return false;
            }
            else
            {
                lblwebapi.Text = "";
                lbldataaccess.Text = "";
                lblbusinessentity.Text = "";
                lblbusinesslogic.Text = "";
                lblconnectionstring.Text = "";

                return true;
            }
        }

        public bool IsValidWebApi()
        {
            if (txtBusinessLogicProjectName.Text == "")
            {
                lblbusinesslogic.Text = "business Logic name must fill";
                return false;
            }
            if (txtBusinessEntityProjectName.Text == "")
            {
                lblbusinessentity.Text = "business entity name must fill";
                return false;
            }
            if (txtDataAccessProjectName.Text == "")
            {
                lbldataaccess.Text = "dataAccess logic name must fill";
                return false;
            }
            if (txtWebApiProjectName.Text == "")
            {
                lblwebapi.Text = "webApi name must fill";
                return false;
            }
            else if (txtConnectionString.Text == "")
            {
                lblconnectionstring.Text = "connection string must fill";
                return false;
            }
            else
            {
                lblwebapi.Text = "";
                lbldataaccess.Text = "";
                lblbusinessentity.Text = "";
                lblbusinesslogic.Text = "";
                lblconnectionstring.Text = "";

                return true;
            }
        }

        private void btnGenerateLogic_Click(object sender, EventArgs e)
        {
            if (IsValidLogic())
            {
                GenerateBusinessLogic generateLogic = new GenerateBusinessLogic();
                generateLogic.GenerateLogicForAllTables(txtConnectionString.Text, txtFolderLocation.Text, "TIMSDB", txtBusinessLogicProjectName.Text, txtBusinessEntityProjectName.Text, txtDataAccessProjectName.Text);
            }
        }

        private void btnGenerateModel_Click(object sender, EventArgs e)
        {
            if (IsValidLogic())
            {
                GenerateModel generateModel = new GenerateModel();
                generateModel.GenerateModelForAllTables(txtConnectionString.Text, txtFolderLocation.Text, txtWebApiProjectName.Text, txtBusinessEntityProjectName.Text);
            }
        }

        private void btnGenerateController_Click(object sender, EventArgs e)
        {
            if (IsValidWebApi())
            {
                GenerateController generateController = new GenerateController();
                generateController.GenerateControllerForAllTables(txtConnectionString.Text, txtFolderLocation.Text, txtBusinessLogicProjectName.Text, txtBusinessEntityProjectName.Text, txtWebApiProjectName.Text);
            }
        }

        private void txtWebApiProjectName_TextChanged(object sender, EventArgs e)
        {
            if(txtWebApiProjectName.Text != string.Empty)
            {
                lblwebapi.Text = string.Empty;
            }
            else
            {
                lblwebapi.Text = "project name must fill";
            }
        }

        private void txtDataAccessProjectName_TextChanged(object sender, EventArgs e)
        {
            if (txtDataAccessProjectName.Text != string.Empty)
            {
                lbldataaccess.Text = string.Empty;
            }
            else
            {
                lbldataaccess.Text = "Data Access project name must fill";
            }
        }

        private void txtBusinessEntityProjectName_TextChanged(object sender, EventArgs e)
        {
            if (txtBusinessEntityProjectName.Text != string.Empty)
            {
                lblbusinessentity.Text = string.Empty;
            }
            else
            {
                lblbusinessentity.Text = "Business Entity project name must fill";
            }
        }

        private void txtBusinessLogicProjectName_TextChanged(object sender, EventArgs e)
        {
            if (txtBusinessLogicProjectName.Text != string.Empty)
            {
                lblbusinesslogic.Text = string.Empty;
            }
            else
            {
                lblbusinesslogic.Text = "Business Logic project name must fill";
            }
        }

        private void btnGenerateInterface_Click(object sender, EventArgs e)
        {
            GenerateAngularInterface generateAngularInterface = new Code_Generator.GenerateAngularInterface();
            generateAngularInterface.GenerateInterfaceForAllTables(txtConnectionString.Text, txtFolderLocation.Text, txtInterfaceFolderName.Text);
        }

        private void btnGenerateService_Click(object sender, EventArgs e)
        {
            GenerateAngularService generateAngularService = new Code_Generator.GenerateAngularService();
            generateAngularService.GenerateServiceForAllTables(txtConnectionString.Text, txtFolderLocation.Text, txtInterfaceFolderName.Text, txtServiceFolderName.Text);
        }

        private void btnGenerateMvcController_Click(object sender, EventArgs e)
        {
            if (IsValidWebApi())
            {
                GenerateMVCController generateMVCController = new GenerateMVCController();
                generateMVCController.GenerateControllerForAllTables(txtConnectionString.Text, txtFolderLocation.Text, txtBusinessLogicProjectName.Text, txtBusinessEntityProjectName.Text, txtWebApiProjectName.Text);
            }
        }
    }
}
