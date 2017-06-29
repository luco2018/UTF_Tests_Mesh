Shader "Unlit/HueShader"
{
	Properties
	{
		[Toggle(HUE)] _HUE("Keyword toggle", Float) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile __ HUE
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float3 HsvToRgb(float3 c)
			{
			    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			    float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			    return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
			}

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				half h = i.uv.y;
				#if HUE
					h = i.uv.x;
				#endif
				fixed3 col = HsvToRgb(float3(h, 1, 1));
				return float4(col, 1);
			}
			ENDCG
		}
	}
}
