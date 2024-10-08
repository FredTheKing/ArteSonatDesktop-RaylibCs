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

  public static CenteredBox Auth_Login_Box;
  public static SimpleInput Auth_Login_LoginInput;
  public static SimpleText TestingScene_TestText;

  public static void ObjectsInitialisation(Registry registry)
  {
    Auth_Login_Box = registry.Register("Box", ["Auth/Login"], [0], new CenteredShadowBox(new Vector2(300, 300), new Vector2(600, 400), Color.White, new Color(230, 230, 230, 60), 8, new Vector2(0, -20)));
    Auth_Login_LoginInput = registry.Register("LoginInput", ["Auth/Login"], [1], new SimpleInput(new Vector2(400, 100), new Vector2(600, 50), 3, 24, "Login Here"));
    
    TestingScene_TestText = registry.Register("TestText", ["TestingScene"], [0], new SimpleText(new Vector2(400, 300), new Vector2(300, 50), "DEFAULT TEXT!!", Color.Black));
    
    registry.EndRegistration(start_scene_name);
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
    registry.SwitchDebugMode();
    
    return registry;
  }
}