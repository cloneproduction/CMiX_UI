float RedAmount : register(C0);
float GreenAmount : register(C1);
float BlueAmount : register(C2);

sampler2D Texture : register(S0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
   float4 color = tex2D(texture, uv);
   color.r *= RedAmount;
   color.g *= GreenAmount;
   color.b *= BlueAmount;
   return color;
}