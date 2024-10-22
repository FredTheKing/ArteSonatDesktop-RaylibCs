using System.Numerics;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;

namespace RaylibArteSonat.Source.Scenes;
public class Auth_Login(Registry registry)
{
  public void Activation()
  {
    if(registry.GetAuthentificationManager()?.GetCurrentProfile() == null) registry.GetAuthentificationManager().InitProfiles(registry.GetDatabaseManager().GetTable("Profile"));
    Registration.Objects.AuthLoginProfileClickScroller.SetOptions(registry.GetAuthentificationManager().GetProfilesNames());
  }
    
  public void Update()
  {
    var login_box = Registration.Objects.Auth_Login_Box;
    Registration.Objects.AuthLoginProfileClickScroller.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 160));
    Registration.Objects.Auth_Login_PasswordInput.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 230));
    Registration.Objects.Auth_Login_LoginText.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 40));
  }
    
  public void Draw()
  {
    
  }
}