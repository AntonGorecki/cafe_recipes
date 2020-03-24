// Test Changes
using System;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DataContext : IDataContext
    {
        public string ConnectionString
        {
            get; private set;
        }

        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public void ExecuteAction(Action<SqlConnection> action, SqlConnection connection = null)
        {
            if(connection != null)
            {
                action(connection);
            }
            else
            {
                using(var newConnection = new SqlConnection(ConnectionString))
                {
                    newConnection.Open();
                    action(newConnection);
                }
            }
        }

        public T ExecuteCommand<T>(Func<SqlCommand, T> action)
        {
            using(var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    return action(command);
                }
            }
        }

        public T ExecuteCommand<T>(SqlCommand cmd, Func<SqlCommand, T> action, SqlConnection connection = null, SqlTransaction transation = null)
        {
            if(connection != null)
            {
                cmd.Connection = connection;
                if(transation != null)
                {
                    cmd.Transaction = transation;
                }
                return action(cmd);
            }

            using(var newConnecton = new SqlConnection(ConnectionString))
            {
                newConnecton.Open();
                cmd.Connection = newConnecton;
                if (transation != null)
                {
                    cmd.Transaction = transation;
                }

                return action(cmd);
            }
        }

        public void ExecuteNonQueryCommand(SqlCommand cmd, SqlConnection connection = null, SqlTransaction transaction = null)
        {
            if(connection != null)
            {
                cmd.Connection = connection;
                if (transaction != null)
                {
                    cmd.Transaction = transaction;
                }
                cmd.ExecuteNonQuery();
            }
            else
            {
                using(var newConnection = new SqlConnection(ConnectionString))
                {
                    newConnection.Open();
                    cmd.Connection = newConnection;
                    if(transaction != null)
                    {
                        cmd.Transaction = transaction;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
