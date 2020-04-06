using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateBusinessLogic
    {
        public string GenerateLogicForAllTables(string connectionString, string location, string databaseName, string businessLogicProjectName, string entityProjectName, string dataAccessProjectName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select table_name from information_schema.tables where TABLE_NAME <> 'sysdiagrams' and TABLE_TYPE = 'BASE TABLE'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("TableName");

            if (Utilities.CreateFolder(location, businessLogicProjectName))
            {
                while (reader.Read())
                {
                    table.Rows.Add(reader.GetString(0));

                    string tablename = reader.GetString(0);

                    string prefix = tablename.Substring(0, 3);
                    string entityName = tablename.Replace(prefix, "");

                    GenerateSingleLogic(location, databaseName, businessLogicProjectName, entityProjectName, dataAccessProjectName, entityName, tablename);
                }

                return string.Empty;
            }
            else
            {
                return "Folder not created";
            }
        }

        public void GenerateSingleLogic(string location, string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
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

            GenerateBusinessMainCode(table, location, databaseName, businessProjectName, entityProjectName, dataAccessProjectName, entityName, tableName);
        }

        public string GenerateBusinessMainCode(DataTable table, string location, string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "using System;" + Environment.NewLine;
            entityCode = entityCode + "using System.Collections.Generic;" + Environment.NewLine;
            entityCode = entityCode + "using System.Linq;" + Environment.NewLine;
            entityCode = entityCode + "using System.Text;" + Environment.NewLine;
            entityCode = entityCode + "using System.Threading.Tasks;" + Environment.NewLine;
            entityCode = entityCode + "using " + entityProjectName + ";" + Environment.NewLine;
            entityCode = entityCode + "using " + dataAccessProjectName + ";" + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "namespace " + businessProjectName + Environment.NewLine;
            entityCode = entityCode + "    {" + Environment.NewLine;
            entityCode = entityCode + "         public class " + entityName + "Manager" + Environment.NewLine;
            entityCode = entityCode + "         {" + Environment.NewLine;

            //business logic
            entityCode = entityCode + "             " + GenerateGetAllCode(table, databaseName, businessProjectName, entityProjectName, dataAccessProjectName, entityName, tableName) + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;
            entityCode = entityCode + "             " + GenerateGetSingleCode(table, databaseName, businessProjectName, entityProjectName, dataAccessProjectName, entityName, tableName) + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;
            entityCode = entityCode + "             " + GenerateSaveCode(table, databaseName, businessProjectName, entityProjectName, dataAccessProjectName, entityName, tableName) + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;
            entityCode = entityCode + "             " + GenerateUpdateCode(table, databaseName, businessProjectName, entityProjectName, dataAccessProjectName, entityName, tableName) + Environment.NewLine;
            entityCode = entityCode + Environment.NewLine;
            entityCode = entityCode + "             " + GenerateDeleteCode(databaseName, businessProjectName, entityProjectName, dataAccessProjectName, entityName, tableName) + Environment.NewLine;

            entityCode = entityCode + "         }" + Environment.NewLine;
            entityCode = entityCode + "    }" + Environment.NewLine;

            Utilities.CreateFile(location, businessProjectName, entityName + "Manager.cs", entityCode);

            return entityCode;
        }

        private string GenerateGetAllCode(DataTable table, string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "public List<" + entityName + "Entity> GetAll() " + Environment.NewLine;
            entityCode = entityCode + "         { " + Environment.NewLine;
            entityCode = entityCode + "             try " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 List<" + entityName + "Entity> " + entityName + "Entities = new List<" + entityName + "Entity>(); " + Environment.NewLine;
            entityCode = entityCode + "                 " + databaseName + "Entities entity = new " + databaseName + "Entities(); " + Environment.NewLine;
            entityCode = entityCode + "                 List<" + tableName + "> " + Utilities.MakePlural(Utilities.RemoveTbl(tableName)) + " = entity." + Utilities.MakePlural(tableName) + ".ToList(); " + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                 foreach (" + tableName + " " + Utilities.RemoveTbl(tableName) + " in " + Utilities.MakePlural(Utilities.RemoveTbl(tableName)) + ") " + Environment.NewLine;
            entityCode = entityCode + "                 { " + Environment.NewLine;
            entityCode = entityCode + "                     " + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + " = new " + entityName + "Entity(); " + Environment.NewLine;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                     " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + Utilities.RemoveID(columnName) + "Entity = new " + Utilities.RemoveID(columnName) + "Entity(" + Utilities.RemoveTbl(tableName) + ".tbl" + Utilities.RemoveID(columnName) + "); " + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                     " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + columnName + " = " + Utilities.RemoveTbl(tableName) + "." + columnName + "; " + Environment.NewLine;
                }
            }

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                     " + entityName + "Entities.Add(" + Utilities.MakeFirstLetterLowerCase(entityName) + "); " + Environment.NewLine;
            entityCode = entityCode + "                 } " + Environment.NewLine;

            entityCode = entityCode + "                 return " + entityName + "Entities; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "             catch (Exception ex) " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 return null; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "         } " + Environment.NewLine;

            return entityCode;
        }

        private string GenerateGetSingleCode(DataTable table, string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "public " + entityName + "Entity GetSingle(int ID) " + Environment.NewLine;
            entityCode = entityCode + "         { " + Environment.NewLine;
            entityCode = entityCode + "             try " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 " + databaseName + "Entities entity = new " + databaseName + "Entities(); " + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                 " + tableName + " " + Utilities.RemoveTbl(tableName) + " = entity." + Utilities.MakePlural(tableName) + ".Where(x => x.ID == ID).FirstOrDefault(); " + Environment.NewLine;

            entityCode = entityCode + "                 " + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + " = new " + entityName + "Entity(); " + Environment.NewLine;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                 " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + Utilities.RemoveID(columnName) + "Entity = new " + Utilities.RemoveID(columnName) + "Entity(" + Utilities.RemoveTbl(tableName) + ".tbl" + Utilities.RemoveID(columnName) + "); " + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                 " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + columnName + " = " + Utilities.RemoveTbl(tableName) + "." + columnName + "; " + Environment.NewLine;
                }
            }

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                 return " + Utilities.MakeFirstLetterLowerCase(entityName) + "; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "             catch (Exception ex) " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 return null; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "         } " + Environment.NewLine;

            return entityCode;
        }

        private string GenerateSaveCode(DataTable table, string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "public bool Save(" + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + ") " + Environment.NewLine;
            entityCode = entityCode + "         { " + Environment.NewLine;
            entityCode = entityCode + "             try " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 " + tableName + " " + Utilities.RemoveTbl(tableName) + " = new " + tableName + "(); " + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                 " + Utilities.RemoveTbl(tableName) + "." + columnName + " = " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + Utilities.RemoveID(columnName) + "Entity.ID; " + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                 " + Utilities.RemoveTbl(tableName) + "." + columnName + " = " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + columnName + "; " + Environment.NewLine;
                }
            }

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                 " + databaseName + "Entities entity = new " + databaseName + "Entities(); " + Environment.NewLine;
            entityCode = entityCode + "                 entity." + Utilities.MakePlural(tableName) + ".Add(" + Utilities.RemoveTbl(tableName) + "); " + Environment.NewLine;
            entityCode = entityCode + "                 entity.SaveChanges(); " + Environment.NewLine;

            entityCode = entityCode + "                 return true; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "             catch (Exception) " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 throw; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "         } " + Environment.NewLine;

            return entityCode;
        }

        private string GenerateUpdateCode(DataTable table, string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "public bool Update(" + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + ") " + Environment.NewLine;
            entityCode = entityCode + "         { " + Environment.NewLine;
            entityCode = entityCode + "             try " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 " + tableName + " new" + Utilities.RemoveTbl(tableName) + " = new " + tableName + "(); " + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string columnName = table.Rows[i][0].ToString();
                string relatioshipType = Utilities.GetRelationshipType(columnName);
                if (relatioshipType != string.Empty)
                {
                    entityCode = entityCode + "                 new" + Utilities.RemoveTbl(tableName) + "." + columnName + " = " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + Utilities.RemoveID(columnName) + "Entity.ID; " + Environment.NewLine;
                }
                else
                {
                    entityCode = entityCode + "                 new" + Utilities.RemoveTbl(tableName) + "." + columnName + " = " + Utilities.MakeFirstLetterLowerCase(entityName) + "." + columnName + "; " + Environment.NewLine;
                }
            }

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                 " + databaseName + "Entities entity = new " + databaseName + "Entities(); " + Environment.NewLine;

            entityCode = entityCode + "                 " + tableName + " old" + Utilities.RemoveTbl(tableName) + " = entity." + Utilities.MakePlural(tableName) + ".Where(x => x.ID == " + Utilities.MakeFirstLetterLowerCase(entityName) + ".ID).FirstOrDefault(); " + Environment.NewLine;
            entityCode = entityCode + "                 entity.Entry(old" + Utilities.RemoveTbl(tableName) + ").CurrentValues.SetValues(new" + Utilities.RemoveTbl(tableName) + "); " + Environment.NewLine;
            entityCode = entityCode + "                 entity.SaveChanges(); " + Environment.NewLine;

            entityCode = entityCode + "                 return true; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "             catch (Exception) " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 throw; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "         } " + Environment.NewLine;

            return entityCode;
        }

        private string GenerateDeleteCode(string databaseName, string businessProjectName, string entityProjectName, string dataAccessProjectName, string entityName, string tableName)
        {
            string entityCode = "public bool Delete(" + entityName + "Entity " + Utilities.MakeFirstLetterLowerCase(entityName) + ") " + Environment.NewLine;
            entityCode = entityCode + "         { " + Environment.NewLine;
            entityCode = entityCode + "             try " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;

            entityCode = entityCode + "                 " + databaseName + "Entities entity = new " + databaseName + "Entities(); " + Environment.NewLine;
            entityCode = entityCode + "                 " + tableName + " " + Utilities.RemoveTbl(tableName) + " = entity." + Utilities.MakePlural(tableName) + ".Where(x => x.ID == " + Utilities.MakeFirstLetterLowerCase(entityName) + ".ID).FirstOrDefault(); " + Environment.NewLine;

            entityCode = entityCode + Environment.NewLine;

            entityCode = entityCode + "                 entity." + Utilities.MakePlural(tableName) + ".Remove(" + Utilities.RemoveTbl(tableName) + "); " + Environment.NewLine;
            entityCode = entityCode + "                 entity.SaveChanges(); " + Environment.NewLine;

            entityCode = entityCode + "                 return true; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "             catch (Exception) " + Environment.NewLine;
            entityCode = entityCode + "             { " + Environment.NewLine;
            entityCode = entityCode + "                 throw; " + Environment.NewLine;
            entityCode = entityCode + "             } " + Environment.NewLine;
            entityCode = entityCode + "         } " + Environment.NewLine;

            return entityCode;
        }

    }
}
