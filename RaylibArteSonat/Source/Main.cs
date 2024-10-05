using RaylibArteSonat;
using RaylibArteSonat.Packages.Objects.Etc;
using RaylibArteSonat.Packages.Registry;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
Raylib.InitWindow(1920, 1080, "Window!");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Registry registry = Registration.Initialisation();
while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Color.Black);
  
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
  
  Raylib.EndDrawing();
}
Raylib.CloseWindow();
Raylib.CloseAudioDevice();
rlImGui.Shutdown();