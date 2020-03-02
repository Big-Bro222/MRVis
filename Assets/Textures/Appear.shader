Shader "Personal/Unlit/Appear"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_DisolveTexture("Dissolve Texture", 2D) = "white" {}
		_DisolveY("Current Y of the dissolve effect", Float) = 0
		_DisolveSize("Size of the effect", Float) = 2
		_StartingY("Starting point of the effect", Float) = -10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;			
			float4 _Color;
            float4 _MainTex_ST;
			sampler2D _DisolveTexture;
			float _DisolveY;
			float _DisolveSize;
			float _StartingY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				clip(i.uv.y - _DisolveY + _DisolveSize);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

				if (i.uv.y - _DisolveY < _DisolveSize && i.uv.y - _DisolveY > -_DisolveSize)
					col = _Color;
                
				return col;
            }
            ENDCG
        }
    }
}