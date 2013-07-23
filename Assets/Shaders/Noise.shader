Shader "Custom/Noise" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" { }
		_NoiseTex ("Noise (R)", 2D) = "" { }
	}

	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};
	
	sampler2D _MainTex;
	float4 _MainTex_TexelSize;

	sampler2D _NoiseTex;
	float4 _NoiseTex_TexelSize;

	fixed4 _Color;
	half _Intensity;
	half _Opacity;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);	
		
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : COLOR {
		half2 coords = i.uv / _MainTex_TexelSize.xy * _NoiseTex_TexelSize.x;

		half noise = tex2D(_NoiseTex, unity_DeltaTime.yy);
		noise = tex2D(_NoiseTex, (float2)(noise + coords.x));
		noise = tex2D(_NoiseTex, (float2)(noise + coords.y));

		return lerp(
			tex2D(_MainTex, i.uv),
			lerp(_Color, _Color * noise, _Intensity),
			_Opacity);
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