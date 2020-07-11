
Shader "Custom/Cliptest"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MainColor("Color", Color) = (0,0,0,255)
		_Top("Top",float)=5.0
	    _Bottom("Bottom",float) = 3.0
		_Right("Right",float) = 3.0
		_Left("Left",float) = 3.0

	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				// #pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : POSITION;
					float4 worldPos : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _MainColor;
				float _Top;
				float _Bottom;
				float _Right;
				float _Left;



				v2f vert(appdata v)
				{
					v2f o;
					o.worldPos = mul(unity_ObjectToWorld, v.vertex);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);

				    col.rgba = _MainColor;
					clip(i.worldPos.y - _Bottom);
					clip(-i.worldPos.y + _Top);
					clip(-i.worldPos.x + _Right);
					clip(i.worldPos.x - _Left);


					// if(i.worldPos.y<1){
					//  discard;
					// }        
					return col;
				}
				ENDCG
			}
		}
}