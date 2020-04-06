using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Generator
{
    public static class Utilities
    {
        public static string GetRelationshipType(string columnName)
        {
            if (columnName.Length > 2)
            {
                string suffix = columnName.Substring(columnName.Length - 2);
                if (suffix.ToLower() == "id")
                {
                    string prefix = columnName.Substring(0, columnName.Length - 2);
                    return prefix + "Entity";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static string CreateFile(string location, string folderName, string filename, string content)
        {
            try
            {
                string path = location + "\\" + folderName + "\\" + filename;
                // Delete the file if it exists.
                if (File.Exists(path))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
                }

                // Create the file.
                using (FileStream fs = File.Create(path))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(content);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return filename;
            }
        }

        public static bool CreateFolder(string location, string folderName)
        {
            try
            {
                // To create a string that specifies the path to a subfolder under your 
                // top-level folder, add a name for the subfolder to folderName.
                string pathString = System.IO.Path.Combine(location, folderName);
                System.IO.Directory.CreateDirectory(pathString);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetCodeDataType(string databaseDataType)
        {
            if (databaseDataType == "int")
            {
                return "int";
            }
            else if (databaseDataType == "nvarchar")
            {
                return "string";
            }
            else if (databaseDataType == "money")
            {
                return "decimal";
            }
            else if (databaseDataType == "bit")
            {
                return "bool";
            }
            else if (databaseDataType == "date" || databaseDataType == "datetime")
            {
                return "DateTime";
            }
            else
            {
                return "string";
            }
        }

        public static string MakeFirstLetterLowerCase(string input)
        {
            string prefix = input.Substring(0, 1);
            string suffix = input.Substring(1);

            return prefix.ToLower() + suffix;
        }

        public static string RemoveTbl(string tablename)
        {
            string prefix = tablename.Substring(0, 3);
            return tablename.Replace(prefix, "");
        }

        public static string RemoveID(string tablename)
        {
            string prefix = tablename.Substring(0, tablename.Length - 2);
            return prefix;
        }

        public static string RemoveIDFromLastIfAny(string input)
        {
            string lastTwoChars = input.Substring(input.Length - 2);

            if (lastTwoChars.Equals("ID"))
            {
                return input.Substring(0, input.Length - 2);
            }
            else
            {
                return input;
            }
        }

        public static string MakePlural(string input)
        {
            if(input.Substring(input.Length-1).ToLower() == "y")
            {
                return input.Substring(0, input.Length - 1) + "ies";
            }
            else
            {
                return input + "s";
            }
        }

    }
}
