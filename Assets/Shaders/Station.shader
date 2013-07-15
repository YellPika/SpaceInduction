Shader "Custom/Station" {
	Properties {
		_Color ("Main Color", Color) = (1,1,1,1)
		
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		
		_CeilingEmitColor ("Ceiling Emissive Color", Color) = (1, 1, 1, 1)
		_FloorEmitColor ("Floor Emissive Color", Color) = (1, 1, 1, 1)
		_Intensity ("Intensity", Range (0, 10)) = 1

		_MainTex ("Base (RGB)", 2D) = "white" {}
		_CeilingEmitTex ("Ceiling (RGB)", 2D) = "white" {}
		_FloorEmitTex ("Floor (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong

		fixed4 _Color;
		half _Shininess;

		fixed4 _CeilingEmitColor;
		fixed4 _FloorEmitColor;
		half _Intensity;

		sampler2D _MainTex;
		sampler2D _CeilingEmitTex;
		sampler2D _FloorEmitTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color.rgb;
			o.Emission = tex2D(_CeilingEmitTex, IN.uv_MainTex).rgb * _CeilingEmitColor.rgb;
			o.Emission += tex2D(_FloorEmitTex, IN.uv_MainTex).rgb * _FloorEmitColor.rgb;
			o.Emission *= _Intensity;
			o.Gloss = 1;
			o.Alpha = 1;
			o.Specular = _Shininess;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
