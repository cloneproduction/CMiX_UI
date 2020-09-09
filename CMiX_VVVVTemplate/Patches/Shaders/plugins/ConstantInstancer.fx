Texture2D texture2d; 

SamplerState g_samLinear : IMMUTABLE
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Clamp;
    AddressV = Clamp;
};

//StructuredBuffer< float4x4> sbWorld;
StructuredBuffer<float4> Amb;
StructuredBuffer<float4x4> InstancerTransform;
StructuredBuffer<float4x4> SceneTransform;
StructuredBuffer<float>ExplodeAmount;


cbuffer cbPerObj : register( b1 )
{
	//float4x4 tTex <string uiname="Texture Transform"; bool uvspace=true; >;
	float4x4 ObjTransform;
	float4x4 GroupTransform;
	float4 ObjColor  <bool color=true; String uiname="Entity Color";>  = {0.15, 0.15, 0.15, 1};
};


cbuffer cbPerDraw : register( b0 )
{
	float4x4 tVP : VIEWPROJECTION;
	float4x4 tIVP: INVERSEVIEWPROJECTION;
	float4x4 tWVP: WORLDVIEWPROJECTION;
	float4x4 tIWVP: INVERSEWORLDVIEWPROJECTION;
	float4x4 tV : VIEW;
	float4x4 tP : PROJECTION;
	float4x4 tWV : WORLDVIEW;
	float4x4 tW : WORLD;
	float4x4 tWIT: WORLDLAYERINVERSETRANSPOSE;
	
	float4x4 sceneTransform;
	int InstanceStartIndex = 0;
};

cbuffer cbLightData : register(b3)
{
	float3 lDir <string uiname="Light Direction";> = {0, -5, 2}; 
	float4 lAmb  <bool color=true; String uiname="Ambient Color";>  = {0.15, 0.15, 0.15, 1};
	float4 lDiff <bool color=true;String uiname="Diffuse Color";>  = {0.85, 0.85, 0.85, 1};
	float4 lSpec <bool color=true; String uiname="Specular Color";> = {0.35, 0.35, 0.35, 1};
	float lPower <String uiname="Power"; float uimin=3.0;> = 25.0;     	
};


struct VS_IN
{
	uint ii : SV_InstanceID;
	float4 PosO : POSITION;
	float3 NormO: NORMAL;
	float2 TexCd : TEXCOORD0;

};

struct vs2psVisual
{
	uint iid:SV_InstanceID;
    float4 Pos: SV_POSITION;
	float4 Color: TEXCOORD1;
    float2 TexCd: TEXCOORD2;
	float3 NormV: TEXCOORD4;
	float4 Diffuse: COLOR0;
	float4 Specular: COLOR1;
};

vs2psVisual VS(VS_IN input)
{
    //inititalize all fields of output struct with 0
    vs2psVisual Out = (vs2psVisual)0;
	
	//float4x4 w = sbWorld[input.ii];
	//w=mul(w,tW);
	Out.Pos = input.PosO;
    //Out.PosWVP  = mul(input.PosO,mul(w,tVP));
	Out.iid = input.ii;
	Out.Color = Amb[input.ii];
    Out.TexCd = input.TexCd;
	Out.NormV = input.NormO;
    return Out;
}

[maxvertexcount(3)]
void GS(triangle vs2psVisual input[3], inout TriangleStream<vs2psVisual> gsout)
{
	vs2psVisual elem = (vs2psVisual)0;

	float3 p1 = input[0].Pos.xyz;
	float3 p2 = input[1].Pos.xyz;
	float3 p3 = input[2].Pos.xyz;

	float3 faceEdgeA = p2 - p1;
    float3 faceEdgeB = p1 - p3;
    float3 norm = cross(faceEdgeB, faceEdgeA);
	norm = normalize(norm);	
	
	//float3 col0 = ControlTexture.SampleLevel(g_samLinear, float3(mul(input[0].TexCd, gsfxTransformTex[input[0].iid + InstanceStartIndex]).xy, 0), 0).xyz;
	//float3 col1 = ControlTexture.SampleLevel(g_samLinear, float3(mul(input[1].TexCd, gsfxTransformTex[input[1].iid + InstanceStartIndex]).xy, 0), 0).xyz;
	//float3 col2 = ControlTexture.SampleLevel(g_samLinear, float3(mul(input[2].TexCd, gsfxTransformTex[input[2].iid + InstanceStartIndex]).xy, 0), 0).xyz;
	
	float amt = 1.0;// 1-(dot(col0, 0.33) + dot(col1, 0.33) + dot(col2, 0.33))/3;

	[unroll]
	for(int i = 0; i < 3; i++)
	{
		int id = input[i].iid + InstanceStartIndex;

		float4 ObjPos = mul(input[i].Pos, ObjTransform);
		float4 NewPosition = mul(ObjPos, InstancerTransform[id]);

		float4 position = float4(NewPosition.xyz+(norm*amt*ExplodeAmount[id]), NewPosition.w);
		float4 worldPos = mul(position, tW);
		float4 scenePos = mul(worldPos, sceneTransform);
		float3 LightDirV = normalize(-mul(float4(lDir,0.0f), tV).xyz);
	
	    //normal in view space
	    float3 NormV = normalize(mul(mul(mul(input[i].NormV.xyz, tW), tWIT),tV)).xyz;
		
	    //view direction = inverse vertexposition in viewspace
	    float4 PosV = mul(worldPos, tWV);
	    float3 ViewDirV = normalize(-PosV.xyz);
	
	    //halfvector
	    float3 H = normalize(ViewDirV + LightDirV);
	
	    //compute blinn lighting
	    float3 shades = lit(dot(NormV, LightDirV), dot(NormV, H), lPower).xyz;
	    float4 diff = lDiff * shades.y;
	    float4 spec = lSpec * shades.z;
		
//sceneTransform
		elem.Pos = mul(scenePos, tVP);
		elem.Diffuse = diff + lAmb;
	    elem.Specular = spec;
		elem.TexCd = input[i].TexCd;
		elem.Color = ObjColor;// Amb[id];
		elem.iid = input[i].iid;
		
		gsout.Append(elem);
	}
	
	gsout.RestartStrip();
}

float4 PS(vs2psVisual In): SV_Target
{
	int id = In.iid + InstanceStartIndex;
    float4 col = texture2d.Sample( g_samLinear, In.TexCd) * In.Color;// In.Color;
    return col;
}


technique10 Constant
{
	pass P0
	{
		SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetGeometryShader ( CompileShader( gs_5_0, GS() ) );
		SetPixelShader( CompileShader( ps_5_0, PS() ) );
	}
}