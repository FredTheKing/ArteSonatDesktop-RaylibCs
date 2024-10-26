using System.Numerics;
using ImGuiNET;
using Raylib_cs;
using RaylibArteSonat.Source.Packages.Module;
namespace RaylibArteSonat.Source.Packages.Objects.Shadow;

public class RectangleShadow(Vector2 position, Vector2 size, Color color, int shadow_size) : ObjectTemplate(position, size)
{
  private readonly Color _shadow_color = color;
  private readonly int _shadow_size = shadow_size;
  
  protected string debugger_name = "Shadow-" + new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4)
    .Select(s => s[new Random().Next(s.Length)]).ToArray());
  
  public new void CallDebuggerInfo(Registry registry)
  {
    if (ImGui.TreeNode(debugger_name))
    {
      ImGui.Text($" > Position: {_position.X}, {_position.Y}");
      ImGui.Text($" > Size: {_size.X}, {_size.Y}");
      ImGui.Text($" > Color: {_shadow_color.R}, {_shadow_color.G}, {_shadow_color.B}, {_shadow_color.A}");
      ImGui.Text($" > Shadow Size: {_shadow_size}");
      ImGui.TreePop();
    }
  }
  
  public void Draw(Rectangle rectangle)
  {
    var posX = (int)_position.X;
    var posY = (int)_position.Y;
    var sizeX = (int)_size.X;
    var sizeY = (int)_size.Y;

    int shadow_half = _shadow_size / 2;
    
    Raylib.DrawRectangleGradientV(posX, posY-_shadow_size+shadow_half, sizeX, _shadow_size, Color.Blank, _shadow_color);
    Raylib.DrawRectangleGradientV(posX, posY+sizeY-shadow_half, sizeX, _shadow_size, _shadow_color, Color.Blank);
    
    Raylib.DrawRectangleGradientH(posX-_shadow_size+shadow_half, posY, _shadow_size, sizeY, Color.Blank, _shadow_color);
    Raylib.DrawRectangleGradientH(posX+sizeX-shadow_half, posY, _shadow_size, sizeY, _shadow_color, Color.Blank);
    
    Raylib.DrawRectangleGradientEx(new Rectangle(posX-_shadow_size+shadow_half, posY-_shadow_size+shadow_half, _shadow_size-shadow_half, _shadow_size-shadow_half), Color.Blank, Color.Blank, _shadow_color, Color.Blank);
    Raylib.DrawRectangleGradientEx(new Rectangle(posX+sizeX, posY-_shadow_size+shadow_half, _shadow_size-shadow_half, _shadow_size-shadow_half), Color.Blank, _shadow_color, Color.Blank, Color.Blank);
    Raylib.DrawRectangleGradientEx(new Rectangle(posX-_shadow_size+shadow_half, posY+sizeY, _shadow_size-shadow_half, _shadow_size-shadow_half), Color.Blank, Color.Blank, Color.Blank, _shadow_color);
    Raylib.DrawRectangleGradientEx(new Rectangle(posX+sizeX, posY+sizeY, _shadow_size-shadow_half, _shadow_size-shadow_half), _shadow_color, Color.Blank, Color.Blank, Color.Blank);
  }
}