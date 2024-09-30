using System.Text;
using EasyCompressor;
using Raylib_cs;

namespace ResourcesGenerator.Source.Packages;

public class ResourceGenerator
{
  private Dictionary<string, string> _resources = new Dictionary<string, string>();
  
  public void AddResource(string resource_name, string filename)
  {
    _resources.Add(resource_name, filename);
  }

  public void ExecuteGeneration()
  {
    if (Directory.Exists("Generated")) Directory.Delete("Generated", true);
    this.GenerateFiles();
    this.EncryptFiles(this.PullData());
  }

  private void GenerateFiles()
  {
    Directory.CreateDirectory("Generated/Raw");
    foreach (KeyValuePair<string, string> resource in _resources)
    {
      var image = Raylib.LoadImage("../../../" + resource.Value);
      Raylib.ExportImageAsCode(image, "Generated/Raw/" + resource.Key + ".h");
    }
  }
  
  private List<List<string>> PullData()
  {
    List<List<string>> data = new List<List<string>>();
    foreach (KeyValuePair<string, string> resource in _resources)
    {
      List<string> list = new List<string>();
      using (StreamReader reader = new StreamReader("Generated/Raw/" + resource.Key + ".h"))
      {
        list.Add(resource.Key);
        string? readed_line = reader.ReadLine();
        for (int i = 0; i < 11; i++)
        {
          readed_line = reader.ReadLine();
        }
        for (int i = 0; i < 3; i++)
        {
          readed_line = reader.ReadLine();
          string[] splitted = readed_line.Split("   ");
          list.Add(splitted[1]);
        }
        readed_line = reader.ReadLine();
        readed_line = reader.ReadToEnd();
        readed_line = readed_line.Replace(" ", "");
        readed_line = readed_line.Replace("\n", "");
        readed_line = readed_line.Replace("\r", "");
        readed_line = readed_line.Split("{")[1];
        readed_line = readed_line.Split("}")[0];
        list.Add(readed_line);
      }
      data.Add(list);
    }

    return data;
  }
  
  private void EncryptFiles(List<List<string>> data_to_encrypt)
  {
    List<string> output = new List<string>();
    foreach (List<string> data_object in data_to_encrypt)
    {
      string str = "";
      for (int i = 0; i < 5; i++)
      {
        str += data_object[i] + " ";
      }
      
      MemoryStream new_stream = new MemoryStream();
      LZ4Compressor compressor = new LZ4Compressor();
      compressor.Compress(new MemoryStream(Encoding.UTF8.GetBytes(str ?? "")), new_stream);
      string new_str = Encoding.UTF8.GetString(new_stream.ToArray());
      
      output.Add(new_str);
    }
    Directory.Delete("Generated/Raw", true);
    using (StreamWriter writer = new StreamWriter("Generated/Resources.h"))
    {
      foreach (string s in output)
      {
        writer.WriteLine(s);
      }
    }
  }
}