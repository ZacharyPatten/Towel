namespace Towel.Graphics.OpenGL
{
    public static class Shaders
    {
        public static string AnimatedModelVertexShader =
        #region AnimatedModelVertexShader glsl code
@"#version 150

const int MAX_JOINTS = 50;//max joints allowed in a skeleton
const int MAX_WEIGHTS = 3;//max number of joints that can affect a vertex

in vec3 in_position;
in vec2 in_textureCoords;
in vec3 in_normal;
in ivec3 in_jointIndices;
in vec3 in_weights;

out vec2 pass_textureCoords;
out vec3 pass_normal;

uniform mat4 jointTransforms[MAX_JOINTS];
uniform mat4 projectionViewMatrix;

void main(void){
	
	vec4 totalLocalPos = vec4(0.0);
	vec4 totalNormal = vec4(0.0);
	
	for(int i=0;i<MAX_WEIGHTS;i++){
		mat4 jointTransform = jointTransforms[in_jointIndices[i]];
		vec4 posePosition = jointTransform * vec4(in_position, 1.0);
		totalLocalPos += posePosition * in_weights[i];
		
		vec4 worldNormal = jointTransform * vec4(in_normal, 0.0);
		totalNormal += worldNormal * in_weights[i];
	}
	
	gl_Position = projectionViewMatrix * totalLocalPos;
	pass_normal = totalNormal.xyz;
	pass_textureCoords = in_textureCoords;

}";
        #endregion

        public static string AnimatedModelFragmentShader =
        #region AnimatedModelFragmentShader glsl code
@"#version 150

const vec2 lightBias = vec2(0.7, 0.6);//just indicates the balance between diffuse and ambient lighting

in vec2 pass_textureCoords;
in vec3 pass_normal;

out vec4 out_colour;

uniform sampler2D diffuseMap;
uniform vec3 lightDirection;

void main(void){
	
	vec4 diffuseColour = texture(diffuseMap, pass_textureCoords);		
	vec3 unitNormal = normalize(pass_normal);
	float diffuseLight = max(dot(-lightDirection, unitNormal), 0.0) * lightBias.x + lightBias.y;
	out_colour = diffuseColour * diffuseLight;
	
}";
        #endregion

        internal const string StaticModelVertexShader =
        #region StaticModelVertexShader glsl code
@"varying vec3 light;
varying vec3 normal;

void main()
{
  normal = normalize(gl_NormalMatrix * gl_Normal);
  light = normalize(vec3(gl_LightSource[0].position));
  gl_TexCoord[0] = gl_MultiTexCoord0;
  gl_Position = ftransform();
}";
        #endregion

        public static string StaticModelFragmentShader =
        #region StaticModelFragmentShader glsl code
@"varying vec3 light;
varying vec3 normal;
uniform sampler2D tex;
void main()
{
     vec3 ct,cf;
     vec4 texel;
     float intensity,at,af;
     intensity = max(dot(light,normalize(normal)),0.0);
     cf = intensity * (gl_FrontMaterial.diffuse).rgb + gl_FrontMaterial.ambient.rgb;
     af = gl_FrontMaterial.diffuse.a;
     texel = texture2D(tex,gl_TexCoord[0].st);
     ct = texel.rgb;
     at = texel.a;
     gl_FragColor = vec4(ct * cf, at * af);	
}";
        #endregion
    }
}
