Shader "Custom/DistortCamera"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Randomizer("Random Texture", 2D) = "white" {}
		_ChromaticAbbrevation("Chromatic Abbrevation", Float) = 1
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
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
			float _DistortionPosition;
			sampler2D _Randomizer;
			float _DistortionRange;
			float _ChromaticAbbrevation;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uv = i.uv;
				fixed4 col = tex2D(_MainTex, uv);

				if (_DistortionPosition != -1){
					float randomMov = tex2D(_Randomizer, uv.y - uv.x).r;
					if (uv.y > _DistortionPosition - (_DistortionRange / 2) && uv.y < _DistortionPosition + (_DistortionRange / 2)) {
						uv.x += randomMov / 5;
					}

					float movX = pow(abs(((uv.x * 2) - 1) / 10), _ChromaticAbbrevation);
					float movY = pow(abs(((uv.y * 2) - 1) / 10), _ChromaticAbbrevation);
					float2 uvR = float2(uv.x - movX, uv.y);
					float2 uvG = float2(uv.x + movX, uv.y);
					float2 uvB = float2(uv.x, uv.y - movY);
					fixed4 colR = tex2D(_MainTex, uvR);
					fixed4 colG = tex2D(_MainTex, uvG);
					fixed4 colB = tex2D(_MainTex, uvB);
					col = fixed4(colR.r, colG.g, colB.b, 1);
				}
				return col;
			}
			ENDCG
		}
	}
}
