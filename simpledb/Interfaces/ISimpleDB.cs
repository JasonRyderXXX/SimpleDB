using System;
using System.Collections.Generic;
using System.Data;

namespace SimpleDB.Interfaces
{
    public interface ISimpleDB:IDisposable
    {
        /// <summary>
        /// Returns an IDataReader from a sql result
        /// </summary>
        /// <param name="sql">SQL String to be executed</param>
        /// <param name="parameters">Parameters of sql and object</param>
        /// <returns>Returns Generic IDataReader</returns>
        IDataReader GetDataReader(string sql, IDictionary<string, object> parameters);
        /// <summary>
        /// Returns an IDataReader array from a sql result
        /// </summary>
        /// <param name="sql">SQL String to be executed</param>
        /// <param name="parameters">Parameters of sql and object</param>
        /// <returns>Returns Generic IDataReader array</returns>
        IDataReader[] GetDataReaders(string sql, IDictionary<string, object> parameters);
        /// <summary>
        /// Executes a statement that returns only a row count or the first row is int.
        /// </summary>
        /// <param name="sql">SQL String to be executed</param>
        /// <param name="parameters">Parameters of sql and object</param>
        /// <returns>row count or scaler that returns int</returns>
        int ExecuteStatement(string sql, IDictionary<string, object> parameters);
        /// <summary>
        /// Executes a statement that returns DataSet, with the intent of reducing round trips to the DB server.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, IDictionary<string, object> parameters);
        /// <summary>
        /// Returns a string from the first parameter. 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object GetScaler(string sql, IDictionary<string, object> parameters);
    }
}
