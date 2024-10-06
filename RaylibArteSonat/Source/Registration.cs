namespace RaylibArteSonat.Source;

using Raylib_cs;
using RaylibArteSonat.Packages.Objects.Image;
using RaylibArteSonat.Source.Packages.Module;
using RaylibArteSonat.Source.Scenes;
using System.Numerics;
using RaylibArteSonat.Packages.Objects.Box;

public static class Registration
{

  private static string[] scenes_names = new[] { "Auth/Registration", "Auth/Login", "Page/Main", "Page/Search", "Page/Favourite", "Page/MyPublications", "Page/UploadSong", "Page/UploadPlaylist", "Page/Profile" };
  private static string start_scene_name = "Auth/Login";
  
  public static CenteredBox LoginBox;
  public static CenteredBox LoginBox2;
  public static HitboxImage Imageee;

  public static void ObjectsInitialisation(Registry registry)
  {
    LoginBox = registry.Register("LoginBox", ["Auth/Login"], [0], new CenteredBox(new Vector2(300, 300), new Vector2(1000, 720), Color.Gray));
    LoginBox2 = registry.Register("LoginBox2", ["Auth/Login"], [1], new CenteredBox(new Vector2(300, 300), new Vector2(1000, 720), Color.Blue, new Vector2(100, 100)));

    Imageee = registry.Register("Imageee", ["Auth/Login"], [-1], new HitboxImage("photo.png", new Vector2(0, 0)));
    
    registry.EndRegistration(start_scene_name);
  }
  
  public static Registry RegistryInitialisation()
  {
    Registry registry = new Registry(scenes_names);
    
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