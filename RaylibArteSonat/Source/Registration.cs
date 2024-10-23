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

  private static string[] scenes_names = ["TestingScene", "Auth/Registration", "Auth/Login", "Page/Main", "Page/Search", "Page/Favourite", "Page/MyPublications", "Page/UploadSong", "Page/UploadPlaylist", "Page/Profile"];
  private static string start_scene_name = "Auth/Login";
  
  public static class Materials
  {
    public static FontResource Global_Font;
  }
  
  public static class Objects
  {
    public static SimpleText Auth_Login_LoginText;
    public static CenteredBox Auth_Login_Box;
    public static SimpleInput Auth_Login_PasswordInput;
    public static SimpleClickScroller AuthLoginProfileClickScroller;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.Global_Font = registry.RegisterMaterial("GlobalFont", ["*"], new FontResource("Resources/Font/regular.ttf"));

    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.Auth_Login_Box = registry.RegisterObject("Box", ["Auth/Login"], [0], new CenteredShadowBox(new Vector2(0, -20), new Vector2(620, 400), Color.White, new Color(230, 230, 230, 60), 8));
    Objects.Auth_Login_LoginText = registry.RegisterObject("LoginText", ["Auth/Login"], [1], new SimpleText(new Vector2(600, 100), new Vector2(500, 80), 64, "Pick Profile", Color.Black, Materials.Global_Font, true, true));
    Objects.AuthLoginProfileClickScroller = registry.RegisterObject("ProfileScroller", ["Auth/Login"], [1], new SimpleClickScroller(new Vector2(600, 200), new Vector2(500, 50), Materials.Global_Font, registry.GetAuthentificationManager().GetProfilesNames()));
    Objects.Auth_Login_PasswordInput = registry.RegisterObject("PasswordInput", ["Auth/Login"], [1], new PasswordInput(new Vector2(600, 100), new Vector2(500, 50), 3, 40, Materials.Global_Font, "Encrypt Key here (if needed)"));
    
    registry.EndObjectsRegistration(start_scene_name);
  }
  
  public static Registry RegistryInitialisation()
  {
    Registry registry = new Registry(scenes_names);
    
    registry.AssignSceneScript("TestingScene", new TestingScene(registry));
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