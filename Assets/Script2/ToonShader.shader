﻿
Shader"Roystan/Toon"
{
	Properties
	{
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_MainTex("Main Texture", 2D) = "white" {}
		// Ambient light is applied uniformly to all surfaces on the object.
		[HDR]
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		[HDR]
		_SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)
		// specular color that tints / glos that controls size of the reflection 
		_Glossiness("Glossiness", Float) = 32
		[HDR]
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0,1)) = 0.716
		// Control how smoothly the rim blends when approaching unlit
		// parts of the surface.
	    _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	}
	
	SubShader
	{
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}
				
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
		    #pragma multi_compile_fwdbase

#include "UnityCG.cginc"
#include "Lighting.cginc" 
#include "AutoLight.cginc" 

struct appdata
{
    float4 vertex : POSITION;
    float4 uv : TEXCOORD0;
				// acceses object's normal data 
    float3 normal : NORMAL;
};

struct v2f
{
					
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
				// view direction from the vertex towards the camera 
    float3 viewDir : TEXCOORD1;
				// acceses object's normal data 
    float3 worldNormal : NORMAL;
				SHADOW_COORDS(2)
};

sampler2D _MainTex;
float4 _MainTex_ST;

v2f vert(appdata v)
{
					
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    o.worldNormal = UnityObjectToWorldNormal(v.normal);
    o.viewDir = WorldSpaceViewDir(v.vertex);
    TRANSFER_SHADOW(o)

    return o;
}

float4 _Color;
float4 _AmbientColor;
float _Glossiness;
float4 _SpecularColor;
float4 _RimColor;
float _RimAmount;
float _RimThreshold;

float4 frag(v2f i) : SV_Target
{
				// light bouncing off the surface of object and scatters in atmosphere
    float3 normal = normalize(i.worldNormal);
    float3 viewDir = normalize(i.viewDir);
				// renderrs out a realistic style of illuminations 

    float NdotL = dot(_WorldSpaceLightPos0, normal);
				// intensity into light and dark smoothy 

    float shadow = SHADOW_ATTENUATION(i);

				
    float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
				// multiply by light intensity and colour 
    float4 light = lightIntensity * _LightColor0;
    float4 sample = tex2D(_MainTex, i.uv);

    float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
    float NdotH = dot(normal, halfVector);

    float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
    float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
    float4 specular = specularIntensitySmooth * _SpecularColor;

    float4 rimDot = 1 - dot(viewDir, normal);
    float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
    rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
    float4 rim = rimIntensity * _RimColor;
				



    return _Color * sample * (_AmbientColor + light + specular + rim);
}

			ENDCG
				
			}

			// Pass that is used by Unity during the shadow casting step of the rendering process 
UsePass"Legacy Shaders/VertexLit/SHADOWCASTER"
		}
}