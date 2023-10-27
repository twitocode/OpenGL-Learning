using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Graphics;

public class Game : GameWindow
{
    int bufferId;
    int programId;
    int vaoId;
    Shader shader;

    public Game(int width, int height, string windowName) : base(new GameWindowSettings(), new NativeWindowSettings { Title = windowName, Size = new Vector2i { X = width, Y = height } })
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        //OpenGL has no idea that this data is positional data
        //We have to tell it that it is 
        float[] positions = new float[9] {
          /* X      Y */
            -0.5f, -0.5f, 0f,
             0.0f,  0.5f, 0f,
             0.5f, -0.5f, 0f
        };


        //Creating a buffer in memeory and giving us the id of it back
        bufferId = GL.GenBuffer();
        vaoId = GL.GenVertexArray();

        GL.BindVertexArray(vaoId);

        //Selecting the buffer we want with the bufferId and the type of buffer it is, in this case
        // It is an array buffer
        //Buffers don't need to have data put into it immediately, you already created space in memory
        //It can be populated later
        GL.BindBuffer(BufferTarget.ArrayBuffer, bufferId);

        //the size of the buffer is in bytes, the amount of bytes 1 float takes can be calculated with sizeof()
        //Then just multiply it by 6 for 6 data points
        //BIG ISSUE, OpenGL does not know what the data in the buffer is used for. It has no idea that this is positional data
        GL.BufferData(BufferTarget.ArrayBuffer, positions.Length * sizeof(float), positions, BufferUsageHint.StaticDraw);

        //Best practice to enable array before setting it
        GL.EnableVertexAttribArray(0);

        //Color, position, normal, texture coordinate, All of these are ATTRIBUTES

        //index - attribute number - used to access this vertex attribute in the vertex shader
        //size - the # the vector is (vec1, vec2, vec3, vec4)
        //type
        //normalized - floats are already normalized
        //stride - # of bytes between each vertex
        //In this case, the amount of bytes you need to go forward to get to the next positonal vertex
        //pointer - amount of bytes in a vector that indicates the start of a new attribute (position - 0=2, colour - 24, etc)

        //BUFFER MUST BE BOUND FIRST

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

        //We are done with the buffer, so unbind it
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        shader = new Shader("res/shaders/Basic.shader");

        programId = shader.Create();
        shader.Use();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        shader.Use();
        GL.BindVertexArray(vaoId);

        //OpenGL is a state machine, it knows that it should render the triangle specified in the OnLoad method
        //With the buffer that was generated. No need to specify in this function call.
        //Draws the currently bound buffer
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        GL.DeleteProgram(programId);
    }
}
