/* Stat Selection - Fetches stat data from database for clickable stat fields */

using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace Dashboard
{
    class StatsSelection
    {
        public static DataView TotalPipelineStudents(SQLiteConnection conn)
        {
            string cmd = QueryLib.GetTotalPipelineStudents();
            return TempDB.FetchData(conn, cmd).DefaultView;
        }
        public static DataView TotalTransferInStudents(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetPipelineTransferStudents(pipeline);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }
        public static DataView PipelineFullCompletion(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetPipelineFullCompletion(pipeline);

            //Console.WriteLine(cmd);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }

        public static DataView PipelinePartialCompletion(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetPipelinePartialCompletion(pipeline);

            //Console.WriteLine(cmd);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }

        public static DataView PipelineFirstAttemptCompletion(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetPipelineFirstAttemptCompletion(pipeline);

            //Console.WriteLine(cmd);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }

        public static DataView PipelineCompletion(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetPipelineCompletion(pipeline);

            //Console.WriteLine(cmd);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }

        public static DataView NonPipelineTransferStudents(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetNonPipelineTransferStudents(pipeline);

            //Console.WriteLine(cmd);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }

        public static DataView PipelineNonTransferCompletion(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = QueryLib.GetPipelineNonTransferCompletion(pipeline);

            //Console.WriteLine(cmd);
            return TempDB.FetchData(conn, cmd).DefaultView;
        }
    }
}
