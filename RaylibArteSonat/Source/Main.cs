using RaylibArteSonat.Source;
using RaylibArteSonat.Source.Packages.Module;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
Raylib.InitWindow(1920, 1080, "Window!");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Raylib.SetWindowMinSize(690, 490);
Registry registry = Registration.RegistryInitialisation();
Registration.MaterialsInitialisation(registry);
Registration.ObjectsInitialisation(registry);
while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  Raylib.ClearBackground(new Color(240, 240, 240, 255));
  
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
  
  Raylib.EndDrawing();
}
registry.GetDatabaseManager().DisableDatabase();
Raylib.CloseWindow();
Raylib.CloseAudioDevice();
rlImGui.Shutdown();