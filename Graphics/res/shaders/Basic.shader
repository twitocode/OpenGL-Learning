//Runs for every vertex (NOT A LOT, FOR A TRIANGLE ITS 3)
//You can pass data from a vertex shader to a fragment shader
//Determines the position of a vertex. Why not do it in the code? because of things like camera 
//0.5x is not in the same spot when a camera is moving
//It is normal for games to create many shaders on the fly (grpahics settings, etc)
//YOU HAVE TO ENALBE SHADERS TO ACTUALLY USE THEM (like every other OpenGL object)

#shader vertex

//core = no deprecated functions
#version 330 core

//Lets the shader know which index vector position attribute are which was specified in the 
//initialization code
//We are using vec4 instead of vec2 because gl_position is a vec4 and OpenGL automatically knows to cast our vec2 to its vec4
layout (location = 0) in vec4 position;

void main() {
	gl_Position = position;
}

//Runs for every pixel (MOST OF THE TIME IT IS A LOT)
//This program decides which colour the pixel will be so that the 
//rasterization layer (drawing pixels) knows what to fill the triangle with
//Do not do any complex math (anything above multiplication) in here, it'll get called like a million times

#shader fragment
#version 330 core

out vec4 outputColor;

void main()
{
    outputColor = vec4(1.0, 0.2, 0.75, 6.0);
}