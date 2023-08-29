// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Cartoon FX/Mobile Particles Additive Alpha8" {
Properties {
 _MainTex ("Particle Texture (Alpha8)", 2D) = "white" { }
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Blend SrcAlpha One
  GpuProgramID 9864
CGPROGRAM
//#pragma target 4.0

#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"


#define CODE_BLOCK_VERTEX
//uniform float4x4 UNITY_MATRIX_MVP;
uniform float4 _MainTex_ST;
uniform sampler2D _MainTex;
struct appdata_t
{
    float4 vertex :POSITION;
    float4 color :COLOR;
    float4 texcoord :TEXCOORD0;
};

struct OUT_Data_Vert
{
    float4 xlv_COLOR0 :COLOR0;
    float2 xlv_TEXCOORD0 :TEXCOORD0;
    float4 vertex :SV_POSITION;
};

struct v2f
{
    float4 xlv_COLOR0 :COLOR0;
    float2 xlv_TEXCOORD0 :TEXCOORD0;
};

struct OUT_Data_Frag
{
    float4 color :SV_Target0;
};

OUT_Data_Vert vert(appdata_t in_v)
{
    OUT_Data_Vert out_v;
    float4 tmpvar_1;
    float4 tmpvar_2;
    tmpvar_2 = clamp(in_v.color, 0, 1);
    tmpvar_1 = tmpvar_2;
    float4 tmpvar_3;
    tmpvar_3.w = 1;
    tmpvar_3.xyz = in_v.vertex.xyz;
    out_v.xlv_COLOR0 = tmpvar_1;
    out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
    out_v.vertex = UnityObjectToClipPos(tmpvar_3);
    return out_v;
}

#define CODE_BLOCK_FRAGMENT
OUT_Data_Frag frag(v2f in_f)
{
    OUT_Data_Frag out_f;
    float4 col_1;
    col_1.xyz = in_f.xlv_COLOR0.xyz;
    col_1.w = (tex2D(_MainTex, in_f.xlv_TEXCOORD0).w * in_f.xlv_COLOR0.w);
    out_f.color = col_1;
    return out_f;
}


ENDCG

}
}
}