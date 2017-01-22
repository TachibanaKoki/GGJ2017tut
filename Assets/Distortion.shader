Shader "GGJ/ExplordDistortion"
{
	Properties
	{
		[PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_shiftPower("power", Range(0, 1)) = 1
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		GrabPass {"_Bg"}
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma target 2.0
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnityCG.cginc"

		struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;

		UNITY_VERTEX_INPUT_INSTANCE_ID
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord  : TEXCOORD0;
		float4 grabPos : TEXCOORD1;
		UNITY_VERTEX_OUTPUT_STEREO
	};

	sampler2D _MainTex;
	sampler2D _Bg;
	float4 _MainTex_ST;
	sampler2D _AlphaTex;
	fixed4 _Color;
	float _shiftPower;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		UNITY_SETUP_INSTANCE_ID(IN);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.color = IN.color * _Color;
		OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
		OUT.grabPos = ComputeGrabScreenPos(OUT.vertex); 
		return OUT;
	}


	fixed4 SampleSpriteTexture(float2 uv)
	{
		fixed4 color = tex2D(_MainTex, uv);
		return color;
	}

	fixed4 frag(v2f IN) : SV_Target
	{
		fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
		float2 dist = min(float2(1,1), (IN.texcoord) * _shiftPower * c.a);
		IN.grabPos.xy *= dist;
		float4 grab = tex2Dproj(_Bg, UNITY_PROJ_COORD(IN.grabPos)) ;
		grab.rgb *= grab.a * c.rgb;
		grab.a *= c.a  * IN.color.a;
		return grab;
	}
		ENDCG
	}
	}
}
