using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
namespace DataLayer
{
    public interface IDataContext
    {
        string ConnectionString { get;}
        void ExecuteAction(Action<SqlConnection> action, SqlConnection connection = null);
        void ExecuteNonQueryCommand(SqlCommand cmd, SqlConnection connection = null, SqlTransaction transaction = null );
        T ExecuteCommand<T>(Func<SqlCommand, T> action);
        T ExecuteCommand<T>(SqlCommand cmd, Func<SqlCommand, T> action, SqlConnection connection = null, SqlTransaction transation = null);
    }
}
