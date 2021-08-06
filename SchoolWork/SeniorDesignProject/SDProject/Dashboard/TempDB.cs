/* Temp Database - contains import, database dreation, and database interaction Functions */

using System;
using System.Windows; // For error throwing
using System.Collections; // For ArrayLists
using System.Data; // For data table/data view/ etc
using System.IO; // For file io
using System.Text; // For text encoding
using System.Data.SQLite; // For SQLite interaction
using Microsoft.VisualBasic.FileIO; // For TextField Parser
using Spire.Xls; // For excel file interaction

 class TempDB
    {
        // Global table counter for database tables
        public static int tablenum = 0;

        //===============================================================================================================//
        //================================================== Importing ==================================================//
        //===============================================================================================================//

        // Parse imported CSV file and initiate transfer of data into SQLite DB
        // Takes an active SQLite connection and a string containing file location
        public static void ImportData(SQLiteConnection conn, String file)
        {
            ArrayList headers = new ArrayList(); // Holds column headers
            ArrayList rows = new ArrayList(); // Holds row strings
            int i = 0;

        // If file is excel format convert to csv
        int fileFlag = 0;
            if (file.Contains(".xlsx"))
            {
                //System.Environment.Exit(1);
                fileFlag = 1;
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(file);
                Worksheet sheet = workbook.Worksheets[0];
                sheet.SaveToFile("temp.csv", ",", Encoding.UTF8);
                file = "temp.csv";
            }

            // Parse CSV using "," delimeter
            using (TextFieldParser parser = new TextFieldParser(file))
            {
                parser.Delimiters = new string[] { "," };

                while (true)
                {
                    string[] parts = parser.ReadFields();

                    // Get column headers
                    if (i == 0)
                    {
                        foreach (string part in parts)
                            headers.Add(part);
                    }

                    // Break at end of row
                    if (parts == null)
                    {
                        break;
                    }
                    // Get row data after headers
                    if (i > 0)
                    {
                        foreach (string part in parts)
                            if(part.Contains("/"))
                            {
                                try
                                {
                                    var date = DateTime.Parse(part);
                                    rows.Add(dateConvert(date));
                                }
                                catch (Exception ex)
                                {
                                    System.Windows.MessageBox.Show("Dataset may contain incorrect dates. Please check dataset and re-import.", "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    System.Environment.Exit(1);
                                }
                            
                             }
                            else rows.Add(part);
                    }
                    i++;
                }
            }
            int numRows = i - 1; // Total number of rows minus header
            int numCols = headers.Count; // Total number of columns

            // Delete converted csv file
            if (fileFlag == 1)
                File.Delete(file);

            //Console.WriteLine("Number of rows: " + numRows); // Prints number of rows in dataset - for testing
            //Console.WriteLine("Number of cols: " + numCols); // Prints number of columns in dataset - for testing

            // Initiate transfer of data to SQLite DB
            TempTable(conn, headers, numCols);
            TempData(conn, rows, numRows, numCols);
        }

        // Converts dates to yyyy-MM-dd format
        public static string dateConvert(DateTime date)
        {
            var dateVal = date.ToString("yyyy-MM-dd");
            return dateVal;
        }

        // Union all imported tables into one final table
        public static void TableUnion(SQLiteConnection conn)
        {
            String cmd = "CREATE TABLE TempTableFinal AS Select * FROM TempTable0";
            
            if (tablenum > 0)
            {
                for (int i = 1; i <= tablenum; i++)
                {
                    cmd += " UNION Select * FROM 'TempTable" + i + "'";
                }
            }

            RunNonQuery(conn, cmd);

        }

        // Dynamically create temp SQL table command using file headers from imported file
        public static void TempTable(SQLiteConnection conn, ArrayList headers, int numCols)
        {
            var tableFlag = 1;
            
            // Check if table is already in database
            while (tableFlag > 0)
            {
                string check = "SELECT count(*) FROM sqlite_master WHERE type = 'table' AND name = 'TempTable" + tablenum + "'";
                DataTable table = TempDB.FetchData(conn, check);
                tableFlag = int.Parse(table.Rows[0][0].ToString());
                
                if (tableFlag > 0)
                    tablenum++;
            }
            

            String cmd = "CREATE TABLE TempTable" + tablenum + "(";
            for (int i = 0; i < numCols; i++)
            {
                cmd += "'" + headers[i];
                if (i == numCols - 1)
                    cmd += "' TEXT";
                else
                    cmd += "' TEXT, ";
            }
            cmd += ")";

            // Create the new table
            RunNonQuery(conn, cmd);
        }

        // Creates INSERT SQL command for imported rows
        private static void TempData(SQLiteConnection conn, ArrayList rows, int numRows, int numCols)
        {
            int line = 1; // keeps track of each line/row added to statement
            int cursor = 0; // keeps track of current position in rows

            // Iterates through each row
            for (int j = 0; j < numRows; j++)
            {
                // Creates single insert statement for each row/line
                String cmd = "INSERT INTO TempTable" +tablenum+" VALUES(";
                for (int i = cursor; i < (numCols * line); i++)
                {
                    cmd += ("\'" + rows[i] + "\'");
                    if (i == (numCols * line) - 1)
                    {
                        cursor++;
                        break;
                    }
                    else
                        cmd += ", ";
                    cursor++;
                }
                cmd += ")";
                line++;

            //Console.WriteLine(cmd);

                // Insert each row into DB
                RunNonQuery(conn, cmd);
            }
        }
        
         //======================================================================================================================================//
        //================================================== Database Creation and Interaction ==================================================//
        //=======================================================================================================================================//

        // Create a new Temporary SQLite DB Connection in memory
        public static SQLiteConnection CreateConnection()
        {
        
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source = :memory:;");

            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return sqlite_conn;
        }

        // Executes a SQLite Non-Query Script
        // Takes in active SQLite connection and string containing SQL command
        public static void RunNonQuery(SQLiteConnection conn, String cmd)
        {
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = cmd;
            sqlite_cmd.ExecuteNonQuery();
        }

        // Read data from SQLite DB
        // Takes in active SQLite connection and string containing SQL command
        public static DataTable FetchData(SQLiteConnection conn, String cmd)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            DataTable table = new DataTable();

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = cmd;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            table.Load(sqlite_datareader); // Load query results into DataTable

        // Use the following code to print database contents to console - for testing
        /*
            // Read all DataTable contents
            //Console.WriteLine();
            foreach (DataRow dataRow in table.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }
        */
            return table;
        }
        
        // Returns single cell value for stat field
        public static Object StatResult(SQLiteConnection conn, String cmd)
        {
            DataTable table = TempDB.FetchData(conn, cmd);
            Object number = table.Rows[0][0];

            return number;
        }

        // Close SQLite DB connection
        // Takes in active SQLite connection
        public static void CloseConnection(SQLiteConnection conn)
        {
            conn.Close();
        }
}

