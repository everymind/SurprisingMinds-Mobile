#version 400
uniform sampler2D tex;
uniform float bias;
in vec2 texCoord;
out vec4 fragColor;

void main()
{
  vec4 texel = texture(tex, texCoord);
  texel.r = texel.r > 0.5 ? texel.r : bias - texel.r;
  fragColor = texel;
}
