using RaylibCsTemplate;
using RaylibCsTemplate.Packages.Objects.Etc;
using RaylibCsTemplate.Packages.Registry;
using Raylib_cs;
using rlImGui_cs;

Raylib.SetConfigFlags(ConfigFlags.AlwaysRunWindow | ConfigFlags.ResizableWindow);
Raylib.InitWindow(1280, 720, "Window!");
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