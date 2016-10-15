Shader "Custom/PixelateCamera"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScreenSize ("Screen Size", Vector) = (160,144,0,0)
	}

	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _ScreenSize;
			half4x4 _ColorMatrix;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = round(i.uv * _ScreenSize.xy) / _ScreenSize.xy;
				return tex2D(_MainTex, uv);
			}
			ENDCG
		}
	}
}