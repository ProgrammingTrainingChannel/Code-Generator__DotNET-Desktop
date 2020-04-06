using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public class GenerateAngularComponent
    {
        public string GenerateComponentForAllTables(string connectionString, string location, string interfaceProjectName, string serviceProjectName)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "select table_name from information_schema.tables where TABLE_NAME <> 'sysdiagrams' and TABLE_TYPE = 'BASE TABLE'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Columns.Add("TableName");

            if (Utilities.CreateFolder(location, serviceProjectName))
            {
                while (reader.Read())
                {
                    table.Rows.Add(reader.GetString(0));

                    string tablename = reader.GetString(0);

                    string prefix = tablename.Substring(0, 3);
                    string entityName = tablename.Replace(prefix, "");

                    GenerateSingleComponent(location, interfaceProjectName, serviceProjectName, entityName, tablename);
                }

                return string.Empty;
            }
            else
            {
                return "Folder not created";
            }
        }

        public void GenerateSingleComponent(string location, string interfaceProjectName, string serviceProjectName, string entityName, string tableName)
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

            GenerateComponentMainCode(table, location, interfaceProjectName, serviceProjectName, entityName, tableName);
        }

        public string GenerateComponentMainCode(DataTable table, string location, string interfaceProjectName, string serviceProjectName, string entityName, string tableName)
        {
            string serviceCode = "import { Injectable } from '@angular/core';" + Environment.NewLine;
            serviceCode = serviceCode + "import { Http, Response } from '@angular/http';" + Environment.NewLine;
            serviceCode = serviceCode + "import { Headers, RequestOptions } from '@angular/http';" + Environment.NewLine;
            serviceCode = serviceCode + "import { Observable } from 'rxjs/Observable';" + Environment.NewLine;

            serviceCode = serviceCode + "import 'rxjs/add/operator/map';" + Environment.NewLine;
            serviceCode = serviceCode + "import 'rxjs/add/operator/catch';" + Environment.NewLine;

            serviceCode = serviceCode + "import { ITeacher } from '../interface/teacher.interface';" + Environment.NewLine + Environment.NewLine;

            serviceCode = serviceCode + "@Injectable()" + Environment.NewLine;
            serviceCode = serviceCode + "export class "+ Utilities.MakeFirstLetterLowerCase(entityName) + "Service" + Environment.NewLine;
            serviceCode = serviceCode + "       {" + Environment.NewLine;

            serviceCode = serviceCode + "             constructor(private _http: Http) { }" + Environment.NewLine + Environment.NewLine;

            //service
            serviceCode = serviceCode + "             " + GenerateGetAllCode(table, interfaceProjectName, serviceProjectName, entityName) + Environment.NewLine;
            serviceCode = serviceCode + Environment.NewLine;
            serviceCode = serviceCode + "             " + GenerateGetSingleCode(table, interfaceProjectName, serviceProjectName, entityName) + Environment.NewLine;
            serviceCode = serviceCode + Environment.NewLine;
            serviceCode = serviceCode + "             " + GenerateSaveUpdateCode(table, interfaceProjectName, serviceProjectName, entityName) + Environment.NewLine;
            serviceCode = serviceCode + Environment.NewLine;
            serviceCode = serviceCode + "             " + GenerateDeleteCode(interfaceProjectName, serviceProjectName, entityName) + Environment.NewLine;

            serviceCode = serviceCode + "       }" + Environment.NewLine;

            Utilities.CreateFile(location, serviceProjectName, entityName + ".service.cs", serviceCode);

            return serviceCode;
        }

        private string GenerateImports(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "getAll(): Observable < I" + entityName + "[] > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  return this._http.get('http://localhost:5570/api/'" + entityName + "'/GetAll')" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((response: Response) => < I" + entityName + "[] > response.json());" + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateInitialization(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "getAll(): Observable < I" + entityName + "[] > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  return this._http.get('http://localhost:5570/api/'" + entityName + "'/GetAll')" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((response: Response) => < I" + entityName + "[] > response.json());" + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateComboAndTablePopulation(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "getAll(): Observable < I" + entityName + "[] > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  return this._http.get('http://localhost:5570/api/'" + entityName + "'/GetAll')" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((response: Response) => < I" + entityName + "[] > response.json());" + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateDataValidation(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "getAll(): Observable < I" + entityName + "[] > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  return this._http.get('http://localhost:5570/api/'" + entityName + "'/GetAll')" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((response: Response) => < I" + entityName + "[] > response.json());" + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateGetAllCode(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "getAll(): Observable < I" + entityName + "[] > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  return this._http.get('http://localhost:5570/api/'" + entityName + "'/GetAll')" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((response: Response) => < I" + entityName + "[] > response.json());" + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateGetSingleCode(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "getSingle(id: number): Observable < IStudent[] > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  return this._http.get(\"http://localhost:5570/api/" + entityName + "/GetSingle?ID=\" + id)" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((response: Response) => < IStudent[] > response.json());" + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateSaveUpdateCode(DataTable table, string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "save("+ Utilities.MakeFirstLetterLowerCase(entityName) +": I"+ entityName +"): Observable < any > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  let bodyString = JSON.stringify(" + Utilities.MakeFirstLetterLowerCase(entityName) + ");" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  let headers = new Headers({ 'Content-Type': 'application/json' });" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  let options = new RequestOptions({ headers: headers });" + Environment.NewLine + Environment.NewLine;

            serviceCode = serviceCode + "   	     	  return this._http.post(\"http://localhost:5570/api/" + entityName +"/Save\", bodyString, options) " + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((res: Response) => res.json())" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .catch((error: any) => Observable.throw(error.json().error || 'Server error')); " + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }

        private string GenerateDeleteCode(string interfaceProjectName, string serviceProjectName, string entityName)
        {
            string serviceCode = "delete(" + Utilities.MakeFirstLetterLowerCase(entityName) + ": I" + entityName + "): Observable < any > {" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  let bodyString = JSON.stringify(" + Utilities.MakeFirstLetterLowerCase(entityName) + ");" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  let headers = new Headers({ 'Content-Type': 'application/json' });" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	  let options = new RequestOptions({ headers: headers });" + Environment.NewLine + Environment.NewLine;

            serviceCode = serviceCode + "   	     	  return this._http.post(\"http://localhost:5570/api/" + entityName + "/Delete\", bodyString, options) " + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .map((res: Response) => res.json())" + Environment.NewLine;
            serviceCode = serviceCode + "   	     	         .catch((error: any) => Observable.throw(error.json().error || 'Server error')); " + Environment.NewLine;
            serviceCode = serviceCode + "	     };" + Environment.NewLine;

            return serviceCode;
        }
    }
}
