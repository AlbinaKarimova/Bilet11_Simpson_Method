using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.ComponentModel;

namespace Exam11_Simpson
{
    public class DBHelper
    {
        private static MySqlConnection? conn = null;

        // Конструктор будет вызваться только внутри этого класса,
        // чтобы не было других экземпляров класса
        private DBHelper(
            String host,
            int port,
            String user,
            String password,
            String database
        )
        {
            // Строка подключения
            var connStr = $"Server={host};port={port};database={database};User Id={user};password={password}";
            conn = new MySqlConnection(connStr);
            // Открываем строку подключения
            conn?.Open();
        }

        // Поле единственное для класса
        private static DBHelper instance = null;
        public static DBHelper GetInstance(
                        String host = "localhost",
                        int port = 0,
                        String user = "root",
                        String password = "",
                        String database = ""
                        )
        {
            if (instance == null)
            {
                instance = new DBHelper(host, port, user, password, database);
            }
            return instance;
        }

        // Метод выводит id, login, name всех пользователей  
        public string GetAll()
        {
            string res = " ";
            var queryStr = "SELECT * FROM `users`";

            // Создание команды
            var cmd = conn?.CreateCommand();
            cmd.CommandText = queryStr;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res += reader[0].ToString() + " " + reader[1].ToString() + reader[3].ToString() + " " + '\n';
                    }
                }
            }
            return res;
        }
        // Метод выводит id пользователя по его логину
        public string SelectId(string login)
        {
            string res = " ";
            var queryStr = $"SELECT `id` FROM `users` WHERE `login`=@login";

            // Создание команды
            var cmd = conn?.CreateCommand();
            cmd.CommandText = queryStr;
            cmd.Parameters.AddWithValue("@login", login);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res = reader[0].ToString();
                    }
                }
            }
            return res;
        }

        // Запрос на добавление даты и результатов пользователя
        public void InsertNew(double _result, int _id, DateTime _dt)
        {
            try
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO `integral` (result, user_id, date)" +
                    "VALUES (@result, @user_id, @date);";
                cmd.Parameters.Add(new MySqlParameter("@result", _result));
                cmd.Parameters.Add(new MySqlParameter("@user_id", _id));
                cmd.Parameters.Add(new MySqlParameter("@date", _dt)); ;
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Запрос на добавление данных не выполнен");
            }
        }
    }
}
