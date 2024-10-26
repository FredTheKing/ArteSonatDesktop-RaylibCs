using System.Numerics;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;

namespace RaylibArteSonat.Source.Scenes;
public class Auth_Registration(Registry registry)
{
  public void Activation()
  {
    
  }
    
  public void Update()
  {
    var login_box = Registration.Objects.Auth_Box;
    var warning_timer = Registration.Objects.Auth_Registration_WarningTextTimer;
    var warning_text = Registration.Objects.Auth_Registration_WarningText;
    var username_input = Registration.Objects.Auth_Registration_UsernameInput;
    
    login_box.SetPosition(new Vector2(Raylib.GetRenderWidth() / 2 - login_box.GetSize().X / 2, Raylib.GetRenderHeight() / 2 - login_box.GetSize().Y / 2 - 20));
    
    Registration.Objects.Auth_Registration_RegistrationText.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 35));
    warning_text.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 120));
    username_input.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 155));
    Registration.Objects.Auth_Registration_AgeInput.SetPosition(new Vector2(login_box.GetPosition().X + username_input.GetSize().X + 70, login_box.GetPosition().Y + 155));
    Registration.Objects.Auth_Registration_PasswordInput.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 225));
    Registration.Objects.Auth_Registration_CreateProfileButton.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 295));
    Registration.Objects.Auth_Registration_BackToProfiles.SetPosition(new Vector2(login_box.GetPosition().X + 60, login_box.GetPosition().Y + 415));
    
    if (warning_timer.IsEnded())
    {
      warning_timer.StopTimer();
      warning_text.SetText("");
    }
    
    if (Registration.Objects.Auth_Registration_BackToProfiles.GetHitbox().GetMousePressed(MouseButton.Left)) registry.GetSceneManager().ChangeScene("Auth/Login");
    
    if (Registration.Objects.Auth_Registration_CreateProfileButton.GetHitbox().GetMousePressed(MouseButton.Left))
    {
      if (Registration.Objects.Auth_Registration_UsernameInput.GetText() == "" || Registration.Objects.Auth_Registration_AgeInput.GetText() == "")
      {
        warning_timer.StartTimer();
        warning_text.SetText("Profile name and age are required!");
        return;
      }
      
      if (!int.TryParse(Registration.Objects.Auth_Registration_AgeInput.GetText(), out _))
      {
        warning_timer.StartTimer();
        warning_text.SetText("Age should be a number!");
        return;
      }
      
      registry.GetAuthentificationManager().AddProfile(registry, Registration.Objects.Auth_Registration_UsernameInput.GetText(), Registration.Objects.Auth_Registration_PasswordInput.GetText(), Registration.Objects.Auth_Registration_AgeInput.GetText());
      registry.GetSceneManager().ChangeScene("Auth/Login");
    }
  }
    
  public void Draw()
  {
    
  }
}