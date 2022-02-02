using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML.Core.Database
{
    public static class Base
    {
        public static string SELECT = "SELECT ";
        public static string FROM = " FROM ";
        public static string WHERE = " WHERE ";
        public static string INSERT = "INSERT ";
        public static string UPDATE = "UPDATE ";
        public static string TOP = "TOP";
        public static string GROUPBY = " GROUP BY ";
        public static string ORDERBY = " ORDER BY ";
        public static string ASC = "ASC";
        public static string DESC = "DESC";
        public static string LIMIT = "LIMIT";
        public static string JOIN = "JOIN";
        public static string LEFTJOIN = "LEFT JOIN";
        public static string RIGHTJOIN = "RIGHT JOIN";
        public static string ON = "ON";

        public static StringBuilder sb = new StringBuilder();

        public delegate StringBuilder SqlHandle(string param);
        //public static Base()
        //{
        //    sb = new StringBuilder();
        //}

        public static void Select(string fields)
        {
            sb.Append($"{SELECT}{fields}");
        }

        public static void Form(string tables)
        {
            sb.Append($"{FROM}{tables}");
        }

        public static void Demo()
        {
            List<string> vs = new List<string>();
            vs.Select(x => true).ToList();
        }
    }
}
