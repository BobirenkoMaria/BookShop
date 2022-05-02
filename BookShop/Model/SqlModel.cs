using BookShop.DTO;
using BookShop.Tools;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Model
{
    public class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();
            return sqlModel;
        }

        public int Insert<T>(T value)
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDB.GetDB();
            // лучше эти 2 запроса объединить в один с помощью транзакции
            int id = db.GetNextID(table);
            db.ExecuteNonQuery(query.Item1, query.Item2);
            return id;
        }
        // обновляет объект в бд по его id
        public void Update<T>(T value) where T : BaseDTO
        {
            string table;
            List<(string, object)> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }

        public void Delete<T>(T value) where T : BaseDTO
        {
            var type = value.GetType();
            string table = GetTableName(type);
            var db = MySqlDB.GetDB();
            string query = $"delete from `{table}` where id = {value.ID}";
            db.ExecuteNonQuery(query);
        }

        public int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDB.GetDB().GetRowsCount(table);
        }

        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Table;
        }

        public List<Books> SelectBooksDB()
        {
            var groups = new List<Books>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `books`";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        groups.Add(new Books
                        {
                            ID = dr.GetInt32("id"),
                            Title = dr.GetString("Title"),
                            Author = dr.GetString("Author"),
                            ReleaseDate = dr.GetDateTime("ReleaseDate"),
                            Description = dr.GetString("Description"),
                            Genre = dr.GetString("Genre")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return groups;
        }

        public List<Sales> SelectSalesDB()
        {
            var groups = new List<Sales>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `sales`";

            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        groups.Add(new Sales
                        {
                            ID = dr.GetInt32("id"),
                            ImportBooks = dr.GetInt32("ImportBooks"),
                            Expenses = dr.GetInt32("Expenses"),
                            BooksSold = dr.GetInt32("BooksSold"),
                            Price = dr.GetInt32("Price")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return groups;
        }

        public List<Operations> SelectOperationsDB()
        {
            var groups = new List<Operations>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `operations`";

            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        groups.Add(new Operations
                        {
                            ID = dr.GetInt32("id"),
                            Book_id = dr.GetInt32("Book_id"),
                            ImportBooks = dr.GetInt32("ImportBooks"),
                            Expenses = dr.GetInt32("Expenses"),
                            BooksSold = dr.GetInt32("BooksSold"),
                            Price = dr.GetInt32("Price"),
                            OperationDate = dr.GetDateTime("OperationDate")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return groups;
        }

        public List<OperationsStr> SelectOperationsStrDB()
        {
            var groups = new List<OperationsStr>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT * FROM `operations`, `books` WHERE operations.Book_id = books.id";

            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        groups.Add(new OperationsStr
                        {
                            ID = dr.GetInt32("id"),
                            Book_id = dr.GetString("Title"),
                            ImportBooks = dr.GetInt32("ImportBooks"),
                            Expenses = dr.GetInt32("Expenses"),
                            BooksSold = dr.GetInt32("BooksSold"),
                            Price = dr.GetInt32("Price"),
                            OperationDate = dr.GetDateTime("OperationDate")
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return groups;
        }

        public List<DateModel> SelectStatisticDB(string RowTitle)
        {
            List<DateModel> StrInRow = new List<DateModel>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"SELECT {RowTitle}, OperationDate FROM `operations`";

            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        int Value;
                        StrInRow.Add(
                            new DateModel
                            {
                                Date = dr.GetDateTime("OperationDate"),
                                Value = dr.GetInt32(RowTitle)
                            });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return StrInRow;
        }

        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<(string, object)> values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<(string, object)> values, int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static List<MySqlParameter> InitParameters(List<(string, object)> values, StringBuilder stringBuilder)
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Item2));
                return $"{s.Item1} = @p{count++}";
            });
            stringBuilder.Append(string.Join(',', rows));
            return parameters;
        }

        private static void GetMetaData<T>(T value, out string table, out List<(string, object)> values)
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<(string, object)>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new(column, prop.GetValue(value)));
                }
            }
        }
    }
}
