/* Query Library - contains various SQLite queries used throughout application */

using System;
using System.Collections;
using System.Data.SQLite; // For SQLite interaction

namespace Dashboard
{
    class QueryLib
    {
        // Global Header Variables
        public static String FY = "FY";
        public static String courseCD = "\"Course Cd\""; //Course Number
        public static String Course = "Course"; // Course Description
        public static String classNo = "\"Class No\""; // Iteration
        public static String Name = "Name";
        public static String SSN = "SSN"; // ID
        public static String startDt = "\"Start Date\""; // Started
        public static String endDt = "\"End Date\""; // Ended
        public static String Ctry = "Ctry";
        public static String Comp = "Comp"; // Race
        public static String Rank = "Rnk";
        public static String PMOS = "PMOS"; //Role
        public static String Training = "\"Training MOS\"";
        public static String Gender = "Gender";
        public static String Instat = "\"IPStatus Cd\"";
        public static String instatReason = "\'IPStatus Reason\'";
        public static String Outstat = "\"OPStatus Cd\"";
        public static String outstatReason = "OPReason"; // Reason
        public static String Remarks = "Remarks";


        // Returns each student's status in the pipeline courses selected, returning a 1
        // if they have passed the pipeline course and a 0 if they have failed
        public static void CreatePipelineTable(SQLiteConnection conn, ArrayList pipeline)
        {
            string cmd = "DROP TABLE IF EXISTS TempPipelineTable; CREATE TABLE TempPipelineTable AS SELECT DISTINCT TBF1." + Name + ", TBF1." + SSN + ", TBF1." + Comp + ", TBF1." + Rank + ", TBF1." + PMOS + ", ";
            for (int i = 0; i < pipeline.Count; i++)
            {
                cmd += "CAST ((SELECT IFNULL(GROUP_CONCAT(TBF2." + Instat + " || TBF2." + Outstat + ", ', '), '') FROM TempTableFinal AS TBF2 WHERE (TBF2." + courseCD + " ||'_'|| TBF2." + classNo + ") = '" + pipeline[i] + "' " +
                    "AND TBF1." +SSN+ " = TBF2." + SSN + ") AS TEXT) AS Pathway_" + pipeline[i];

                if (i < pipeline.Count - 1)
                {
                    cmd += ", ";
                }
            }

            cmd += " FROM TempTableFinal AS TBF1 ORDER BY " + Name + ", " + SSN + "";

            TempDB.RunNonQuery(conn, cmd);

            string cmd2 = "DROP TABLE IF EXISTS PipelineTable; CREATE TABLE PipelineTable AS SELECT DISTINCT * FROM TempPipelineTable TBF WHERE (SELECT GROUP_CONCAT( ";
            for (int j = 0; j < pipeline.Count; j++)
            {
                cmd2 += "Pathway_" + pipeline[j];
                if (j < pipeline.Count - 1)
                {
                    cmd2 += " || ";
                }

            }
            cmd2 += ", '') FROM TempPipelineTable WHERE " + SSN + " = TBF." + SSN + ") != '' ";
            TempDB.RunNonQuery(conn, cmd2);
        }

        // Returns the full Pipeline Table
        public static string GetPipelineTable()
        {
            string cmd = "SELECT * FROM PipelineTable";

            return cmd;
        }

        // Returns all data for students in a selected pipeline
        public static string PipelineStudents(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM TempTableFinal WHERE (" + courseCD + " || '_' || " + classNo + ") IN (";

            for (int i = 0; i < pipeline.Count; i++)
            {
                cmd += "'" + pipeline[i] + "'";
                

                if (i < pipeline.Count - 1)
                {
                    cmd += ", ";
                }
            }

            cmd += ") ORDER BY " + Name + ", " + SSN + "";


            return cmd;
        }

        // Returns total number of students in one or more pipeline courses
        public static string TotalPipelineStudents()
        {
            string cmd = "SELECT COUNT(DISTINCT " + SSN + ") FROM PipelineTable";


            return cmd;
        }

        // Returns students in one or more pipeline courses
        public static string GetTotalPipelineStudents()
        {
            string cmd = "SELECT DISTINCT * FROM PipelineTable";


            return cmd;
        }

        // Returns total number of students NOT in first pipeline course
        // but in subsequent pipeline courses i.e. students that transferred in
        public static string PipelineTransferStudents(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE Pathway_" + pipeline[0] + " = ''";

            return cmd;
        }

        // Returns students NOT in first pipeline course
        // but in subsequent pipeline courses i.e. students that transferred in
        public static string GetPipelineTransferStudents(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE Pathway_" + pipeline[0] + " = ''";

            return cmd;
        }

        // Returns total number of students that did not transfer in
        public static string NonPipelineTransferStudents(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE Pathway_" + pipeline[0] + " != ''";

            return cmd;
        }

        // Returns students that did not transfer in
        public static string GetNonPipelineTransferStudents(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE Pathway_" + pipeline[0] + " != ''";

            return cmd;
        }

        // Returns total number of students that completed all pipeline courses without failing
        public static string PipelineFullCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE Pathway_" + pipeline[0] + " IN ('NP', 'RP') ";

            if (pipeline.Count > 1)
            {
                cmd += " AND ";

                for (int i = 1; i < pipeline.Count; i++)
                {
                    cmd += "Pathway_" + pipeline[i] + " = 'NP' ";

                    if (i < pipeline.Count - 1)
                    {
                        cmd += " AND ";
                    }
                }
            }

             cmd += " ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns students that completed all pipeline courses without failing
        public static string GetPipelineFullCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE Pathway_" + pipeline[0] + " IN ('NP', 'RP') ";

            if (pipeline.Count > 1)
            {
                cmd += " AND ";

                for (int i = 1; i < pipeline.Count; i++)
                {
                    cmd += "Pathway_" + pipeline[i] + " = 'NP' ";

                    if (i < pipeline.Count - 1)
                    {
                        cmd += " AND ";
                    }
                }
            }

            cmd += " ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns total number of students that transferred into pipeline and completed last pipeline item
        public static string PipelinePartialCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE " +
                "Pathway_" + pipeline[0] + " = '' AND " +
                "Pathway_" + pipeline[pipeline.Count - 1] + " LIKE '%P' " +
                "ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns students that transferred into pipeline and completed last pipeline item
        public static string GetPipelinePartialCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE " +
                "Pathway_" + pipeline[0] + " = '' AND " +
                "Pathway_" + pipeline[pipeline.Count - 1] + " LIKE '%P' " +
                "ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns total number of students that did not transfer into pipeline and completed last pipeline item
        public static string PipelineNonTransferCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE " +
                "Pathway_" + pipeline[0] + " != '' AND " +
                "Pathway_" + pipeline[pipeline.Count - 1] + " LIKE '%P' " +
                "ORDER BY " + Name + ", " + SSN + "";


            return cmd;
        }

        // Returns students that did not transfer into pipeline and completed last pipeline item
        public static string GetPipelineNonTransferCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE " +
                "Pathway_" + pipeline[0] + " != '' AND " +
                "Pathway_" + pipeline[pipeline.Count - 1] + " LIKE '%P' " +
                "ORDER BY " + Name + ", " + SSN + "";


            return cmd;
        }

        // Returns total number of students that completed all pipeline courses 
        // with Course 1 being their first attempt ever i.e. Instat for Course 1 = N
        public static string PipelineFirstAttemptCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE ";

            for (int i = 0; i < pipeline.Count; i++)
            {
                cmd += "Pathway_" + pipeline[i] + " = 'NP' ";

                if (i < pipeline.Count - 1)
                    cmd += " AND ";
            }

            cmd += " ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns students that completed all pipeline courses without failing
        public static string GetPipelineFirstAttemptCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE ";

            for (int i = 0; i < pipeline.Count; i++)
            {
                cmd += "Pathway_" + pipeline[i] + " = 'NP' ";

                if (i < pipeline.Count - 1)
                    cmd += " AND ";
            }
         
            cmd += " ORDER BY " + Name + ", " + SSN + "";

            

            return cmd;
        }

        // Returns total number of students that completed all pipeline courses 
        // whether or not Course 1 was their first attempt
        public static string PipelineCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT COUNT(*) FROM PipelineTable WHERE Pathway_" + pipeline[pipeline.Count - 1] + " LIKE '%P' ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns students that completed all pipeline courses 
        // whether or not Course 1 was their first attempt
        public static string GetPipelineCompletion(ArrayList pipeline)
        {
            string cmd = "SELECT * FROM PipelineTable WHERE Pathway_" + pipeline[pipeline.Count - 1] + " LIKE '%P' ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Returns the pass rate for each pipeline course select
        public static string PipeLineChart(ArrayList pipeline)
        {
            string cmd = "SELECT ";
            for (int i = 0; i < pipeline.Count; i++)
            {
                cmd += "( 100.0 * (1.0 * (SELECT Count(" + Outstat + ") FROM TempTableFinal WHERE " + Outstat + " = 'P' AND (" + courseCD + " || '_' || " + classNo + ") = '" + pipeline[i] +"')" +
                    " / (SELECT Count(" + Outstat + ") FROM TempTableFinal WHERE (" + courseCD + " || '_' || " + classNo + ") = '" + pipeline[i] + "') ))" +
                    " AS Pathway_" + pipeline[i];

                if (i < pipeline.Count - 1)
                {
                    cmd += ", ";
                }
            }

            return cmd;
        }

        // Returns the total count of courses in the dataset
        public static string CourseCount()
        {
            string cmd = "SELECT Count(DISTINCT " + courseCD + ") FROM TempTableFinal";

            return cmd;
        }

        // Returns the total count of students in the dataset
        public static string StudentCount()
        {
            string cmd = "SELECT Count(DISTINCT " + SSN + ") FROM TempTableFinal";

            return cmd;
        }

        // Returns overall aggregate pass rate for all courses in dataset
        public static string TotalPassRate()
        {
            string cmd = "SELECT (1.0 * (SELECT Count(" + Outstat + ") FROM TempTableFinal WHERE " + Outstat + " = 'P')) " +
            "/ (SELECT Count(" + Outstat + ") FROM TempTableFinal)";

            return cmd;
        }

        // Returns the total count of retaken courses in the dataset
        public static string TotalRetaken()
        {
            string cmd = "SELECT Count(*) FROM TempTableFinal WHERE " + Instat + " = 'R'";

            return cmd;
        }

        // Returns list of students that have retaken a course and failed
        public static string AtRiskStudents()
        {
            string cmd = "SELECT DISTINCT * FROM TempTableFinal WHERE " + Instat + " = 'R' AND " + Outstat + " = 'F' ORDER BY " + Name + ", " + SSN + ", " +Training+ ", " + classNo + "";

            return cmd;
        }

        // Returns list of all students in dataset
        public static string AllStudents()
        {
            string cmd = "SELECT DISTINCT * FROM TempTableFinal ORDER BY " + Name + ", " + SSN + ", " + Training + ", " + classNo + "";

            return cmd;
        }

        // Query ID's currently on DataGrid to toggle pipeline view
        public static string TogglePipeline(ArrayList ID)
        {
            string cmd = "SELECT DISTINCT * FROM PipelineTable WHERE " + SSN + " IN ( "; 

            for (int i =0; i < ID.Count; i++)
            {
                cmd += "'" + ID[i] + "'";

                if (i < ID.Count - 1)
                {
                    cmd += ", ";
                }
            }
            
            cmd += ") ORDER BY " + Name + ", " + SSN + "";

            return cmd;
        }

        // Query ID's currently on DataGrid to toggle full view
        public static string ToggleFull(ArrayList ID)
        {
            string cmd = "SELECT DISTINCT * FROM TempTableFinal WHERE " + SSN + " IN ( ";

            for (int i = 0; i < ID.Count; i++)
            {
                cmd += "'" + ID[i] + "'";

                if (i < ID.Count - 1)
                {
                    cmd += ", ";
                }
            }

            cmd += ") ORDER BY " + Name + ", " + SSN + ", " + courseCD + ", " + classNo + "";

            return cmd;
        }

    }
}
