namespace RaylibArteSonat.Source;
using Raylib_cs;
using RaylibArteSonat.Packages.Objects.Image;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Packages.Objects.Input;
using RaylibArteSonat.Source.Packages.Objects.Text;
using RaylibArteSonat.Source.Scenes;
using System.Numerics;
using RaylibArteSonat.Packages.Objects.Box;

public static class Registration
{

  private static string[] scenes_names = new[] { "TestingScene", "Auth/Registration", "Auth/Login", "Page/Main", "Page/Search", "Page/Favourite", "Page/MyPublications", "Page/UploadSong", "Page/UploadPlaylist", "Page/Profile" };
  private static string start_scene_name = "Auth/Login";
  
  public static class Materials
  {
    public static FontResource Global_Font;
  }
  
  public static class Objects
  {
    public static CenteredBox Auth_Login_Box;
    public static SimpleInput Auth_Login_LoginInput;
    public static SimpleText TestingScene_TestText;
  }
  
  public static void MaterialsInitialisation(Registry registry)
  {
    Materials.Global_Font = registry.RegisterFont("GlobalFont", ["*"], new FontResource("Resources/Font/consolas.ttf"));

    registry.EndMaterialsRegistration();
  }

  public static void ObjectsInitialisation(Registry registry)
  {
    Objects.Auth_Login_Box = registry.RegisterObject("Box", ["Auth/Login"], [0], new CenteredShadowBox(new Vector2(0, -20), new Vector2(600, 400), Color.White, new Color(230, 230, 230, 60), 8));
    Objects.Auth_Login_LoginInput = registry.RegisterObject("LoginInput", ["Auth/Login"], [1], new SimpleInput(new Vector2(600, 100), new Vector2(600, 50), 3, 24, Materials.Global_Font, "Login Here"));
    
    Objects.TestingScene_TestText = registry.RegisterObject("TestText", ["TestingScene"], [0], new SimpleText(new Vector2(600, 300), new Vector2(300, 50), 18, "DEFAULT TEXT!!", Color.Black, true, true));
    
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