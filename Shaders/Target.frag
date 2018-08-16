#version 400
uniform float bias;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  float dist = length(texCoord * 2 - 1);
  float alpha = dist < 1 ? 1 : 0;
  float value = texCoord.x < 0.5 ^^ texCoord.y < 0.5 ? 1 : 0;
  fragColor = vec4(value, value > 0 ? value : bias - value, value, alpha);
}
