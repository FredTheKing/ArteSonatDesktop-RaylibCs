using RaylibArteSonat.Source.Packages.Objects.Button;
using RaylibArteSonat.Source.Packages.Objects.Timer;
using static RaylibArteSonat.Source.Registration.Objects;

namespace RaylibArteSonat.Source;
using Raylib_cs;
using Packages.Module;
using Packages.Objects.Box;
using Packages.Objects.Input;
using Packages.Objects.Text;
using Packages.Objects.Scroller;
using Packages.Objects.Image;
using Scenes;
using System.Numerics;

public static class Registration
{

  private static string[] scenes_names = ["Debugger/TestingScene", "Auth/Registration", "Auth/Login", "Page/Main", "Page/Search", "Page/Favourite", "Page/MyPublications", "Page/UploadSong", "Page/UploadPlaylist", "Page/Profile"];
  private static string start_scene_name = "Auth/Login";
  
  public static class Materials
  {
    public static FontResource Global_Font;
  }
  
  public static class Objects
  {
    public static ShadowBox Auth_Box;
    
    public static SimpleText Auth_Login_LoginText;
    public static SimpleClickScroller Auth_Login_ProfileClickScroller;
    public static PasswordInput Auth_Login_PasswordInput;
    public static SimpleButton Auth_Login_LogInButton;
    public static SimpleText Auth_Login_WarningText;
    public static SimpleTimer Auth_Login_WarningTextTimer;
    public static SimpleButton Auth_Login_NewProfile;
    
    public static SimpleInput Auth_Registration_UsernameInput;
    public static SimpleInput Auth_Registration_AgeInput;
    public static SimpleInput Auth_Registration_PasswordInput;
    public static SimpleButton Auth_Registration_CreateProfileButton;
    public static SimpleText Auth_Registration_RegistrationText;
    public static SimpleText Auth_Registration_WarningText;
    public static SimpleTimer Auth_Registration_WarningTextTimer;
    public static SimpleButton Auth_Registration_BackToProfiles;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.Global_Font = registry.RegisterMaterial("GlobalFont", ["*"], new FontResource("Resources/Font/regular.ttf"));

    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Auth_Box = registry.RegisterObject("Box", ["Auth/Login", "Auth/Registration"], [0],
      new ShadowBox(new Vector2(0, -20), new Vector2(620, 500), Color.White, new Color(230, 230, 230, 60), 8));
    
    Auth_Login_LoginText = registry.RegisterObject("LoginText", ["Auth/Login"], [1],
      new SimpleText(new Vector2(600, 100), new Vector2(500, 80), 64, "Pick Profile", Color.Black,
        Materials.Global_Font, true, true));
    Auth_Login_ProfileClickScroller = registry.RegisterObject("ProfileScroller", ["Auth/Login"], [1],
      new SimpleClickScroller(new Vector2(600, 200), new Vector2(500, 50), Materials.Global_Font,
        registry.GetAuthentificationManager().GetProfilesNames()));
    Auth_Login_PasswordInput = registry.RegisterObject("PasswordInput", ["Auth/Login"], [1],
      new PasswordInput(new Vector2(600, 100), new Vector2(500, 50), 3, 32, Materials.Global_Font,
        "Encrypt Key (if needed)"));
    Auth_Login_LogInButton = registry.RegisterObject("LogInButton", ["Auth/Login"], [1],
      new SimpleButton(new Vector2(600, 300), new Vector2(500, 50), "Pick Profile", Materials.Global_Font));
    Auth_Login_WarningText = registry.RegisterObject("WarningText", ["Auth/Login"], [1],
      new SimpleText(new Vector2(600, 100), new Vector2(500, 30), 18, "", Color.Orange, Materials.Global_Font, true,
        true));
    Auth_Login_WarningTextTimer = registry.RegisterObject("WarningTimer", ["Auth/Login"], [1], new SimpleTimer(2.4));
    Auth_Login_NewProfile = registry.RegisterObject("NewProfile", ["Auth/Login"], [1],
      new SimpleButton(new Vector2(600, 200), new Vector2(500, 50), "Make new Profile", Materials.Global_Font));
    
    Auth_Registration_UsernameInput = registry.RegisterObject("UsernameInput", ["Auth/Registration"], [1],
      new SimpleInput(new Vector2(600, 100), new Vector2(426, 50), 3, 26, Materials.Global_Font,
        "Profile Username"));
    Auth_Registration_AgeInput = registry.RegisterObject("AgeInput", ["Auth/Registration"], [1],
      new SimpleInput(new Vector2(600, 100), new Vector2(64, 50), 3, 2, Materials.Global_Font,
        "Age"));
    Auth_Registration_PasswordInput = registry.RegisterObject("PasswordInput", ["Auth/Registration"], [1],
      new SimpleInput(new Vector2(600, 100), new Vector2(500, 50), 3, 32, Materials.Global_Font,
        "Encrypt Key (if needed)"));
    Auth_Registration_CreateProfileButton = registry.RegisterObject("CreateProfile", ["Auth/Registration"], [1],
      new SimpleButton(new Vector2(600, 300), new Vector2(500, 50), "Create new Profile", Materials.Global_Font));
    Auth_Registration_RegistrationText = registry.RegisterObject("RegistrationText", ["Auth/Registration"], [1],
      new SimpleText(new Vector2(600, 100), new Vector2(500, 80), 64, "Make Profile", Color.Black,
        Materials.Global_Font, true, true));
    Auth_Registration_WarningText = registry.RegisterObject("WarningText", ["Auth/Registration"], [1],
      new SimpleText(new Vector2(600, 100), new Vector2(500, 30), 18, "", Color.Orange, Materials.Global_Font, true,
        true));
    Auth_Registration_WarningTextTimer = registry.RegisterObject("WarningTimer", ["Auth/Registration"], [1], new SimpleTimer(2.4));
    Auth_Registration_BackToProfiles = registry.RegisterObject("NewProfile", ["Auth/Registration"], [1],
      new SimpleButton(new Vector2(600, 200), new Vector2(500, 50), "Return to Profiles", Materials.Global_Font));
    
    registry.EndObjectsRegistration(start_scene_name);
  }
  
  public static Registry RegistryInitialisation()
  {
    Registry registry = new Registry(scenes_names);
    
    registry.AssignSceneScript("Debugger/TestingScene", new Debugger_TestingScene(registry));
    registry.AssignSceneScript("Auth/Registration", new Auth_Registration(registry));
    registry.AssignSceneScript("Auth/Login", new Auth_Login(registry));
    registry.AssignSceneScript("Page/Main", new Page_Main(registry));
    registry.AssignSceneScript("Page/Search", new Page_Search(registry));
    registry.AssignSceneScript("Page/Favourite", new Page_Favourite(registry));
    registry.AssignSceneScript("Page/MyPublications", new Page_MyPublications(registry));
    registry.AssignSceneScript("Page/UploadSong", new Page_UploadSong(registry));
    registry.AssignSceneScript("Page/UploadPlaylist", new Page_UploadPlaylist(registry));
    registry.AssignSceneScript("Page/Profile", new Page_Profile(registry));
    registry.AssignGlobalScript(new GlobalSceneOverlay(registry));
    registry.AssignGuiScript(new DebuggerWindow(registry));
    
    registry.GetDatabaseManager().EnableDatabase();
    registry.SwitchDebugMode();
    
    return registry;
  }
}