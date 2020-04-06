using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateAngularInterface
    {
        public string GenerateInterfaceForAllTables(string connectionString, string location, string interfaceProjectName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select table_name from information_schema.tables where TABLE_NAME <> 'sysdiagrams' and TABLE_TYPE = 'BASE TABLE'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (Utilities.CreateFolder(location, interfaceProjectName))
            {
                while (reader.Read())
                {
                    string tablename = reader.GetString(0);

                    string prefix = tablename.Substring(0, 3);
                    string entityName = tablename.Replace(prefix, "");
                    string modelName = tablename.Replace(prefix, "");

                    GenerateSingleInterface(location, interfaceProjectName, entityName);
                }

                return string.Empty;
            }
            else
            {
                return "Folder not created";
            }
        }

        public void GenerateSingleInterface(string location, string interfaceProjectName, string entityName)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            string query = "select COLUMN_NAME, DATA_TYPE from information_schema.COLUMNS where TABLE_NAME = 'tbl" + entityName + "'";

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

            GenerateAndSave(location, table, interfaceProjectName, entityName);
        }

        public void GenerateAndSave(string location, DataTable table, string interfaceProjectName, string entityName)
        {
            string interfaceCode = "export interface I" + entityName + Environment.NewLine;
            interfaceCode = interfaceCode + "{" + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    interfaceCode = interfaceCode + "            " + Utilities.RemoveIDFromLastIfAny(columnName) + "Model :" + " null" + Environment.NewLine;
                }
                else
                {
                    interfaceCode = interfaceCode + "            " + columnName + " : " + Utilities.GetCodeDataType(columnName) + Environment.NewLine;
                }
            }

            interfaceCode = interfaceCode + "}" + Environment.NewLine;

            Utilities.CreateFile(location, interfaceProjectName, entityName + ".interface.cs", interfaceCode);
        }
    }
}
