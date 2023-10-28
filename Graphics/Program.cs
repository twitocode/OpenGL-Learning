using Graphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

var gameWindowSettings = GameWindowSettings.Default;

var nativeWindowSettings = new NativeWindowSettings
{
    Size = new OpenTK.Mathematics.Vector2i { X = 800, Y = 600 },
    Flags = ContextFlags.Debug
};

using var game = new Game(gameWindowSettings, nativeWindowSettings);
game.Run();
