using System;
using System.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DudelkaBot
{
    public partial class DataBaseInterface
    {
        public Tuple<T1, T2> Get<T1, T2>(string query, params object[] args)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader == null || !reader.HasRows)
                        return null;

                    reader.Read();
                    return new Tuple<T1, T2>(
                        ReadValue<T1>(reader, 0),
                        ReadValue<T2>(reader, 1)
                    );
                }
            }
        }

        public List<TResult> GetArray<T1, T2, TResult>(Func<Tuple<T1, T2>, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1, T2>(query, args).Select(selector).ToList();
        }

        public List<Tuple<T1, T2>> GetArray<T1, T2>(string query, params object[] args)
        {
            var result = new List<Tuple<T1, T2>>();

            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            result.Add(new Tuple<T1, T2>(
                                ReadValue<T1>(reader, 0),
                                ReadValue<T2>(reader, 1)
                            ));
                        }
            }

            return result;
        }

        public Tuple<T1, T2, T3> Get<T1, T2, T3>(string query, params object[] args)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader == null || !reader.HasRows)
                        return null;

                    reader.Read();
                    return new Tuple<T1, T2, T3>(
                        ReadValue<T1>(reader, 0),
                        ReadValue<T2>(reader, 1),
                        ReadValue<T3>(reader, 2)
                    );
                }
            }
        }

        public List<TResult> GetArray<T1, T2, T3, TResult>(Func<Tuple<T1, T2, T3>, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1, T2, T3>(query, args).Select(selector).ToList();
        }

        public List<Tuple<T1, T2, T3>> GetArray<T1, T2, T3>(string query, params object[] args)
        {
            var result = new List<Tuple<T1, T2, T3>>();

            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            result.Add(new Tuple<T1, T2, T3>(
                                ReadValue<T1>(reader, 0),
                                ReadValue<T2>(reader, 1),
                                ReadValue<T3>(reader, 2)
                            ));
                        }
            }

            return result;
        }

        public Tuple<T1, T2, T3, T4> Get<T1, T2, T3, T4>(string query, params object[] args)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader == null || !reader.HasRows)
                        return null;

                    reader.Read();
                    return new Tuple<T1, T2, T3, T4>(
                        ReadValue<T1>(reader, 0),
                        ReadValue<T2>(reader, 1),
                        ReadValue<T3>(reader, 2),
                        ReadValue<T4>(reader, 3)
                    );
                }
            }
        }

        public List<TResult> GetArray<T1, T2, T3, T4, TResult>(Func<Tuple<T1, T2, T3, T4>, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1, T2, T3, T4>(query, args).Select(selector).ToList();
        }

        public List<Tuple<T1, T2, T3, T4>> GetArray<T1, T2, T3, T4>(string query, params object[] args)
        {
            var result = new List<Tuple<T1, T2, T3, T4>>();

            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            result.Add(new Tuple<T1, T2, T3, T4>(
                                ReadValue<T1>(reader, 0),
                                ReadValue<T2>(reader, 1),
                                ReadValue<T3>(reader, 2),
                                ReadValue<T4>(reader, 3)
                            ));
                        }
            }

            return result;
        }

        public Tuple<T1, T2, T3, T4, T5> Get<T1, T2, T3, T4, T5>(string query, params object[] args)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader == null || !reader.HasRows)
                        return null;

                    reader.Read();
                    return new Tuple<T1, T2, T3, T4, T5>(
                        ReadValue<T1>(reader, 0),
                        ReadValue<T2>(reader, 1),
                        ReadValue<T3>(reader, 2),
                        ReadValue<T4>(reader, 3),
                        ReadValue<T5>(reader, 4)
                    );
                }
            }
        }

        public List<TResult> GetArray<T1, T2, T3, T4, T5, TResult>(Func<Tuple<T1, T2, T3, T4, T5>, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1, T2, T3, T4, T5>(query, args).Select(selector).ToList();
        }

        public List<Tuple<T1, T2, T3, T4, T5>> GetArray<T1, T2, T3, T4, T5>(string query, params object[] args)
        {
            var result = new List<Tuple<T1, T2, T3, T4, T5>>();

            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            result.Add(new Tuple<T1, T2, T3, T4, T5>(
                                ReadValue<T1>(reader, 0),
                                ReadValue<T2>(reader, 1),
                                ReadValue<T3>(reader, 2),
                                ReadValue<T4>(reader, 3),
                                ReadValue<T5>(reader, 4)
                            ));
                        }
            }

            return result;
        }

        public Tuple<T1, T2, T3, T4, T5, T6> Get<T1, T2, T3, T4, T5, T6>(string query, params object[] args)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader == null || !reader.HasRows)
                        return null;

                    reader.Read();
                    return new Tuple<T1, T2, T3, T4, T5, T6>(
                        ReadValue<T1>(reader, 0),
                        ReadValue<T2>(reader, 1),
                        ReadValue<T3>(reader, 2),
                        ReadValue<T4>(reader, 3),
                        ReadValue<T5>(reader, 4),
                        ReadValue<T6>(reader, 5)
                    );
                }
            }
        }

        public List<TResult> GetArray<T1, T2, T3, T4, T5, T6, TResult>(Func<Tuple<T1, T2, T3, T4, T5, T6>, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1, T2, T3, T4, T5, T6>(query, args).Select(selector).ToList();
        }

        public List<Tuple<T1, T2, T3, T4, T5, T6>> GetArray<T1, T2, T3, T4, T5, T6>(string query, params object[] args)
        {
            var result = new List<Tuple<T1, T2, T3, T4, T5, T6>>();

            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);
                using (var reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            result.Add(new Tuple<T1, T2, T3, T4, T5, T6>(
                                ReadValue<T1>(reader, 0),
                                ReadValue<T2>(reader, 1),
                                ReadValue<T3>(reader, 2),
                                ReadValue<T4>(reader, 3),
                                ReadValue<T5>(reader, 4),
                                ReadValue<T6>(reader, 5)
                            ));
                        }
            }

            return result;
        }

        public Tuple<T1, T2, T3, T4, T5, T6, T7> Get<T1, T2, T3, T4, T5, T6, T7>(string query, params object[] args)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader == null || !reader.HasRows)
                        return null;

                    reader.Read();
                    return new Tuple<T1, T2, T3, T4, T5, T6, T7>(
                        ReadValue<T1>(reader, 0),
                        ReadValue<T2>(reader, 1),
                        ReadValue<T3>(reader, 2),
                        ReadValue<T4>(reader, 3),
                        ReadValue<T5>(reader, 4),
                        ReadValue<T6>(reader, 5),
                        ReadValue<T7>(reader, 6)
                    );
                }
            }
        }

        public List<TResult> GetArray<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<Tuple<T1, T2, T3, T4, T5, T6, T7>, TResult> selector, string query, params object[] args)
        {
            return GetArray<T1, T2, T3, T4, T5, T6, T7>(query, args).Select(selector).ToList();
        }

        public List<Tuple<T1, T2, T3, T4, T5, T6, T7>> GetArray<T1, T2, T3, T4, T5, T6, T7>(string query, params object[] args)
        {
            var result = new List<Tuple<T1, T2, T3, T4, T5, T6, T7>>();

            using (MySqlCommand cmd = new MySqlCommand(query, Connection))
            {
                BindParammers(cmd, args);
                using (var reader = cmd.ExecuteReader())
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            result.Add(new Tuple<T1, T2, T3, T4, T5, T6, T7>(
                                ReadValue<T1>(reader, 0),
                                ReadValue<T2>(reader, 1),
                                ReadValue<T3>(reader, 2),
                                ReadValue<T4>(reader, 3),
                                ReadValue<T5>(reader, 4),
                                ReadValue<T6>(reader, 5),
                                ReadValue<T7>(reader, 6)
                            ));
                        }
            }

            return result;
        }

    }
}