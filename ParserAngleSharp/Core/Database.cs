using MySql.Data.MySqlClient;

namespace ParserAngleSharp
{
    class Database
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=boardgames");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed) 
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
