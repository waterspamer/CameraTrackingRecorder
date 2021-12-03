Shader "Diffuse Normal Specular Dissolve"
{
 Properties
 {
 _Color ("Color Tint", Color) = (1, 1, 1, 1)
 _MainTex ("Main Tex", 2D) = "white" {}
 _BumpMap ("Normal Map", 2D) = "bump" {}
 _SpecularColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
 _Gloss ("Gloss", Range(8.0, 256.0)) = 20
 _DissolveColor ("Color Dissolve", Color) = (1, 1, 1, 1)
 _DissolveMap("Dissolve Map", 2D) = "white" {}
 [Enum(uv,0,WorldPos,1)] _SwitchDisMap("imposition Dissolve Map", Int) = 0.0
 _EmitStrength("Emit Strength", Range(0.0, 10.0)) = 1.0
 _DissolveValue("Dissolve Value", Range(0,1)) = 0.0
 _DissolveGradientWidth("Ramp Width", Range(0,1)) = 0.2
 _Cutoff("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
 LOD 100
 Pass {
 Tags{ "QUEUE" = "AlphaTest" "IGNOREPROJECTOR" = "true" "RenderType" =
"TransparentCutout" }

Tags{ "LightMode" = "ForwardBase" }
 Blend SrcAlpha OneMinusSrcAlpha
 CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #pragma fragmentoption ARB_precision_hint_fastest
 #pragma glsl_no_auto_normalization
 #include "Lighting.cginc"
 #include "AutoLight.cginc"
 #include "UnityCG.cginc"
 #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmapnovertexlight
 fixed4 _Color;
 fixed4 _SpecularColor;
 float _Gloss;
 sampler2D _MainTex;
 float4 _MainTex_ST;
 sampler2D _BumpMap;
 float4 _BumpMap_ST;
 sampler2D _SpecMap;
 float4 _SpecMap_ST;
 fixed _Cutoff;
 float _EmitStrength;
 fixed4 _DissolveColor;
 sampler2D _DissolveMap;
 float4 _DissolveMap_ST;
 float _SwitchDisMap;
 half _DissolveValue;
 half _DissolveGradientWidth;

 struct a2v
 {
 float4 vertex : POSITION;
 float3 normal : NORMAL;
 float4 tangent : TANGENT;
 float4 texcoord : TEXCOORD0;
 };

 struct v2f
 {
 float4 pos : SV_POSITION;
 float4 uv : TEXCOORD0;
 float4 TtoW0 : TEXCOORD1;
 float4 TtoW1 : TEXCOORD2;
 float4 TtoW2 : TEXCOORD3;
 float3 worldNormal : TEXCOORD4;
 SHADOW_COORDS(5)
 fixed3 diff : COLOR0;
 fixed3 ambient : COLOR1;
 };

 v2f vert(a2v v)
 {
 v2f o;
 o.pos = UnityObjectToClipPos(v.vertex);
 o.uv.xy = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
 o.uv.xy = v.texcoord.xy * _DissolveMap_ST.xy + _DissolveMap_ST.zw;

o.uv.zw = v.texcoord.xy * _BumpMap_ST.xy + _BumpMap_ST.zw;

 float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
 fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
 fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
 fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w;

 o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x,
worldPos.x);
 o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y,
worldPos.y);
 o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z,
worldPos.z);
 o.worldNormal = worldNormal;
 half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
 o.diff = nl * _LightColor0.rgb;
 o.ambient = ShadeSH9(half4(worldNormal, 1));
 TRANSFER_SHADOW(o)
 return o;
 }

 fixed4 frag(v2f i) : SV_Target
 {
 float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
 fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
 fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));

 fixed3 bump = UnpackNormal(tex2D(_BumpMap, i.uv.zw));
 bump = normalize(half3(dot(i.TtoW0.xyz, bump), dot(i.TtoW1.xyz, bump),
dot(i.TtoW2.xyz, bump)));

 float4 albedo = tex2D(_MainTex, i.uv.xy);
 fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo * 2;
 fixed3 worldNormal = normalize(i.worldNormal);
 fixed halfLamber = dot(worldNormal, lightDir);

 fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(bump, lightDir));
 fixed3 halfDir = normalize(lightDir + viewDir);
 fixed3 specular = _LightColor0.rgb * _SpecularColor.rgb * pow(max(0,
dot(bump, halfDir)), _Gloss);
 UNITY_LIGHT_ATTENUATION(atten, i, worldPos);
 fixed2 dslv_impos = i.uv;
 if (_SwitchDisMap == 0.0)
 {
 dslv_impos = i.uv;
 }
 else
 {
 dslv_impos = worldPos;
 }
 fixed4 dslv = tex2D(_DissolveMap, dslv_impos);
 float3 diffuseReflection = _LightColor0.rgb * saturate(dot(bump,
lightDir));
 float3 specularReflection = diffuseReflection * specular *
pow(saturate(dot(reflect(-lightDir, bump), viewDir)), _Gloss);
 float3 lightFinal = (albedo.a * specularReflection) *
UNITY_LIGHTMODEL_AMBIENT.rgb * dslv.xyz;


  float dissValue = lerp(-_DissolveGradientWidth, 1, _DissolveValue * 1.5);
 float dissolveUV = smoothstep(dslv.r - _DissolveGradientWidth, dslv.r +
_DissolveGradientWidth, dissValue);
 _DissolveColor *= lerp(0, 3.0, dissolveUV);
 lightFinal += lightFinal +_DissolveColor.rgb;
 fixed3 color = (diffuse + specular + ambient + lightFinal) * atten;
 float alpha = dslv.r + _Cutoff - dissValue;
 return fixed4(color.rgb * _Color.rgb, alpha * 2 * _EmitStrength);
 }
 ENDCG
 }

 Pass
 {
 Tags {"LightMode"="ShadowCaster"}
 CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #pragma multi_compile_shadowcaster
 #include "UnityCG.cginc"
 struct v2f {
 V2F_SHADOW_CASTER;
 };
 v2f vert(appdata_base v)
 {
 v2f o;
 TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
 return o;
 }
 float4 frag(v2f i) : SV_Target
 {
 SHADOW_CASTER_FRAGMENT(i)
 }
 ENDCG
 }
 }
 Fallback "Diffuse"
}

