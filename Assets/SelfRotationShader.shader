

Shader "Unlit/Curved Rotation"
Properties
_MainTex ("Texture", 2D) = "white" {}
SubShader
Tags { "RenderType"="Opaque" "DisableBatching" "True"}
2
3
{
4
5 B
{
6
7
}
8
9日
{
10
11
12
13
14
{
15
16
17
18
19
20
LOD 100
Pass
CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members vertex)
#pragma exclude_renderers d3d11
#pragma vertex vert #pragma fragment frag // make fog work
#pragma multi_compile_fog
21 #include "UnityCG.cginc"
struct appdata
22
23
24
{
25
26
float4 vertex POSITION; float2 uv: TEXCOORDO;
float4 color: COLOR;
27
28
};
29
30
31
{
32
33
34 B
35
struct v2f
float2 uv
TEXCOORDO;
float4 color: TEXCOORD1;
UNITY_FOG_COORDS (2)
float4 vertex: SV_POSITION;
36
};
37
38
sampler2D _MainTex;
39
float4 _MainTex_ST;
40
41
42
43
44 B
{
45
46
47
48
49
50
51
52
53
54
float _CurveStrength;
v2f vert(appdata v)
v2f 0;
float _Horizon = 100.0f; float _FadeDist
50.0f;
float4 rotVert = v.vertex;
rotVert.y = v.vertex.y * cos(_Time.y * 3.14f) v.vertex.x sin(_Time.y rotVert.x = v.vertex.y * sin(_Time.y * 3.14f) + v.vertex.x * cos(_Time.y
o.vertex = UnityObjectToClipPos(rotVert);
3.14f);
* 3.14f);
55
56
float dist =
UNITY_Z_8_FAR_FROM_CLIPSPACE (o.vertex.z);
57
58
59
60
61
62
63
64
o.vertex.y -= _CurveStrength * dist * dist * _ProjectionParams.x;
o.uv = TRANSFORM_TEX(v.uv, _MainTex); o.color = v.color;
UNITY_TRANSFER_FOG(0, 0.vertex);
return 0;
65
}
66
67
fixed4 frag(v2f i): SV_Target
// sample the texture
fixed4 col = tex2D(_MainTex, i.uv) * i.color;
// apply fog
UNITY_APPLY_FOG(i.fogCoord, col);
return col;
68
{
69
70
71
72
73
74
}
75
76
77
}
78
}
79
}
ENDCG