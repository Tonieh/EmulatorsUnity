// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ColorShader" {
	Properties {
		_Color ("Main Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Pass {
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				float4 _Color;

				struct data {
					float4 vertex : POSITION;
				};

				struct v2f {
					float4 position : POSITION;
				};

				v2f vert(data i){ 
					v2f o; 
					o.position = UnityObjectToClipPos(i.vertex);
					return o; 
				}

				half4 frag( v2f i ) : COLOR
				{
					return _Color;
				}

			ENDCG 
		}
	}
	FallBack "Diffuse"
}
