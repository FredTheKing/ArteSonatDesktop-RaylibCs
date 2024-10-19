using System.Numerics;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;

namespace RaylibArteSonat.Source.Scenes;
public class Auth_Login(Registry registry)
{
  public void Activation()
  {
    if(registry.GetAuthentificationManager()?.GetCurrentProfile() == null) registry.GetAuthentificationManager().InitProfiles(registry.GetDatabaseManager().GetTable("Profile"));
  }
    
  public void Update()
  {
    
  }
    
  public void Draw()
  {
    
  }
}