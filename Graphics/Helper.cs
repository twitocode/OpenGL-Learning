using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace Graphics;

public static class Helper
{
    //Clears the errors first so that we do not have any errors from other functions
    public static void GLClearError()
    {
        while (GL.GetError() != ErrorCode.NoError) ;
    }

    ////Calll the function first, then we check the errors
    //public static void GLCheckError()
    //{
    //    ErrorCode error;
    //    int i = 0;

    //    while ((error = GL.GetError()) != ErrorCode.NoError)
    //    {
    //        Console.WriteLine($"[OpenGL Error] [{i++}]: {error}");
    //    }
    //}

    //Calll the function first, then we check the errors
    public static bool GLLogCall()
    {
        ErrorCode error;

        while ((error = GL.GetError()) != ErrorCode.NoError)
        {
            Console.WriteLine($"[OpenGL Error]: {error}");
            return false;
        }

        return true;
    }

    public static void Assert(bool condition)
    {
        if (!condition) Debugger.Break();
    }

    public static void GLCall(Action x)
    {
        GLClearError();
        x();
        Assert(GLLogCall());
    }
}
