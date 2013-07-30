Shader "Custom/Bar" {
	Properties {
		_BorderColor ("Border Color", Color) = (1, 1, 1, 1)
		_BarColor ("Bar Color", Color) = (1, 1, 1, 1)
		_BorderTex ("Border (RGB)", 2D) = "white" {}
		_BarTex ("Bar (RGB)", 2D) = "white" {}
		_AlphaTex ("Alpha (A)", 2D) = "white" {}
		_Cutoff ("Alpha Cutoff", Range (0.25, 0.75)) = 0.75
	}
	SubShader {
		Tags { "Queue" = "Transparent" }
		Pass {
			Lighting Off
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			fixed4 _BorderColor, _BarColor;
			sampler2D _BorderTex, _BarTex, _AlphaTex;
			half _Cutoff;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert(appdata_base v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			half4 frag(v2f i) : COLOR {
				fixed4 border = tex2D(_BorderTex, i.texcoord) * _BorderColor;
				fixed4 bar = tex2D(_BarTex, i.texcoord) * _BarColor;

				fixed4 alpha = tex2D(_AlphaTex, i.texcoord);
				clip(alpha <= _Cutoff ? -1 : 1);

				return border + bar;
			}

			ENDCG
		}
	} 
	FallBack Off
}
