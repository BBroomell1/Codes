using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Dashboard
{
    class SaveFile
    {
        // Creates a string with column seperated value to add to a CSV file
        public static void Makeworksheet(System.Data.DataTable dataTable)
        {
            StringBuilder sb = new StringBuilder();
            string[] columnNames;

            columnNames = dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
            sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dataTable.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field => string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    sb.AppendLine(string.Join(",", fields));
                }


            SaveToCSV(sb);
        }

        // Takes the string from MakeWorksheet and adds it to a csv and saves that file using Dialog box
        private static void SaveToCSV(StringBuilder sb)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "CSV|*.csv";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Throws error if error saving file
                try
                {
                    File.WriteAllText(sf.FileName, sb.ToString());
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), "Alert", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // Takes unique IDs from passed arraylist and queries the original data set for those students and outputs the command to use in a query
        public static String commandForSave(ArrayList IDList)
        {
            String cmd = "SELECT * FROM TempTableFinal WHERE (ID) IN (";

            for (int i = 0; i < IDList.Count; i++)
            {
                cmd += "'" + IDList[i] + "'";


                if (i < IDList.Count - 1)
                {
                    cmd += ", ";
                }
            }

            cmd += ") ORDER BY Name , ID";
            return cmd;
        }
    }

}
