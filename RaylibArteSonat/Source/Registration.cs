using RaylibCsTemplate.Packages.Objects.Image;
using RaylibCsTemplate.Scenes;

namespace RaylibCsTemplate;
using RaylibCsTemplate.Packages.Registry;
using System.Numerics;
using RaylibCsTemplate.Packages.Objects.Box;
using ZeroElectric.Vinculum;

public static class Registration
{
  public static Registry Initialisation()
  {
    Registry registry = new Registry("TestSceneOne", "TestSceneTwo");
    registry.AssignSceneScript("TestSceneOne", new TestSceneOne(registry));
    registry.AssignSceneScript("TestSceneTwo", new TestSceneTwo(registry));
    registry.AssignGlobalScript(new GlobalSceneOverlay(registry));
    registry.SwitchDebugMode();

    registry.Register("TestCodeImage", "TestSceneTwo", 0, new HitboxImage("Resources/photo.png", new Vector2(400, 100)));
    
    registry.Register("TestImage", "TestSceneOne", 4, new ResizableImage("Resources/photo.png", new Vector2(400, 100), new Vector2(854, 480)));
    
    registry.Register("TestObject", "TestSceneOne", 0, new SimpleBox(new Vector2(200, 200), new Vector2(100, 100), Raylib.WHITE));
    registry.Register("NewTestObject", "TestSceneTwo", 0, new SimpleBox(new Vector2(200, 500), new Vector2(100, 100), Raylib.WHITE));
    
    
    registry.EndRegistration("TestSceneOne");
    return registry;
  }
}