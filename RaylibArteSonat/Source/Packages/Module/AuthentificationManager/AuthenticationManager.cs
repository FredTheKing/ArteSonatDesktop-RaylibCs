namespace RaylibArteSonat.Source.Packages.Module;

public class AuthenticationManager : CallDebuggerInfoTemplate
{
  private Dictionary<string, Profile> _profiles = [];
  private Profile _current_profile;
  
  public void InitProfiles(List<List<dynamic>> profiles_list)
  {
    foreach (List<dynamic> profile_list in profiles_list)
    {
      Profile new_profile = new(profile_list);
      _profiles.Add(new_profile.GetName(), new_profile);
    }
    _current_profile = _profiles[_profiles.Keys.ToList()[0]];
    Console.WriteLine("INFO: AUTH: Profiles loaded successfully");
  }

  public void AddProfile(Registry registry, string nickname, string encrypt_key, string age)
  {
    registry.GetDatabaseManager().RunCommand("INSERT INTO Profile (name, encrypt_key, age) VALUES ('" + nickname + "', " + (encrypt_key != "" ? "'" + encrypt_key + "'" : "null") + ", " + Convert.ToInt32(age) + ")");
    _profiles.Clear();
    registry.GetAuthentificationManager().InitProfiles(registry.GetDatabaseManager().GetTable("Profile"));
    Console.WriteLine("INFO: AUTH: Profile added successfully");
  }
  
  public Dictionary<string, Profile> GetProfiles() => _profiles;

  public List<string> GetProfilesNames() => _profiles.Keys.ToList();

  public void ChangeProfile(string name)
  {
    _current_profile = _profiles[name];
    Console.WriteLine("INFO: AUTH: Profile changed successfully");
  }

  public Profile GetCurrentProfile() => _current_profile;
}