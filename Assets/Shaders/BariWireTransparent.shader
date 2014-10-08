Shader "WireFrameTransparent"
{
    Properties
    {
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _GridColor ("Grid Color", Color) = (0,0,0,0)
        _LineWidth ("Line Width", float) = 0.05
    }
    SubShader
    {
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			uniform float4 _LineColor;
			uniform float4 _GridColor;
			uniform float _LineWidth;
			
			// vertex input: position, uv1, uv2
			struct appdata {
			    float4 vertex : POSITION;
			    float4 texcoord1 : TEXCOORD0;
			    float4 color : COLOR;
			};
			
			struct v2f {
			    float4 pos : POSITION;
			    float4 texcoord1 : TEXCOORD0;
			    float4 color : COLOR;
			};
			
			v2f vert (appdata v) {
			    v2f o;
			    o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
			    o.texcoord1 = v.texcoord1;
			    o.color = v.color;
			    return o;
			}
			
			float4 frag(v2f i ) : COLOR
			{
			    float2 uv = i.texcoord1;
			    float d = uv.x - uv.y;
			    if (uv.x < _LineWidth)                     // 0,0 to 1,0
			        return _LineColor;
			    else if(uv.x > 1 - _LineWidth)             // 1,0 to 1,1
			        return _LineColor;
			    else if(uv.y < _LineWidth)                 // 0,0 to 0,1
			        return _LineColor;
			    else if(uv.y > 1 - _LineWidth)             // 0,1 to 1,1
			        return _LineColor;
			    else if(d < _LineWidth && d > -_LineWidth) // 0,0 to 1,1
			        return _LineColor;
			    else
                    return _GridColor;
			}
			ENDCG
        }
    }
    Fallback "Vertex Colored", 1
}