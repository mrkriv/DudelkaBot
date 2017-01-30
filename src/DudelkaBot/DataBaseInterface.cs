using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DudelkaBot
{
    public partial class DataBaseInterface
    {
        public readonly MySqlConnection Connection;

        public DataBaseInterface(string host, uint port, string database, string login, string password)
        {
            string connectString = string.Format("Server={0};Port={1};database={2};UID={3};password={4};Allow User Variables=True",
                host, port, database, login, password);

            Connection = new MySqlConnection(connectString);
            Connection.Open();
        }

        public bool Execute(string query, params object[] args)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(string.Format(query, args), Connection))
                    return cmd.ExecuteNonQuery() != 0;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public T Get<T>(string query, params object[] args)
        {
            bool temp;
            return Get<T>(query, out temp, args);
        }

        public T Get<T>(string query, out bool success, params object[] args)
        {
            T result = default(T);
            success = false;

            try
            {
                using (var cmd = new MySqlCommand(string.Format(query, args), Connection))
                using (var reader = cmd.ExecuteReader())
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        result = ReadValue<T>(reader, 0);
                        success = true;
                    }
            }
            catch (MySqlException)
            {
                success = false;
            }

            return result;
        }

        public List<TResult> GetArray<T1, TResult>(Func<T1, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1>(query, args).Select(selector).ToList();
        }

        public List<T1> GetArray<T1>(string query, params object[] args)
        {
            var result = new List<T1>();

            using (MySqlCommand cmd = new MySqlCommand(string.Format(query, args), Connection))
            using (var reader = cmd.ExecuteReader())
                if (reader.HasRows)
                    while (reader.Read())
                        result.Add(ReadValue<T1>(reader, 0));

            return result;
        }

        public T ReadValue<T>(System.Data.Common.DbDataReader reader, int index)
        {
            object obj = reader.GetValue(index);
            if (obj is T)
                return (T)obj;

            try
            {
                return (T)obj;
            }
            catch { }

            return default(T);
        }


        private void BindParammers(MySqlCommand cmd, object[] args)
        {
            for (int i = 0; i < args.Length; i++)
                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@" + i.ToString(),
                    Value = args[i],
                });
        }
    }
}