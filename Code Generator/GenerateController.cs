using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateController
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
            controllerCode = controllerCode + "using " + entityProjectName + ";" + Environment.NewLine;
            controllerCode = controllerCode + "using " + businessProjectName + ";" + Environment.NewLine;

            controllerCode = controllerCode + Environment.NewLine;

            controllerCode = controllerCode + "namespace " + webapiProjectName + Environment.NewLine;
            controllerCode = controllerCode + "    {" + Environment.NewLine;
            controllerCode = controllerCode + "         public class " + modelName + "Controller" + Environment.NewLine;
            controllerCode = controllerCode + "         {" + Environment.NewLine;

            //controller
            controllerCode = controllerCode + "             " + GenerateGetAllCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateGetSingleCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateSaveCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
            controllerCode = controllerCode + Environment.NewLine;
            controllerCode = controllerCode + "             " + GenerateUpdateCode(table, businessProjectName, entityProjectName, webapiProjectName, entityName, modelName) + Environment.NewLine;
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
            string controllerCode =           "[HttpGet]" + Environment.NewLine;
            controllerCode = controllerCode + "             [Route(\"api/" + modelName + "/GetAll\")]" + Environment.NewLine;

            controllerCode = controllerCode + "             public List<Models." + modelName + "Model> GetAll()" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + businessProjectName + "."+ modelName +"Manager "+ modelName +"Manager = new "+ businessProjectName + "." + modelName + "Manager();" + Environment.NewLine;

            controllerCode = controllerCode + "                 List<" + entityProjectName + "."+ entityName +"Entity> "+ entityName + "Entities = "+ entityName + "Manager.GetAll().OrderBy(x => x.Name).ToList();" + Environment.NewLine;
            controllerCode = controllerCode + "                 List<Models." + modelName +"Model> "+ modelName + "Models = new List<Models." + modelName + "Model>();" + Environment.NewLine;

            controllerCode = controllerCode + "                 foreach (" + entityProjectName + "."+ entityName + "Entity " + entityName + " in "+ entityName + "Entities)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                      Models." + modelName +"Models.Add(new Models."+ modelName +"Model("+ entityName + "));" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;

            controllerCode = controllerCode + "                 return " + modelName +"Models;" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateGetSingleCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode =           "[HttpGet]" + Environment.NewLine;
            controllerCode = controllerCode + "             [Route(\"api/" + modelName +"/GetSingle\")]" + Environment.NewLine;
            controllerCode = controllerCode + "             public Models." + modelName +"Model GetSingle(int ID)" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + businessProjectName + "."+ entityName +"Manager "+ entityName +"Manager = new "+ businessProjectName + "."+ businessProjectName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + "."+ entityName + " "+ entityName +" = "+ entityName +"Manager.GetSingle(ID);" + Environment.NewLine;

            controllerCode = controllerCode + "                 return " + entityName + ".MapToModel<Models."+ modelName +"Model>();" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateSaveCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "[HttpPost]" + Environment.NewLine;
            controllerCode = controllerCode + "             [Route(\"api /" + modelName +"/Save\")]" + Environment.NewLine;
            controllerCode = controllerCode + "             public " + entityProjectName + ".Result Save(Models."+ modelName +"Model "+ modelName +")" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + ".Result result = new "+ entityProjectName + ".Result();" + Environment.NewLine;
            controllerCode = controllerCode + "                 try" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + businessProjectName + "."+ entityName +"Manager "+ entityName +"Manager = new "+ businessProjectName + "."+ entityName +"Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                     result = " + entityName +"Manager.Save("+ modelName +".MapToEntity<"+ entityProjectName + "."+ modelName +">());" + Environment.NewLine;

            controllerCode = controllerCode + "                     return result;" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "                 catch (Exception)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                     result.Status = false;" + Environment.NewLine;
            controllerCode = controllerCode + "                     result.Message = \"" + modelName +" save failed.\";" + Environment.NewLine;

            controllerCode = controllerCode + "                     return result;" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateUpdateCode(DataTable table, string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "[HttpPost]" + Environment.NewLine;
            controllerCode = controllerCode + "             [Route(\"api /" + modelName + "/Update\")]" + Environment.NewLine;
            controllerCode = controllerCode + "             public " + entityProjectName + ".Result Update(Models."+ modelName +"Model "+ modelName +")" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + ".Result result = new " + entityProjectName + ".Result();" + Environment.NewLine;
            controllerCode = controllerCode + "                 try" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                     result = " + entityName + "Manager.Update(" + modelName + ".MapToEntity<" + entityProjectName + "." + modelName + ">());" + Environment.NewLine;

            controllerCode = controllerCode + "                     return result;" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "                 catch (Exception)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                     result.Status = false;" + Environment.NewLine;
            controllerCode = controllerCode + "                     result.Message = \"" + modelName + " update failed.\";" + Environment.NewLine;

            controllerCode = controllerCode + "                     return result;" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }

        private string GenerateDeleteCode(string businessProjectName, string entityProjectName, string webapiProjectName, string entityName, string modelName)
        {
            string controllerCode = "[HttpPost]" + Environment.NewLine;
            controllerCode = controllerCode + "             [Route(\"api /" + modelName + "/Delete\")]" + Environment.NewLine;
            controllerCode = controllerCode + "             public " + entityProjectName + ".Result Delete(Models." + modelName + "Model " + modelName + ")" + Environment.NewLine;
            controllerCode = controllerCode + "             {" + Environment.NewLine;
            controllerCode = controllerCode + "                 " + entityProjectName + ".Result result = new " + entityProjectName + ".Result();" + Environment.NewLine;
            controllerCode = controllerCode + "                 try" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                     " + businessProjectName + "." + entityName + "Manager " + entityName + "Manager = new " + businessProjectName + "." + entityName + "Manager();" + Environment.NewLine;
            controllerCode = controllerCode + "                     result = " + entityName + "Manager.Delete(" + modelName + ".MapToEntity<" + entityProjectName + "." + modelName + ">());" + Environment.NewLine;

            controllerCode = controllerCode + "                     return result;" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "                 catch (Exception)" + Environment.NewLine;
            controllerCode = controllerCode + "                 {" + Environment.NewLine;
            controllerCode = controllerCode + "                     result.Status = false;" + Environment.NewLine;
            controllerCode = controllerCode + "                     result.Message = \"" + modelName + " delete failed.\";" + Environment.NewLine;

            controllerCode = controllerCode + "                     return result;" + Environment.NewLine;
            controllerCode = controllerCode + "                 }" + Environment.NewLine;
            controllerCode = controllerCode + "             }" + Environment.NewLine;

            return controllerCode;
        }
    }
}
