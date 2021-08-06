using System;
using System.Data.SQLite;
using Spire.Xls;
using System.Text;
using System.IO;
using System.Data;

public class DB
{
    public static void Main()

    {
        string file = "D:\\SP\\Senior-Design\\DummyData.csv";
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

        SQLiteConnection sqlite_conn;
        sqlite_conn = TempDB.TempDB.CreateConnection();
        TempDB.TempDB.ImportData(sqlite_conn, file);

        System.Console.Write("Number of Unique ID's: ");
        string cmd = "SELECT COUNT(DISTINCT ID) FROM TempTable";
        TempDB.TempDB.FetchData(sqlite_conn, cmd);

        System.Console.Write("Number of Students that have retaken a course: ");
        string cmd2 = "SELECT COUNT(DISTINCT ID) FROM TempTable WHERE InStat = 'R'";
        TempDB.TempDB.FetchData(sqlite_conn, cmd2);

        System.Console.Write("Number of Students that have retaken a course and failed it: ");
        string cmd3 = "SELECT COUNT(DISTINCT ID) FROM TempTable WHERE InStat = 'R' AND OutStat = 'F'";
        TempDB.TempDB.FetchData(sqlite_conn, cmd3);

        System.Console.Write("Names and ID's of Students that have retaken a course and failed it: \n");
        string cmd4 = "SELECT DISTINCT Name, ID FROM TempTable WHERE InStat = 'R' AND OutStat = 'F'";
        TempDB.TempDB.FetchData(sqlite_conn, cmd4);


        TempDB.TempDB.CloseConnection(sqlite_conn);
        
        if (fileFlag == 1)
            File.Delete(file);
    }
}
