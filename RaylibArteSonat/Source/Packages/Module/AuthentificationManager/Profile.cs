namespace RaylibArteSonat.Source.Packages.Module;

public class Profile(List<dynamic> profile_list)
{
  private int _id = Convert.ToInt32(profile_list[0]);
  private String _name = profile_list[1];
  private String _encrypt_key = profile_list[2];
  private String _image_filename = profile_list[3];
  private String _email = profile_list[4];
  private int _age = Convert.ToInt32(profile_list[5]);
  
  public int GetId() => _id;
  public String GetName() => _name;
  public String GetEncryptKey() => _encrypt_key;
  public String GetImageFilename() => _image_filename;
  public String GetEmail() => _email;
  public int GetAge() => _age;
  
  public bool IsEncrypted() => _encrypt_key != "";
}