namespace RaylibCsTemplate;

using Raylib_cs;
using RaylibCsTemplate.Packages.Objects.Image;
using RaylibCsTemplate.Scenes;
using RaylibCsTemplate.Packages.Registry;
using System.Numerics;
using RaylibCsTemplate.Packages.Objects.Box;

public static class Registration
{
  public static Registry Initialisation()
  {
    Registry registry = new Registry("Auth/Registration", "Auth/Login", "Page/Main", "Page/Search", "Page/Favourite", "Page/MyPublications", "Page/UploadSong", "Page/UploadPlaylist", "Page/Profile");
    registry.AssignSceneScript("Auth/Registration", new Auth_Registration(registry));
    registry.AssignSceneScript("Auth/Login", new Auth_Registration(registry));
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
    
    registry.Register("LoginBox", ["Auth/Login"], [0], new CenteredBox(new Vector2(300, 300), new Vector2(1000, 720), Color.Gray));
    registry.Register("LoginBox2", ["Auth/Login"], [1], new CenteredBox(new Vector2(300, 300), new Vector2(1000, 720), Color.Blue, new Vector2(100, 100)));

    registry.Register("Imageee", ["Auth/Login"], [-1], new HitboxImage("photo.png", new Vector2(0, 0)));
    
    registry.EndRegistration("Auth/Login");
    return registry;
  }
}