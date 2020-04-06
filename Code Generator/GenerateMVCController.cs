using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateMVCController
    {
        public string GenerateControllerForAllTables(string connectionString, string location, string businessLogicProjectName, string entityProjectName, string webapiProjectName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select table_name from information_schema.tables where TABLE_NAME <> 'sysdiagrams' and TABLE_TYPE = 'BASE TABLE'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("TableName");

            if (Utilities.CreateFolder(location, webapiProjectName))
            {
                while (reader.Read())
                {
                    table.Rows.Add(reader.GetString(0));

                    string tablename = reader.GetString(0);

                    string prefix = tablename.Substring(0, 3);
                    string entityName = tablename.Replace(prefix, "");
                    string modelName = tablename.Replace(prefix, "");

                    GenerateSingleController(location, businessLogicProjectName, entityProjectName, webapiProjectName, entityName, tablename, modelName);
                }

                return string.Empty;
            }
            else
            {
                return "Folder not created";
            }
        }

        public void GenerateSingleController(string location, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string tableName, string modelName)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            string query = "select COLUMN_NAME, DATA_TYPE from information_schema.COLUMNS where TABLE_NAME = '" + tableName + "'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("ColumnName");
            table.Columns.Add("DataType");

            while (reader.Read())
            {
                table.Rows.Add(reader.GetString(0), reader.GetString(1));

            }

            GenerateControllerMainCode(table, location, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName);
        }

        public void GenerateControllerMainCode(DataTable table, string location, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "using System;" + Environment.NewLine;
            controllerCode = controllerCode + "using System.Collections.Generic;" + Environment.NewLine;
            controllerCode = controllerCode + "using System.Linq;" + Environment.NewLine;
            controllerCode = controllerCode + "using System.Text;" + Environment.NewLine;
            controllerCode = controllerCode + "using System.Threading.Tasks;" + Environment.NewLine;
            controllerCode = controllerCode + "using System.Web.Mvc;" + Environment.NewLine;
            controllerCode = controllerCode + "using " + entityProjectName + ";" + Environment.NewLine;
            controllerCode = controllerCode + "using " + businessProjectName + ";" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + "namespace " + webapiProjectName + ".Controllers" + Environment.NewLine;
            controllerCode = controllerCode + "    {" + Environment.NewLine;
            controllerCode = controllerCode + "         public class " + modelName + "Controller : Controller" + Environment.NewLine;
            controllerCode = controllerCode + "         {" + Environment.NewLine;

            //controller
            controllerCode = controllerCode + "             " + GenerateGetAllCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateGetSingleCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateGetForSaveCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateSaveCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateGetForEditCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateEditCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateGetForDeleteCode(businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateDeleteCode(businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;

            controllerCode = controllerCode + "         }" + Environment.NewLine;
            controllerCode = controllerCode + "    }" + Environment.NewLine;

            try
            {
                Utilities.CreateFile(location, webapiProjectName, modelName + "Controller.cs", controllerCode);
            }
            catch(Exception ex)
            {
                return;
            }
        }

        private string GenerateGetAllCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "public ActionResult Index()" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + businessProjectName + "."+ modelName +"Manager "+ modelName +"Manager = new "+ businessProjectName + "." + modelName + "Manager();" + Environment.NewLine;

            controllerCode = controllerCode + "                 List<" + entityProjectName + "."+ entityName +"Entity> "+ entityName + "Entities = "+ entityName + "Manager.GetAll().OrderBy(x => x.ID).ToList();" + Environment.NewLine;
            controllerCode = controllerCode + "                 List<Models." + modelName +"Model> "+ modelName + "Models = new List<Models." + modelName + "Model>();" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                 foreach (" + entityProjectName + "."+ entityName + "Entity " + entityName + " in "+ entityName + "Entities)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                      " + modelName +"Models.Add(new Models."+ modelName +"Model("+ entityName + "));" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                 return View(" + modelName +"Models);" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateGetSingleCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "public ActionResult Details(int ID)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + businessProjectName + "."+ entityName +"Manager "+ entityName +"Manager = new "+ businessProjectName + "."+ entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + "."+ entityName + "Entity "+ entityName +" = "+ entityName +"Manager.GetSingle(ID);" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                 return View(new Models." + modelName +"Model("+ modelName +"));" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateGetForSaveCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "public ActionResult Create()" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 return View();" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateSaveCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "[HttpPost]" + Environment.NewLine;
            controllerCode = controllerCode + "             public ActionResult Create(FormCollection collection)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 try" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;

            controllerCode = controllerCode + "                     " + entityProjectName + "." + entityName + "Entity " + entityName + "Entity = new " + entityProjectName + "." + entityName + "Entity();" + Environment.NewLine;
            for (int i = 1; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string dataType = Utilities.GetCodeDataType(table.Rows[i][1].ToString());

                if (dataType == "int")
                {
                    string relatioshipType = Utilities.GetRelationshipType(columnName);
                    if (relatioshipType != string.Empty)
                    {
                        controllerCode = controllerCode + "                     " + entityName + "Entity." + Utilities.RemoveIDFromLastIfAny(columnName) + "Entity.ID = int.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                    }
                    else
                    {
                        controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = int.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                    }
                }
                else if (dataType == "decimal")
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = decimal.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                }
                else if (dataType == "bool")
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = bool.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                }
                else if (dataType == "DateTime")
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = DateTime.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                }
                else
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = collection[\"" + columnName + "\"];" + Environment.NewLine;
                }
            }
            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + "                     " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + entityName + "Manager.Save(" + entityName + "Entity);" + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                     return RedirectToAction(\"Index\");" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "                 catch (Exception)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                     return View();" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateGetForEditCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "public ActionResult Edit(int ID)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + "." + entityName + "Entity " + entityName + " = " + entityName + "Manager.GetSingle(ID);" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                 return View(new Models." + modelName + "Model(" + modelName + "));" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateEditCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "[HttpPost]" + Environment.NewLine;
            controllerCode = controllerCode + "             public ActionResult Edit(int ID, FormCollection collection)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 try" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;

            controllerCode = controllerCode + "                     " + entityProjectName + "." + entityName + "Entity " + entityName + "Entity = new " + entityProjectName + "." + entityName + "Entity();" + Environment.NewLine;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string dataType = Utilities.GetCodeDataType(table.Rows[i][1].ToString());

                if (dataType == "int")
                {
                    string relatioshipType = Utilities.GetRelationshipType(columnName);
                    if (relatioshipType != string.Empty)
                    {
                        controllerCode = controllerCode + "                     " + entityName + "Entity." + Utilities.RemoveIDFromLastIfAny(columnName) + "Entity.ID = int.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                    }
                    else
                    {
                        controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = int.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                    }
                }
                else if (dataType == "decimal")
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = decimal.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                }
                else if (dataType == "bool")
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = bool.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                }
                else if (dataType == "DateTime")
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = DateTime.Parse(collection[\"" + columnName + "\"]);" + Environment.NewLine;
                }
                else
                {
                    controllerCode = controllerCode + "                     " + entityName + "Entity." + columnName + " = collection[\"" + columnName + "\"];" + Environment.NewLine;
                }
            }
            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + "                     " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + entityName + "Manager.Update(" + entityName + "Entity);" + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                     return RedirectToAction(\"Index\");" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "                 catch (Exception)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                     return View();" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateGetForDeleteCode(string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "public ActionResult Delete(int ID)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + "." + entityName + "Entity " + entityName + " = " + entityName + "Manager.GetSingle(ID);" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                 return View(new Models." + modelName + "Model(" + modelName + "));" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateDeleteCode(string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "[HttpPost]" + Environment.NewLine;
            controllerCode = controllerCode + "             public ActionResult Delete(int ID, FormCollection collection)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 try" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;

            controllerCode = controllerCode + "                     " + entityProjectName + "." + entityName + "Entity " + entityName + "Entity = new " + entityProjectName + "." + entityName + "Entity();" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + entityName + "Entity.ID" + " = ID;" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + "                     " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + entityName + "Manager.Delete(" + entityName + "Entity);" + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                     return RedirectToAction(\"Index\");" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "                 catch (Exception)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "                     return View();" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }
    }
}
