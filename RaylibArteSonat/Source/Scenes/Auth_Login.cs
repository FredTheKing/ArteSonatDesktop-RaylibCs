using System.Numerics;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;

namespace RaylibArteSonat.Source.Scenes;
public class Auth_Login(Registry registry)
{
  public void Activation()
  {
    if(registry.GetAuthentificationManager()?.GetCurrentProfile() == null) registry.GetAuthentificationManager().InitProfiles(registry.GetDatabaseManager().GetTable("Profile"));
    Registration.Objects.Auth_Login_ProfileClickScroller.SetOptions(registry.GetAuthentificationManager().GetProfilesNames());
  }
    
  public void Update()
  {
    var login_box = Registration.Objects.Auth_Box;
    var warning_timer = Registration.Objects.Auth_Login_WarningTextTimer;
    var warning_text = Registration.Objects.Auth_Login_WarningText;
    
    login_box.SetPosition(new Vector2(Raylib.GetRenderWidth() / 2 - login_box.GetSize().X / 2, Raylib.GetRenderHeight() / 2 - login_box.GetSize().Y / 2 - 20));
    
    Registration.Objects.Auth_Login_LoginText.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 35));
    warning_text.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 120));
    Registration.Objects.Auth_Login_ProfileClickScroller.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 155));
    Registration.Objects.Auth_Login_PasswordInput.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 225));
    Registration.Objects.Auth_Login_LogInButton.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 295));
    Registration.Objects.Auth_Login_NewProfile.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 415));
    
    if (warning_timer.IsEnded())
    {
      warning_timer.StopTimer();
      warning_text.SetText("");
    }
    
    if (Registration.Objects.Auth_Login_LogInButton.GetHitbox().GetMousePressed(MouseButton.Left))
    {
      var current_profile = registry.GetAuthentificationManager().GetProfiles()[Registration.Objects.Auth_Login_ProfileClickScroller.GetOption()];
      
      if (current_profile.IsEncrypted())
      {
        if (current_profile.GetEncryptKey() != Registration.Objects.Auth_Login_PasswordInput.GetText())
        {
          warning_timer.StartTimer();
          warning_text.SetText("Encryption key is wrong/required!");
          return;
        }
      }
      registry.GetAuthentificationManager().ChangeProfile(current_profile.GetName());
      registry.GetSceneManager().ChangeScene("Page/Main");
    }

    if (Registration.Objects.Auth_Login_NewProfile.GetHitbox().GetMousePressed(MouseButton.Left)) registry.GetSceneManager().ChangeScene("Auth/Registration");
  }
    
  public void Draw()
  {
    
  }
}