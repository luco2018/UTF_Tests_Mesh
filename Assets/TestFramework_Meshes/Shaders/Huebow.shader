Shader "Custom/Huebow" {
	Properties {
		[HideInInspector] _MainTex ("Texture", 2D) = "white" {}
		[Toggle(HUE)] _HUE("Keyword toggle", Float) = 1
		_Multi ("Multiplier", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		#pragma multi_compile __ HUE

		struct Input {
			float2 uv_MainTex;
		};

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		half _Multi;

		float3 HsvToRgb(float3 c)
		{
			float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
			float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
			return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			half h = IN.uv_MainTex.y * _Multi;
			#if HUE
				h = IN.uv_MainTex.x * _Multi;
			#endif
			o.Albedo = fixed3(0, 0, 0);
			o.Metallic = 0;
			o.Smoothness = 0;
			o.Emission = HsvToRgb(float3(h, 1, 1)) * 2;
			o.Alpha = 1;
		}
		ENDCG
	}
	CustomEditor "LegacyIlluminShaderGUI"
}
