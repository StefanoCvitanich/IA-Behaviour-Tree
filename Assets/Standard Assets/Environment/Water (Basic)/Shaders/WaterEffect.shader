// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/WaterEffect"
{
	Properties
	{
		_Color ("Color", COLOR) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_WaveSpeedMult("Wave Speed Multiplier", Range(0,2)) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			//#pragma geometry geom
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 n : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 worldPosition : TEXCOORD1;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _Color;
			float _Alpha;
			float _WaveSpeedMult;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.n;
				return o;
			}

			/*[maxvertexcount(48)]
			void geom(triangle v2f input[3], inout TriangleStream<v2f> OutputStream)
			{
				float3 normal = normalize(cross(input[1].worldPosition.xyz - input[0].worldPosition.xyz, input[2].worldPosition.xyz - input[0].worldPosition.xyz));
				for (int i = 0; i < 3; i++)
				{
					v2f test = (v2f)i;
					test.normal = normal;
					test.vertex = input[i].vertex;
					test.vertex.y += abs(sin(test.vertex.x)) * _SinTime.w * 120;
					test.uv = input[i].uv;
					OutputStream.Append(test);
				}
			}*/
			
			fixed4 frag (v2f i) : SV_Target
			{
				i.uv.x += _SinTime.y * _WaveSpeedMult;
				i.uv.y += _CosTime.x * _WaveSpeedMult;
				fixed4 col = tex2D(_MainTex, i.uv);
				col.a = _Color.a * col.a;
				col.r = _Color.r * col.r;
				col.g = _Color.g * col.g;
				col.b = _Color.b * col.b;
				return col;
			}


			ENDCG
		}
	}
}
