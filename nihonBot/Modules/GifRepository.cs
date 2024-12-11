using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class GifRepository
{
    private const string ConnectionString = "Data Source=Gifs.db";
    private readonly string _gifDirectory;

    public GifRepository(string gifDirectory)
    {
        _gifDirectory = gifDirectory;
    }

    public void AddGif(string fileName)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Gifs (FileName) VALUES (@fileName)";
            command.Parameters.AddWithValue("@fileName", fileName);
            command.ExecuteNonQuery();
        }
    }

    // Bullshit method, delete later
    public void AddGifs(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            string fileName = $"{i}.gif";
            AddGif(fileName);
        }
    }

    public List<string> GetAllGifs()
    {
        var gifs = new List<string>();

        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT FileName FROM Gifs";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    gifs.Add(reader.GetString(0));
                }
            }
        }

        return gifs;
    }

    public string GetRandomGif()
    {
        var gifs = GetAllGifs();
        if (gifs.Count == 0)
        {
            return "";
        }
        var random = new Random();
        int index = random.Next(gifs.Count);
        return Path.Combine(_gifDirectory, gifs[index]);
    }

    public void ClearDatabase()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Gifs";
            command.ExecuteNonQuery();
        }
    }
}