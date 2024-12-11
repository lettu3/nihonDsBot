using Microsoft.Data.Sqlite;

public static class DatabaseInitializer
{
    public static void Initialize()
    {
        using (var connection = new SqliteConnection("Data Source=Gifs.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS Gifs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    FileName TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}