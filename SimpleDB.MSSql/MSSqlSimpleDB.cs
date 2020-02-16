using SimpleDB.Interfaces;
using SimpleDB.MSSql.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace SimpleDB.MSSql
{
    public class MSSqlSimpleDB : ISimpleDB
    {
        private ISimpleConnectionString Connectionstring { get; }

        public MSSqlSimpleDB(ISimpleConnectionString connectionstring)
        {
            Connectionstring = connectionstring;
        }
        private SqlConnection connection;
        private SqlConnection sqlconnection()
        {
            if (connection != null && connection.State != ConnectionState.Open)
            {
                connection.Dispose();
                connection = new SqlConnection(Connectionstring.GetConnectionString());
                connection.Open();
            } else if (connection == null)
            {
                connection = new SqlConnection(Connectionstring.GetConnectionString());
                connection.Open();
            }

            return connection;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="tresult"></typeparam>
        /// <param name="requestit"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private tresult Execute<tresult>(Func<SqlCommand,tresult> requestit, string sql, IDictionary<string, object> parameters)
        {
            tresult ret = default(tresult);
            var sqlclient = sqlconnection();
            {
                    using (var command = sqlclient.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.Parameters.AddRange(parameters.GetSqlParameters());
                        ret = requestit(command);
                    }
            }
            return ret;
        }
        public int ExecuteStatement(string sql, IDictionary<string, object> parameters) =>
            Execute<int>((a) => a.ExecuteNonQuery(), sql, parameters);

        public IDataReader GetDataReader(string sql, IDictionary<string, object> parameters) =>
            Execute<IDataReader>((a) => a.ExecuteReader(CommandBehavior.CloseConnection), sql, parameters);

        public IDataReader[] GetDataReaders(string sql, IDictionary<string, object> parameters) =>
            Execute<IDataReader[]>((a) => a
            .GetDataSet()
            .ToEnumerable()
            .Select(b=> b.CreateDataReader())
            .ToArray(), sql, parameters);


        public DataSet GetDataSet(string sql, IDictionary<string, object> parameters) =>
            Execute<DataSet>((a) =>a.GetDataSet(), sql, parameters);

        public object GetScaler(string sql, IDictionary<string, object> parameters)=>
        Execute<object>((a) => a.ExecuteScalar(), sql, parameters);
    }
}
