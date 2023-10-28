using OpenTK.Graphics.OpenGL4;

namespace Graphics;

public class Shader : IDisposable
{
    private string VertexShaderPath { get; set; }
    private int Program { get; set; }
    private bool disposedValue;

    public Shader(string vertexShaderPath)
    {
        VertexShaderPath = vertexShaderPath;
    }

    public int Create()
    {
        //Think of this like GenPrograms as in GenBuffers
        //Creates a program (shader) that will be ran on the GPU
        int program = GL.CreateProgram();
        ParseShader(VertexShaderPath, out string vertexShader, out string fragmentShader);

        int vs = CompileShader(ShaderType.VertexShader, vertexShader);
        int fs = CompileShader(ShaderType.FragmentShader, fragmentShader);

        //Attach both to the shaders to one program
        GL.AttachShader(program, vs);
        GL.AttachShader(program, fs);

        GL.LinkProgram(program);

        //checks to see whether the executables contained in program can execute given the current OpenGL state
        GL.ValidateProgram(program);


        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string infoLog = GL.GetProgramInfoLog(program);
            Console.WriteLine(infoLog);
        }

        //We dont need the shaders anymore, they are already linked within the program
        GL.DetachShader(program, vs);
        GL.DetachShader(program, fs);
        GL.DeleteShader(vs);
        GL.DeleteShader(fs);

        Program = program;
        return program;
    }

    public void ParseShader(string filePath, out string vs, out string fs)
    {
        ShaderType type = ShaderType.VertexShader;

        vs = "";
        fs = "";

        foreach (string line in File.ReadLines(filePath))
        {
            if (line == "#shader vertex")
            {
                type = ShaderType.VertexShader;
                continue;
            }
            else if (line == "#shader fragment")
            {
                type = ShaderType.FragmentShader;
                continue;
            }

            if (type == ShaderType.VertexShader) vs += line + "\n";
            else if (type == ShaderType.FragmentShader) fs += line + "\n";
        }
    }

    public void Use()
    {
        GL.UseProgram(Program);
    }

    private int CompileShader(ShaderType type, string src)
    {
        int shaderId = GL.CreateShader(type);

        //Tells OpenGL where the shader is and the type
        GL.ShaderSource(shaderId, src);
        GL.CompileShader(shaderId);

        GL.GetShader(shaderId, ShaderParameter.CompileStatus, out int success);

        if (success == 0)
        {
            GL.DeleteShader(shaderId);
            string infoLog = GL.GetShaderInfoLog(shaderId);

            throw new Exception($"Error while compiling shader of type ${type} Source: \n{src} \n{infoLog}");
        }

        //TODO: Error Handling
        return shaderId;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            GL.DeleteProgram(Program);

            disposedValue = true;
        }
    }

    ~Shader()
    {
        if (disposedValue == false)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
