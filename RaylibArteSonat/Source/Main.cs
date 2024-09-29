using RaylibCsTemplate;
using RaylibCsTemplate.Packages.Objects.Etc;
using RaylibCsTemplate.Packages.Registry;
using ZeroElectric.Vinculum;

Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_ALWAYS_RUN | ConfigFlags.FLAG_WINDOW_RESIZABLE);
Raylib.InitWindow(1280, 720, "Window!");
Raylib.InitAudioDevice();
Raylib.SetTargetFPS(-1);
Registry registry = Registration.Initialisation();
while (!Raylib.WindowShouldClose())
{
  Raylib.BeginDrawing();
  Raylib.ClearBackground(Raylib.BLACK);
  
  MainLooper.GlobalActivation(registry);
  MainLooper.GlobalUpdate(registry);
  MainLooper.GlobalDraw(registry);
  
  Raylib.EndDrawing();
}
Raylib.CloseWindow();
Raylib.CloseAudioDevice();