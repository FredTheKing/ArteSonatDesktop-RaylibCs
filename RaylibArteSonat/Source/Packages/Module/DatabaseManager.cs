using System.Data.SQLite;

namespace RaylibArteSonat.Source.Packages.Module;

public class DatabaseManager : CallDebuggerInfoTemplate
{
  private readonly SQLiteConnection _connection = new SQLiteConnection("Data Source=Resources/db.db");

  public List<List<dynamic>> GetTable(string table_name)
  {
    SQLiteCommand command = new SQLiteCommand("SELECT * FROM " + table_name, _connection);
    SQLiteDataReader reader = command.ExecuteReader();
    
    List<List<dynamic>> full_list = [];
    while (reader.Read())
    {
      List<dynamic?> obj_list = [];
      for (int i = 0; i < reader.FieldCount; i++)
        obj_list.Add(reader[i].ToString());
      full_list.Add(obj_list);
    }
    return full_list;
  }
  
  public void EnableDatabase()
  { 
    _connection.Open();
    Console.WriteLine("INFO: DB: Database opened successfully");
  }

  public void DisableDatabase()
  {
    _connection.Close();
    Console.WriteLine("INFO: DB: Database closed successfully");
  }
}