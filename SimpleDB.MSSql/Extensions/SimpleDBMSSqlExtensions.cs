using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SimpleDB.MSSql.Extensions
{
    public static class SimpleDBMSSqlExtensions
    {
        public static SqlParameter[] GetSqlParameters(this IDictionary<string, object> parameters) =>
            parameters
            .ToList()
            .Select(a => new SqlParameter(a.Key, a.Value))
            .ToArray();
        public static IEnumerable<DataTable> ToEnumerable(this DataSet dataSet)
        {
            foreach (DataTable dt in dataSet.Tables)
                yield return dt;
        }
        public static DataSet GetDataSet(this SqlCommand command)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
        

    }
}
