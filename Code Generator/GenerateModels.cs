using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateModel
    {
        public string GenerateModelForAllTables(string connectionString, string location, string modelProjectName, string entityProjectName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select table_name from information_schema.tables where TABLE_NAME <> 'sysdiagrams' and TABLE_TYPE = 'BASE TABLE'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (Utilities.CreateFolder(location, modelProjectName))
            {
                while (reader.Read())
                {
                    string tablename = reader.GetString(0);

                    string prefix = tablename.Substring(0, 3);
                    string entityName = tablename.Replace(prefix, "");
                    string modelName = tablename.Replace(prefix, "");

                    GenerateSingleModel(location, modelProjectName, entityProjectName, modelName, entityName);
                }

                return string.Empty;
            }
            else
            {
                return "Folder not created";
            }
        }

        public void GenerateSingleModel(string location, string modelProjectName, string entityProjectName, string modelName, string entityName)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            string query = "select COLUMN_NAME, DATA_TYPE from information_schema.COLUMNS where TABLE_NAME = 'tbl" + modelName + "'";

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

            GenerateAndSave(location, table, modelProjectName, entityProjectName, modelName, entityName);
        }

        public void GenerateAndSave(string location, DataTable table, string modelProjectName, string entityProjectName, string modelName, string entityName)
        {
            string entityCode = "using System;" + Environment.NewLine;
            entityCode = entityCode + "using System.Collections.Generic;" + Environment.NewLine;
            entityCode = entityCode + "using System.Linq;" + Environment.NewLine;
            entityCode = entityCode + "using System.Text;" + Environment.NewLine;
            entityCode = entityCode + "using System.Threading.Tasks;" + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "namespace " + modelProjectName + ".Models" + Environment.NewLine;
            entityCode = entityCode + "    {" + Environment.NewLine;
            entityCode = entityCode + "        public class " + modelName + "Model" + Environment.NewLine;
            entityCode = entityCode + "        {" + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "            public " + Utilities.RemoveID(columnName) + "Model " + Utilities.RemoveID(columnName) + "Model { get; set; }" + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "            public " + Utilities.GetCodeDataType(table.Rows[i][1].ToString()) + " " + columnName + " { get; set; }" + Environment.NewLine;
                }
            }

            entityCode = entityCode + Environment.NewLine;

            //generate default constructor
            entityCode = entityCode + "            public " + modelName + "Model()" + Environment.NewLine;
            entityCode = entityCode + "            {" + Environment.NewLine;
            entityCode = entityCode + "            }" + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;

            //generate constructor to convert entity to model
            entityCode = entityCode + "            public " + modelName + "Model(" + entityProjectName + "." + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + "Entity)" + Environment.NewLine;
            entityCode = entityCode + "            {" + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                this." + Utilities.RemoveID(columnName) + "Model = new " + Utilities.RemoveID(columnName) + "Model(" + Utilities.MakeFirstLetterLowerCase(entityName) + "Entity." + Utilities.RemoveID(columnName) + "Entity);" + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                this." + columnName + " = " + Utilities.MakeFirstLetterLowerCase(entityName) + "Entity." + columnName + ";" + Environment.NewLine;
                }
            }
            entityCode = entityCode + "            }" + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;

            //generate constructor to convert model to entity
            entityCode = entityCode + "            public T MapToEntity<T>() where T : class" + Environment.NewLine;
            entityCode = entityCode + "            {" + Environment.NewLine;
            entityCode = entityCode + "                " + entityProjectName + "." + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + " = new " + entityProjectName + "." + entityName + "Entity();" + Environment.NewLine;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + Utilities.RemoveID(columnName) + "Entity = this." + Utilities.RemoveID(columnName) + "Model.MapToEntity<" + entityProjectName + "." + Utilities.RemoveID(columnName) + "Entity>();" + Environment.NewLine;
                    //entityCode = entityCode + "                " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + Utilities.RemoveID(columnName) + "Entity = new " + Utilities.RemoveID(columnName) + "Entity(this." + Utilities.RemoveID(columnName) + "Model);" + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + columnName + " = this." + columnName + ";" + Environment.NewLine;
                }
            }
            entityCode = entityCode + "                return " + Utilities.MakeFirstLetterLowerCase(entityName) + " as T;" + Environment.NewLine;
            entityCode = entityCode + "            }" + Environment.NewLine;

            entityCode = entityCode + "        }" + Environment.NewLine;
            entityCode = entityCode + "    }" + Environment.NewLine;

            Utilities.CreateFile(location, modelProjectName, modelName + "Model.cs", entityCode);
        }
    }
}
