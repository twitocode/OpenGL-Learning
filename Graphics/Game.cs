using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Graphics;

public class Game : GameWindow
{
    public Game(int width, int height, string windowName) : base(new GameWindowSettings(), new NativeWindowSettings { Title = windowName, Size = new Vector2i { X = width, Y = height } })
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();


        float[] positions = new float[6] {
            -0.5f, -0.5f,
             0.0f,  0.5f,
             0.5f, -0.5f
        };

        //Creating a buffer in memeory and giving us the id of it back
        int bufferId = GL.GenBuffer();

        //Selecting the buffer we want with the bufferId and the type of buffer it is, in this case
        // It is an array buffer
        //Buffers don't need to have data put into it immediately, you already created space in memory
        //It can be populated later
        GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);

        //the size of the buffer is in bytes, the amount of bytes 1 float takes can be calculated with sizeof()
        //Then just multiply it by 6 for 6 data points
        //BIG ISSUE, OpenGL does not know what the data in the buffer is used for. It has no idea that this is positional data
        GL.BufferData(BufferTarget.ArrayBuffer, 6 * sizeof(float), positions, BufferUsageHint.StaticDraw);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        SwapBuffers();
    }
}
