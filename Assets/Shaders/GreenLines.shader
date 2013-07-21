Shader "Custom/GreenLines" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "" {}
		_NoiseTex ("Noise Texture", 2D) = "" {}
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

	float _Offset;
	float _Period;
	fixed4 _Color;

	float _LineIntensity;
	float _NoiseIntensity;

	v2f vert(appdata_img v) {
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);	
		
		o.uv = v.texcoord.xy;
		return o;
	} 
	
	half4 frag(v2f i) : COLOR {
		int2 coords = i.uv.xy / _MainTex_TexelSize.xy;

		half4 color = tex2D(_MainTex, i.uv);
		int y = i.uv.y / _MainTex_TexelSize.y;
		float4 lines = lerp(color, _Color, frac((y + (int)_Offset) / _Period) * _LineIntensity);

		float noise = _Offset;
		noise = tex2D(_NoiseTex, (float2)(coords.x / 255.0f + noise)).x;
		noise = tex2D(_NoiseTex, (float2)(coords.y / 255.0f + noise)).x;

		return lerp(lines, noise, _NoiseIntensity);
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

} // shader