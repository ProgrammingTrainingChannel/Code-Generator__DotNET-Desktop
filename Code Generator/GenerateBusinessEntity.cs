using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateBusinessEntity
    {

        public string GenerateEntityForAllTables(string connectionString, string location, string entityProjectName, string dataAccessProjectName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select table_name from information_schema.tables where TABLE_NAME <> 'sysdiagrams' and TABLE_TYPE = 'BASE TABLE'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("TableName");

            if (Utilities.CreateFolder(location, entityProjectName))
            {
                while (reader.Read())
                {
                    table.Rows.Add(reader.GetString(0));

                    string tablename = reader.GetString(0);

                    string prefix = tablename.Substring(0, 3);
                    string entityName = tablename.Replace(prefix, "");

                    GenerateSingleEntity(location, entityProjectName, dataAccessProjectName, entityName, tablename);
                }

                return string.Empty;
            }
            else
            {
                return "Folder not created";
            }
        }

        public void GenerateSingleEntity(string location, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
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

            GenerateAndSave(location, table, entityProjectName, dataAccessProjectName, entityName, tableName);
        }

        public void GenerateAndSave(string location, DataTable table, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "using System;" + Environment.NewLine;
            entityCode = entityCode + "using System.Collections.Generic;" + Environment.NewLine;
            entityCode = entityCode + "using System.Linq;" + Environment.NewLine;
            entityCode = entityCode + "using System.Text;" + Environment.NewLine;
            entityCode = entityCode + "using System.Threading.Tasks;" + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "namespace " + entityProjectName + Environment.NewLine;
            entityCode = entityCode + "    {" + Environment.NewLine;
            entityCode = entityCode + "        public class " + entityName + "Entity" + Environment.NewLine;
            entityCode = entityCode + "        {" + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "            public " + Utilities.RemoveIDFromLastIfAny(columnName) + "Entity " + Utilities.RemoveIDFromLastIfAny(columnName) + "Entity { get; set; }" + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "            public " + Utilities.GetCodeDataType(table.Rows[i][1].ToString()) + " " + columnName + " { get; set; }" + Environment.NewLine;
                }
            }

            entityCode = entityCode + Environment.NewLine;

            //generate default constructor
            entityCode = entityCode + "            public " + entityName + "Entity()" + Environment.NewLine;
            entityCode = entityCode + "            {" + Environment.NewLine;
            entityCode = entityCode + "            }" + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;

            //generate constructor to convert tbl to entity
            entityCode = entityCode + "            public " + entityName + "Entity(" + dataAccessProjectName + "." + tableName + " " + Utilities.MakeFirstLetterLowerCase(tableName) + ")" + Environment.NewLine;
            entityCode = entityCode + "            {" + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                this." + Utilities.RemoveIDFromLastIfAny(columnName) + "Entity = new " + Utilities.RemoveIDFromLastIfAny(columnName) + "Entity(" + Utilities.MakeFirstLetterLowerCase(tableName) + ".tbl" + columnName + ");" + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                this." + columnName + " = " + Utilities.MakeFirstLetterLowerCase(tableName) + "." + columnName + ";" + Environment.NewLine;
                }
            }
            entityCode = entityCode + "            }" + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;

            //generate constructor to convert entity to tbl
            entityCode = entityCode + "            public T MapToTableModel<T>() where T : class" + Environment.NewLine;
            entityCode = entityCode + "            {" + Environment.NewLine;
            entityCode = entityCode + "                " + dataAccessProjectName + "." + tableName + " " + Utilities.MakeFirstLetterLowerCase(Utilities.RemoveTbl(tableName)) + " = new " + dataAccessProjectName + "." + tableName + "();" + Environment.NewLine;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                " + Utilities.MakeFirstLetterLowerCase(Utilities.RemoveTbl(tableName)) + "." + Utilities.RemoveIDFromLastIfAny(columnName) + " = this." + columnName + "Entity.ID;" + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                " + Utilities.MakeFirstLetterLowerCase(Utilities.RemoveTbl(tableName)) + "." + columnName + " = this." + columnName + ";" + Environment.NewLine;
                }
            }
            entityCode = entityCode + "                return " + Utilities.MakeFirstLetterLowerCase(Utilities.RemoveTbl(tableName)) + " as T;" + Environment.NewLine;
            entityCode = entityCode + "            }" + Environment.NewLine;

            entityCode = entityCode + "        }" + Environment.NewLine;
            entityCode = entityCode + "    }" + Environment.NewLine;

            Utilities.CreateFile(location, entityProjectName, entityName + "Entity.cs", entityCode);
        }
    }
}
