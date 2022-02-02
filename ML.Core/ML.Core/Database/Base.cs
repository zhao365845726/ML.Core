using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Core.Database
{
    public class Base
    {
        public const string SELECT = "SELECT ";
        public const string FROM = " FROM ";
        public const string WHERE = " WHERE ";
        public const string INSERT = "INSERT ";
        public const string UPDATE = "UPDATE ";
        public const string TOP = "TOP";
        public const string GROUPBY = " GROUP BY ";
        public const string ORDERBY = " ORDER BY ";
        public const string ASC = "ASC";
        public const string DESC = "DESC";
        public const string LIMIT = "LIMIT";

        public StringBuilder sb;
        public Base()
        {
            sb = new StringBuilder();
        }

        public void Select(string fields)
        {
            sb.Append($"{SELECT}{fields}");
        }

        public void Form(string tables)
        {
            sb.Append($"{FROM}{tables}");
        }
    }
}
