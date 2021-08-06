using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dashboard
{
    public partial class Pipeline : System.Windows.Controls.Page
    {
        public class createPipeline
        {
            ArrayList pipeline = new ArrayList();
            DataTable pipelinedTable = new DataTable();
        }

        public static ArrayList GetCourses(SQLiteConnection conn)
        {
            ArrayList courses = new ArrayList();
            string cmd = "SELECT DISTINCT " + QueryLib.courseCD + " FROM TempTableFinal ORDER BY " + QueryLib.courseCD + "";
            DataTable table = TempDB.FetchData(conn, cmd);


            foreach (DataRow dataRow in table.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    courses.Add(item.ToString());
                }
            }

            //Console.WriteLine(courses);

            return courses;
        }

        public static ArrayList GetPipeLineOptions(SQLiteConnection conn, ArrayList courses)
        {
            ArrayList pipelineOptions = new ArrayList();
            var numCourses = courses.Count;
            String command = "SELECT DISTINCT (" + QueryLib.courseCD + " || '_' || " + QueryLib.classNo + ") FROM TempTableFinal WHERE " + QueryLib.courseCD + " IN(";
            for (var i = 0; i < numCourses; i++)
            {
                if (i != numCourses - 1)
                    command += "'" +courses[i] + "', ";


                else
                    command += "'" +courses[i]+ "'";
            }
            command += ") ORDER BY " + QueryLib.courseCD + "";

            //Console.WriteLine(command);
            DataTable table4 = TempDB.FetchData(conn, command);

            foreach (DataRow row in table4.Rows)
                foreach (String dc in row.ItemArray)
                    pipelineOptions.Add(dc.ToString());

            return pipelineOptions;
        }
    }
}
