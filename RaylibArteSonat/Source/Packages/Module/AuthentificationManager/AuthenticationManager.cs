namespace RaylibArteSonat.Source.Packages.Module;

public class AuthenticationManager
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
  
  public Dictionary<string, Profile> GetProfiles() => _profiles;

  public List<string> GetProfilesNames() => _profiles.Keys.ToList();

  public void ChangeProfile(string name) => _current_profile = _profiles[name];

  public Profile GetCurrentProfile() => _current_profile;
}