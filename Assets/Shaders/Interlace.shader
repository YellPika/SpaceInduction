Shader "Custom/Interlace" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" { }
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
	float4 _MainTex_TexelSize;

	float _Period;
	float _Speed;

	fixed4 _Color;
	float _Intensity;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);	
		
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : COLOR {
		return lerp(
			tex2D(_MainTex, i.uv),
			_Color,
			frac((int)(i.uv.y / _MainTex_TexelSize.y + _Time.y * _Speed) / _Period) * _Intensity);
	}
	ENDCG 
	
	Subshader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }      

			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest 
      
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}
	
	Fallback off
}